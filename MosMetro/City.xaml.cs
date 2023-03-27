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
    /// Логика взаимодействия для City.xaml
    /// </summary>
    public partial class City : Page
    {
        CityTableAdapter CityTableAdapter = new CityTableAdapter();
        public City()
        {
            InitializeComponent();
            Cities.ItemsSource = CityTableAdapter.GetData();
            RedactTable.Background = new SolidColorBrush(Color.FromRgb(255,255,255));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).frame.Content = new Page1();
        }

        private void AddCity_Click(object sender, RoutedEventArgs e)
        {
            if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(255, 255, 255))
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(Name.Text) || Convert.ToInt32(Population.Text) < 0)
                    {
                        throw new Exception();
                    }
                    CityTableAdapter.InsertQuery(Name.Text, Convert.ToInt32(Population.Text));
                    Name.Text = ""; Population.Text = "";
                    Cities.ItemsSource = CityTableAdapter.GetData();
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
                    if (String.IsNullOrWhiteSpace(Name.Text) || Convert.ToInt32(Population.Text) < 0)
                    {
                        throw new Exception();
                    }
                    object id = (Cities.SelectedItem as DataRowView).Row[0];
                    CityTableAdapter.UpdateQuery(Name.Text, Convert.ToInt32(Population.Text), Convert.ToInt32(id));
                    Cities.ItemsSource = CityTableAdapter.GetData();
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
                object id = (Cities.SelectedItem as DataRowView).Row[0];
                CityTableAdapter.DeleteQuery(Convert.ToInt32(id));
                Cities.ItemsSource = CityTableAdapter.GetData();
            }
            catch
            {
                MessageBox.Show("Error 404");
            }
        }

        private void RedactTable_Click(object sender, RoutedEventArgs e)
        {
            if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(255, 255, 255))
            {
                RedactTable.Background = new SolidColorBrush(Color.FromRgb(191, 0, 255));
                AddCity.Content = "сохранить изменения";
                Name.Text = "";
                Population.Text = "";
            }
            else
            {
                RedactTable.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                AddCity.Content = "добавить город";
                Name.Text = "";
                Population.Text = "";
            }
        }

        private void Cities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(191, 0, 255))
                {
                    var choosed = Cities.SelectedItem as DataRowView;
                    Name.Text = Convert.ToString(choosed.Row[1]);
                    Population.Text = Convert.ToString(choosed.Row[2]);
                }
            }
            catch
            {
                Name.Text = "";
                Population.Text = "";
            }
        }
    }
}
