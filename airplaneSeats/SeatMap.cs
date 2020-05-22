using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace airplaneSeats
{
    [XmlRoot("Seats")]
    public class SeatMap
    {
        private Seat[,] seatsMap = new Seat[4, 4];
        private SerializebleSeatMapList list;

        public SeatMap() {
            seatsMap = new Seat[4, 4];

            for(int i = 0; i <= 3; i++) {
                for (int j = 0; j <= 3; j++) {
                    seatsMap[i, j] = new Seat(i, j);
                }
            }
        }

        public bool ReserveSeat(string row, string colunm, string purchaserName) {
            Seat seat = seatsMap[Int32.Parse(row)-1, Seat.IntColunmFromString(colunm)];

            if (seat.IsAvailable)
            {
                seat.PurchaserName = purchaserName;
                seat.IsAvailable = false;
                return true;
            }
            else { 
                return false;
            }
        }

        public bool DeleteSeat(string row, string colunm) {
            if (seatsMap[Int32.Parse(row) - 1, Seat.IntColunmFromString(colunm)].PurchaserName != "")
            {
                seatsMap[Int32.Parse(row) - 1, Seat.IntColunmFromString(colunm)].PurchaserName = "";
                seatsMap[Int32.Parse(row) - 1, Seat.IntColunmFromString(colunm)].IsAvailable = true;
                return true;
            }
            else {
                return false;
            }
        }

        public bool DeleteSeat(string purchaserName)
        {
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (seatsMap[i, j].PurchaserName == purchaserName) {
                        return true;
                    }
                }
            }

            return false;
        }

        public void ClearSeatMap() {
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    seatsMap[i, j].PurchaserName = "";
                    seatsMap[i, j].IsAvailable = true;
                }
            }
        }

        public Seat GetSeatByPurchaserName(string purchaserName) {
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (seatsMap[i, j].PurchaserName == purchaserName)
                    {
                        seatsMap[i, j].PurchaserName = "";
                        seatsMap[i, j].IsAvailable = true;
                        return seatsMap[i, j];
                    }
                }
            }
            return null;
        }

        public void SerializeSeatMap()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SerializebleSeatMapList));
            TextWriter writer = new StreamWriter("seatmap_serialized.xml");
            serializer.Serialize(writer, GetSerializebleList());
            writer.Close();
        }

        public SerializebleSeatMapList GetSerializebleList() {
            list = new SerializebleSeatMapList();
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    list.Add(seatsMap[i,j]);
                }
            }
            return list;
        }

        internal void DeserializeSeatMap()
        {
            SerializebleSeatMapList deserializedList = null;

            string filePath = "seatmap_serialized.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(SerializebleSeatMapList));

            StreamReader reader = new StreamReader(filePath);

            deserializedList = (SerializebleSeatMapList)serializer.Deserialize(reader);
   
            reader.Close();

            UpdateSeatMap(deserializedList);
        }

        private void UpdateSeatMap(SerializebleSeatMapList deserializedList)
        {
            int index = 0;
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    seatsMap[i, j] = deserializedList[index];
                    index++;
                }
            }
        }

        public List<Seat> GetAllSeats()
        {
            List<Seat> list = new List<Seat>();
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    list.Add(seatsMap[i, j]);
                }
            }
            return list;
        }
    }
}
