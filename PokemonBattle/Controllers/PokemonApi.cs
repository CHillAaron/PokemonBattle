using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PokemonBattle.CORE;
using PokemonBattle.DAL;

namespace PokemonBattle.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonApi : ControllerBase
    {
        private readonly PokemonAPIService _apiService;

        public PokemonApi(PokemonAPIService apiService)
        {
            _apiService = apiService;
        }

        //[HttpGet]
        //public List<CaughtPokemon> Get()
        //{
        //    return _apiService.GetALLPokemon();
        //}
        [HttpGet]
        public List<Pokemon> Get()
        {
            return _apiService.GetALLPokemon();
        }

    }
}
