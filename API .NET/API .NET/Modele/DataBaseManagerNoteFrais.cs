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

        public string AddNoteFrais(NoteFrais noteFrais)
        {
            try
            {
                this.connection.Open();
                MySqlCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = $"INSERT INTO note_frais (montant_total, statut, employe_id, mois) VALUES ({noteFrais.montant_total.ToString(CultureInfo.InvariantCulture)}, '{noteFrais.statut}', {noteFrais.employe_id}, {noteFrais.mois})";
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

        public int SearchOrCreateNoteFrais(int employe_id, int mois)
        {
            int note_frais_id = -1;
            try
            {
                this.connection.Open();
                MySqlCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = $"SELECT * FROM note_frais WHERE employe_id={employe_id} AND mois={mois}";
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
                        dataBaseManagerNoteFrais.AddNoteFrais(new NoteFrais(0,"En attente", employe_id, mois));
                        note_frais_id = SearchOrCreateNoteFrais(employe_id, mois);
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
