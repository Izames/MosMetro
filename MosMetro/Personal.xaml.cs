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
    /// Логика взаимодействия для Personal.xaml
    /// </summary>
    public partial class Personal : Page
    {
        PersonalTableAdapter adapter = new PersonalTableAdapter();
        PersonalFCsTableAdapter PersonalFCs = new PersonalFCsTableAdapter();
        MetroTableAdapter MetroTableAdapter = new MetroTableAdapter();
        public Personal()
        {
            InitializeComponent();
            PersonalsGrid.ItemsSource = adapter.GetData();
            AtPersonal.ItemsSource = PersonalFCs.GetData();
            AtPersonal.DisplayMemberPath = "SecondName";
            AtPersonal.SelectedValuePath = "id";
            AtMetro.ItemsSource = MetroTableAdapter.GetData();
            AtMetro.DisplayMemberPath = "MetroName";
            AtMetro.SelectedValuePath = "id";
            RedactTable.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void AddPersonal_Click(object sender, RoutedEventArgs e)
        {
            if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(255, 255, 255))
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(Post.Text) || String.IsNullOrWhiteSpace(parol.Text) || Convert.ToInt32(AtPersonal.SelectedValue) == -1 || Convert.ToInt32(salary.Text) < 0 || Convert.ToInt32(AtMetro.SelectedValue) == -1)
                    {
                        throw new Exception();
                    }
                    adapter.InsertQuery(Convert.ToInt32(AtPersonal.SelectedValue), Post.Text, Convert.ToInt32(salary.Text), Convert.ToInt32(AtMetro.SelectedValue), parol.Text);
                    PersonalsGrid.ItemsSource = adapter.GetData();
                    PersonalsGrid.Columns[1].Visibility = Visibility.Collapsed;
                    PersonalsGrid.Columns[2].Visibility = Visibility.Collapsed;
                    salary.Text = ""; Post.Text = ""; AtMetro.SelectedValue = -1; AtPersonal.SelectedValue = -1; parol.Text = "";
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
                    if (String.IsNullOrWhiteSpace(Post.Text) || String.IsNullOrWhiteSpace(parol.Text) || Convert.ToInt32(AtPersonal.SelectedValue) == -1 || Convert.ToInt32(salary.Text) < 0 || Convert.ToInt32(AtMetro.SelectedValue) == -1)
                    {
                        throw new Exception();
                    }
                    int id = Convert.ToInt32((PersonalsGrid.SelectedItem as DataRowView).Row[0]);
                    adapter.UpdateQuery(Convert.ToInt32(AtPersonal.SelectedValue), Post.Text, Convert.ToInt32(salary.Text), Convert.ToInt32(AtMetro.SelectedValue), parol.Text, id);
                    PersonalsGrid.ItemsSource = adapter.GetData();
                    PersonalsGrid.Columns[1].Visibility = Visibility.Collapsed;
                    PersonalsGrid.Columns[2].Visibility = Visibility.Collapsed;
                    salary.Text = ""; Post.Text = ""; AtMetro.SelectedValue = -1; AtPersonal.SelectedValue = -1; parol.Text = "";
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
                object id = (PersonalsGrid.SelectedItem as DataRowView).Row[0];
                adapter.DeleteQuery(Convert.ToInt32(id));
                PersonalsGrid.ItemsSource = adapter.GetData();
                PersonalsGrid.Columns[1].Visibility = Visibility.Collapsed;
                PersonalsGrid.Columns[2].Visibility = Visibility.Collapsed;
                salary.Text = ""; Post.Text = ""; AtMetro.SelectedValue = -1; AtPersonal.SelectedValue = -1; parol.Text = "";
            }
            catch
            {
                MessageBox.Show("Errore");
            }
        }

        private void RedactTable_Click(object sender, RoutedEventArgs e)
        {
            if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(255, 255, 255))
            {
                RedactTable.Background = new SolidColorBrush(Color.FromRgb(191, 0, 255));
                AddPersonal.Content = "Сохранить изменения";
                salary.Text = ""; Post.Text = ""; AtMetro.SelectedValue = -1; AtPersonal.SelectedValue = -1; parol.Text = "";
            }
            else
            {
                RedactTable.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                AddPersonal.Content = "Добавить";
                salary.Text = ""; Post.Text = ""; AtMetro.SelectedValue = -1; AtPersonal.SelectedValue = -1; parol.Text = "";
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PersonalsGrid.Columns[1].Visibility = Visibility.Collapsed;
            PersonalsGrid.Columns[2].Visibility = Visibility.Collapsed;
        }

        private void PersonalsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((SolidColorBrush)RedactTable.Background).Color == Color.FromRgb(191, 0, 255))
            {
                try
                {
                    var choosed = PersonalsGrid.SelectedItem as DataRowView;
                    AtPersonal.SelectedValue = Convert.ToInt32(choosed.Row[1]);
                    AtMetro.SelectedValue = Convert.ToInt32(choosed.Row[2]);
                    Post.Text = Convert.ToString(choosed.Row[3]);
                    salary.Text = Convert.ToString(choosed.Row[4]);
                }
                catch
                {
                    salary.Text = ""; Post.Text = ""; AtMetro.SelectedValue = -1; AtPersonal.SelectedValue = -1; parol.Text = "";
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).frame.Content =new Page1();
        }
    }
}
