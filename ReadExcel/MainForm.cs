using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ReadExcel.DataClasses;

namespace ReadExcel
{
    public partial class MainForm : Form
    {

        ////////////////////////////////////////////////////////
        //
        // ΒΑΣΙΚΗ ΔΟΜΗ - FLOW
        //
        ////////////////////////////////////////////////////////

        // Read Parameters
        //      -   read oper & seas
        //      -   read hotels.unl & rooms.unl & mappings.unl
        // Read Excel
        //      -   read bookings data: 9 columns as of: 
        //          Line, Ref, Hotel, Room, Meal, Checkin, Checkout, Adults, Children
        // Κάνε αντιστοιχήσεις κωδικών
        //      -   ψάξε πρώτα στα mappings
        //      -   κάνε %LIKE% με ν,ν-1,..1 tokens from Operator's Data Description
        //      -   ask user to specify
        // Save αντιστοιχήσεις
        // Output results

        ////////////////////////////////////////////////////////

        #region private fields
        private BasicOptions options;
        private MappingProject project;
        #endregion

        #region constructor and Load
        public MainForm()
        {
            InitializeComponent();
            options = new BasicOptions();
            propertyGrid1.SelectedObject = options;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            DisplayCounters();
        }
        #endregion

        #region private methods
        private void DisplayCounters()
        {
            int nHtl, nRoo, nMap, nExc, nBoo;
            int a1, a2, a3, a4;
            nHtl = nRoo = nMap = nExc = nBoo = 0;
            a1 = a2 = a3 = a4 = 0;

            if (project != null)
            {
                nHtl = project.HotelLinesRead;
                nRoo = project.RoomLinesRead;
                nMap = project.MappingsSoFar;
                nExc = project.BookingLinesRead;
                nBoo = project.DBBookingsRead;

                a1 = project.AChanged;
                a2 = project.AMissing;
                a3 = project.ANotConfirmed;
                a4 = project.AConfirmed;
            }
            DisplayCounters(nHtl, nRoo, nMap, nExc, nBoo, a1, a2, a3, a4);
        }
        private void DisplayCounters(int nHtl, int nRoo, int nMap, int nExc, int nBoo, int a1, int a2, int a3, int a4)
        {
            lblHotl.Text = nHtl.ToString();
            lblRoom.Text = nRoo.ToString();
            lblMapp.Text = nMap.ToString();
            lblBLin.Text = nExc.ToString();
            lblBook.Text = nBoo.ToString();

            lbla1.Text = a1.ToString();
            lbla2.Text = a2.ToString();
            lbla3.Text = a3.ToString();
            lbla4.Text = a4.ToString();
        }
        private void ShowProjectResults(BookingStatus status)
        {
            if (project != null)
            {
                string s = project.OutputResults(status);
                if (!String.IsNullOrWhiteSpace(s))
                {
                    FShowResults f = new FShowResults(s);
                    f.Show();
                //    using (FShowResults f = new FShowResults(s))
                //    {
                //        f.ShowDialog();
                //    }
                }
            }
        }
        #endregion

        #region GUI handlers
        private void button1_Click(object sender, EventArgs e)
        {
            project = new MappingProject(options);
            bool ret = project.ReadInternalDataFiles();
            DisplayCounters();
            if (!ret)
            {
                MessageBox.Show(project.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnReadMatch_Click(object sender, EventArgs e)
        {
            if (options == null)
            {
                MessageBox.Show("Δεν βρέθηκαν στοιχεία προεπιλογών" + Environment.NewLine +
                                "Η διαδικασία δεν μπορεί να συνεχιστεί");
                return;
            }
            if (project == null || project.HotelLinesRead <= 0 || project.RoomLinesRead <= 0)
            {
                MessageBox.Show("Δεν βρέθηκαν στοιχεία Ξενοδοχείων/Δωματίων" + Environment.NewLine + 
                                "Η διαδικασία δεν μπορεί να συνεχιστεί");
                return;
            }
            string filename = String.Empty;
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.DefaultExt = ".xlsx";
                dlg.InitialDirectory = options.MappingFileFolder;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    filename = dlg.FileName;
                    if (project.ReadExcelBookingData(dlg.FileName))
                    {
                        DisplayCounters();
                        if (project.MatchTableData())
                        {
                            DisplayCounters();
                            if (project.WriteExcelBookingData())
                            {
                                MessageBox.Show("Η εργασία ολοκληρώθηκε, παρακαλώ δείτε το αρχικό excel file");
                            }
                            else
                            {
                                string msg = "Πρόβλημα στην παραγωγή του αποτελέσματος.";
                                if (project.HasErrors)
                                {
                                    msg = msg + Environment.NewLine + project.ErrorMessage;
                                    MessageBox.Show(msg);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Η διαδικασία διακόπηκε.");
                        }
                    }
                    else
                    {
                        MessageBox.Show(project.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void btnReadDBBook_Click(object sender, EventArgs e)
        {
            if (options == null)
            {
                MessageBox.Show("Δεν βρέθηκαν στοιχεία προεπιλογών" + Environment.NewLine +
                                "Η διαδικασία δεν μπορεί να συνεχιστεί");
                return;
            }
            if (project == null || project.HotelLinesRead <= 0 || project.RoomLinesRead <= 0)
            {
                MessageBox.Show("Δεν βρέθηκαν στοιχεία Ξενοδοχείων/Δωματίων" + Environment.NewLine +
                                "Η διαδικασία δεν μπορεί να συνεχιστεί");
                return;
            }
            if (project.BookingLinesRead <= 0)
            {
                MessageBox.Show("Παρακαλώ να διαβαστεί πρώτα κάποιο excel κρατήσεων συνεργάτη" + Environment.NewLine +
                                "Η διαδικασία δεν μπορεί να συνεχιστεί");
                return;
            }
            string filename = String.Empty;
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.DefaultExt = ".txt";
                dlg.InitialDirectory = options.MappingFileFolder;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    filename = dlg.FileName;
                    if (project.ReadGeneroBookings(filename))
                    {
                        DisplayCounters();
                        MessageBox.Show(String.Format("Διαβάστηκαν {0} κρατήσεις", project.DBBookingsRead));
                        if (project.DBBookingsRead > 0)
                        {
                            project.CrossBookings();
                            DisplayCounters();
                        }
                    }
                    else
                    {
                        MessageBox.Show(project.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnShowChanged_Click(object sender, EventArgs e)
        {
            ShowProjectResults(BookingStatus.Changed);
        }
        private void btnShowMissing_Click(object sender, EventArgs e)
        {
            ShowProjectResults(BookingStatus.Missing);
        }
        private void btnShowNotConfirmed_Click(object sender, EventArgs e)
        {
            ShowProjectResults(BookingStatus.NotConfirmed);
        }
        private void btnShowConfirmed_Click(object sender, EventArgs e)
        {
            ShowProjectResults(BookingStatus.Confirmed);
        }
        private void btnEditMapp_Click(object sender, EventArgs e)
        {
            if (project != null)
            {
                project.PreviewMappings();
            }
        }
        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (options != null)
            {
                options.SaveSettings();
            }
        }
    }
}
