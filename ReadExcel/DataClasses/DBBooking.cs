using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReadExcel.DataClasses
{
    public class DBBooking
    {
        public int Line { get; set; }
        public string RefId { get; set; }
        public string HotelCode { get; set; }
        public string RoomCode { get; set; }
        public string Meal { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
        public BookingStatus CheckedStatus { get; set; }
        public int CheckLine { get; set; }
        public string CheckRoomCode { get; set; }
        public string CheckMeal { get; set; }
        public int CheckAdults { get; set; }
        public int CheckChildren { get; set; }
        public DateTime CheckCheckin { get; set; }
        public DateTime CheckCheckout { get; set; }

        private static Logger logger = new Logger();

        public DBBooking()
        {

        }

        public static bool HasErrors { get { return logger.HasErrors;  } }
        public static string ErrorMessage { get { return logger.ErrorMessage;  } }

        public static List<DBBooking> LoadDBBookingAscii(string filename)
        {
            List<DBBooking> lista = new List<DBBooking>();
            logger.MessageClear();
            try
            {
                var lines = File.ReadAllLines(filename);
                foreach (string line in lines)
                {
                    var s = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    lista.Add(new DBBooking()
                    {
                        Line = Convert.ToInt32(s[0].Trim()),
                        RefId = s[1].Trim(),
                        HotelCode = s[2].Trim(),
                        RoomCode = s[3].Trim(),
                        Meal = s[4].Trim(),
                        Adults = Convert.ToInt32(s[5].Trim()),
                        Children = Convert.ToInt32(s[6].Trim()),
                        Checkin = Convert.ToDateTime(s[7].Trim()),
                        Checkout = Convert.ToDateTime(s[8].Trim()),
                        CheckedStatus = BookingStatus.NotConfirmed,
                        CheckLine = 0
                    });
                }
            }
            catch (Exception ex)
            {
                logger.MessageAdd(ex.Message);
            } 
            return lista;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            switch (CheckedStatus)
            {
                case BookingStatus.Confirmed:
                    sb.Append(String.Format("{0,8}", CheckLine));
                    sb.Append("\t");
                    sb.Append(RefId);
                    sb.Append("\t");
                    sb.Append(HotelCode);
                    sb.Append("\t");
                    sb.Append(RoomCode);
                    sb.Append("\t");
                    sb.Append(Meal);
                    sb.Append("\t");
                    sb.Append(Checkin.ToShortDateString());
                    sb.Append("\t");
                    sb.Append(Checkout.ToShortDateString());
                    sb.Append("\t");
                    sb.Append(String.Format("{0,2}", Adults));
                    sb.Append("\t");
                    sb.Append(String.Format("{0,2}", Children));
                    sb.AppendLine("\t");

                    sb.Append(String.Format("{0,8}", Line));
                    sb.Append("\t");
                    sb.Append(RefId);
                    sb.Append("\t");
                    sb.Append(HotelCode);
                    sb.Append("\t");
                    sb.Append(RoomCode);
                    sb.Append("\t");
                    sb.Append(Meal);
                    sb.Append("\t");
                    sb.Append(Checkin.ToShortDateString());
                    sb.Append("\t");
                    sb.Append(Checkout.ToShortDateString());
                    sb.Append("\t");
                    sb.Append(String.Format("{0,2}", Adults));
                    sb.Append("\t");
                    sb.Append(String.Format("{0,2}", Children));
                    sb.AppendLine("\t");
                    break;
                case BookingStatus.NotConfirmed:
                    sb.Append(String.Format("{0,8}", Line));
                    sb.Append("\t");
                    sb.Append(RefId);
                    sb.Append("\t");
                    sb.Append(HotelCode);
                    sb.Append("\t");
                    sb.Append(RoomCode);
                    sb.Append("\t");
                    sb.Append(Meal);
                    sb.Append("\t");
                    sb.Append(Checkin.ToShortDateString());
                    sb.Append("\t");
                    sb.Append(Checkout.ToShortDateString());
                    sb.Append("\t");
                    sb.Append(String.Format("{0,2}", Adults));
                    sb.Append("\t");
                    sb.Append(String.Format("{0,2}", Children));
                    sb.Append("\t");
                    break;
                case BookingStatus.Changed:
                    sb.Append(String.Format("{0,8}", CheckLine));
                    sb.Append("\t");
                    sb.Append(RefId);
                    sb.Append("\t");
                    sb.Append(HotelCode);
                    sb.Append("\t");
                    sb.Append(CheckRoomCode);
                    sb.Append("\t");
                    sb.Append(CheckMeal);
                    sb.Append("\t");
                    sb.Append(CheckCheckin.ToShortDateString());
                    sb.Append("\t");
                    sb.Append(CheckCheckout.ToShortDateString());
                    sb.Append("\t");
                    sb.Append(String.Format("{0,2}", CheckAdults));
                    sb.Append("\t");
                    sb.Append(String.Format("{0,2}", CheckChildren));
                    sb.AppendLine("\t");

                    sb.Append(String.Format("{0,8}", Line));
                    sb.Append("\t");
                    sb.Append(RefId);
                    sb.Append("\t");
                    sb.Append(HotelCode);
                    sb.Append("\t");
                    sb.Append(RoomCode);
                    sb.Append("\t");
                    sb.Append(Meal);
                    sb.Append("\t");
                    sb.Append(Checkin.ToShortDateString());
                    sb.Append("\t");
                    sb.Append(Checkout.ToShortDateString());
                    sb.Append("\t");
                    sb.Append(String.Format("{0,2}", Adults));
                    sb.Append("\t");
                    sb.Append(String.Format("{0,2}", Children));
                    sb.AppendLine("\t");
                    break;
                case BookingStatus.Missing:
                    sb.Append(String.Format("{0,8}", CheckLine));
                    sb.Append("\t");
                    sb.Append(RefId);
                    sb.Append("\t");
                    sb.Append(HotelCode);
                    sb.Append("\t");
                    sb.Append(RoomCode);
                    sb.Append("\t");
                    sb.Append(Meal);
                    sb.Append("\t");
                    sb.Append(Checkin.ToShortDateString());
                    sb.Append("\t");
                    sb.Append(Checkout.ToShortDateString());
                    sb.Append("\t");
                    sb.Append(String.Format("{0,2}", Adults));
                    sb.Append("\t");
                    sb.Append(String.Format("{0,2}", Children));
                    sb.Append("\t");
                    break;
                default:
                    break;
            }
            return sb.ToString();
        }
    }

    public enum BookingStatus
    {
        Confirmed,          // matched
        NotConfirmed,       // Προς έλεγχο
        Changed,            // found altered
        Missing             // added based on excel checking file
    }
}
