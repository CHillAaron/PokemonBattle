using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonBattle.CORE;
using PokemonBattle.DAL;
using System.Linq;

namespace PokemonBattle.Controllers
{
    public class HomeController : Controller
    {
        private readonly PokemonContext _context;
        private readonly PokemonService _pokemonService;

        public HomeController(PokemonContext context, PokemonService pokemonService)
        {
            _context = context;
            _pokemonService = pokemonService;
        }
        public User UserInDb()
        {
            return _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(User reg)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == reg.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use");
                    return View("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                reg.Password = Hasher.HashPassword(reg, reg.Password);
                _context.Users.Add(reg);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("UserId", reg.UserId);
                return RedirectToAction("DashBoard");
            }
            else
            {
                return View("Index");
            }
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginUser userSubmission)
        {
            if (ModelState.IsValid)
            {
                // If inital ModelState is valid, query for a user with provided email
                var userInDb = _context.Users.FirstOrDefault(u => u.Email == userSubmission.LogEmail);
                // If no user exists with provided email
                if (userInDb == null)
                {
                    // Add an error to ModelState and return to View!
                    ModelState.AddModelError("LogEmail", "Invalid Email/Password");
                    return View("Index");
                }

                // Initialize hasher object
                var hasher = new PasswordHasher<LoginUser>();

                // verify provided password against hash stored in db
                var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.LogPassword);

                // result can be compared to 0 for failure
                if (result == 0)
                {
                    // handle failure (this should be similar to how "existing email" is handled)
                    ModelState.AddModelError("LogEmail", "Invalid Email/Password");
                    return View("Index");

                }

                HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                return RedirectToAction("DashBoard");
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet("DashBoard")]
        public IActionResult DashBoard()
        {
            User userInDb = UserInDb();
            if (userInDb == null)
            {
                return RedirectToAction("LogOut");
            }
            List<CaughtPokemon> usersPokemon = _context.CaughtPokemons
                                                       .ToList();
            DashBoardView dashBoardModel = new DashBoardView()
            {
                UserName = $"{userInDb.FirstName}",
                AllCaughtPokemon = usersPokemon.Any(p => p.UserId == userInDb.UserId)
            };

            return View(dashBoardModel);
        }
        [HttpPost("CatchPokemon")]
        public IActionResult CatchPokemon()
        {
            User userInDb = UserInDb();
            if (userInDb == null)
            {
                return RedirectToAction("LogOut");
            }
            _pokemonService.CreatePokemon(userInDb.UserId);
            
            return RedirectToAction("DashBoard");
        }

        [HttpGet("LogOut")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}