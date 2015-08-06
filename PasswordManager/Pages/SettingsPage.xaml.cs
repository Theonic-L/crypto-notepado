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
using PasswordManager.Cryptography;

namespace PasswordManager.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            txtPath.Text = Properties.Settings.Default.AutoOpenPath;
            if (Properties.Settings.Default.AutoOpenPassOn)
            {
                checkPass.IsChecked = true;
                txtPath.IsEnabled = true;
                btnOpen.IsEnabled = true;
            }
            if (Properties.Settings.Default.AutoOpenPassOn)
            {
                txtPass.Text = Properties.Settings.Default.AutoOpenPass;
                checkPass.IsEnabled = true;
                if (Convert.ToBoolean(checkPass.IsChecked))
                    txtPass.IsEnabled = true;
            }

            if (Properties.Settings.Default.AutoOpen)
                checkAutoOpenOn.IsChecked = true;
            checkPass.Checked +=checkPass_Checked;
            checkAutoOpenOn.Checked +=checkAutoOpenOn_Checked;
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
            Properties.Settings.Default.AutoOpenPassOn = true;
            Properties.Settings.Default.Save();
        }

        private void checkPass_Unchecked(object sender, RoutedEventArgs e)
        {
            txtPass.IsEnabled = false;
            Properties.Settings.Default.AutoOpenPassOn = false;
            Properties.Settings.Default.Save();
        }

        private void checkAutoOpenOn_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.AutoOpen = true;
            Properties.Settings.Default.Save();
            txtPath.IsEnabled = true;
            btnOpen.IsEnabled = true;
            checkPass.IsEnabled = true;
            if (Convert.ToBoolean(checkPass.IsChecked))
                txtPass.IsEnabled = true;
        }

        private void checkAutoOpenOn_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.AutoOpen = false;
            Properties.Settings.Default.Save();
            tabAutoUnchecked();
        }
        private void tabAutoUnchecked()
        {
            txtPath.IsEnabled = false;
            btnOpen.IsEnabled = false;
            checkPass.IsEnabled = false;
            txtPass.IsEnabled = false;
        }

        private void txtPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default.AutoOpenPath = txtPath.Text;
            Properties.Settings.Default.Save();
        }

        private void txtPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.AutoOpenPass = txtPass.Text;
            Properties.Settings.Default.Save();
            //MessageBox.Show(Properties.Settings.Default.AutoOpenPass);
        }

        private void Grid_Unloaded(object sender, RoutedEventArgs e)
        {
            //Properties.Settings.Default.Save();
        }
    }
}
