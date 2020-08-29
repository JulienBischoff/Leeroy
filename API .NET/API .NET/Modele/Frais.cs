using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_.NET.Modele
{
    public class Frais
    {
        public int id { get; set; }
        public int employe_id { get; set; }
        public string intitule { get; set; }
        public float montant { get; set; }
        public string devise { get; set; }
        public DateTime date { get; set; }
        public string statut { get; set; }
        public string motif { get; set; }

        public Frais()
        {
        }
        public Frais(int id, int employe_id, string intitule, float montant, string devise, DateTime date, string statut, string motif)
        {
            this.id = id;
            this.employe_id = employe_id;
            this.intitule = intitule;
            this.montant = montant;
            this.devise = devise;
            this.date = date;
            this.statut = statut;
            this.motif = motif;
        }
        public override string ToString()
        {
            return $"{id} {employe_id} {intitule} {montant} {devise} {date} {statut} {motif}";
        }
    }
}
