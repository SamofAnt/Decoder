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
    /// Interaction logic for Catalog.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        public Catalog()
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
            this.IsRand.IsChecked = false;
            this.KeyTextBox.IsReadOnly = false;
            this.exprTextBox.Text = "";
            this.KeyTextBox.Text = "";
            this.decrypted.Text = "";
            this.encrypted.Text = "";
        }
        private static string GetKey(string key, string line)
        {
            int index = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (index == key.Length)
                    index = 0;
                line = line.Remove(i, 1);
                line = line.Insert(i, key[index++].ToString());
            }
            return line;
        }
        private string Encrypt(string str)
        {
            if (!(bool)this.IsRand.IsChecked)
            {
                this.KeyTextBox.IsReadOnly = false;
                if (key.Length > 0)
                    key.Remove(1);
                key = GetKey(this.KeyTextBox.Text, this.exprTextBox.Text);
                this.KeyTextBox.Text = key;
            }
            for (int i = 0; i < str.Length; i++)
            {
                int index_line = 0;
                int index_key = 0;
                int index_crypt = 0;
                for (int j = i; j < str.Length; j++)
                {
                    index_line = alphabet.IndexOf(str[j]);
                    index_key = alphabet.IndexOf(key[j]);
                    break;
                }
                if ((index_line != -1) && (index_key != -1))
                {
                    index_crypt = (index_line + index_key) % SIZE;
                    str = str.Remove(i, 1);
                    str = str.Insert(i, alphabet[index_crypt].ToString());
                }
            }
            return str;
        }
        private string Decrypt(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                int index_line = 0;
                int index_key = 0;
                int index_crypt = 0;
                for (int j = i; j < str.Length; j++)
                {
                    index_line = alphabet.IndexOf(str[j]);
                    index_key = alphabet.IndexOf(key[j]);
                    break;
                }
                if ((index_line != -1) && (index_key != -1))
                {
                    if ((index_line - index_key) >= 0)
                        index_crypt = (index_line - index_key) % SIZE;
                    else
                        index_crypt = (index_line - index_key + SIZE) % SIZE;
                    str = str.Remove(i, 1);
                    str = str.Insert(i, alphabet[index_crypt].ToString());
                }
            }
            return str;
        }
        string random_key(string line)
        {
            int index = 0;
            string rand_key = "";
            Random rnd = new Random();
            for (int i = 0; i < line.Length; i++)
            {
                index = rnd.Next(0, SIZE);
                rand_key = rand_key.Insert(i, alphabet[index].ToString());
            }
            return rand_key;
        }

        private void IsRand_Click(object sender, RoutedEventArgs e)
        {
            if (this.exprTextBox.Text != "")
            {
                if ((bool)IsRand.IsChecked)
                {
                    this.KeyTextBox.IsReadOnly = true;
                    this.KeyTextBox.Text = "";
                    if (key.Length > 0)
                        key.Remove(1);
                    key = random_key(this.exprTextBox.Text);
                    this.KeyTextBox.Text = key;
                }
                else
                {
                    this.KeyTextBox.Text = "";
                    this.KeyTextBox.IsReadOnly = false;
                }
            }
        }
        private void Encrypt_btn_Click(object sender, RoutedEventArgs e)
        {
            string encrypted = Encrypt(this.exprTextBox.Text);
            if (this.exprTextBox.Text != encrypted)
                this.encrypted.Text += encrypted;
            else
                this.encrypted.Text = "";

        }
        private void Navigate_Click(object sender, RoutedEventArgs e) => this.Close();
       
        private void Decrypt_btn_Click(object sender, RoutedEventArgs e)
        {
            if (this.encrypted.Text != "")
                this.decrypted.Text += Decrypt(this.encrypted.Text);
            else
                this.decrypted.Text = "";
        }

        private const string alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя .,!?";
        private string key = "";
        private int SIZE = alphabet.Length;
    }
}
