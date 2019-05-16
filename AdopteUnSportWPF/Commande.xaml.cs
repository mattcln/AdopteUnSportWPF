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
                ListeProd.Content += IDProduit +"-";
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
            string ListeProduit = (string)ListeProd.Content;
            string ProdIndispo = VérifierStockProduit(ListeProduit);
            if(ProdIndispo == "")
            {
                ProdDispo.Opacity = 1;
                EnregistrerCommande();
            }
            else
            {
                ProdNonDispo.Opacity = 1;
                ProdNonDispo.Content += ProdIndispo;
            }
        }

        private string VérifierStockProduit(string ListeIDProduits)
        {
            ListeIDProduits = ListeIDProduits.Substring(0, ListeIDProduits.Length - 1);
            string[] TabIDProduits = ListeIDProduits.Split('-');
            string ProdIndispo = "";
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = MATIbol78;";
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
                IDProdIndispo = IDProdIndispo.Substring(0, IDProdIndispo.Length - 1);
                return IDProdIndispo;
            }
            else return "";
            

            //if (Livraison != "")
            //{
            //    Livraison = Livraison.Substring(0, Livraison.Length - 1);
            //    if (Livraison.Length == 1)
            //    {
            //        Console.WriteLine(" L'article " + Livraison + " n'est plus disponible en magasin.");
            //        Console.WriteLine(" Est-ce que le client veut se le faire livrer à son domicile ?");
            //    }
            //    else
            //    {
            //        Console.WriteLine(" Les articles " + Livraison + " ne sont plus disponibles en magasin.");
            //        Console.WriteLine(" Est-ce que le client veut se les faire livrer à son domicile ?");
            //    }
            //    string RéponseLivraison = OuiNon();
            //    if (RéponseLivraison == "oui")
            //    {
            //        string[] tabProduitLivraison = Livraison.Split(',');
            //        int InterTab;
            //        Livraison = "";
            //        for (int a = 0; a < tabProduitLivraison.Length; a++)
            //        {
            //            InterTab = Convert.ToInt32(tabProduitLivraison[a]);
            //            InterTab--;
            //            Livraison += TabIDProduits[InterTab] + ",";
            //        }
            //        LivraisonProduits(Livraison, IDClient);
            //    }
            //    else
            //    {
            //        ProduitDispo = ProduitDispo.Substring(0, ProduitDispo.Length - 1);
            //        ListeIDProduits = "";
            //        string[] TabProduitDispo = ProduitDispo.Split(',');
            //        int TabInter;
            //        for (int k = 0; k < TabProduitDispo.Length; k++)
            //        {
            //            TabInter = Convert.ToInt32(TabProduitDispo[k]);
            //            ListeIDProduits += TabIDProduits[TabInter] + ",";
            //        }
            //        ListeIDProduits = ListeIDProduits.Substring(0, ListeIDProduits.Length - 1);
            //    }
            //}
            //Console.WriteLine(" La commande contient maintenant les articles : " + ListeIDProduits);
            //return Livraison;
        }

        private void EnregistrerCommande()                                                                                            // CA MARCHE
        {
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = MATIbol78;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();
            string IDClient = TextBlockIDC.Text;
            string IDProduit = (string)ListeProd.Content;
            IDProduit = IDProduit.Substring(0, IDProduit.Length - 1);
            string[] TabIDProd = IDProduit.Split('-');
            int NbArticles = TabIDProd.Length;
            string IDCommande = CréationIDCommande();


            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "insert into Commande values ('" + IDCommande + "','" + IDClient + "','" + NbArticles + "')";
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            maConnexion.Close();
        }
        private string CréationIDCommande()
        {
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = MATIbol78;";
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
