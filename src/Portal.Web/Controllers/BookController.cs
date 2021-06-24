using Microsoft.AspNetCore.Mvc;
using Portal.Data.Models;
using Portal.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var data = _db.Books.ToList();
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Create(Book model)
        {
           var entery= _db.Books.Add(model);
            _db.SaveChanges();

            
            return Ok(entery.Entity.Id);
        }
    }
}
