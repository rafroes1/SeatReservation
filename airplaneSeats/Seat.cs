using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace airplaneSeats
{
    public class Seat
    {
        private string purchaserName;
        private int row;
        private int colunm;
        private bool isAvailable;

        public string PurchaserName
        {
            get { return this.purchaserName; }
            set { this.purchaserName = value; }
        }

        public int Row
        {
            get { return this.row; }
            set { this.row = value; }
        }

        public int Colunm
        {
            get { return this.colunm; }
            set { this.colunm = value; }
        }

        public bool IsAvailable
        {
            get { return this.isAvailable; }
            set { this.isAvailable = value; }
        }

        public Seat()
        {
            this.purchaserName = "";
            this.row = 0;
            this.colunm = 0;
            this.isAvailable = true;
        }

        public Seat(int row, int colunm)
        {
            this.purchaserName = "";
            this.isAvailable = true;
            this.row = row;
            this.colunm = colunm;
        }

        static public int IntColunmFromString(string colunm)
        {
            if (colunm == "A")
            {
                return 0;
            }
            else if (colunm == "B")
            {
                return 1;
            }
            else if (colunm == "C")
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        static public string StringColunmFromInt(int colunm)
        {
            if (colunm == 0)
            {
                return "A";
            }
            else if (colunm == 1)
            {
                return "B";
            }
            else if (colunm == 2)
            {
                return "C";
            }
            else
            {
                return "D";
            }
        }
    }
}
