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
        DataBaseManager dataBaseManager;
        public NoteFraisController()
        {
            dataBaseManager = new DataBaseManager();
        }

        [HttpPost]
        [Route("noteFraisList")]
        public string NoteFraisList([FromBody]NoteFrais noteFrais)
        {
            return dataBaseManager.AddNoteFrais(noteFrais);
        }
    }
}
