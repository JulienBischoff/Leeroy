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
        public List<Frais> GetEmployeFraisPerYearMonth(int employe_id, int annee, int mois)
        {
            List<Frais> fraisList = new List<Frais>();
            try
            {
                this.connection.Open();
                MySqlCommand cmd = this.connection.CreateCommand();
                //TODO gérer les années
                cmd.CommandText = $"SELECT * FROM Frais WHERE employe_id={employe_id} AND date >='{new DateTime(annee, mois, 1):u}' AND date<'{new DateTime(annee, mois, 1).AddMonths(1):u}'";

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
        
        public List<Frais> GetFraisPerNoteFraisID(int note_frais_id)
        {
            List<Frais> fraisList = new List<Frais>();
            try
            {
                this.connection.Open();
                MySqlCommand cmd = this.connection.CreateCommand();
                //TODO gérer les années
                cmd.CommandText = $"SELECT * FROM Frais WHERE note_frais_id={note_frais_id}";

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

        public List<Frais> GetAllFraisOfNoteFrais(int note_frais_id)
        {
            List<Frais> fraisList = new List<Frais>();
            try
            {
                this.connection.Open();
                MySqlCommand cmd = this.connection.CreateCommand();
                //TODO gérer les années
                cmd.CommandText = $"SELECT * FROM Frais WHERE note_frais_id={note_frais_id}";

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
                cmd.CommandText = $"INSERT INTO frais (employe_id, intitule, montant, devise, date, note_frais_id) VALUES ({frais.employe_id}, '{frais.intitule}', {frais.montant.ToString(CultureInfo.InvariantCulture)}, '{frais.devise}', '{frais.date:yyyy-MM-dd HH:mm:ss}' , {frais.note_frais_id})";
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

        public string UpdateFrais(Frais frais)
        {
            try
            {
                this.connection.Open();
                MySqlCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = $"UPDATE frais SET employe_id = {frais.employe_id}, intitule = '{frais.intitule}', montant = {frais.montant.ToString(CultureInfo.InvariantCulture)}, devise = '{frais.devise}', date = '{frais.date:yyyy-MM-dd HH:mm:ss}', statut = '{frais.statut}', motif = '{frais.motif}', note_frais_id = {frais.note_frais_id} WHERE id = {frais.id}";
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
        public string ValidateFrais(int note_frais_id)
        {
            try
            {
                this.connection.Open();
                MySqlCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = $"UPDATE frais SET statut = 'Validé' WHERE note_frais_id = {note_frais_id}";
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

        public string RefuseFrais(List<Frais> refusedFrais)
        {
            try
            {
                this.connection.Open();
                MySqlCommand cmd = this.connection.CreateCommand();
                foreach (Frais frais in refusedFrais)
                {
                    cmd.CommandText = $"UPDATE frais SET statut = 'Refusé', motif = '{frais.motif}' WHERE id = {frais.id}";
                    cmd.ExecuteNonQuery();
                }
                this.connection.Close();
                return "ok";
            }
            catch (Exception e)
            {
                Console.WriteLine($"Generic Exception Handler: {e}");
                return e.Message;
            }
        }
        public float CalculateTotalFrais(int note_frais_id, DateTime date)
        {
            try
            {
                DataBaseManagerTauxDevises dataBaseManagerTauxDevises = new DataBaseManagerTauxDevises();


                //Chercher les taux pour le mois
                List<TauxDevise> tauxDevises = dataBaseManagerTauxDevises.GetTauxDevises(date);

                List<Frais> allFrais = GetAllFraisOfNoteFrais(note_frais_id);

                float totalFrais = 0;
                foreach (Frais frais in allFrais)
                {
                    if (frais.devise == "EUR")
                    {
                        totalFrais += frais.montant;
                    }
                    else
                    {
                        TauxDevise tauxDevise = tauxDevises.First(x => x.nom_taux == frais.devise);
                        totalFrais += frais.montant * tauxDevise.taux;
                    }
                }


                return totalFrais;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Generic Exception Handler: {e}");
                return -1;
            }
        }
    }
}
