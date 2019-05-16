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

    public partial class InfoClient : Window
    {
        public InfoClient()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InformationsClient();
        }
        
        private void InformationsClient()                                                                                                                            // CA MARCHE
        {


            string Moyen = "";
            string InfoB = "";
            if (NomC.Text != "")
            {

                InfoB = NomC.Text;
                Moyen = "nom";
            }
            if (MailC.Text != "")
            {

                InfoB = MailC.Text;
                Moyen = "email";
            }
            if (AdresseC.Text != "")
            {

                InfoB = AdresseC.Text;
                Moyen = "adresse";
            }
            if (IdClient.Text != "")
            {

                InfoB = IdClient.Text;
                Moyen = "IDClient";
            }

            RetrouverInformationsclient(Moyen, InfoB);
        }
        private void RetrouverInformationsclient(string Moyen, string InfoB)                                                                                         // CA MARCHE
        {
            bool Existe = false;
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = Prekodragan3;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();
            string IDClient = ""; string Nom = ""; string Prénom = ""; int dateNaiss = 0; string adresse = ""; string ville = ""; int depenses = 0; string email = "";
            MySqlCommand command = maConnexion.CreateCommand();
            MySqlDataReader reader;
            if (Moyen == "IDClient")
            {
                command.CommandText = "select nom , prenom, dateNaiss , adresse, ville, depenses, email from Clients where IDClients = '" + InfoB + "'";

                reader = command.ExecuteReader();
                string InfoClient = "";
                while (reader.Read())       // parcours ligne par ligne
                {
                    InfoClient = "";
                    for (int i = 0; i < reader.FieldCount; i++)  //parcours cellule par cellule
                    {
                        string valeurattribut = reader.GetValue(i).ToString();
                        InfoClient += valeurattribut + ",";
                    }
                }                
                string[] TabInfoClient = InfoClient.Split(',');
                if (InfoClient.Length > 15)
                {
                    IDClient = InfoB;
                    Nom = TabInfoClient[0];
                    Prénom = TabInfoClient[1];
                    dateNaiss = Convert.ToInt32(TabInfoClient[2]);
                    adresse = TabInfoClient[3];
                    ville = TabInfoClient[4];
                    depenses = Convert.ToInt32(TabInfoClient[5]);
                    email = TabInfoClient[6];
                    Existe = true;
                }
            }
            if (Moyen == "nom")
            {
                command.CommandText = "select IDClients , prenom, dateNaiss , adresse, ville, depenses, email from Clients where nom = '" + InfoB + "'";

                reader = command.ExecuteReader();
                string InfoClient = "";
                while (reader.Read())       // parcours ligne par ligne
                {
                    InfoClient = "";
                    for (int i = 0; i < reader.FieldCount; i++)  //parcours cellule par cellule
                    {
                        string valeurattribut = reader.GetValue(i).ToString();
                        InfoClient += valeurattribut + ",";
                    }
                }

                string[] TabInfoClient = InfoClient.Split(',');
                if (InfoClient.Length > 15)
                {
                    IDClient = TabInfoClient[0];
                    Nom = InfoB;
                    Prénom = TabInfoClient[1];
                    dateNaiss = Convert.ToInt32(TabInfoClient[2]);
                    adresse = TabInfoClient[3];
                    ville = TabInfoClient[4];
                    depenses = Convert.ToInt32(TabInfoClient[5]);
                    email = TabInfoClient[6];
                    Existe = true;
                }

            }

            if (Moyen == "adresse")
            {
                
                command.CommandText = "select IDClients, nom, prenom , dateNaiss, ville, depenses, email from Clients where adresse = '" + InfoB + "'";

                reader = command.ExecuteReader();
                string InfoClient = "";
                while (reader.Read())       // parcours ligne par ligne
                {
                    InfoClient = "";
                    for (int i = 0; i < reader.FieldCount; i++)  //parcours cellule par cellule
                    {
                        string valeurattribut = reader.GetValue(i).ToString();
                        InfoClient += valeurattribut + ",";
                    }
                }
                string[] TabInfoClient = InfoClient.Split(',');
                if (InfoClient.Length > 15)
                {
                    IDClient = TabInfoClient[0];
                    Nom = TabInfoClient[1];
                    Prénom = TabInfoClient[2];
                    dateNaiss = Convert.ToInt32(TabInfoClient[3]);
                    adresse = InfoB;
                    ville = TabInfoClient[4];
                    depenses = Convert.ToInt32(TabInfoClient[5]);
                    email = TabInfoClient[6];
                    Existe = true;
                }
            }

            if (Moyen == "email")
            {
                command.CommandText = "select IDClients, nom, prenom , dateNaiss, adresse, ville, depenses from Clients where email = '" + InfoB + "'";

                reader = command.ExecuteReader();
                string InfoClient = "";
                while (reader.Read())       // parcours
                {
                    InfoClient = "";
                    for (int i = 0; i < reader.FieldCount; i++)  //parcours cellule par cellule
                    {
                        string valeurattribut = reader.GetValue(i).ToString();
                        InfoClient += valeurattribut + ",";
                    }
                }
                string[] TabInfoClient = InfoClient.Split(',');
                if (InfoClient.Length > 15)
                {
                    IDClient = TabInfoClient[0];
                    Nom = TabInfoClient[1];
                    Prénom = TabInfoClient[2];
                    dateNaiss = Convert.ToInt32(TabInfoClient[3]);
                    adresse = TabInfoClient[4];
                    ville = TabInfoClient[5];
                    depenses = Convert.ToInt32(TabInfoClient[6]);
                    email = InfoB;
                    Existe = true;
                }
                            
            }
            maConnexion.Close();
            AffichageInfoClient(IDClient, Nom, Prénom, dateNaiss, adresse, ville, depenses, email, Existe);
        }
        private void AffichageInfoClient(string IDClient, string nom, string Prénom, int DateNaiss, string adresse, string ville, int Dépenses, string email, bool Existe)        // CA MARCHE
        {
            if(Existe == false)
            {
                Id.Opacity = 0;
                Nom.Opacity = 0;
                Prenom.Opacity = 0;
                Naissance.Opacity = 0;
                Adresse.Opacity = 0;
                Ville.Opacity = 0;
                Depense.Opacity = 0;
                Email.Opacity = 0;
                Erreur.Opacity = 1;
            }
            else
            {
                Id.Opacity = 1;
                Nom.Opacity = 1;
                Prenom.Opacity = 1;
                Naissance.Opacity = 1;
                Adresse.Opacity = 1;
                Ville.Opacity = 1;
                Depense.Opacity = 1;
                Email.Opacity = 1;
                Erreur.Opacity = 0;
                Id.Content = "Identifiant client : " + IDClient;
                Nom.Content = "Nom : " + nom;
                Prenom.Content = "Prénom : " + Prénom;
                Naissance.Content = "Année de naissance : " + DateNaiss;
                Adresse.Content = "Adresse : " + adresse;
                Ville.Content = "Ville : " + ville;
                Depense.Content = "Dépense dans le magasin : " + Dépenses;
                Email.Content = "Adresse Email : " + email;
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Menu Mn = new Menu();
            Visibility = Visibility.Hidden;
            Mn.ShowDialog();

        }
    }

}