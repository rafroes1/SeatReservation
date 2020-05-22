using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace airplaneSeats
{
    [XmlRoot("Seats")]
    public class SerializebleSeatMapList : IEnumerable<Seat>
    {
        private List<Seat> seatList = null;

        public SerializebleSeatMapList() {
            seatList = new List<Seat>();
        }

        public void Add(Seat seat) {
            seatList.Add(seat);
        }

        public int Count
        {
            get { return seatList.Count; }
        }

        public Seat this[int i]
        {
            get { return seatList[i]; }
            set { seatList[i] = value; }
        }

        public IEnumerator<Seat> GetEnumerator()
        {
            return ((IEnumerable<Seat>)seatList).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Seat>)seatList).GetEnumerator();
        }
    }
}
