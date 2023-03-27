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
    /// Логика взаимодействия для Cheques.xaml
    /// </summary>
    public partial class Cheques : Page
    {
        chequesTableAdapter chequesTableAdapter = new chequesTableAdapter();
        ticketsTableAdapter ticketsTableAdapter = new ticketsTableAdapter();
        buyersTableAdapter buyersTableAdapter = new buyersTableAdapter();
        MetroTableAdapter MetroTableAdapter = new MetroTableAdapter();
        public Cheques()
        {
            InitializeComponent();
            ChequesGrid.ItemsSource = chequesTableAdapter.GetData();
            AtTicket.ItemsSource = ticketsTableAdapter.GetData();
            AtTicket.DisplayMemberPath = "TypeOfTicket";
            AtTicket.SelectedValuePath = "id";
            AtBuyer.ItemsSource = buyersTableAdapter.GetData();
            AtBuyer.DisplayMemberPath = "SecondName";
            AtBuyer.SelectedValuePath = "id";
            AtMetro.ItemsSource = MetroTableAdapter.GetData();
            AtMetro.DisplayMemberPath = "MetroName";
            AtMetro.SelectedValuePath = "id";
            RedactTable.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void AddCheque_Click(object sender, RoutedEventArgs e)
        {
            if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(255, 255, 255))
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(Date.Text) || Convert.ToInt32(AtMetro.SelectedValue) == -1 || Convert.ToInt32(AtTicket.SelectedValue) == -1)
                    {
                        throw new Exception();
                    }
                    chequesTableAdapter.InsertQuery(Convert.ToInt32(AtTicket.SelectedValue), Convert.ToInt32(AtBuyer.SelectedValue), Date.Text, Convert.ToInt32(AtMetro.SelectedValue));
                    ChequesGrid.ItemsSource = chequesTableAdapter.GetData();
                    OutCheque.Outing(Convert.ToInt32((ChequesGrid.Items[ChequesGrid.Items.Count - 2] as DataRowView).Row[0]), Convert.ToString((ChequesGrid.Items[ChequesGrid.Items.Count - 2] as DataRowView).Row[5]), Convert.ToInt32((ticketsTableAdapter.GetData().Rows)[Convert.ToInt32(AtTicket.SelectedValue)-1][2]));
                    ChequesGrid.Columns[1].Visibility = Visibility.Collapsed;
                    ChequesGrid.Columns[2].Visibility = Visibility.Collapsed;
                    ChequesGrid.Columns[4].Visibility = Visibility.Collapsed;
                    Date.Text = ""; AtMetro.SelectedValue = -1; AtTicket.SelectedValue = -1; AtBuyer.SelectedValue = -1;
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
                    if (String.IsNullOrWhiteSpace(Date.Text) || Convert.ToInt32(AtMetro.SelectedValue) == -1 || Convert.ToInt32(AtTicket.SelectedValue) == -1)
                    {
                        throw new Exception();
                    }
                    int id = Convert.ToInt32((ChequesGrid.SelectedItem as DataRowView).Row[0]);
                    chequesTableAdapter.UpdateQuery(Convert.ToInt32(AtTicket.SelectedValue), Convert.ToInt32(AtBuyer.SelectedValue), Date.Text, Convert.ToInt32(AtMetro.SelectedValue), id);
                    ChequesGrid.ItemsSource = chequesTableAdapter.GetData();
                    ChequesGrid.Columns[1].Visibility = Visibility.Collapsed;
                    ChequesGrid.Columns[2].Visibility = Visibility.Collapsed;
                    ChequesGrid.Columns[4].Visibility = Visibility.Collapsed;
                    Date.Text = ""; AtMetro.SelectedValue = -1; AtTicket.SelectedValue = -1; AtBuyer.SelectedValue = -1;
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
                object id = (ChequesGrid.SelectedItem as DataRowView).Row[0];
                chequesTableAdapter.DeleteQuery(Convert.ToInt32(id));
                ChequesGrid.ItemsSource = chequesTableAdapter.GetData();
                ChequesGrid.Columns[1].Visibility = Visibility.Collapsed;
                ChequesGrid.Columns[2].Visibility = Visibility.Collapsed;
                ChequesGrid.Columns[4].Visibility = Visibility.Collapsed;
                Date.Text = ""; AtMetro.SelectedValue = -1; AtTicket.SelectedValue = -1; AtBuyer.SelectedValue = -1;
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
                AddCheque.Content = "Сохранить изменения";
                Date.Text = ""; AtMetro.SelectedValue = -1; AtTicket.SelectedValue = -1; AtBuyer.SelectedValue = -1;
            }
            else
            {
                RedactTable.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                AddCheque.Content = "Добавить";
                Date.Text = ""; AtMetro.SelectedValue = -1; AtTicket.SelectedValue = -1; AtBuyer.SelectedValue = -1;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
/*            ChequesGrid.Columns[1].Visibility = Visibility.Collapsed;
            ChequesGrid.Columns[2].Visibility = Visibility.Collapsed;
            ChequesGrid.Columns[4].Visibility = Visibility.Collapsed;*/
        }

        private void ChequesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(191, 0, 255))
            {
                try
                {
                    var choosed = ChequesGrid.SelectedItem as DataRowView;
                    Date.Text = choosed.Row[3].ToString();
                    AtTicket.SelectedValue = (int)choosed.Row[1];
                    AtBuyer.SelectedValue = (int)choosed.Row[2];
                    AtMetro.SelectedValue = (int)choosed.Row[4];
                }
                catch
                {
                    Date.Text = ""; AtMetro.SelectedValue = -1; AtTicket.SelectedValue = -1; AtBuyer.SelectedValue = -1;
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).frame.Content = new Page1();
        }

        private void Out_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var choosed = ChequesGrid.SelectedItem as DataRowView;
                int priceid = Convert.ToInt32(choosed[1]);
                var tickets = ticketsTableAdapter.GetData();
                int price = 0;
                for (int i = 0; i < tickets.Count; i++)
                {
                    if (priceid == Convert.ToInt32(tickets[i][0]))
                    {
                        price = Convert.ToInt32(tickets[i][2]);
                        break;
                    }
                }
                OutCheque.Outing(Convert.ToInt32((ChequesGrid.SelectedItem as DataRowView)[0]),
                Convert.ToString((ChequesGrid.SelectedItem as DataRowView)[5]),
                price);
            }
            catch
            {
                MessageBox.Show("что-то не так");
            }
        }
    }
}
