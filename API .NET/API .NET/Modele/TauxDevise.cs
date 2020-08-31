using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_.NET.Modele
{
    public class TauxDevise
    {
        public int id { get; set; }
        public string nom_taux { get; set; }
        public float taux { get; set; }
        public DateTime date { get; set; }


        public TauxDevise()
        {

        }

        public TauxDevise(string nom_taux, float taux, DateTime date, int id=0)
        {
            this.nom_taux = nom_taux;
            this.taux = taux;
            this.date = date;
            this.id = id;
        }
    }
}
