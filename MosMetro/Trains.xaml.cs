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
    public partial class Trains : Page
    {
        TrainsTableAdapter trainsTableAdapter = new TrainsTableAdapter();
        MetroWayTableAdapter MetroWayTableAdapter = new MetroWayTableAdapter();
        public Trains()
        {
            InitializeComponent();
            TrainsGrid.ItemsSource = trainsTableAdapter.GetData();
            AtMetroWay.ItemsSource = MetroWayTableAdapter.GetData();
            AtMetroWay.DisplayMemberPath = "ColorOfWay";
            AtMetroWay.SelectedValuePath = "id";
            RedactTable.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void AddTrain_Click(object sender, RoutedEventArgs e)
        {

            if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(255, 255, 255))
            {
                try
                {
                    if(String.IsNullOrWhiteSpace(TrainModel.Text) || Convert.ToInt32(Quentity.Text)<0|| Convert.ToInt32(AtMetroWay.SelectedValue) == -1)
                    {
                        throw new Exception();
                    }
                    trainsTableAdapter.InsertQuery(TrainModel.Text, Convert.ToInt32(Quentity.Text), Convert.ToInt32(AtMetroWay.SelectedValue));
                    TrainsGrid.ItemsSource =trainsTableAdapter.GetData();
                    TrainsGrid.Columns[3].Visibility = Visibility.Collapsed;
                    TrainModel.Text = ""; Quentity.Text = ""; AtMetroWay.SelectedValue = -1;
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
                    if (String.IsNullOrWhiteSpace(TrainModel.Text) || Convert.ToInt32(Quentity.Text) < 0 || Convert.ToInt32(AtMetroWay.SelectedValue) == -1)
                    {
                        throw new Exception();
                    }
                    int id = Convert.ToInt32((TrainsGrid.SelectedItem as DataRowView).Row[0]);
                    trainsTableAdapter.UpdateQuery(TrainModel.Text, Convert.ToInt32(Quentity.Text), Convert.ToInt32(AtMetroWay.SelectedValue), id);
                    TrainsGrid.ItemsSource = trainsTableAdapter.GetData();
                    TrainsGrid.Columns[3].Visibility = Visibility.Collapsed;
                    TrainModel.Text = ""; Quentity.Text = ""; AtMetroWay.SelectedValue = -1;
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
                object id = (TrainsGrid.SelectedItem as DataRowView).Row[0];
                trainsTableAdapter.DeleteQuery(Convert.ToInt32(id));
                TrainsGrid.ItemsSource = trainsTableAdapter.GetData();
                TrainsGrid.Columns[3].Visibility = Visibility.Collapsed;
                TrainModel.Text = ""; Quentity.Text = ""; AtMetroWay.SelectedValue = -1;
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
                AddTrain.Content = "Сохранить изменения";
                TrainModel.Text = ""; Quentity.Text = ""; AtMetroWay.SelectedValue = -1;
            }
            else
            {
                RedactTable.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                AddTrain.Content = "Добавить";
                TrainModel.Text = ""; Quentity.Text = ""; AtMetroWay.SelectedValue = -1;
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TrainsGrid.Columns[3].Visibility = Visibility.Collapsed;
        }

        private void TrainsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(191, 0, 255))
            {
                try
                {
                    var choosed = TrainsGrid.SelectedItem as DataRowView;
                    TrainModel.Text = choosed.Row[1].ToString();
                    Quentity.Text = choosed.Row[2].ToString();
                    AtMetroWay.SelectedValue = (int)choosed.Row[3];
                }
                catch
                {
                    TrainModel.Text = ""; Quentity.Text = ""; AtMetroWay.SelectedValue = -1;
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).frame.Content = new Page1();
        }
    }
}
