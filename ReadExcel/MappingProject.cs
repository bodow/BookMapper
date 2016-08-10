using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.XlsIO;
using System.Data;
using ReadExcel.DataClasses;
using System.IO;

namespace ReadExcel
{
    public class MappingProject: ILogger
    {
        #region private fields
        private Logger logger;
        private const string ROOMKEYPATTERN = "{0}++{1}";
        private DataTable bookingLinesDT;
        private BasicOptions options;
        private List<Hotel> Hotels;
        private List<Room> Rooms;
        private Dictionary<string, string> Mapping;
        private List<DBBooking> DBBookings;
        private string LocalExcelFileName = "test.xlsx";
        #endregion

        #region constructor
        public MappingProject(BasicOptions options)
        {
            logger = new Logger();
            this.options = options;

            Hotels = new List<Hotel>();
            Rooms = new List<Room>();
            Mapping = new Dictionary<string, string>();
            DBBookings = new List<DBBooking>();
        }
        #endregion

        #region public properties
        public bool HasErrors { get { return logger.HasErrors; } }
        public string ErrorMessage { get { return logger.ErrorMessage; } }

        public int BookingLinesRead 
        {
            get
            {
                return bookingLinesDT != null ? bookingLinesDT.Rows.Count : 0;
            }
        }

        //public List<Hotel> Hotels { get; set; }
        //public List<Room> Rooms { get; set; }
        public Dictionary<string, string> Mappings { get { return Mapping; } }

        public int HotelLinesRead { get { return Hotels.Count; } }
        public int RoomLinesRead { get { return Rooms.Count;  } }
        public int MappingsSoFar { get { return Mapping.Count;  } }
        public int DBBookingsRead { get { return DBBookings.Where(b => b.Line > 0).Count(); } }

        public List<DBBooking> Bookings { get { return DBBookings; } }
        public string RoomKeyPattern { get { return ROOMKEYPATTERN; } }

        public int AChanged
        { 
            get 
            {
                return DBBookings == null ? 0 : Bookings.Where(b => b.CheckedStatus == BookingStatus.Changed).Count();
            } 
        }
        public int AMissing
        {
            get
            {
                return DBBookings == null ? 0 : Bookings.Where(b => b.CheckedStatus == BookingStatus.Missing).Count();
            }
        }
        public int ANotConfirmed
        {
            get
            {
                return DBBookings == null ? 0 : Bookings.Where(b => b.CheckedStatus == BookingStatus.NotConfirmed).Count();
            }
        }
        public int AConfirmed
        {
            get
            {
                return DBBookings == null ? 0 : Bookings.Where(b => b.CheckedStatus == BookingStatus.Confirmed).Count();
            }
        }

        #endregion

        #region public methods
        public bool ReadInternalDataFiles()
        {
            // Read:
            //
            // 1. SO16.6.hotels.unl
            // 2. SO16.6.rooms.unl
            // 3. SO16.6.mappings.unl
            //
            // ..into internal private fields

            logger.MessageClear();

            string hotelsFileName = Path.Combine(options.MappingFileFolder, options.DefaultOperSeasPrefix + ".hotels.unl");
            string roomsFileName = Path.Combine(options.MappingFileFolder, options.DefaultOperSeasPrefix + ".rooms.unl");
            string mappingFileName = options.MappingFileName;

            // clear excel and DB booking data
            ClearBookingData();
            ClearDBBookingData();


            // 1. Hotels
            //
            Hotels = Hotel.LoadHotelAscii(hotelsFileName);
            if (Hotel.HasErrors)
            {
                logger.MessageAdd(Hotel.ErrorMessage);
            }
            if (!HasErrors && Hotels.Count <= 0)
            {
                logger.MessageAdd("Empty hotel File");
            }

            // 2. Rooms
            //
            Rooms = Room.LoadRoomAscii(roomsFileName);
            if (Room.HasErrors)
            {
                logger.MessageAdd(Room.ErrorMessage);
            }
            if (!HasErrors && Rooms.Count <= 0)
            {
                logger.MessageAdd("Empty room File");
            }

            // 3. Mappings
            //
            if (!HasErrors)
            {
                // read mapping file
                Mapping = ReadMappings();
            }
            return !HasErrors;
        }
        public bool SaveMappings()
        {
            logger.MessageClear();
            string filename = options.MappingFileName;
            StringBuilder sb = new StringBuilder();
            foreach (var imap in Mapping)
            {
                sb.Append(imap.Key);
                sb.Append("|");
                sb.Append(imap.Value);
                sb.AppendLine("|");
            }
            try
            {
                File.WriteAllText(filename, sb.ToString());
            }
            catch (Exception ex)
            {
                logger.MessageAdd(ex.Message);
            }
            return !HasErrors;
        }

