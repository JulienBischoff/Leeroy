using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace API_.NET.Modele
{
    public class DataBaseManagerFrais : DataBaseManager
    {
        
        public DataBaseManagerFrais() : base()
        {
            
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
                                                    reader.GetInt32("note_frais_id"),
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
                                                    reader.GetInt32("note_frais_id"),
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

        public string AddFrais(Frais frais)
        {
            try
            {
                this.connection.Open();
                MySqlCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = $"INSERT INTO frais (employe_id, intitule, montant, devise, date, note_frais_id) VALUES ({frais.employe_id}, '{frais.intitule}', {frais.montant.ToString(CultureInfo.InvariantCulture)}, '{frais.devise}', '{frais.date:yyyy-MM-dd HH:mm:ss}' , 1)";
                cmd.ExecuteNonQuery();
                this.connection.Close();
                return "ok";
            }
            catch (Exception e)
            {
                Console.WriteLine($"Generic Exception Handler: {e}");
                return e.Message;
            }
        }
    }
}
