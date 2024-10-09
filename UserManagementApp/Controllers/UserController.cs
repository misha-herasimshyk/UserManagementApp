using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagementApp.Data;
using UserManagementApp.Models;

namespace UserManagementApp.Controllers
{

    public class UserController : Controller
    {
        private static List<User> users = new List<User>
    {
        new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com", RegistrationDate = DateTime.Now.AddDays(-10) },
        new User { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "jane@example.com", RegistrationDate = DateTime.Now.AddDays(-20) },
    };
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var users = _context.Users;
            return View(users);
        }

        public IActionResult Details(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }

            
            return RedirectToAction("Index");
        }

        
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.RegistrationDate = DateTime.Now;
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index", "User");  
            }

            return View(user);
        }



    }
}