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
    public partial class Stations : Page
    {
        StationsTableAdapter StationsTableAdapter = new StationsTableAdapter();
        MetroWayTableAdapter MetroWayTableAdapter = new MetroWayTableAdapter();
        public Stations()
        {
            InitializeComponent();
            MetroStations.ItemsSource = StationsTableAdapter.GetData();
            AtMetroWay.ItemsSource = MetroWayTableAdapter.GetData();
            AtMetroWay.DisplayMemberPath = "ColorOfWay";
            AtMetroWay.SelectedValuePath = "id";
            RedactTable.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(255, 255, 255))
            {
                try
                {
                    if(String.IsNullOrWhiteSpace(StationName.Text)|| Convert.ToInt32(PeopleAtDay.Text)<0|| Convert.ToInt32(AtMetroWay.SelectedValue) == -1)
                    {
                        throw new Exception();
                    }
                    StationsTableAdapter.InsertQuery(StationName.Text, Convert.ToInt32(PeopleAtDay.Text), Convert.ToInt32(AtMetroWay.SelectedValue));
                    MetroStations.ItemsSource = StationsTableAdapter.GetData();
                    MetroStations.Columns[3].Visibility = Visibility.Collapsed;
                    StationName.Text = ""; PeopleAtDay.Text = ""; AtMetroWay.SelectedValue = -1;
                }
                catch
                {
                    MessageBox.Show("Ошибка значения");
                }
            }
            else
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(StationName.Text) || Convert.ToInt32(PeopleAtDay.Text) < 0 || Convert.ToInt32(AtMetroWay.SelectedValue) == -1)
                    {
                        throw new Exception();
                    }
                    int id = Convert.ToInt32((MetroStations.SelectedItem as DataRowView).Row[0]);
                    StationsTableAdapter.UpdateQuery(StationName.Text, Convert.ToInt32(PeopleAtDay.Text), Convert.ToInt32(AtMetroWay.SelectedValue), id);
                    MetroStations.ItemsSource = StationsTableAdapter.GetData();
                    MetroStations.Columns[3].Visibility = Visibility.Collapsed;
                    StationName.Text = ""; PeopleAtDay.Text = ""; AtMetroWay.SelectedValue = -1;
                }
                catch 
                {
                    MessageBox.Show("Ошибка значения");
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (MetroStations.SelectedItem as DataRowView).Row[0];
                StationsTableAdapter.DeleteQuery(Convert.ToInt32(id));
                MetroStations.ItemsSource = StationsTableAdapter.GetData();
                MetroStations.Columns[3].Visibility = Visibility.Collapsed;
                StationName.Text = ""; PeopleAtDay.Text = ""; AtMetroWay.SelectedValue = -1;
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
                AddStation.Content = "Сохранить изменения";
                StationName.Text = ""; PeopleAtDay.Text = ""; AtMetroWay.SelectedValue = -1;
            }
            else
            {
                RedactTable.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                AddStation.Content = "Добавить";
                StationName.Text = ""; PeopleAtDay.Text = ""; AtMetroWay.SelectedValue = -1;
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MetroStations.Columns[3].Visibility = Visibility.Collapsed;
        }

        private void MetroStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(191, 0, 255))
            {
                try
                {
                    var choosed = MetroStations.SelectedItem as DataRowView;
                    StationName.Text = choosed.Row[1].ToString();
                    PeopleAtDay.Text = choosed.Row[2].ToString();
                    AtMetroWay.SelectedValue = (int)choosed.Row[3];
                }
                catch
                {
                    StationName.Text = ""; PeopleAtDay.Text = ""; AtMetroWay.SelectedValue = -1;
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).frame.Content = new Page1();
        }
    }
}
