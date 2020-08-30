using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace API_.NET.Modele
{
    public class DataBaseManagerNoteFrais : DataBaseManager
    {
        public DataBaseManagerNoteFrais() : base()
        {

        }
        public List<NoteFrais> GetAllNoteFrais()
        {
            List<NoteFrais> noteFraisList = new List<NoteFrais>();
            try
            {
                this.connection.Open();
                MySqlCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM note_frais";

                // Exécution de la commande SQL
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // Récupérez l'indexe (index) de colonne Emp_ID dans l'instruction de requête SQL.
                            NoteFrais noteFrais = new NoteFrais(reader.GetFloat("montant_total"),
                                                    reader.GetString("statut"),
                                                    reader.GetInt32("employe_id"),
                                                    reader.GetDateTime("date"),
                                                    reader.GetInt32("id"));
                            noteFraisList.Add(noteFrais);
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
            return noteFraisList;
        }

        public string AddNoteFrais(NoteFrais noteFrais)
        {
            try
            {
                this.connection.Open();
                MySqlCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = $"INSERT INTO note_frais (montant_total, statut, employe_id, date) VALUES ({noteFrais.montant_total.ToString(CultureInfo.InvariantCulture)}, '{noteFrais.statut}', {noteFrais.employe_id}, '{noteFrais.date:yyyy-MM-dd HH:mm:ss}')";
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

        public int SearchOrCreateNoteFrais(int employe_id, DateTime date)
        {
            int note_frais_id = -1;
            try
            {
                this.connection.Open();
                MySqlCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = $"SELECT * FROM note_frais WHERE employe_id={employe_id} AND date='{date:u}'";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        note_frais_id = reader.GetInt32("id");
                    }
                    else
                    {
                        DataBaseManagerNoteFrais dataBaseManagerNoteFrais = new DataBaseManagerNoteFrais();
                        dataBaseManagerNoteFrais.AddNoteFrais(new NoteFrais(0,"En attente", employe_id, date));
                        note_frais_id = SearchOrCreateNoteFrais(employe_id, date);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Generic Exception Handler: {e}");
            }
            return note_frais_id;
        }
    }
}
