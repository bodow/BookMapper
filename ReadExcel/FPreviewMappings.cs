using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReadExcel.DataClasses;

namespace ReadExcel
{
    public partial class FPreviewMappings : Form
    {
        #region private fields
        private BindingList<Hotel> Hotels;
        private BindingList<Room> Rooms;
        private Dictionary<string, string> Mappings;
        private MappingProject caller;
        private BindingList<RoomJoin> dataso;
        #endregion

        #region constructor and Load
        public FPreviewMappings(MappingProject parent, List<Hotel> hotels, List<Room> rooms)
        {
            InitializeComponent();

            caller = parent;
            Hotels = new BindingList<Hotel>(hotels);
            Rooms = new BindingList<Room>(rooms);
            Mappings = caller.Mappings;

            dataso = new BindingList<RoomJoin>();
            ChangesDone = false;
        }
        private void FPreviewMappings_Load(object sender, EventArgs e)
        {
            // DataBind
            bsHotels.SuspendBinding();
            bsHotels.DataSource = Hotels.OrderBy(h => h.Code);
            bsHotels.ResumeBinding();
        }
        #endregion

        #region public Properties
        public bool ChangesDone { get; private set; }
        #endregion

        #region GUI handlers
        private void bsHotels_CurrentChanged(object sender, EventArgs e)
        {
            // Μόλις ο χρήστης επιλέξει κάποιο Ξενοδοχείο από το πάνω-πάνω combobox
            // το πρόγραμμα διαμορφώνει τις δύο σχετικές λίστες:
            //  - Πάνω την λίστα των διαθέσιμων δωματίων από genero list
            //  - Κάτω, τις αντιστοιχίες των δωματίων που έχει so far το parent Mapping

            Hotel htl = bsHotels.Current as Hotel;
            if (htl != null)
            {
                var filteredRooms = Rooms.Where(r => r.HotelCode == htl.Code).OrderBy(o => o.Code);
                bsRooms.SuspendBinding();
                bsRooms.DataSource = filteredRooms;
                bsRooms.ResumeBinding();

                dataso.Clear();
                var q = Mappings.Where(m => m.Key.StartsWith(htl.Code+"++"));
                foreach (var itm in q)
                {
                    RoomJoin rj = new RoomJoin() { HotelCode = htl.Code, RoomReference = itm.Key.Substring(6), MappedTo = itm.Value };
                    rj.Description = Rooms
                                        .Where(r => r.HotelCode == htl.Code && r.Code == rj.MappedTo)
                                        .FirstOrDefault()
                                        .Description;
                    dataso.Add(rj);
                }
                bsMappings.SuspendBinding();
                bsMappings.DataSource = dataso;
                bsMappings.ResumeBinding();
            }
        }
        private void btnDo_Click(object sender, EventArgs e)
        {
            var a = bsRooms.Current as Room;
            var b = bsMappings.Current as RoomJoin;
            if (a != null && b != null)
            {
                var c = a.Code;
                var d = String.Format(caller.RoomKeyPattern, b.HotelCode, b.RoomReference);
                Mappings[d] = c;
                // βρες το συγκεκριμένο RoomJoin εντός του dataso και άλλαξε το
                var cell = dataso
                            .Where(rj => rj.HotelCode == b.HotelCode && rj.RoomReference == b.RoomReference)
                            .FirstOrDefault();
                if (cell != null)
                {
                    cell.MappedTo = c;
                    cell.Description = a.Description;
                    bsMappings.ResetBindings(false);
                    ChangesDone = true;
                }
            }
        }
        //private void cboHotel_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var htl = cboHotel.SelectedItem;
        //}
        #endregion
    }
}
