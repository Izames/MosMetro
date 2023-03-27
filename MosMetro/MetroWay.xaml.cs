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

    public partial class MetroWay : Page
    {
        MetroWayTableAdapter MetroWayTableAdapter = new MetroWayTableAdapter();
        MetroTableAdapter MetroTableAdapter = new MetroTableAdapter();
        public MetroWay()
        {
            InitializeComponent();
            MetroWays.ItemsSource = MetroWayTableAdapter.GetData();
            AtMetro.ItemsSource = MetroTableAdapter.GetData();
            AtMetro.DisplayMemberPath = "MetroName";
            AtMetro.SelectedValuePath = "id";
            RedactTable.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        

        private void AddMetroWay_Click(object sender, RoutedEventArgs e)
        {
            if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(255, 255, 255))
            {
                try
                {
                    if(String.IsNullOrWhiteSpace(Colors.Text) || Convert.ToInt32(LengthKm.Text)<0 || Convert.ToInt32(AtMetro.SelectedValue) == -1)
                    {
                        throw new Exception();
                    }
                    MetroWayTableAdapter.InsertQuery(Colors.Text,Convert.ToInt32(LengthKm.Text), Convert.ToInt32(AtMetro.SelectedValue));
                    MetroWays.ItemsSource = MetroWayTableAdapter.GetData();
                    MetroWays.Columns[3].Visibility = Visibility.Collapsed;
                    Colors.Text = ""; LengthKm.Text = ""; AtMetro.SelectedValue = -1;
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
                    if (String.IsNullOrWhiteSpace(Colors.Text) || Convert.ToInt32(LengthKm.Text) < 0 || Convert.ToInt32(AtMetro.SelectedValue) == -1)
                    {
                        throw new Exception();
                    }
                    int id = Convert.ToInt32((MetroWays.SelectedItem as DataRowView).Row[0]);
                    MetroWayTableAdapter.UpdateQuery(Colors.Text, Convert.ToInt32(LengthKm.Text), Convert.ToInt32(AtMetro.SelectedValue), id);
                    MetroWays.ItemsSource = MetroWayTableAdapter.GetData();
                    MetroWays.Columns[3].Visibility = Visibility.Collapsed;
                    Colors.Text = ""; LengthKm.Text = ""; AtMetro.SelectedValue = -1;
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
                object id = (MetroWays.SelectedItem as DataRowView).Row[0];
                MetroWayTableAdapter.DeleteQuery(Convert.ToInt32(id));
                MetroWays.ItemsSource = MetroWayTableAdapter.GetData();
                MetroWays.Columns[3].Visibility = Visibility.Collapsed;
                Colors.Text = ""; LengthKm.Text = ""; AtMetro.SelectedValue = -1;
            }
            catch
            {
                MessageBox.Show("Error 404");
            }
        }

        private void RedactTable_Click_1(object sender, RoutedEventArgs e)
        {
            if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(255, 255, 255))
            {
                RedactTable.Background = new SolidColorBrush(Color.FromRgb(191, 0, 255));
                AddMetroWay.Content = "Сохранить изменения";
                Colors.Text = "";
                LengthKm.Text = "";
                AtMetro.SelectedValue = -1;
            }
            else
            {
                RedactTable.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                AddMetroWay.Content = "Добавить";
                Colors.Text = "";
                LengthKm.Text = "";
                AtMetro.SelectedValue = -1;
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MetroWays.Columns[3].Visibility = Visibility.Collapsed;
        }

        private void MetroWays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(191, 0, 255))
            {
                try
                {
                    var choosed = MetroWays.SelectedItem as DataRowView;
                    Colors.Text = choosed.Row[1].ToString();
                    LengthKm.Text = choosed.Row[2].ToString();
                    AtMetro.SelectedValue = (int)choosed.Row[3];
                }
                catch
                {
                    Colors.Text = "";
                    LengthKm.Text = "";
                    AtMetro.SelectedValue = -1;
                }
            }
        }

        private void Back_Click_1(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).frame.Content = new Page1();
        }
    }
}