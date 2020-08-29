using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_.NET.Modele
{
    public class DataBaseManager
    {
        private MySqlConnection connection;

        public DataBaseManager()
        {
            this.InitConnexion();
        }

        private void InitConnexion()
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=business_leeroy;UID=root;PASSWORD=";
            this.connection = new MySqlConnection(connectionString);
        }

        public List<Frais> GetAllFrais()
        {
            List<Frais> fraisList = new List<Frais>();
            try
            {
                this.connection.Open();
                MySqlCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Frais";

                // Exécution de la commande SQL
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // Récupérez l'indexe (index) de colonne Emp_ID dans l'instruction de requête SQL.
                            Frais frais = new Frais(reader.GetInt32("id"),
                                                    reader.GetInt32("employe_id"),
                                                    reader.GetString("intitule"),
                                                    reader.GetFloat("montant"),
                                                    reader.GetString("devise"),
                                                    reader.GetDateTime("date"),
                                                    reader.GetString("statut"),
                                                    reader.GetString("motif"));
                            fraisList.Add(frais);
                        }
                    }
                }

                // Fermeture de la connexion
                this.connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Generic Exception Handler: {e}");
            }
            return fraisList;
        }
        public List<Frais> GetEmployeFrais(int employe_id)
        {
            List<Frais> fraisList = new List<Frais>();
            try
            {
                this.connection.Open();
                MySqlCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = $"SELECT * FROM Frais WHERE employe_id={employe_id}";

                // Exécution de la commande SQL
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // Récupérez l'indexe (index) de colonne Emp_ID dans l'instruction de requête SQL.
                            Frais frais = new Frais(reader.GetInt32("id"),
                                                    reader.GetInt32("employe_id"),
                                                    reader.GetString("intitule"),
                                                    reader.GetFloat("montant"),
                                                    reader.GetString("devise"),
                                                    reader.GetDateTime("date"),
                                                    reader.GetString("statut"),
                                                    reader.GetString("motif"));
                            fraisList.Add(frais);
                        }
                    }
                }

                // Fermeture de la connexion
                this.connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Generic Exception Handler: {e}");
            }
            return fraisList;
        }

    }
}
