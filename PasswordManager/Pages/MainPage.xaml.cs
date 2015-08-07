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
using PasswordManager.Engine;

namespace PasswordManager
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            Links.txtText = txtText;
            txtText.Text = Loader.text_decrypt;
            txtText.TextChanged += txtText_TextChanged;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.QuestSave)
            {
                MessageBoxResult result = MessageBox.Show("Сохранить файл - вы уверены?", "Сохранение файла", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Loader.Save();
                        break;
                    case MessageBoxResult.No:
                        // User pressed No button
                        // ...
                        break;
                }
            }
            else Loader.Save();
        }

        private void txtText_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (txtText.Text != "TextBox")
                Loader.text_decrypt = txtText.Text;
        }
    }
}
