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

namespace DecoderVig
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UserInitialize();

        }
        private void UserInitialize()
        {
            var CesAlgo =
                new Button()
                {
                    Name = "firstAlgo",
                    Content = "Шифр Виженера",
                    MinWidth = 50,
                    MinHeight = 60,
                    FontSize = 18,
                    Margin = new Thickness(5)
                };
            CesAlgo.Click += new RoutedEventHandler(OnFirstAlgo);

            this.NmbWrapper.Children.Add(CesAlgo);

            var VigAlgo =
                new Button()
                {
                    Name = "secondAlgo",
                    Content = "Шифр Цезаря",
                    Width = 175,
                    Height = 60,
                    FontSize = 18,
                    Margin = new Thickness(5),
                };
            VigAlgo.Click += new RoutedEventHandler(OnSecondAlgo);

            this.NmbWrapper.Children.Add(VigAlgo);
        }
        private void OnFirstAlgo(object sender, RoutedEventArgs e)
        {
            Catalog window = new Catalog();
            window.ShowDialog();
        }
        private void OnSecondAlgo(object sender, RoutedEventArgs e)
        {
            DecoderFirst window = new DecoderFirst();
            window.ShowDialog();
        }
        private void Navigate_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}

