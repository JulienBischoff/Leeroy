using API_.NET.Modele;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace API_.NET.Controllers
{
    [Route("api/noteFrais")]
    public class NoteFraisController : ControllerBase
    {
        DataBaseManagerNoteFrais dataBaseManagerNoteFrais;
        JWTManager jwtManager;
        public NoteFraisController()
        {
            dataBaseManagerNoteFrais = new DataBaseManagerNoteFrais();
            jwtManager = new JWTManager();
        }

        [HttpGet]
        [Route("list")]
        public List<NoteFrais> NoteFraisList()
        {
            if (CheckToken())
            {
                return dataBaseManagerNoteFrais.GetAllNoteFrais();
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        [Route("create")]
        public string create([FromBody] NoteFrais noteFrais)
        {
            if (CheckToken())
            {
                return dataBaseManagerNoteFrais.AddNoteFrais(noteFrais);
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        [Route("validate")]
        public string validate([FromBody] NoteFrais noteFrais)
        {
            if (CheckToken())
            {
                return dataBaseManagerNoteFrais.ValidateNoteFrais(noteFrais.id, noteFrais.date);
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public string refuse(int note_frais_id, int annee, int mois, [FromBody]List<Frais> refusedFrais)
        {
            if (CheckToken())
            {
                return dataBaseManagerNoteFrais.RefuseNoteFrais(note_frais_id, new DateTime(annee, mois, 1), refusedFrais);
            }
            else
            {
                return null;
            }
        }

        public bool CheckToken()
        {
            var headers = Request.Headers;
            string token = headers["Authorization"];

            return jwtManager.IsTokenValid(token);
        }

    }
}
