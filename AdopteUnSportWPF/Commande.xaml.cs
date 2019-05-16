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
using MySql.Data.MySqlClient;

namespace AdopteUnSportWPF
{

    public partial class Commande : Window
    {
        public Commande()
        {
            InitializeComponent();
        }

        private void OuiClient_Click(object sender, RoutedEventArgs e)
        {
                OuiClientID.IsEnabled = true;
                OuiClientID.Opacity = 1;
                NonClientID.IsEnabled = true;
                NonClientID.Opacity = 1;
                TextBlockQ.Opacity = 1;            
        }

        private void NonClient_Click(object sender, RoutedEventArgs e)
        {
            Client Clt = new Client();
            Clt.ShowDialog();
            OuiClient_Click(sender, e);
        }
        private void OuiClientID_Click(object sender, RoutedEventArgs e)
        {
            IDClientValider.IsEnabled = true;
            IDClientValider.Opacity = 1;
            TextBlockIDC.IsEnabled = true;
            TextBlockIDC.Opacity = 1;
            TextIDClient.Opacity = 1;
            TextBlockIDC.Opacity = 1;
        }

        private void NonClientID_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void IDClientValider_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string IDClient = TextBlockIDC.Text;
            bool ExistenceCli = ExistenceClient(IDClient);
            if (ExistenceCli == true)
            {
                WrongC.Opacity = 0;
                TrueC.Opacity = 1;

                //InformationProduit(IDProduit);
            }
            else
            {
                WrongC.Opacity = 1;
                TrueC.Opacity = 0;
            }
        }
        private void btnAjouterP_Click(object sender, RoutedEventArgs e)
        {
            string IDProduit = TextBlockIDP.Text;
            bool ExistenceProd = ExistenceProduit(IDProduit);
            if (ExistenceProd == true)
            {
                WrongP.Opacity = 0;
                TrueP.Opacity = 1;
                if(ListeProd.Content != "")
                {
                    TextPanier.Content = "Voici les produits enregistrés :";
                }
                ListeProd.Content += IDProduit +" - ";
                //InformationProduit(IDProduit);
            }
            else
            {
                WrongP.Opacity = 1;
                TrueP.Opacity = 0;
            }
        }
        private bool ExistenceClient(string IDClient)
        {
            bool Existence = false;
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = MATIbol78;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();

            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "SELECT IDClients from Clients"; // exemple de requête

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string ligne = reader.GetValue(i).ToString();
                    if (ligne == IDClient)
                    {
                        Existence = true;
                    }
                }
            }
            maConnexion.Close();
            return Existence;
        }
        private bool ExistenceProduit(string IDProduit)
        {
            bool Existence = false;
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = MATIbol78;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();

            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "SELECT IDProduit from Produit"; // exemple de requête

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string ligne = reader.GetValue(i).ToString();
                    if (ligne == IDProduit)
                    {
                        Existence = true;
                        Console.WriteLine(" Le produit a été trouvé.");
                    }
                }
            }
            maConnexion.Close();
            return Existence;
        }
    }
}