        public void ClearBookingData()
        {
            if (bookingLinesDT != null)
                bookingLinesDT.Clear();
        }
        public void ClearDBBookingData()
        {
            DBBookings = new List<DBBooking>();
        }
        public bool ReadExcelBookingData(string filename) 
        {
            return ReadExcelBookingData(filename, ExcelVersion.Excel2013);
        }
        public bool ReadExcelBookingData(string filename, ExcelVersion excelVersion)
        {
            logger.MessageClear();
            LocalExcelFileName = filename; 
            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            try
            {
                IWorkbook workbook = application.Workbooks.Open(filename, excelVersion);
                IWorksheet sheet = workbook.Worksheets[0];
                bookingLinesDT = sheet.ExportDataTable(sheet.UsedRange, ExcelExportDataTableOptions.ColumnNames | ExcelExportDataTableOptions.DetectColumnTypes);
                workbook.Close();
                if (CheckExcelSchema())
                {
                    AddCodeColumnsToDT();
                }
                else
                {
                    logger.MessageAdd("Το αρχείο excel δεν έχει την καθορισμένη μορφή");
                    bookingLinesDT.Clear();
                }
            }
            catch (Exception ex)
            {
                logger.MessageAdd(ex.Message);
            }
            finally
            {
                excelEngine.Dispose();
            }
            return !HasErrors;
        }
        public bool WriteExcelBookingData()
        {
            logger.MessageClear();
            if (!String.IsNullOrEmpty(LocalExcelFileName) && BookingLinesRead > 0)
            {
                string filename = LocalExcelFileName;
                ExcelEngine excelEngine = new ExcelEngine();
                IApplication application = excelEngine.Excel;

                try
                {
                    IWorkbook workbook = application.Workbooks.Open(filename);
                    IWorksheet sheet = workbook.Worksheets.Create();
                    sheet.ImportDataTable(bookingLinesDT, 2, 1, false);
                    workbook.Save();
                    workbook.Close();
                }
                catch (Exception ex)
                {
                    logger.MessageAdd(ex.Message);
                }
                finally
                {
                    excelEngine.Dispose();
                }
                return !HasErrors;
            }
            return false;
        }
        public bool MatchTableData()
        {
            // Απαραίτητη προυπόθεση να έχουμε έτοιμες λίστες hotels, rooms, mappings 
            // φυσικά να έχουμε input data for matching
            //
            // Κάνουμε match all table rows, με όποιον τρόπο μπορούμε, και επιστρέφουμε 
            // true:  Γίναν όλα τα mappings
            // false: Δεν ολοκληρώθηκαν όλα τα mappings
            //
            // Σε κάθε περίπτωση κάνουμε save την ενημερωμένη λίστα Mappings
            //
            // Σε περίπτωση λαθών, καταγράφουμε μηνύματα στο .ErrorMessage

            logger.MessageClear();

            // Απαραίτητη προυπόθεση να έχουμε έτοιμες λίστες hotels, rooms, mappings 
            // φυσικά να έχουμε input data for matching
            if (Hotels == null || Hotels.Count == 0)
            {
                logger.MessageAdd("Δεν έχουν διαβαστεί ονόματα Ξενοδοχείων");
                return false;
            }
            if (Rooms == null || Rooms.Count == 0)
            {
                logger.MessageAdd("Δεν έχουν διαβαστεί τύποι Δωματίων");
                return false;
            }
            if (Mapping == null) Mapping = new Dictionary<string, string>();
            if (BookingLinesRead <= 0)
            {
                logger.MessageAdd("Δεν έχουν διαβαστεί γραμμές excel κρατήσεων προς έλεγχο");
                return false;
            }


            bool canceledByUser = false;

            // match every row in bookingLinesDT
            foreach (DataRow row in bookingLinesDT.Rows)
            {
                Tuple<string, string> tmap = GetRowMapping(row);
                if (!String.IsNullOrEmpty(tmap.Item1) && !String.IsNullOrEmpty(tmap.Item2))
                {
                    row.SetField<string>("HotelCode", tmap.Item1);
                    row.SetField<string>("RoomCode", tmap.Item2);
                }
                else
                {
                    canceledByUser = true;
                    break;
                }
            }

            // save updated mappings
            SaveMappings();

            return !canceledByUser;
        }

