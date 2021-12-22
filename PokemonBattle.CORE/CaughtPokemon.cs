using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonBattle.CORE
{
    public class CaughtPokemon
    {
        [Key]
        public int CaughtPokemonId { get; set; }
        public string CaughtPokemonName { get; set; }
        //public string Move1 { get; set; }
        //public string Move2 { get; set; }
        //public string Move3 { get; set; }
        //public string Move4 { get; set; }
        //the one to many relationship with user
        public int UserId { get; set; }
        public User Trainer { get; set; }
    }
}
