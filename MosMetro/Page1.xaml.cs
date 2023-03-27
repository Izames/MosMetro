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
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void BackToAutorizate_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).Height = 310;
            (Application.Current.MainWindow as MainWindow).Width = 300;
            (Application.Current.MainWindow as MainWindow).frame.Content = null;
        }

        private void City_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new City();
        }

        private void Metro_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new Metro();
        }

        private void MetroWay_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new MetroWay();
        }

        private void Stations_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new Stations();
        }

        private void Trains_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new Trains();
        }

        private void Tickets_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new Tickets();
        }

        private void Buyers_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new Buyers();
        }

        private void Cheques_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new Cheques();
        }

        private void PersonalFCs_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new PersonalFCs();
        }

        private void Personal_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new Personal();
        }

        private void Queries_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new BuyersQueries();
        }
    }
}
