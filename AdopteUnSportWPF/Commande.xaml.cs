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
            OuiClient.IsEnabled = false;
            NonClient.IsEnabled = false;
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
            OuiClientID.IsEnabled = false;
            NonClientID.IsEnabled = false;
        }

        private void NonClientID_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IDClientValider_Click(object sender, RoutedEventArgs e)
        {
            string IDClient = TextBlockIDC.Text;
            bool ExistenceCli = ExistenceClient(IDClient);
            if (ExistenceCli == true)
            {
                WrongC.Opacity = 0;
                TrueC.Opacity = 1;
                TextAjoutProduit.Opacity = 1;
                TextBlockIDP.Opacity = 1;
                TextBlockIDP.IsEnabled = true;
                AjouterP.Opacity = 1;
                AjouterP.IsEnabled = true;
                IDClientValider.IsEnabled = false;
                TextBlockIDC.IsEnabled = false;


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
                TextPanier.Opacity = 1;
                CloturerPan.Opacity = 1;
                CloturerPan.IsEnabled = true;
                WrongP.Opacity = 0;
                TrueP.Opacity = 1;
                if (ListeProd.Content != "")
                {
                    TextPanier.Content = "Voici les produits enregistrés :";
                }
                ListeProd.Content += IDProduit + "-";
                //InformationProduit(IDProduit);
            }
            else
            {
                WrongP.Opacity = 1;
                TrueP.Opacity = 0;
            }
        }

        private void btnCloturerP_Click(object sender, RoutedEventArgs e)
        {
            TextBlockIDP.IsEnabled = false;
            AjouterP.IsEnabled = false;
            CloturerPan.IsEnabled = false;

            string ListeProduit = (string)ListeProd.Content;
            string ProdIndispo = VérifierStockProduit(ListeProduit);
            if (ProdIndispo == "")
            {
                ProdDispo.Opacity = 1;
                string IDProduit = (string)ListeProd.Content;
                IDProduit = IDProduit.Substring(0, IDProduit.Length - 1);
                string[] TabIDProd = IDProduit.Split('-');
                int NbArticles = TabIDProd.Length;
                EnregistrerCommande(NbArticles);
                for (int i = 0; i < TabIDProd.Length; i++)
                {
                    SoustraireArticle(TabIDProd[i]);
                }
            }
            else
            {
                TextLivraison.Opacity = 1;
                ProdNonDispo.Opacity = 1;
                OuiLivraison.Opacity = 1;
                NonLivraison.Opacity = 1;
                ProdNonDispoT.Opacity = 1;
                ProdNonDispo.Opacity = 1;
                OuiLivraison.IsEnabled = true;
                NonLivraison.IsEnabled = true;
                ProdNonDispo.Content += ProdIndispo;

            }
        }
        private void OuiLivraison_Click(object sender, RoutedEventArgs e)
        {
            OuiLivraison.IsEnabled = false;
            NonLivraison.IsEnabled = false;
            LivraisonProduits();
        }
        private void NonLivraison_Click(object sender, RoutedEventArgs e)
        {
            OuiLivraison.IsEnabled = false;
            NonLivraison.IsEnabled = false;
            string IDProduit = TextBlockIDP.Text;
            IDProduit = IDProduit.Substring(0, IDProduit.Length - 1);
            string IDProduitIndispo = (string)ProdNonDispo.Content;
            string[] TabIDProduit = IDProduit.Split(',');
            string[] TabIDProduitIndispo = IDProduitIndispo.Split(',');
            IDProduit = "";
            for (int i = 0; i < TabIDProduit.Length; i++)
            {
                for (int j = 0; j < TabIDProduitIndispo.Length; j++)
                {
                    if (TabIDProduit[i] == TabIDProduitIndispo[j])
                    {
                        TabIDProduit[i] = "";
                    }
                }
            }
            for (int k = 0; k < TabIDProduit.Length; k++)
            {
                IDProduit += TabIDProduit[k] + "-";
            }
            IDProduit = IDProduit.Substring(0, IDProduit.Length - 1);
            string[] TabIDProd = IDProduit.Split('-');
            int NbArticles = TabIDProd.Length;
            EnregistrerCommande(NbArticles);
            for (int i = 0; i < TabIDProd.Length; i++)
            {
                SoustraireArticle(TabIDProd[i]);
            }
            ProdDispoS.Content += IDProduitIndispo;
            ProdDispoS.Opacity = 1;
        }

        private void LivraisonProduits()
        {
            string Produits = (string)ProdNonDispo.Content;
            string IDClient = TextBlockIDC.Text;
            string AdClient = RetrouverAdresse();
            AdresseClient.Content = AdClient;
            AdresseClient.Opacity = 1;
            string[] tabProduits = Produits.Split(',');
            string IDFournisseur; string ListeFournisseur = "";
            for (int i = 0; i < tabProduits.Length; i++)
            {
                IDFournisseur = RetrouverFournisseur(tabProduits[i]);
                ListeFournisseur += IDFournisseur + ",";
                EnregisterLivraison(tabProduits[i], IDFournisseur);
            }
            if (ListeFournisseur.Length == 6)
            {
                ListeFournisseur = ListeFournisseur.Substring(0, ListeFournisseur.Length - 1);
                Fournisseurs.Content = " Le fournisseur " + ListeFournisseur + " a été contacté.";
            }
            else
            {
                ListeFournisseur = ListeFournisseur.Substring(0, ListeFournisseur.Length - 1);
                Fournisseurs.Content = " Les fournisseurs " + ListeFournisseur + " ont été contactés.";
            }
            Fournisseurs.Opacity = 1;
            string IDProduit = (string)ListeProd.Content;
            IDProduit = IDProduit.Substring(0, IDProduit.Length - 1);
            string[] TabIDProd = IDProduit.Split('-');
            int NbArticles = TabIDProd.Length;
            for (int i = 0; i < TabIDProd.Length; i++)
            {
                SoustraireArticle(TabIDProd[i]);
            }

            EnregistrerCommande(NbArticles);
        }
        private void EnregisterLivraison(string IDProduit, string IDFournisseur)
        {
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = Buzenal1998;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();
            string IDClient = TextBlockIDC.Text;
            int numLivraison = CréationNumLivraison();
            string InformationClient = RécupérationInformationClient(IDClient);
            string[] TabInformationClient = InformationClient.Split(',');
            string nom = TabInformationClient[0];
            string Prénom = TabInformationClient[1];
            string Adresse = TabInformationClient[2];
            string Ville = TabInformationClient[3];

            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "insert into Livraison values ('" + numLivraison + "','" + IDClient + "','" + nom + "','" + Prénom + "','" + Adresse + "','" + Ville + "','" + IDProduit + "','" + IDFournisseur + "')";
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            maConnexion.Close();
        }
        private string RécupérationInformationClient(string IDClient)
        {
            string InformationClient = "";

            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = Buzenal1998;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();

            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "SELECT nom, prenom, adresse, ville from Clients where IDClients = '" + IDClient + "'"; // exemple de requête

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string ligne = reader.GetValue(i).ToString();
                    InformationClient += ligne + ",";
                }
            }
            maConnexion.Close();
            InformationClient = InformationClient.Substring(0, InformationClient.Length - 1);
            return InformationClient;
        }
        private int CréationNumLivraison()
        {
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = Buzenal1998;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "select count(numLivraison)+1 from Livraison";
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string SnumLivraison = "";
            int numLivraison = 0;
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    SnumLivraison = reader.GetValue(i).ToString();
                }
            }
            numLivraison = Convert.ToInt32(SnumLivraison);

            maConnexion.Close();
            return numLivraison;
        }
        private string RetrouverFournisseur(string IDProduit)
        {
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = Buzenal1998;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();

            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "select IDFournisseur from Produit where IDProduit = '" + IDProduit + "'"; // exemple de requête

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string IDFournisseur = "";
            while (reader.Read())
            {
                IDFournisseur = reader.GetValue(0).ToString();
            }

            maConnexion.Close();
            return IDFournisseur;
        }
        private string RetrouverAdresse()
        {
            string IDClient = TextBlockIDC.Text;
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = Buzenal1998;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();

            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "select adresse, ville from Clients where IDClients = '" + IDClient + "'"; // exemple de requête

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string AdresseClient = "";
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string ligne = reader.GetValue(i).ToString();
                    AdresseClient += ligne + ",";
                }
            }
            maConnexion.Close();
            string[] TabAdresseClient = AdresseClient.Split(',');
            AdresseClient = " L'adresse du client est " + TabAdresseClient[0] + ", " + TabAdresseClient[1] + ".";
            return AdresseClient;
        }
        private string VérifierStockProduit(string ListeIDProduits)
        {
            ListeIDProduits = ListeIDProduits.Substring(0, ListeIDProduits.Length - 1);
            string[] TabIDProduits = ListeIDProduits.Split('-');
            string ProdIndispo = "";
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = Buzenal1998;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            int stock = 0;
            for (int i = 0; i < TabIDProduits.Length; i++)
            {
                maConnexion.Open();
                MySqlCommand command = maConnexion.CreateCommand();
                MySqlDataReader reader;
                command.CommandText = "select IDProduit, objet , stock from Produit where IDProduit = '" + TabIDProduits[i] + "'";

                reader = command.ExecuteReader();
                string InfoProduit = "";
                while (reader.Read())       // parcours ligne par ligne
                {
                    InfoProduit = "";
                    for (int j = 0; j < reader.FieldCount; j++)  //parcours cellule par cellule
                    {
                        string valeurattribut = reader.GetValue(j).ToString();
                        InfoProduit += valeurattribut + ",";
                    }
                }
                string[] TabInfoProduit = InfoProduit.Split(',');
                stock = Convert.ToInt32(TabInfoProduit[2]);
                if (stock == 0)
                {
                    ProdIndispo += i + ",";
                }
                maConnexion.Close();
            }
            if (ProdIndispo != "")
            {
                ProdIndispo = ProdIndispo.Substring(0, ProdIndispo.Length - 1);

                string[] TabIDProdIndispo = ProdIndispo.Split(',');
                int InterTab;
                string IDProdIndispo = "";
                for (int k = 0; k < TabIDProdIndispo.Length; k++)
                {
                    InterTab = Convert.ToInt32(TabIDProdIndispo[k]);
                    IDProdIndispo += TabIDProduits[InterTab] + ",";
                }
                TextBlockQ.Text += IDProdIndispo;
                IDProdIndispo = IDProdIndispo.Substring(0, IDProdIndispo.Length - 1);
                return IDProdIndispo;
            }
            else return "";
        }
        private void EnregistrerCommande(int NbArticles)
        {
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = Buzenal1998;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();
            string IDClient = TextBlockIDC.Text;


            string IDCommande = CréationIDCommande();


            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "insert into Commande values ('" + IDCommande + "','" + IDClient + "','" + NbArticles + "')";
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            maConnexion.Close();
        }
        private string CréationIDCommande()
        {
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = Buzenal1998;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "select count(IDCommande)+1 from Commande";
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string IDCommande = "";
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    IDCommande = reader.GetValue(i).ToString();
                }
            }
            while (IDCommande.Length < 4)
            {
                IDCommande = "0" + IDCommande;
            }
            IDCommande = "C" + IDCommande;
            maConnexion.Close();
            return IDCommande;
        }
        private bool ExistenceClient(string IDClient)
        {
            bool Existence = false;
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = Buzenal1998;";
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
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = Buzenal1998;";
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
        private void SoustraireArticle(string IDProduit)
        {
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = Buzenal1998;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();

            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "UPDATE Produit SET stock = stock - 1 WHERE IDProduit = '" + IDProduit + "'";
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            maConnexion.Close();
        }

        private void OuiClient_Click_1(object sender, RoutedEventArgs e)
        {
            Menu Mn = new Menu();
            Visibility = Visibility.Hidden;
            Mn.ShowDialog();
        }
    }
}
