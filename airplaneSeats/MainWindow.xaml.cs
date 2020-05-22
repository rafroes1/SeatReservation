using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace airplaneSeats
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string row = "";
        private string colunm = "";
        private SeatMap seatMap;
        private Dictionary<string, SeatMapElements> gridElements;

        public MainWindow()
        {
            InitializeComponent();

            seatMap = new SeatMap();

            gridElements = new Dictionary<string, SeatMapElements>();

            setGridElements();
        }

        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;

            if ((name != "")||(row != "" && colunm != ""))
            {
                if (name != "") {
                    if (seatMap.DeleteSeat(name))
                    {
                        Seat seat = seatMap.GetSeatByPurchaserName(name);
                        updateGridSeatRemoved((seat.Row+1).ToString(), Seat.StringColunmFromInt(seat.Colunm));
                        MessageBox.Show(name + "'s seat successfully removed!", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        return;
                    }
                    else {
                        MessageBox.Show(name + " has no seat reserved", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                if (row != "" && colunm != "") {
                    if (seatMap.DeleteSeat(row, colunm))
                    {
                        updateGridSeatRemoved(row, colunm);
                        MessageBox.Show("Seat " + (row + colunm) + " sucessfully removed!", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        return;
                    }
                    else {
                        MessageBox.Show("Seat " + (row+colunm) + " is not reserved", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }
            else {
                MessageBox.Show("To delete fill necessary information: Name or Row and Colunm", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ReserveButton(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;

            if ((name != "") && (row != "") && (colunm != ""))
            {
                if (seatMap.ReserveSeat(row, colunm, nameTextBox.Text))
                {
                    updateGridSeatReserved(name, row, colunm);
                    MessageBox.Show("Seat successfully reserved!", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else
                {
                    MessageBox.Show("Choosen seat is already taken.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else {
                MessageBox.Show("Fill necessary information", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RowComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)rowComboBox.SelectedItem;
            row = typeItem.Content.ToString();
        }

        private void ColunmComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)colunmComboBox.SelectedItem;
            colunm = typeItem.Content.ToString();
        }

        private void setGridElements() {
            //setting first row
            gridElements.Add("1A", new SeatMapElements(label1a, border1a));
            gridElements.Add("1B", new SeatMapElements(label1b, border1b));
            gridElements.Add("1C", new SeatMapElements(label1c, border1c));
            gridElements.Add("1D", new SeatMapElements(label1d, border1d));

            //setting second row
            gridElements.Add("2A", new SeatMapElements(label2a, border2a));
            gridElements.Add("2B", new SeatMapElements(label2b, border2b));
            gridElements.Add("2C", new SeatMapElements(label2c, border2c));
            gridElements.Add("2D", new SeatMapElements(label2d, border2d));

            //setting third row
            gridElements.Add("3A", new SeatMapElements(label3a, border3a));
            gridElements.Add("3B", new SeatMapElements(label3b, border3b));
            gridElements.Add("3C", new SeatMapElements(label3c, border3c));
            gridElements.Add("3D", new SeatMapElements(label3d, border3d));

            //setting fourth row
            gridElements.Add("4A", new SeatMapElements(label4a, border4a));
            gridElements.Add("4B", new SeatMapElements(label4b, border4b));
            gridElements.Add("4C", new SeatMapElements(label4c, border4c));
            gridElements.Add("4D", new SeatMapElements(label4d, border4d));
        }

        private void updateGridSeatReserved(string name, string row, string colunm)
        {
            gridElements[row + colunm].Label.Content = name;
            gridElements[row + colunm].Border.Background = new SolidColorBrush(Color.FromRgb(255, 102, 102));
        }

        private void updateGridSeatRemoved(string row, string colunm){
            gridElements[row + colunm].Label.Content = "available";
            gridElements[row + colunm].Border.Background = new SolidColorBrush(Color.FromRgb(133, 224, 133));
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            seatMap.DeserializeSeatMap();
            UpdateGridAfterLoaded();
            MessageBox.Show("Seat map loaded!", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void UpdateGridAfterLoaded()
        {
            List<Seat> allSeats = seatMap.GetAllSeats();

            for (int i = 0; i < allSeats.Count; i++) {
                if (allSeats[i].IsAvailable == true)
                {
                    int aux = allSeats[i].Row + 1;
                    gridElements[aux + Seat.StringColunmFromInt(allSeats[i].Colunm)].Label.Content = "available";
                    gridElements[aux + Seat.StringColunmFromInt(allSeats[i].Colunm)].Border.Background = new SolidColorBrush(Color.FromRgb(133, 224, 133));
                }
                else
                {
                    int aux = allSeats[i].Row + 1;
                    gridElements[aux + Seat.StringColunmFromInt(allSeats[i].Colunm)].Label.Content = allSeats[i].PurchaserName;
                    gridElements[aux + Seat.StringColunmFromInt(allSeats[i].Colunm)].Border.Background = new SolidColorBrush(Color.FromRgb(255, 102, 102));
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            seatMap.SerializeSeatMap();
            MessageBox.Show("Seat map saved!", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            seatMap.ClearSeatMap();
            foreach (var key in gridElements.Keys)
            {
                gridElements[key].Label.Content = "available";
                gridElements[key].Border.Background = new SolidColorBrush(Color.FromRgb(133, 224, 133));
            }
            MessageBox.Show("All reservations were removed!", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void Linq1_Click(object sender, RoutedEventArgs e)
        {
            outputLabel.Text = "";
            List<Seat> list = seatMap.GetAllSeats();

            var query = from seat in list where seat.IsAvailable == false orderby seat.PurchaserName descending select seat.PurchaserName;

            if (query.Count() != 0)
            {
                foreach (String name in query)
                {
                    outputLabel.Text = outputLabel.Text + name + "\r\n";
                }
                MessageBox.Show("Query executed!", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else {
                MessageBox.Show("No results found", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Linq2_Click(object sender, RoutedEventArgs e)
        {
            outputLabel.Text = "";
            List<Seat> list = seatMap.GetAllSeats();

            var query = from seat in list where seat.IsAvailable == false orderby seat.PurchaserName.Length ascending, seat.PurchaserName ascending select seat.PurchaserName;

            if (query.Count() != 0)
            {
                foreach (String name in query)
                {
                    outputLabel.Text = outputLabel.Text + name + "\r\n";
                }
                MessageBox.Show("Query executed!", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
            {
                MessageBox.Show("No results found", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void Linq3_Click(object sender, RoutedEventArgs e)
        {
            outputLabel.Text = "";
            List<Seat> list = seatMap.GetAllSeats();

            var query = from seat in list where seat.IsAvailable == true orderby seat.Row ascending select seat;

            if (query.Count() != 0)
            {
                foreach (Seat s in query)
                {
                    int row = s.Row + 1;
                    string colunm = Seat.StringColunmFromInt(s.Colunm);
                    string seat = row + colunm;
                    outputLabel.Text = outputLabel.Text + seat + "\r\n";
                }
                MessageBox.Show("Query executed!", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
            {
                MessageBox.Show("No results found", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