        public bool ReadGeneroBookings(string filename)
        {
            logger.MessageClear();

            DBBookings = DBBooking.LoadDBBookingAscii(filename);
            if (DBBooking.HasErrors)
            {
                logger.MessageAdd(DBBooking.ErrorMessage);
            }
            if (!HasErrors && DBBookings.Count <= 0)
            {
                logger.MessageAdd("Empty hotel File");
            }
            return !HasErrors;
        }
        public bool CrossBookings()
        {
            // Συνέκρινε τις κρατήσεις από το excel file ελέγχου του TourOperator
            // με τις κρατήσεις που διαβάστηκαν από την Β.Δ.
            // Τα αποτελέσματα αντικατοπτρίζονται στη λίστα DBBookings με βάση το status
            logger.MessageClear();

            if (BookingLinesRead <= 0)
            {
                logger.MessageAdd("Δεν έχουν διαβαστεί γραμμές excel κρατήσεων προς έλεγχο");
                return false;
            }
            if (DBBookingsRead <= 0)
            {
                logger.MessageAdd("Δεν έχουν διαβαστεί κρατήσεις από την Βάση");
                return false;
            }

            // match every row in bookingLinesDT
            int line, adults, children;
            string refId, hotelCode, roomCode, meal;
            DateTime checkin, checkout;
            foreach (DataRow row in bookingLinesDT.Rows)
            {

                try
                {
                    line = Convert.ToInt32(row.Field<double>("Line"));
                    refId = row.Field<string>("Ref").Trim();
                    hotelCode = row.Field<string>("HotelCode").Trim();
                    roomCode = row.Field<string>("RoomCode").Trim();
                    meal = row.Field<string>("Meal").Trim();
                    adults = Convert.ToInt32(row.Field<double>("Adults"));
                    children = Convert.ToInt32(row.Field<double>("Children"));
                    checkin = Convert.ToDateTime(row.Field<DateTime>("Checkin"));
                    checkout = row.Field<DateTime>("Checkout");
                }
                catch (Exception ex)
                {
                    logger.MessageAdd(ex.Message);
                    return false;
                }

                // Αναζητώ καταρχήν τον συνδυασμό REFID-HOTEL στα ΜΗ-Αναγνωρισμένα ακόμα στοιχεία της λίστας
                // Θα αποφασίσω με βάση το πόσα καταρχήν τέτοια θα βρω.

                int cnt = DBBookings.Where(b => b.RefId == refId && b.HotelCode == hotelCode && b.CheckedStatus == BookingStatus.NotConfirmed).Count();
                if (cnt == 0)
                {
                    // Δεν βρέθηκε τέτοια καταχωρημένη κράτηση
                    // Πέρασε την στην τελική λίστα, με σημάδι: Missing
                    DBBookings.Add(new DBBooking()
                    {
                        Line = 0,
                        RefId = refId,
                        HotelCode = hotelCode,
                        RoomCode = roomCode,
                        Meal = meal,
                        Checkin = checkin,
                        Checkout = checkout,
                        Adults = adults,
                        Children = children,
                        CheckedStatus = BookingStatus.Missing,
                        CheckLine = line
                    });
                    continue;
                }
                else if (cnt == 1)
                {
                    DBBooking bk = DBBookings
                            .Where(b =>
                              b.RefId == refId &&
                              b.HotelCode == hotelCode &&
                              b.CheckedStatus == BookingStatus.NotConfirmed &&
                              b.RoomCode.Substring(1,2) == roomCode.Substring(1,2) &&
                              b.Meal == meal &&
                              b.Adults + b.Children == adults + children &&
                              b.Checkin.Date == checkin.Date &&
                              b.Checkout.Date == checkout.Date)
                              .FirstOrDefault();
                    if (bk != null && bk.Line > 0)
                    {
                        bk.CheckedStatus = BookingStatus.Confirmed;
                        bk.CheckLine = line;
                    }
                    else
                    {
                        bk = DBBookings
                            .Where(b =>
                              b.RefId == refId &&
                              b.HotelCode == hotelCode &&
                              b.CheckedStatus == BookingStatus.NotConfirmed)
                              .FirstOrDefault();
                        if (bk != null) // Αναμένεται φυσικά να συμβαίνει!
                        {
                            bk.CheckedStatus = BookingStatus.Changed;
                            bk.CheckLine = line;
                            bk.CheckRoomCode = roomCode;
                            bk.CheckMeal = meal;
                            bk.CheckAdults = adults;
                            bk.CheckChildren = children;
                            bk.CheckCheckin = checkin;
                            bk.CheckCheckout = checkout;
                        }
                    }
                }
                else
                {
                    // cnt >= 2, δηλ. με συνδυασμό REF-HOTEL βρήκα >1 καταχωρημένες κρατήσεις
                    // Αυτή είναι η δυσκολότερη περίπτωση.
                    // Θα προσπαθήσω να επιτύχω πλήρη ταύτιση, διαφορετικά θα βρω την πρώτη από αυτές
                    // και θα την σημαδέψω ως CHANGED
                    DBBooking bk = DBBookings
                            .Where(b =>
                              b.RefId == refId &&
                              b.HotelCode == hotelCode &&
                              b.CheckedStatus == BookingStatus.NotConfirmed &&
                              b.RoomCode.Substring(1, 2) == roomCode.Substring(1, 2) &&
                              b.Meal == meal &&
                              b.Adults + b.Children == adults + children &&
                              b.Checkin.Date == checkin.Date &&
                              b.Checkout.Date == checkout.Date)
                              .FirstOrDefault();
                    if (bk != null && bk.Line > 0)
                    {
                        bk.CheckedStatus = BookingStatus.Confirmed;
                        bk.CheckLine = line;
                    }
                    else
                    {
                        bk = DBBookings
                            .Where(b =>
                              b.RefId == refId &&
                              b.HotelCode == hotelCode &&
                              b.CheckedStatus == BookingStatus.NotConfirmed)
                              .FirstOrDefault();
                        if (bk != null) // Αναμένεται φυσικά να συμβαίνει!
                        {
                            bk.CheckedStatus = BookingStatus.Changed;
                            bk.CheckLine = line;
                            bk.CheckRoomCode = roomCode;
                            bk.CheckMeal = meal;
                            bk.CheckAdults = adults;
                            bk.CheckChildren = children;
                            bk.CheckCheckin = checkin;
                            bk.CheckCheckout = checkout;
                        }
                    }
                }
            }
            return !HasErrors;

        }

