using System;
using System.Collections.Generic;
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
using MosMetro.DataSet1TableAdapters;

namespace MosMetro
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        buyersTableAdapter adapter = new buyersTableAdapter();
        PersonalTableAdapter PersonalTableAdapter = new PersonalTableAdapter();
        PersonalFCsTableAdapter PersonalFCsTableAdapter = new PersonalFCsTableAdapter();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Autorizate_Click(object sender, RoutedEventArgs e)
        {
            var AdminFCs = PersonalFCsTableAdapter.GetData().Rows;
            var accountsAdmin = PersonalTableAdapter.GetData().Rows;
            for (int i = 0; i < AdminFCs.Count; i++)
            {
                if (AdminFCs[i][1].ToString() == Name.Text && SecondName.Text == AdminFCs[i][2].ToString())
                {
                    for (int j = 0; j < accountsAdmin.Count; j++)
                    {
                        if (accountsAdmin[j][5].ToString() == AdminFCs[i][2].ToString())
                        {
                            if(Password.Password == accountsAdmin[j][7].ToString())
                            {
                                frame.Content = new Page1();
                                Width = 800;
                                Height = 450;
                                Name.Text = "";
                                SecondName.Text = "";
                                Password.Password = "";
                            }
                        }
                    }
                }
            }
            var buyers = adapter.GetData().Rows;
            for (int i = 0; i < buyers.Count; i++)
            {
                if (Name.Text == buyers[i][1].ToString() && SecondName.Text == buyers[i][2].ToString() && Password.Password == buyers[i][3].ToString())
                {
                    frame.Content = new UserPage();
                    Width = 800;
                    Height = 450;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Name.Text = "";
            SecondName.Text = "";
            Password.Password = "";
        }
    }
}
