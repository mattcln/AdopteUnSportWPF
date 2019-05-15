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
    /// Logique d'interaction pour Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btnCommande_Click(object sender, RoutedEventArgs e)
        {
            Commande Cmd = new Commande();
            Cmd.ShowDialog();
        }
        private void btnProduit_Click(object sender, RoutedEventArgs e)
        {
            InformationsProduit InfoProd = new InformationsProduit();
            InfoProd.ShowDialog();
        }
        private void btnLivraison_Click(object sender, RoutedEventArgs e)
        {
            Livraison Lvs = new Livraison();
            Lvs.ShowDialog();
        }
        private void btnClient_Click(object sender, RoutedEventArgs e)
        {
            Client Clt = new Client();
            Clt.ShowDialog();
        }

        private void BtnInfoClient_Click(object sender, RoutedEventArgs e)
        {
 
        }

        private void BtnAjoutProduit_Click(object sender, RoutedEventArgs e)
        {
            AjoutProduit APdt = new AjoutProduit();
            APdt.ShowDialog();
        }
    }
}