        public string OutputResults(BookingStatus status)
        {
            StringBuilder sb = new StringBuilder();
            var q = Bookings.Where(b => b.CheckedStatus == status).ToList();
            foreach (DBBooking book in q)
            {
                sb.AppendLine(book.ToString());
            }
            return sb.ToString();
        }

        // Εμφάνισε ένα παράθυρο επισκόπησης ρων room-mappings.
        // Το παράθυρο επίσης προσφέρει δυνατότητες διορθώσεων σε ήδη υπάρχοντα mappings
        public void PreviewMappings()
        {
            if (HotelLinesRead > 0 && RoomLinesRead > 0 && Mapping != null)
            {
                using (FPreviewMappings f = new FPreviewMappings(this, Hotels, Rooms))
                {
                    f.ShowDialog();
                    if (f.ChangesDone)
                    {
                        SaveMappings();
                    }
                }
            }
        }
        #endregion

        #region private methods
        private bool CheckExcelSchema()
        {
            // Διαπίστωσε αν περιέχονται όλες οι στήλες με το σωστό όνομα
            List<string> colNames = new List<string>();
            colNames.AddRange(new string[] {
                "Line", "Ref", "Hotel", "Room", "Meal", "Checkin", "Checkout", "Adults", "Children"
            });
            foreach (DataColumn col in bookingLinesDT.Columns)
            {
                colNames.Remove(col.ColumnName);
            }
            return colNames.Count == 0;
        }
        private void AddCodeColumnsToDT()
        {
            bookingLinesDT.Columns.Add("HotelCode", typeof(string));
            bookingLinesDT.Columns.Add("RoomCode", typeof(string));
        }
        private Dictionary<string, string> ReadMappings()
        {
            // try to read ready-made file

            logger.MessageClear();
            Dictionary<string, string> map = new Dictionary<string, string>();
            string filename = options.MappingFileName;
            string currline = "";
            try
            {
                var lines = File.ReadAllLines(filename);
                foreach (string line in lines)
                {
                    if (String.IsNullOrWhiteSpace(line))
                        continue;
                    currline = line;
                    var s = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    map.Add(s[0].Trim(), s[1].Trim());
                }
            }
            catch (Exception ex)
            {
                string fullmsg = ex.Message + Environment.NewLine + "line:" + currline;
                logger.MessageAdd("Mappings file error!\n\n" + fullmsg);
                map = new Dictionary<string, string>();
            }
            return map;
        }

