using MosMetro.DataSet1TableAdapters;
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

namespace MosMetro
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        queriesTableAdapter queriesTableAdapter = new queriesTableAdapter();
        ticketsTableAdapter ticketsTableAdapter = new ticketsTableAdapter();
        MetroTableAdapter MetroTableAdapter = new MetroTableAdapter();
        public UserPage()
        {
            InitializeComponent();
            Tickets.ItemsSource = ticketsTableAdapter.GetData();
            Metroes.ItemsSource = MetroTableAdapter.GetData();
            AtTicket.ItemsSource = ticketsTableAdapter.GetData();
            AtTicket.DisplayMemberPath = "TypeOfTicket";
            AtTicket.SelectedValuePath = "id";
            AtMetro.ItemsSource = MetroTableAdapter.GetData();
            AtMetro.DisplayMemberPath = "MetroName";
            AtMetro.SelectedValuePath = "id";
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).frame.Content = null;
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(AtMetro.SelectedValue) == -1 || Convert.ToInt32(AtTicket.SelectedValue) == -1 || String.IsNullOrWhiteSpace((Application.Current.MainWindow as MainWindow).SecondName.Text))
                {
                    throw new Exception();
                }
                queriesTableAdapter.InsertQuery(Convert.ToInt32(AtMetro.SelectedValue), Convert.ToInt32(AtTicket.SelectedValue), (Application.Current.MainWindow as MainWindow).SecondName.Text);
                AtMetro.SelectedValue = -1; AtTicket.SelectedValue = -1;
                MessageBox.Show("Запрос на покупку билета был отправлен");
            }
            catch
            {
                MessageBox.Show("что-то не так");
            }
        }
    }
}
