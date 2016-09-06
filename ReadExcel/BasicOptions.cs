using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;

namespace ReadExcel
{
    public class BasicOptions
    {
        public int TourOperatorCode { get; set; }
        public Season Season { get; set; }
        public int SeasonYear { get; set; }
        public string MappingFileFolder { get; private set; }
        public string MappingFileName 
        {
            get
            {
                string fname = DefaultOperSeasPrefix + ".mappings.unl";
                return System.IO.Path.Combine(MappingFileFolder, fname);
            }
        }

        public BasicOptions()
        {
            try
            {
                TourOperatorCode = Properties.Settings.Default.TourOperatorCode;
            }
            catch { TourOperatorCode = 6; }
            
            Season = ReadExcel.Season.Summer;
            try
            {
                SeasonYear = Properties.Settings.Default.SeasonYear;
            }
            catch { SeasonYear = 2016; }
            try
            {
                MappingFileFolder = Properties.Settings.Default.MappingFileFolder;
            }
            catch { MappingFileFolder = System.IO.Path.GetDirectoryName(Application.ExecutablePath); }
        }
        public string DefaultOperSeasPrefix 
        {
            get
            {
                return String.Format("{0}{1}.{2}", 
                    Season == ReadExcel.Season.Summer ? "SO" : "WI",
                    SeasonYear % 100,
                    TourOperatorCode);
            }
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.TourOperatorCode = this.TourOperatorCode;
            Properties.Settings.Default.SeasonYear = this.SeasonYear;
            Properties.Settings.Default.MappingFileFolder = this.MappingFileFolder;
            Properties.Settings.Default.Save();
        }
    }

    public enum Season
    {
        Winter,
        Summer
    }
}
