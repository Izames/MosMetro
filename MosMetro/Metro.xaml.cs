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
    /// Логика взаимодействия для Metro.xaml
    /// </summary>
    public partial class Metro : Page
    {
        MetroTableAdapter MetroTableAdapter = new MetroTableAdapter();
        CityTableAdapter CityTableAdapter = new CityTableAdapter();
        public Metro()
        {
            InitializeComponent();

            Metroes.ItemsSource = MetroTableAdapter.GetData();
            AtCitys.ItemsSource = CityTableAdapter.GetData();
            AtCitys.DisplayMemberPath = "CityName";
            AtCitys.SelectedValuePath = "id";
            RedactTable.Background = new SolidColorBrush(Color.FromRgb(255,255, 255));  
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).frame.Content = new Page1();
        }

        private void AddMetro_Click(object sender, RoutedEventArgs e)
        {
            if(((SolidColorBrush)RedactTable.Background).Color== Color.FromRgb(255, 255, 255))
            {
                try
                {
                    if(String.IsNullOrWhiteSpace(Name.Text) || String.IsNullOrWhiteSpace(OpenDate.Text) || Convert.ToInt32(AtCitys.SelectedValue)==-1)
                    {
                        throw new Exception();
                    }
                    MetroTableAdapter.InsertQuery(Name.Text, OpenDate.Text, Convert.ToInt32(AtCitys.SelectedValue));
                    Metroes.ItemsSource = MetroTableAdapter.GetData();
                    Metroes.Columns[1].Visibility = Visibility.Collapsed;
                    Name.Text = ""; OpenDate.Text = ""; AtCitys.SelectedValue = -1;
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
                    if (String.IsNullOrWhiteSpace(Name.Text) || String.IsNullOrWhiteSpace(OpenDate.Text) || Convert.ToInt32(AtCitys.SelectedValue) == -1)
                    {
                        throw new Exception();
                    }
                    int id = Convert.ToInt32((Metroes.SelectedItem as DataRowView).Row[0]);
                    MetroTableAdapter.UpdateQuery(Name.Text, OpenDate.Text, Convert.ToInt32(AtCitys.SelectedValue), id);
                    Metroes.ItemsSource = MetroTableAdapter.GetData();
                    Metroes.Columns[1].Visibility = Visibility.Collapsed;
                    Name.Text = ""; OpenDate.Text = ""; AtCitys.SelectedValue = -1;
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
                object id = (Metroes.SelectedItem as DataRowView).Row[0];
                MetroTableAdapter.DeleteQuery(Convert.ToInt32(id));
                Metroes.ItemsSource = MetroTableAdapter.GetData();
                Metroes.Columns[1].Visibility = Visibility.Collapsed;
                Name.Text = ""; OpenDate.Text = ""; AtCitys.SelectedValue = -1;
            }
            catch
            {
                MessageBox.Show("Error 404");
            }
        }

        private void RedactTable_Click(object sender, RoutedEventArgs e)
        {
            if(((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(255, 255, 255))
            {
                RedactTable.Background = new SolidColorBrush(Color.FromRgb(191,0,255));
                AddMetro.Content = "Сохранить изменения";
                Name.Text = "";
                OpenDate.Text = "";
                AtCitys.SelectedValue = -1;
            }
            else
            {
                RedactTable.Background = new SolidColorBrush(Color.FromRgb(255,255,255));
                AddMetro.Content = "Добавить";
                Name.Text = "";
                OpenDate.Text = "";
                AtCitys.SelectedValue = -1;
            }
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Metroes.Columns[1].Visibility = Visibility.Collapsed;

        }

        private void Metroes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(191, 0, 255))
            {
                try
                {
                    var choosed = Metroes.SelectedItem as DataRowView;
                    Name.Text = choosed.Row[2].ToString();
                    OpenDate.Text = choosed.Row[3].ToString();
                    AtCitys.SelectedValue = (int)choosed.Row[1];
                }
                catch
                {
                    Name.Text = "";
                    OpenDate.Text = "";
                    AtCitys.SelectedValue = -1;
                }
            }
        }
    }
}