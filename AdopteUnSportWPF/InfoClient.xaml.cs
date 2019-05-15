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
        private bool ExistenceIDClient(string IDClients)                                                                                                             // CA MARCHE
        {
            bool Existence = false;
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = Buzenval1998;";
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
                    if (ligne == IDClients)
                    {
                        Existence = true;
                        Console.WriteLine(" Le client a été trouvé.");
                    }
                }
            }
            maConnexion.Close();
            return Existence;
        }
        private bool ExistenceNomClient(string nom)                                                                                                                  // CA MARCHE
        {
            bool Existence = false;
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = Buzenval1998;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();

            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "SELECT nom from Clients"; // exemple de requête

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string ligne = reader.GetValue(i).ToString();
                    if (ligne == nom)
                    {
                        Existence = true;
                        Console.WriteLine(" Le client a été trouvé.");
                    }
                }
            }
            maConnexion.Close();
            return Existence;
        }
        private bool ExistenceAdresseClient(string adresse)                                                                                                          // CA MARCHE
        {
            bool Existence = false;
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = MATIbol78;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();

            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "SELECT adresse from Clients"; // exemple de requête

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string ligne = reader.GetValue(i).ToString();
                    if (ligne == adresse)
                    {
                        Existence = true;
                        Console.WriteLine(" Le client a été trouvé.");
                    }
                }
            }
            maConnexion.Close();
            return Existence;
        }
        private bool ExistenceEmailClient(string email)                                                                                                              // CA MARCHE
        {
            bool Existence = false;
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = MATIbol78;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();

            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "SELECT email from Clients"; // exemple de requête

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string ligne = reader.GetValue(i).ToString();
                    if (ligne == email)
                    {
                        Existence = true;
                        Console.WriteLine(" Le client a été trouvé.");
                    }
                }
            }
            maConnexion.Close();
            return Existence;
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
                Moyen = "idclient";
            }

            RetrouverInformationsclient(Moyen, InfoB);
        }
        private void RetrouverInformationsclient(string Moyen, string InfoB)                                                                                         // CA MARCHE
        {
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = Buzenval1998;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();
            string IDClient = ""; string Nom = ""; string Prénom = ""; int dateNaiss = 0; string adresse = ""; string ville = ""; int depenses = 0; string email = "";
            MySqlCommand command = maConnexion.CreateCommand();
            MySqlDataReader reader;
            if (Moyen == "IDClient")
            {
                bool Existence = false;
                Existence = ExistenceIDClient(InfoB);
                while (Existence == false)
                {

                    InfoB = Console.ReadLine();
                    Existence = ExistenceNomClient(InfoB);
                }
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
                IDClient = InfoB;
                Nom = TabInfoClient[0];
                Prénom = TabInfoClient[1];
                dateNaiss = Convert.ToInt32(TabInfoClient[2]);
                adresse = TabInfoClient[3];
                ville = TabInfoClient[4];
                depenses = Convert.ToInt32(TabInfoClient[5]);
                email = TabInfoClient[6];
            }
            if (Moyen == "nom")
            {
                bool Existence = false;
                Existence = ExistenceNomClient(InfoB);
                while (Existence == false)
                {

                    InfoB = Console.ReadLine();
                    Existence = ExistenceNomClient(InfoB);
                }
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
                IDClient = TabInfoClient[0];
                Nom = InfoB;
                Prénom = TabInfoClient[1];
                dateNaiss = Convert.ToInt32(TabInfoClient[2]);
                adresse = TabInfoClient[3];
                ville = TabInfoClient[4];
                depenses = Convert.ToInt32(TabInfoClient[5]);
                email = TabInfoClient[6];
            }

            if (Moyen == "adresse")
            {
                bool Existence = false;
                Existence = ExistenceAdresseClient(InfoB);
                while (Existence == false)
                {
                    Console.WriteLine("L'adresse donnée n'existe pas dans les bases de données, veuillez en donner une nouvelle :");
                    InfoB = Console.ReadLine();
                    Existence = ExistenceNomClient(InfoB);
                }
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
                IDClient = TabInfoClient[0];
                Nom = TabInfoClient[1];
                Prénom = TabInfoClient[2];
                dateNaiss = Convert.ToInt32(TabInfoClient[3]);
                adresse = InfoB;
                ville = TabInfoClient[4];
                depenses = Convert.ToInt32(TabInfoClient[5]);
                email = TabInfoClient[6];
            }

            if (Moyen == "email")
            {
                bool Existence = false;
                Existence = ExistenceEmailClient(InfoB);
                while (Existence == false)
                {

                    InfoB = Console.ReadLine();
                    Existence = ExistenceNomClient(InfoB);
                }
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
                IDClient = TabInfoClient[0];
                Nom = TabInfoClient[1];
                Prénom = TabInfoClient[2];
                dateNaiss = Convert.ToInt32(TabInfoClient[3]);
                adresse = TabInfoClient[4];
                ville = TabInfoClient[5];
                depenses = Convert.ToInt32(TabInfoClient[6]);
                email = InfoB;
            }
            maConnexion.Close();
            AffichageInfoClient(IDClient, Nom, Prénom, dateNaiss, adresse, ville, depenses, email);
        }
        private void AffichageInfoClient(string IDClient, string nom, string Prénom, int DateNaiss, string adresse, string ville, int Dépenses, string email)        // CA MARCHE
        {
            Id.Content = "Identifiant client:" + IDClient;
            Nom.Content = "Nom:" + nom;
            Prenom.Content = "Prénom:" + Prénom;
            Naissance.Content = "Année de naissance:" + DateNaiss;
            Adresse.Content = "Adresse:" + adresse;
            Ville.Content = "Ville:" + ville;
            Depense.Content = "Dépense dans le magasin:" + Dépenses;
            Email.Content = "Adresse Email:" + email;
        }

    }

}