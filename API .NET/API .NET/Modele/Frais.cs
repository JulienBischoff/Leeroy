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
        public int note_frais_id { get; set; }

        public Frais()
        {
        }
        public Frais(int id, int employe_id, string intitule, float montant, string devise, DateTime date, int note_frais_id = 0, string statut = null, string motif = null)
        {
            this.id = id;
            this.employe_id = employe_id;
            this.intitule = intitule;
            this.montant = montant;
            this.devise = devise;
            this.date = date;
            this.statut = statut;
            this.motif = motif;
            this.note_frais_id = note_frais_id;
        }
        public override string ToString()
        {
            return $"{id} {employe_id} {intitule} {montant} {devise} {date} {statut} {motif} {note_frais_id}";
        }
    }
}
