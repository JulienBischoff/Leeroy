using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
    }
}
