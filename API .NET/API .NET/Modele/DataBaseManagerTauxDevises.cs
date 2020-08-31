using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_.NET.Modele
{
    public class DataBaseManagerTauxDevises : DataBaseManager
    {
        public DataBaseManagerTauxDevises() : base()
        {

        }

        public List<TauxDevise> GetTauxDevises(DateTime date)
        {
            date = date.AddMonths(1).AddDays(-1);

            List<TauxDevise> tauxDeviseList = new List<TauxDevise>();

            this.connection.Open();
            MySqlCommand cmd = this.connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM taux_devises WHERE date = '{date:yyyy-MM-dd HH:mm:ss}'";
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        TauxDevise tauxDevise = new TauxDevise(reader.GetString("nom_taux"),
                                                            reader.GetFloat("taux"),
                                                            reader.GetDateTime("date"),
                                                            reader.GetInt32("id"));
                        tauxDeviseList.Add(tauxDevise);
                    }
                }
                else
                {
                    tauxDeviseList = APITauxDevises.GetTauxDevisesFromAPI(date);
                    SetTauxDevises(tauxDeviseList, date);
                }

            }

            this.connection.Close();
            return tauxDeviseList;
        }

        //TODO
        public void SetTauxDevises(List<TauxDevise> tauxDeviseList, DateTime date)
        {

        }

    }
}
