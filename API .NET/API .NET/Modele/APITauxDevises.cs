using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API_.NET.Modele
{
    public class APITauxDevises
    {

        public static List<TauxDevise> GetTauxDevisesFromAPI(DateTime date)
        {
            //TODO
            //Prevoir le cas où l'API répond pas

            string stringURL = $"https://api.exchangeratesapi.io/{date:yyyy-MM-dd}?base=EUR";
            WebRequest request = WebRequest.Create(stringURL);
            request.Method = "GET";
            HttpWebResponse response = null;
            response = (HttpWebResponse)request.GetResponse();


            List<TauxDevise> tauxDevises = new List<TauxDevise>();
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader streamReader = new StreamReader(stream);
                JObject json = JObject.Parse(streamReader.ReadToEnd());
                foreach (JProperty rate in (JToken)json["rates"])
                {
                    TauxDevise tauxDevise = new TauxDevise(rate.Name, (float)rate.Value, date);
                    tauxDevises.Add(tauxDevise);
                }

                streamReader.Close();
            }

            return tauxDevises;
        }
    }
}
