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
    /// <summary>
    /// Logique d'interaction pour InformationsProduit.xaml
    /// </summary>
    public partial class InformationsProduit : Window
    {
        public InformationsProduit()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string IDProduit = TextBlockIDP.Text;
            bool ExistenceProd = ExistenceProduit(IDProduit);
            if (ExistenceProd == true)
            {
                InformationProduit(IDProduit);
            }
            else
            {
                Wrong.Content = "Il n'existe aucun produit avec cet ID.";
            }

        }
        private void InformationProduit(string IDProduit)
        {
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = MATIbol78;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();
            string IDFournisseur = ""; int prix = 0; int stock = 0; string objet = "";
            MySqlCommand command = maConnexion.CreateCommand();
            MySqlDataReader reader;

            command.CommandText = "select IDFournisseur, prix , stock, objet from Produit where IDProduit = '" + IDProduit + "'";

            reader = command.ExecuteReader();
            string InfoProduit = "";
            while (reader.Read())       // parcours ligne par ligne
            {
                InfoProduit = "";
                for (int i = 0; i < reader.FieldCount; i++)  //parcours cellule par cellule
                {
                    string valeurattribut = reader.GetValue(i).ToString();
                    InfoProduit += valeurattribut + ",";
                }
            }
            string[] TabInfoProduit = InfoProduit.Split(',');
            IDFournisseur = TabInfoProduit[0];
            prix = Convert.ToInt32(TabInfoProduit[1]);
            stock = Convert.ToInt32(TabInfoProduit[2]);
            objet = TabInfoProduit[3];
            AffichageInfo(IDFournisseur, prix, stock, objet);
            maConnexion.Close();            
        }
        private void AffichageInfo(string idFournisseur, int prix, int stock, string objet)
        {
            IDFournisseur.Content = "IDFournisseur : " + idFournisseur;
            Prix.Content = "Prix : " + prix;
            Stock.Content = "Stock : " + stock;
            Description.Content = "Description : " + objet;
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
