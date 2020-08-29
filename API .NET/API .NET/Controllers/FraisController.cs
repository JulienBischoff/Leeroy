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
        [Route("fraisList")]
        public List<Frais> FraisList()
        {
            return dataBaseManagerFrais.GetAllFrais();
        }

        [HttpGet]
        [Route("fraisList/{employe_id}")]
        public List<Frais> FraisEmployeList(int employe_id)
        {
            return dataBaseManagerFrais.GetEmployeFrais(employe_id);
        }
    }
}
