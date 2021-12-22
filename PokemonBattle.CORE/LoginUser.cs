using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonBattle.CORE
{
    public class LoginUser
    {
        [EmailAddress]
        [Required]
        public string LogEmail { get; set; } = "";

        [DataType(DataType.Password)]
        [Required]
        public string LogPassword { get; set; } = "";
    }
}
