using Microsoft.EntityFrameworkCore;
using PokemonBattle.CORE;

namespace PokemonBattle.DAL
{
    public class PokemonContext : DbContext
    {
        public PokemonContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<CaughtPokemon> CaughtPokemons { get; set; }
    }
}