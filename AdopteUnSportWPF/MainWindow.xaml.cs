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

namespace AdopteUnSportWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
         
        }


        private void Btnconnexion_Click(object sender, RoutedEventArgs e)
        {
            string Pseudo = TextBlockID.Text; string MDP = TextBlockMDP.Text;
            if (Pseudo == "gpadormi" && MDP == "aled")
            {
                Menu Mn = new Menu();
                Mn.ShowDialog();
            }
            else Wrong.Content = "Mauvaise combinaison pseudonyme / mot de passe";

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
