using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReadExcel.DataClasses
{
    public class Room
    {
        public string HotelCode { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        private static Logger logger = new Logger();

        public Room()
        {

        }

        public static bool HasErrors { get { return logger.HasErrors;  } }
        public static string ErrorMessage { get { return logger.ErrorMessage;  } }

        public static List<Room> LoadRoomAscii(string filename)
        {
            List<Room> lista = new List<Room>();
            logger.MessageClear();
            try
            {
                var lines = File.ReadAllLines(filename);
                foreach (string line in lines)
                {
                    var s = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    lista.Add(new Room() { HotelCode = s[0].Trim(), Code = s[1].Trim(), Description = s[2].Trim() });
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
