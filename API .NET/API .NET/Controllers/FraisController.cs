using API_.NET.Modele;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_.NET.Controllers
{
    [Route("api/frais")]
    public class FraisController : ControllerBase
    {
        DataBaseManagerFrais dataBaseManagerFrais;
        public FraisController()
        {
            dataBaseManagerFrais = new DataBaseManagerFrais();
        }

        [HttpGet]
        [Route("list")]
        public List<Frais> FraisList()
        {
            return dataBaseManagerFrais.GetAllFrais();
        }

        [HttpGet]
        [Route("list/{employe_id}")]
        public List<Frais> FraisEmployeList(int employe_id)
        {
            return dataBaseManagerFrais.GetEmployeFrais(employe_id);
        }
        
        [HttpPost]
        [Route("create")]
        public string AddFrais([FromBody]Frais frais)
        {
            DataBaseManagerNoteFrais dataBaseManagerNoteFrais = new DataBaseManagerNoteFrais();
            frais.note_frais_id = dataBaseManagerNoteFrais.SearchOrCreateNoteFrais(frais.employe_id, frais.date.Month);
            return dataBaseManagerFrais.AddFrais(frais);
        }

        [HttpPost]
        [Route("update")]
        public string UpdateFrais([FromBody] Frais frais)
        {
            return dataBaseManagerFrais.UpdateFrais(frais);
        }
    }
}
