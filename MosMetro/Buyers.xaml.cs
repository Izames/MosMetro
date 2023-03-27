using MosMetro.DataSet1TableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
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
    /// Логика взаимодействия для Buyers.xaml
    /// </summary>
    public partial class Buyers : Page
    {
        buyersTableAdapter buyersTableAdapter = new buyersTableAdapter();
        public Buyers()
        {
            InitializeComponent();
            BuyersGrid.ItemsSource = buyersTableAdapter.GetData();
            RedactTable.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).frame.Content = new Page1();
        }

        private void AddBuyer_Click(object sender, RoutedEventArgs e)
        {
            if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(255, 255, 255))
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(Name.Text)|| String.IsNullOrWhiteSpace(SecondName.Text)|| String.IsNullOrWhiteSpace(Parol.Text))
                    {
                        throw new AccessViolationException();
                    }
                    else
                    {
                        buyersTableAdapter.InsertQuery(Name.Text, SecondName.Text, Parol.Text);
                        BuyersGrid.ItemsSource = buyersTableAdapter.GetData();
                        Name.Text = ""; SecondName.Text = ""; Parol.Text = "";
                    }
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
                    if (String.IsNullOrWhiteSpace(Name.Text) || String.IsNullOrWhiteSpace(SecondName.Text) || String.IsNullOrWhiteSpace(Parol.Text))
                    {
                        throw new AccessViolationException();
                    }
                    else
                    {
                        object id = (BuyersGrid.SelectedItem as DataRowView).Row[0];
                        buyersTableAdapter.UpdateQuery(Name.Text, SecondName.Text, Parol.Text, Convert.ToInt32(id));
                        BuyersGrid.ItemsSource = buyersTableAdapter.GetData();
                    }
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
                object id = (BuyersGrid.SelectedItem as DataRowView).Row[0];
                buyersTableAdapter.DeleteQuery(Convert.ToInt32(id));
                BuyersGrid.ItemsSource = buyersTableAdapter.GetData();
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
                AddBuyer.Content = "сохранить изменения";
                Name.Text = ""; SecondName.Text = ""; Parol.Text = "";
            }
            else
            {
                RedactTable.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                AddBuyer.Content = "Добавить";
                Name.Text = ""; SecondName.Text = ""; Parol.Text = "";
            }
        }

        private void BuyersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(191, 0, 255))
                {
                    var choosed = BuyersGrid.SelectedItem as DataRowView;
                    Name.Text = Convert.ToString(choosed.Row[1]);
                    SecondName.Text = Convert.ToString(choosed.Row[2]);
                    Parol.Text = Convert.ToString(choosed.Row[3]);
                }
            }
            catch
            {
                Name.Text = ""; SecondName.Text = ""; Parol.Text = "";
            }
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<BuyersModel> ForImport = Importing.MyDeserialize<List<BuyersModel>>();
                foreach (var import in ForImport)
                {
                    buyersTableAdapter.InsertQuery(import.Name, import.SecondName, import.parol);
                }
                BuyersGrid.ItemsSource = buyersTableAdapter.GetData();
            }
            catch
            {
                MessageBox.Show("наверное не тот файл");
            }
        }
    }
}
