using MosMetro.DataSet1TableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace MosMetro
{
    /// <summary>
    /// Логика взаимодействия для Tickets.xaml
    /// </summary>
    public partial class Tickets : Page
    {
        ticketsTableAdapter ticketsTableAdapter = new ticketsTableAdapter();
        public Tickets()
        {
            InitializeComponent();
            TicketsGrid.ItemsSource = ticketsTableAdapter.GetData();
            RedactTable.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).frame.Content = new Page1();
        }

        private void AddTicket_Click(object sender, RoutedEventArgs e)
        {
            if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(255, 255, 255))
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(TicketType.Text)|| Convert.ToInt32(Price.Text) < 0)
                    {
                        throw new Exception();
                    }
                    ticketsTableAdapter.InsertQuery(TicketType.Text, Convert.ToInt32(Price.Text));
                    TicketType.Text = ""; Price.Text = "";
                    TicketsGrid.ItemsSource = ticketsTableAdapter.GetData();
                }
                catch
                {
                    MessageBox.Show("Ошибка Значения");
                }
            }
            else
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(TicketType.Text) || Convert.ToInt32(Price.Text) < 0)
                    {
                        throw new Exception();
                    }
                    object id = (TicketsGrid.SelectedItem as DataRowView).Row[0];
                    ticketsTableAdapter.UpdateQuery(TicketType.Text, Convert.ToInt32(Price.Text), Convert.ToInt32(id));
                    TicketsGrid.ItemsSource = ticketsTableAdapter.GetData();
                }
                catch
                {
                    MessageBox.Show("Ошибка Значения");
                }
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (TicketsGrid.SelectedItem as DataRowView).Row[0];
                ticketsTableAdapter.DeleteQuery(Convert.ToInt32(id));
                TicketsGrid.ItemsSource = ticketsTableAdapter.GetData();
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void RedactTable_Click(object sender, RoutedEventArgs e)
        {
            if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(255, 255, 255))
            {
                RedactTable.Background = new SolidColorBrush(Color.FromRgb(191, 0, 255));
                AddTicket.Content = "сохранить изменения";
                TicketType.Text = ""; Price.Text = "";
            }
            else
            {
                RedactTable.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                AddTicket.Content = "Добавить";
                TicketType.Text = ""; Price.Text = "";
            }
        }

        private void TicketsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(191, 0, 255))
                {
                    var choosed = TicketsGrid.SelectedItem as DataRowView;
                    TicketType.Text = Convert.ToString(choosed.Row[1]);
                    Price.Text = Convert.ToString(choosed.Row[2]);
                }
            }
            catch
            {
                TicketType.Text = ""; Price.Text = "";
            }
        }
    }
}
