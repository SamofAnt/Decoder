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
using System.Windows.Shapes;

namespace DecoderVig
{
    /// <summary>
    /// Interaction logic for DecoderFirst.xaml
    /// </summary>
    public partial class DecoderFirst : Window
    {
        public DecoderFirst()
        {
            InitializeComponent();
            UserInitialize();
        }
        private void UserInitialize()
        {
            var encrypt_btn = new Button()
            {
                Name = "encryptCmd"
                ,
                MinWidth = 100
                ,
                MinHeight = 50
                ,
                Margin = new Thickness(5)
            };
            encrypt_btn.Click += new RoutedEventHandler(Encrypt_btn_Click);

            var decrypt_btn = new Button()
            {
                Name = "decryptCmd"
                ,
                MinWidth = 100
                ,
                MinHeight = 50
                ,
                Margin = new Thickness(5)
            };
            decrypt_btn.Click += new RoutedEventHandler(Decrypt_btn_Click);
            var clean_btn = new Button()
            {
                Name = "cleanCmd"
                 ,
                MinWidth = 50
                ,
                MinHeight = 50
                ,
                Margin = new Thickness(5)
            };

            clean_btn.Click += new RoutedEventHandler(OnClean);

        }
        private void OnEncrypt(object sender, RoutedEventArgs e) => this.encrypted.Text += Encrypt(this.exprTextBox.Text);

        private void OnClean(object sender, RoutedEventArgs e)
        {
            this.exprTextBox.Text = "";
            this.decrypted.Text = "";
            this.encrypted.Text = "";
        }
        private string Encrypt(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                int index = 0;
                for (int j = i; j < alphabet.Length; j++)
                {
                    index = alphabet.IndexOf(str[j]);
                    break;
                }
                if (index != -1)
                //    str = str.Replace(str[i], key[index]);
                {
                    str = str.Remove(i, 1);
                    str = str.Insert(i, key[index].ToString());
                }
            }
            return str;
        }
        private string Decrypt(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                int index = 0;
                for (int j = i; j < key.Length; j++)
                {
                    index = key.IndexOf(str[j]);
                    break;
                }
                if (index != -1)
                {
                    str = str.Remove(i, 1);
                    str = str.Insert(i, alphabet[index].ToString());
                }
            }
            return str;
        }
        private void Encrypt_btn_Click(object sender, RoutedEventArgs e)
        {
            if (this.exprTextBox.Text != Encrypt(this.exprTextBox.Text))
                this.encrypted.Text += Encrypt(this.exprTextBox.Text);
            else
                this.encrypted.Text = "";

        }
        private void Navigate_Click(object sender, RoutedEventArgs e) =>this.Close();
        
        private void Decrypt_btn_Click(object sender, RoutedEventArgs e)
        {
            if (this.encrypted.Text != "")
                this.decrypted.Text += Decrypt(this.encrypted.Text);
            else
                this.decrypted.Text = "";
        }
        private const string alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя., !? ";
        private const string key = "ХЦЧШЩЪЫЬЭЮЯМНОПРСТУФАБВГДЕЁЖЗИЙКЛопрстуфхцчш!? .,деёжзийклмнщъыьэюяабвгд";
    }
}
