using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReadExcel.DataClasses
{
    public class Hotel
    {
        public string Code { get; set; }
        public string Description { get; set; }

        private static Logger logger = new Logger();

        public Hotel()
        {

        }

        public static bool HasErrors { get { return logger.HasErrors;  } }
        public static string ErrorMessage { get { return logger.ErrorMessage;  } }

        public string DashedDescription { get {return Code + " - " + Description;} }



        public static List<Hotel> LoadHotelAscii(string filename)
        {
            List<Hotel> lista = new List<Hotel>();
            logger.MessageClear();
            try
            {
                var lines = File.ReadAllLines(filename);
                foreach (string line in lines)
                {
                    var s = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    lista.Add(new Hotel() { Code = s[0].Trim(), Description = s[1].Trim() });
                }
            }
            catch (Exception ex)
            {
                logger.MessageAdd(ex.Message);
            } 
            return lista;
        }
    }

}
