using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace airplaneSeats
{
    class SeatMapElements
    {
        Label label;
        Border border;

        public Label Label {
            get { return this.label; }
        }

        public Border Border {
            get { return this.border; }
        }

        public SeatMapElements() {
            this.label = new Label();
            this.border = new Border();
        }

        public SeatMapElements(Label label, Border border) {
            this.label = label;
            this.border = border;
        }
    }
}
