using PokemonBattle.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonBattle.DAL
{
    public class PokemonService
    {
        private PokemonAPIService _apiSpeciesService;
        private PokemonContext _context;
        Random random = new Random();
        public PokemonService(PokemonAPIService apiSpeciesService, PokemonContext context)
        {
            this._apiSpeciesService = apiSpeciesService;
            this._context = context;
        }

        public Pokemon GetSpecies()
        {
            List<Pokemon> allSpecies = _apiSpeciesService.GetALLPokemon();
            int index = random.Next(allSpecies.Count);
            Pokemon selectedSpecies = allSpecies[index];
            return selectedSpecies;
        }

        public void AddPokemon(CaughtPokemon newPokemon)
        {
            _context.CaughtPokemons.Add(newPokemon);
            _context.SaveChanges();
        }

        public void CreatePokemon(int trainerId)
        {
            Pokemon pm = GetSpecies();
            CaughtPokemon catchPokemon = new CaughtPokemon
            {
                CaughtPokemonName = pm.Name,
                UserId = trainerId
            };

            AddPokemon(catchPokemon);
        }
    }
}
