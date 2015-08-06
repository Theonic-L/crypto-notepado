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
using System.Windows.Media;//
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PasswordManager.Cryptography;
using PasswordManager.Engine;
using System.IO;
using Microsoft.Win32;
using PasswordManager.Pages;

namespace PasswordManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Links.MainFrame = frmMain;
            Properties.Settings.Default.Upgrade();
            if (Properties.Settings.Default.AutoOpen)
            {
                Loader.pass_on = Properties.Settings.Default.AutoOpenPassOn;
                Loader.Load(Properties.Settings.Default.AutoOpenPath, Properties.Settings.Default.AutoOpenPass);
                Links.MainFrame.Navigate(new Uri(@"Pages\MainPage.xaml", UriKind.Relative));
            }
            //string pass = "PASS";
            /*
            if (Properties.Settings.Default.PasswordActive)
            {
                pass = Generator.PasswordCompile("PASS");
            }
            else pass = Generator.PasswordCompile("PASS");
            //CryptAES.EncryptFile(Directory.GetCurrentDirectory() + "/test.txt", Directory.GetCurrentDirectory() + "/testEncr.txt", pass);
            //CryptAES.DecryptFile(Directory.GetCurrentDirectory() + "/testEncr.txt", Directory.GetCurrentDirectory() + "/testDecr.txt", pass);
            
            pass = Generator.PasswordCompile("PASS");
            Loader.text_decrypt = "Всем привет, вы смотрите какой-то channel";
            Loader.Save(Directory.GetCurrentDirectory() + "/test-string.txt", pass);
            Loader.Load(Directory.GetCurrentDirectory() + "/test-string.txt", pass);
            MessageBox.Show(pass);
            MessageBox.Show(Loader.text_decrypt);
             * */
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show("HI CLOSING");
            //Loader.Save();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Upgrade();
            SettingsPage page = new SettingsPage();
            frmMain.Navigate(page);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenPage page = new OpenPage();
            frmMain.Navigate(page);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainPage page = new MainPage();
            frmMain.Navigate(page);
        }


    }
}
