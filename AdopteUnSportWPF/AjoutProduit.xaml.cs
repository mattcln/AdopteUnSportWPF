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
    /// Logique d'interaction pour AjoutProduit.xaml
    /// </summary>
    public partial class AjoutProduit : Window
    {
        public AjoutProduit()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string IDProduit = TextBlockIDP.Text;
            int qte = 0;
            int.TryParse(TextBlockStock.Text, out qte);
            bool ExistenceProd = ExistenceProduit(IDProduit);
            if (ExistenceProd == true && qte != 0)
            {
                WrongID.Content = "";
                WrongS.Content = "";
                AjouterStock(IDProduit);
                Réussite.Content = "Le stock du produit a bien été mis à jour.";
            }
            else
            {
                if (ExistenceProd == false)
                {
                    WrongS.Content = "";
                    WrongID.Content = "Il n'existe aucun produit avec cet ID.";
                }
                if (qte == 0)
                {
                    WrongID.Content = "";
                    WrongS.Content = "Veuillez renseigner une quantité valide.";
                }
                if (ExistenceProd != true && qte == 0)
                {
                    WrongID.Content = "Il n'existe aucun produit avec cet ID.";
                    WrongS.Content = "Veuillez renseigner une quantité valide.";
                }


            }
        }
        private void AjouterStock(string IDProduit)                                                                                                                  // CA MARCHE
        {
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = Buzenval1998;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();

            MySqlCommand command = maConnexion.CreateCommand();
            int.TryParse(TextBlockStock.Text, out int qte);
            command.CommandText = "UPDATE Produit SET stock = stock + " + qte + " WHERE IDProduit = '" + IDProduit + "'";
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            maConnexion.Close();
        }

        private bool ExistenceProduit(string IDProduit)
        {
            bool Existence = false;
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = Buzenval1998;";
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Menu Mn = new Menu();
            Visibility = Visibility.Hidden;
            Mn.ShowDialog();
        }
    }
}