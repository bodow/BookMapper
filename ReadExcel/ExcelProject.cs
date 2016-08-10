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
    public class ExcelProject
    {
        #region private fields
        private static Logger logger = new Logger();

        private const string ROOMKEYPATTERN = "{0}++{1}";
        private DataTable bookingLinesDT;
        #endregion

        #region constructor
        public ExcelProject()
        {
        }
        #endregion

        #region public properties
        public static bool HasErrors { get { return logger.HasErrors; } }
        public static string ErrorMessage { get { return logger.ErrorMessage; } }

        public int BookingLinesRead 
        {
            get
            {
                return bookingLinesDT != null ? bookingLinesDT.Rows.Count : 0;
            }
        }
        public DataTable BTable { get { return bookingLinesDT;  } }

        public List<Hotel> Hotels { get; set; }
        public List<Room> Rooms { get; set; }
        public Dictionary<string, string> Mapping { get; set; }
        public BasicOptions Options { get; set; }

        #endregion

        #region public methods
        public Dictionary<string, string> ReadMappings()
        {
            // try to read ready-made file

            logger.MessageClear();
            Dictionary<string, string> map = new Dictionary<string, string>();
            string filename = Options.MappingFileName;
            try
            {
                var lines = File.ReadAllLines(filename);
                foreach (string line in lines)
                {
                    var s = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    map.Add(s[0].Trim(), s[1].Trim());
                }
            }
            catch (Exception ex)
            {
                logger.MessageAdd("No Mappings file!\n\n" + ex.Message);
                map = new Dictionary<string, string>();
            }
            return map;
        }

        public void SaveMappings(Dictionary<string, string> Mapping)
        {
            string filename = Options.MappingFileName;
            StringBuilder sb = new StringBuilder();
            foreach (var imap in Mapping)
            {
                sb.Append(imap.Key);
                sb.Append("|");
                sb.Append(imap.Value);
                sb.AppendLine("|");
            }
            File.WriteAllText(filename, sb.ToString());
        }

        public bool ReadExcelBookingData(string filename) 
        {
            return ReadExcelBookingData(filename, ExcelVersion.Excel2013);
        }
        public bool ReadExcelBookingData(string filename, ExcelVersion excelVersion)
        {
            bool ret = false;
            logger.MessageClear();
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
                    ret = true;
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
            return ret;
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
            SaveMappings(Mapping);

            return !canceledByUser;
        }
        #endregion

        #region private methods
        private void AddCodeColumnsToDT()
        {
            bookingLinesDT.Columns.Add("HotelCode", typeof(string));
            bookingLinesDT.Columns.Add("RoomCode", typeof(string));
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

            string operHotelDescription = row.Field<string>("Hotel");
            string operRoomDescription = row.Field<string>("Room");

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

        //private void MessageClear()
        //{
        //    sb.Clear();
        //}
        //private void MessageAdd(string msg)
        //{
        //    sb.AppendLine(msg);
        //}
        #endregion

    }
}