        private Tuple<string, string> GetRowMapping(DataRow row)
        {
            // Προσπαθούμε ένα πλήρες map (Hotel & Room δηλαδη) της γραμμής
            // Ή θα το πετύχουμε "αυτόματα", ή θα ζητήσοπυμε από το χρήστη να το κάνει ο ίδιος
            // Ο χρήστης τότε μπορεί να ακυρώσει: Επιστρέφεται String.Empty το οποίο πρακτικά
            // θα σηματοδοτήσει την εγκατάλειψη της όλης προσπάθειας (του συνολικού input αρχείου)

            //
            // ΒΑΣΙΚΟΣ ΑΛΓΟΡΙΘΜΟΣ - FLOW:
            //
            // - Αυτόματο map hotel & room  => όλα καλά, επιστρέφουμε τα δέοντα
            // - Αυτόματο map hotel but no room  => Ask User based on hotel
            // - Αδυναμία map hotel => Ask User for hotel & room
            //


            string maphotel = String.Empty;
            string maproom = String.Empty;

            string operHotelDescription = row.Field<string>("Hotel").Trim();
            string operRoomDescription = row.Field<string>("Room").Trim();

            maphotel = GetHotelMapping(operHotelDescription);
            if (!String.IsNullOrEmpty(maphotel))
            {
                maproom = GetRoomMapping(maphotel, operRoomDescription);
            }
            if (String.IsNullOrEmpty(maproom))
            {
                using (FMappingWindow f = new FMappingWindow(row, Hotels, Rooms, maphotel))
                {
                    if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        maphotel = f.HotelCode;
                        maproom = f.RoomCode;
                    }
                }
            }


