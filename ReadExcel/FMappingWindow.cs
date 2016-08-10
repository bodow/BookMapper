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
    public partial class FMappingWindow : Form
    {
        private DataRow row;
        private List<Hotel> hotels;
        private List<Room> rooms;
        private string hotelCode;

        public FMappingWindow(DataRow row, List<Hotel> hotels, List<Room> rooms, string hotelCode)
        {
            InitializeComponent();
            this.row = row;
            this.hotels = hotels;
            this.rooms = rooms;
            this.hotelCode = hotelCode;
        }

        private void FMappingWindow_Load(object sender, EventArgs e)
        {
            lblLine.Text = row["Line"].ToString();
            lblHotel.Text = row["Hotel"].ToString();
            lblRoom.Text = row["Room"].ToString();
            // DataBind
            bsHotel.SuspendBinding();
            bsHotel.DataSource = hotels.OrderBy(p => p.Description);
            bsHotel.ResumeBinding();

            //bsRoom.DataSource = rooms;

            if (!String.IsNullOrEmpty(hotelCode))
            {
                cboHotel.SelectedValue = hotelCode;
                cboHotel.Enabled = false;
                BindRooms();
            }
            else
            {
                bsHotel.MoveFirst();
                BindRooms();
            }
        }

        public string HotelCode { get; private set; }
        public string RoomCode { get; private set; }

        private void BindRooms()
        {
            bsRoom.DataSource = rooms.Where(r => r.HotelCode == ((Hotel)cboHotel.SelectedItem).Code).OrderBy(p => p.Description);
        }
        private bool LocalValidate()
        {
            if (cboHotel.SelectedValue == null || cboRoom.SelectedValue == null)
            {
                return false;
            }
            else
            {
                HotelCode = cboHotel.SelectedValue.ToString();
                RoomCode = cboRoom.SelectedValue.ToString();
                return true;
            }
        }
        private void cboHotel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboHotel.SelectedItem != null && cboHotel.SelectedItem is Hotel)
            {
                BindRooms();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string msg = "Προσοχή: " + Environment.NewLine +
                         "Διακόπτοντας την διαδικασία αυτή, θα διακοπεί η συνολική εργασία για όλο το αρχείο" + Environment.NewLine +
                         "Είστε βέβαιος ότι θέλετε να σταματήσετε;";
            if (MessageBox.Show(msg, "Ερώτηση", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes) 
            {
                DialogResult = DialogResult.None;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (DialogResult != System.Windows.Forms.DialogResult.None && !LocalValidate())
                DialogResult = System.Windows.Forms.DialogResult.None;
        }
    }
}
