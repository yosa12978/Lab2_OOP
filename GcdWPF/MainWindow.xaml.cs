using GcdLib;
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

namespace GcdWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            string[] strlist = inpText.Text.Replace(" ", "").Split(",");
            int[] inp = new int[strlist.Length];
            for(int i = 0; i < strlist.Length; i++)
                if (!int.TryParse(strlist[i], out inp[i]))
                    return;

            DateTime dateTime = DateTime.Now;
            EuclidLabel.Content = $"Euclid: {Euclid.GCD(inp)}";
            EuclidTimeLabel.Content = $"Time Spent: {DateTime.Now.Subtract(dateTime)}";

            dateTime = DateTime.Now;
            ShteinLabel.Content = $"Shtein: {Shtein.GCD(inp)}";
            ShteinTimeLabel.Content = $"Time Spent: {DateTime.Now.Subtract(dateTime)}";
        }
    }
}
