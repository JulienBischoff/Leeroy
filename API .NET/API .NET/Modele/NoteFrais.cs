using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_.NET.Modele
{
    public class NoteFrais
    {
        public int id { get; set; }
        public float montant_total { get; set; }
        public string statut { get; set; }
        public int employe_id { get; set; }
        public DateTime date { get; set; }

        public NoteFrais()
        {

        }
        
        public NoteFrais(float montant_total, string statut, int employe_id, DateTime date, int id=0)
        {
            this.id = id;
            this.montant_total = montant_total;
            this.statut = statut;
            this.employe_id = employe_id;
            this.date = date;
        }

        public override string ToString()
        {
            return $"{id} {montant_total} {statut} {employe_id} {date}";
        }
    }
}
