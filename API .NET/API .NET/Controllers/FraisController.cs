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
        [HttpGet]
        [Route("fraisList")]
        public List<Frais> FraisList()
        {
            DataBaseManager dataBaseManager = new DataBaseManager();
            return dataBaseManager.GetAllFrais();
        }
    }
}
