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
    /// Логика взаимодействия для BuyersQueries.xaml
    /// </summary>
    public partial class BuyersQueries : Page
    {
        queriesTableAdapter adapter = new queriesTableAdapter();
        ticketsTableAdapter ticketsTableAdapter = new ticketsTableAdapter();
        MetroTableAdapter MetroTableAdapter = new MetroTableAdapter();
        public BuyersQueries()
        {
            InitializeComponent();
            QueriesGrid.ItemsSource=adapter.GetData();
            AtTicket.ItemsSource = ticketsTableAdapter.GetData();
            AtTicket.DisplayMemberPath = "TypeOfTicket";
            AtTicket.SelectedValuePath = "id";
            AtMetro.ItemsSource = MetroTableAdapter.GetData();
            AtMetro.DisplayMemberPath = "MetroName";
            AtMetro.SelectedValuePath = "id";
            RedactTable.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void AddQuery_Click(object sender, RoutedEventArgs e)
        {
            if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(255, 255, 255))
            {
                try
                {
                    if(String.IsNullOrWhiteSpace(User.Text) || Convert.ToInt32(AtMetro.SelectedValue) == -1 || Convert.ToInt32(AtTicket.SelectedValue) == -1)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        adapter.InsertQuery(Convert.ToInt32(AtTicket.SelectedValue), Convert.ToInt32(AtMetro.SelectedValue), User.Text);
                        QueriesGrid.ItemsSource = adapter.GetData();
                        QueriesGrid.Columns[1].Visibility = Visibility.Collapsed;
                        QueriesGrid.Columns[2].Visibility = Visibility.Collapsed;
                        AtMetro.SelectedValue = -1; AtTicket.SelectedValue = -1; User.Text = "";
                    }  
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
                    if (String.IsNullOrWhiteSpace(User.Text) || Convert.ToInt32(AtMetro.SelectedValue) == -1 || Convert.ToInt32(AtTicket.SelectedValue) == -1)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        int id = Convert.ToInt32((QueriesGrid.SelectedItem as DataRowView).Row[0]);
                        adapter.UpdateQuery(Convert.ToInt32(AtMetro.SelectedValue), Convert.ToInt32(AtTicket.SelectedValue), User.Text, id);
                        QueriesGrid.ItemsSource = adapter.GetData();
                        QueriesGrid.Columns[1].Visibility = Visibility.Collapsed;
                        QueriesGrid.Columns[2].Visibility = Visibility.Collapsed;
                        AtMetro.SelectedValue = -1; AtTicket.SelectedValue = -1; User.Text = "";
                    }
                }
                catch (System.FormatException)
                {
                    MessageBox.Show("Ошибка значения");
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (QueriesGrid.SelectedItem as DataRowView).Row[0];
                adapter.DeleteQuery(Convert.ToInt32(id));
                QueriesGrid.ItemsSource = adapter.GetData();
                QueriesGrid.Columns[1].Visibility = Visibility.Collapsed;
                QueriesGrid.Columns[2].Visibility = Visibility.Collapsed;
                AtMetro.SelectedValue = -1; AtTicket.SelectedValue = -1; User.Text = "";
            }
            catch 
            {
                MessageBox.Show("что-то не так");
            }
        }

        private void RedactTable_Click(object sender, RoutedEventArgs e)
        {
            if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(255, 255, 255))
            {
                RedactTable.Background = new SolidColorBrush(Color.FromRgb(191, 0, 255));
                AddQuery.Content = "Сохранить изменения";
                AtMetro.SelectedValue = -1; AtTicket.SelectedValue = -1; User.Text = "";
            }
            else
            {
                RedactTable.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                AddQuery.Content = "Добавить";
                AtMetro.SelectedValue = -1; AtTicket.SelectedValue = -1; User.Text = "";
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            QueriesGrid.Columns[1].Visibility = Visibility.Collapsed;
            QueriesGrid.Columns[2].Visibility = Visibility.Collapsed;
        }

        private void QueriesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(191, 0, 255))
            {
                try
                {
                    var choosed = QueriesGrid.SelectedItem as DataRowView;
                    AtTicket.SelectedValue = (int)choosed.Row[1];
                    AtMetro.SelectedValue = (int)choosed.Row[2];
                    User.Text=Convert.ToString(choosed.Row[5]);
                }
                catch
                {
                    AtMetro.SelectedValue = -1; AtTicket.SelectedValue = -1; User.Text = "";
                }
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).frame.Content = new Page1();
        }
    }
}
