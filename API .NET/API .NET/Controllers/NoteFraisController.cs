using API_.NET.Modele;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_.NET.Controllers
{
    [Route("api/noteFrais")]
    public class NoteFraisController : ControllerBase
    {
        DataBaseManagerNoteFrais dataBaseManagerNoteFrais;
        public NoteFraisController()
        {
            dataBaseManagerNoteFrais = new DataBaseManagerNoteFrais();
        }

        [HttpGet]
        [Route("list")]
        public List<NoteFrais> NoteFraisList()
        {
            return dataBaseManagerNoteFrais.GetAllNoteFrais();
        }

        [HttpPost]
        [Route("create")]
        public string create([FromBody]NoteFrais noteFrais)
        {
            return dataBaseManagerNoteFrais.AddNoteFrais(noteFrais);
        }

        [HttpPost]
        [Route("validate")]
        public string validate([FromBody]NoteFrais noteFrais)
        {
            return dataBaseManagerNoteFrais.ValidateNoteFrais(noteFrais.id, noteFrais.date);
        }
        
        [HttpPost]
        [Route("refuse/{note_frais_id}/{annee}/{mois}")]
        public string refuse(int note_frais_id, int annee, int mois, [FromBody]List<Frais> refusedFrais)
        {
            return dataBaseManagerNoteFrais.RefuseNoteFrais(note_frais_id, new DateTime(annee, mois, 1), refusedFrais);
        }
    }
}
