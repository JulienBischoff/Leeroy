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
        DataBaseManager dataBaseManager;
        public FraisController()
        {
            dataBaseManager = new DataBaseManager();
        }

        [HttpGet]
        [Route("fraisList")]
        public List<Frais> FraisList()
        {
            return dataBaseManager.GetAllFrais();
        }

        [HttpGet]
        [Route("fraisList/{employe_id}")]
        public List<Frais> FraisEmployeList(int employe_id)
        {
            return dataBaseManager.GetEmployeFrais(employe_id);
        }
    }
}
