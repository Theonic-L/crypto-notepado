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
using Microsoft.Win32;
using PasswordManager.Engine;

namespace PasswordManager.Pages
{
    /// <summary>
    /// Interaction logic for OpenPage.xaml
    /// </summary>
    public partial class OpenPage : Page
    {
        public OpenPage()
        {
            InitializeComponent();
            txtPath.Text = Properties.Settings.Default.LastFile;
            checkPass.Checked+=checkPass_Checked;
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myDialog = new OpenFileDialog();
            myDialog.CheckFileExists = true;
            myDialog.Multiselect = false;
            if (myDialog.ShowDialog() == true)
            {
                txtPath.Text = myDialog.FileName;
            }
        }

        private void checkPass_Checked(object sender, RoutedEventArgs e)
        {
            txtPass.IsEnabled = true;
        }

        private void checkPass_Unchecked(object sender, RoutedEventArgs e)
        {
            txtPass.IsEnabled = false;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Loader.pass_on = Convert.ToBoolean(checkPass.IsChecked);
            Loader.Load(txtPath.Text, txtPass.Password);
            Links.MainFrame.Navigate(new Uri(@"Pages\MainPage.xaml", UriKind.Relative));
        }

        private void btnPathSave_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.LastFile = txtPath.Text;
            Properties.Settings.Default.Save();
        }

        private void btnPathDel_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.LastFile = "";
            Properties.Settings.Default.Save();
        }
    }
}
