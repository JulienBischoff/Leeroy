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

        [HttpPost]
        [Route("noteFraisList")]
        public string NoteFraisList([FromBody]NoteFrais noteFrais)
        {
            return dataBaseManagerNoteFrais.AddNoteFrais(noteFrais);
        }
    }
}
