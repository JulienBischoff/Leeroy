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
        JWTManager jwtManager;
        public FraisController()
        {
            dataBaseManagerFrais = new DataBaseManagerFrais();
            jwtManager = new JWTManager();
        }

        [HttpGet]
        [Route("list")]
        public List<Frais> FraisList()
        {
            if (CheckToken())
            {
                return dataBaseManagerFrais.GetAllFrais();
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("list/{employe_id}")]
        public List<Frais> FraisEmployeList(int employe_id)
        {
            if (CheckToken())
            {
                return dataBaseManagerFrais.GetEmployeFrais(employe_id);
            }
            else
            {
                return null;
            }
        }
        
        [HttpGet]
        [Route("list/{employe_id}/{annee}/{mois}")]
        public List<Frais> FraisEmployePerMonthList(int employe_id, int annee ,int mois)
        {
            if (CheckToken())
            {
                return dataBaseManagerFrais.GetEmployeFraisPerYearMonth(employe_id, annee, mois);
            }
            else
            {
                return null;
            }
        }
        
        [HttpPost]
        [Route("create")]
        public string AddFrais([FromBody]Frais frais)
        {
            if (CheckToken())
            {
                DataBaseManagerNoteFrais dataBaseManagerNoteFrais = new DataBaseManagerNoteFrais();
                frais.note_frais_id = dataBaseManagerNoteFrais.SearchOrCreateNoteFrais(frais.employe_id, new DateTime(frais.date.Year, frais.date.Month, 1));
                return dataBaseManagerFrais.AddFrais(frais);
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        [Route("update")]
        public string UpdateFrais([FromBody] Frais frais)
        {
            if (CheckToken())
            {
                return dataBaseManagerFrais.UpdateFrais(frais);
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
