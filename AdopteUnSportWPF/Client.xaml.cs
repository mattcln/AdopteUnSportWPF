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
    /// Logique d'interaction pour Client.xaml
    /// </summary>
    public partial class Client : Window
    {
        public Client()
        {
            InitializeComponent();
        }


        private void CréationClient()                                                                                                                                // CA MARCHE
        {

            string Nom = NomC.Text;
            string Prénom = PrenomC.Text;
            int AnnéeNaiss = Convert.ToInt32(DateC.Text);
            string Adresse = AdresseC.Text;
            string Ville = VilleC.Text;
            string Email = MailC.Text;
            EnregistrementClient(Nom, Prénom, AnnéeNaiss, Adresse, Ville, Email);

        }
        private void EnregistrementClient(string Nom, string Prénom, int AnnéeNaiss, string Adresse, string Ville, string Email)                                     // CA MARCHE
        {
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = Buzenval1998;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();

            string IDClient = CréationIDClient();

            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "insert into Clients values ('" + IDClient + "','" + Nom + "','" + Prénom + "','" + AnnéeNaiss + "','" + Adresse + "','" + Ville + "','" + 0 + "','" + Email + "')";
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            CreerC.Text="Le client a bien été enregistré.";
            maConnexion.Close();
        }
        private string CréationIDClient()                                                                                                                            // CA MARCHE
        {
            string infoConnexion = "SERVER = localhost; PORT = 3306; DATABASE = magasinAdopteUnSport; UID = root; PASSWORD = Buzenval1998;";
            MySqlConnection maConnexion = new MySqlConnection(infoConnexion);
            maConnexion.Open();
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "select count(IDClients)+1 from Clients";
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string IDClient = "";
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    IDClient = reader.GetValue(i).ToString();
                }
            }
            while (IDClient.Length < 4)
            {
                IDClient = "0" + IDClient;
            }
            IDClient = "A" + IDClient;
            maConnexion.Close();
            return IDClient;
        }

        private void BtnValiderC_Click_1(object sender, RoutedEventArgs e)
        {
            CréationClient();
        }

        private void BtnValiderC_Click_2(object sender, RoutedEventArgs e)
        {
            Menu Mn = new Menu();
            Visibility = Visibility.Hidden;
            Mn.ShowDialog();
        }
    }
}
