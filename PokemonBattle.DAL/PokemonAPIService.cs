using Newtonsoft.Json.Linq;
using PokemonBattle.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonBattle.DAL
{
    public class PokemonAPIService
    {
        private HttpClient _httpClient;
        public PokemonAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public List<Pokemon> GetALLPokemon()
        {
            string APIURL = $"https://pokeapi.co/api/v2/pokemon/?offset=0&limit=149";
            var res = _httpClient.GetAsync(APIURL).Result;
            var json = res.Content.ReadAsStringAsync().Result;
            var pokemon = JObject.Parse(json)["results"].ToObject<List<Pokemon>>();
            //var des = JsonConvert.DeserializeObject(json, typeof(APISpecies));
            return pokemon;
            //return JsonConvert.DeserializeObject<List<SpecificSpecies>>(json);
        }
        //public CaughtPokemon GetPokemon(int id)
        //{
        //    string APIURL = $"https://pokeapi.co/api/v2/pokemon/{id}";
        //    var res = _httpClient.GetAsync(APIURL).Result;
        //    var json = res.Content.ReadAsStringAsync().Result;
        //    var pokemon = JObject.Parse(json)["results"].ToObject<CaughtPokemon>();
        //    //var des = JsonConvert.DeserializeObject(json, typeof(APISpecies));
        //    return pokemon;
        //    //return JsonConvert.DeserializeObject<List<SpecificSpecies>>(json);
        //}
    }
}