            // register the mappings!
            if (!String.IsNullOrEmpty(maphotel) && !Mapping.ContainsKey(operHotelDescription))
            {
                Mapping.Add(operHotelDescription, maphotel);
            }
            if (!String.IsNullOrEmpty(maproom) && !Mapping.ContainsKey(String.Format(ROOMKEYPATTERN, maphotel, operRoomDescription)))
            {
                Mapping.Add(String.Format(ROOMKEYPATTERN, maphotel, operRoomDescription), maproom);
            }

            Tuple<string, string> retmap = new Tuple<string, string>(maphotel, maproom);
            return retmap;
        }
        private string GetHotelMapping(string hotel)
        {
            string map = String.Empty;

            // search in ready-made mappings
            if (Mapping.ContainsKey(hotel))
            {
                return Mapping[hotel];
            }

            // search token by token..
            var tokens = hotel.Split(new char[] { ' ' });

            int tokensCount = tokens.Length;
            int firstToken = 0;

            bool found = false;
            while (!found && firstToken <= tokensCount-1)
            {
                for (int n = tokensCount-firstToken; n >= 1; n--)
                {
                    map = TryMatchTokensAgainstHotel(tokens, firstToken, n);
                    if (!String.IsNullOrEmpty(map))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    firstToken++;
            }
            return map;
        }
        private string GetRoomMapping(string hotelCode, string room)
        {
            string map = String.Empty;

            // search in ready-made mappings
            if (Mapping.ContainsKey(String.Format(ROOMKEYPATTERN, hotelCode, room)))
            {
                return Mapping[String.Format(ROOMKEYPATTERN, hotelCode, room)];
            }

            // search token by token..
            var tokens = room.Split(new char[] { ' ' });

            int tokensCount = tokens.Length;
            int firstToken = 0;

            bool found = false;
            while (!found && firstToken <= tokensCount - 1)
            {
                for (int n = tokensCount - firstToken; n >= 1; n--)
                {
                    map = TryMatchTokensAgainstHotelRooms(hotelCode, tokens, firstToken, n);
                    if (!String.IsNullOrEmpty(map))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    firstToken++;
            }
            return map;
        }

        private string TryMatchTokensAgainstHotel(string[] tokens, int firstToken, int n)
        {
            string searchPattern = "";
            for (int i = firstToken; i < firstToken+n && i <= tokens.Length-1; i++)
            {
                if (String.IsNullOrEmpty(searchPattern))
                {
                    searchPattern = tokens[i].ToUpper();
                }
                else
                {
                    searchPattern = searchPattern + " " + tokens[i].ToUpper();
                }
            }

            List<Hotel> q = Hotels.ToList().Where(p => p.Description.Contains(searchPattern)).ToList();
            if (q.Count == 0)
            {
                return String.Empty;
            }
            if (q.Count == 1)
            {
                return q[0].Code;
            }

            // Multiple matches: I don't know the answer: Ask user
            return String.Empty;
        }
        private string TryMatchTokensAgainstHotelRooms(string hotelCode, string[] tokens, int firstToken, int n)
        {
            string searchPattern = "";
            for (int i = firstToken; i < firstToken + n && i <= tokens.Length - 1; i++)
            {
                if (String.IsNullOrEmpty(searchPattern))
                {
                    searchPattern = tokens[i].ToUpper();
                }
                else
                {
                    searchPattern = searchPattern + " " + tokens[i].ToUpper();
                }
            }

            List<Room> q = Rooms.ToList().Where(p => p.HotelCode == hotelCode && p.Description.Contains(searchPattern)).ToList();
            if (q.Count == 0)
            {
                return String.Empty;
            }
            if (q.Count == 1)
            {
                return q[0].Code;
            }

            // Multiple matches: I don't know the answer: Ask user
            return String.Empty;
        }
        #endregion

    }
}
