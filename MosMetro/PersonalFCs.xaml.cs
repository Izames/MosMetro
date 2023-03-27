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
    /// Логика взаимодействия для PersonalFCs.xaml
    /// </summary>
    public partial class PersonalFCs : Page
    {
        PersonalFCsTableAdapter personalFCs = new PersonalFCsTableAdapter();
        public PersonalFCs()
        {
            InitializeComponent();
            PersonalFCsGrid.ItemsSource = personalFCs.GetData();
            RedactTable.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).frame.Content = new Page1();
        }

        private void AddFCs_Click(object sender, RoutedEventArgs e)
        {
            if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(255, 255, 255))
            {
                try
                {
                    if(String.IsNullOrWhiteSpace(Name.Text)||String.IsNullOrWhiteSpace(SecondName.Text))
                    {
                        throw new Exception();
                    }
                    personalFCs.InsertQuery(Name.Text,SecondName.Text);
                    Name.Text = ""; SecondName.Text = "";
                    PersonalFCsGrid.ItemsSource = personalFCs.GetData();
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
                    if (String.IsNullOrWhiteSpace(Name.Text) || String.IsNullOrWhiteSpace(SecondName.Text))
                    {
                        throw new Exception();
                    }
                    object id = (PersonalFCsGrid.SelectedItem as DataRowView).Row[0];
                    personalFCs.UpdateQuery(Name.Text, SecondName.Text, Convert.ToInt32(id));
                    PersonalFCsGrid.ItemsSource = personalFCs.GetData();
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
                object id = (PersonalFCsGrid.SelectedItem as DataRowView).Row[0];
                personalFCs.DeleteQuery(Convert.ToInt32(id));
                PersonalFCsGrid.ItemsSource = personalFCs.GetData();
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
                AddFCs.Content = "сохранить изменения";
                Name.Text = ""; SecondName.Text = "";
            }
            else
            {
                RedactTable.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                AddFCs.Content = "Добавить";
                Name.Text = ""; SecondName.Text = "";
            }
        }

        private void PersonalFCsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(191, 0, 255))
                {
                    var choosed = PersonalFCsGrid.SelectedItem as DataRowView;
                    Name.Text = Convert.ToString(choosed.Row[1]);
                    SecondName.Text = Convert.ToString(choosed.Row[2]);
                }
            }
            catch
            {
                Name.Text = ""; SecondName.Text = "";
            }
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<WorkersModel> ForImport = Importing.MyDeserialize<List<WorkersModel>>();
                foreach (var import in ForImport)
                {
                    personalFCs.InsertQuery(import.Name, import.SecondName);
                }
                PersonalFCsGrid.ItemsSource = personalFCs.GetData();
            }
            catch
            {
                MessageBox.Show("что-то не то");
            }
        }
    }
}
