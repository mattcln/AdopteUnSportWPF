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

namespace AdopteUnSportWPF
{
    /// <summary>
    /// Logique d'interaction pour Commande.xaml
    /// </summary>
    public partial class Commande : Window
    {
        public Commande()
        {
            InitializeComponent();
        }

        private void OuiClient_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void NonClient_Click(object sender, RoutedEventArgs e)
        {
            Client Clt = new Client();
            Clt.ShowDialog();
            OuiClient_Click(sender, e);
        }
        private void OuiClientID_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NonClientID_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
