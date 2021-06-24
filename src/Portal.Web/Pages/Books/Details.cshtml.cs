using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ganss.XSS;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portal.Data.Models;
using Portal.Persistence;

namespace Portal.Web.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public DetailsModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public Book Book { get; set; }
        public HtmlString HtmlDesc { get; set; }
        public void OnGet(int bookId)
        {
            Book = _db.Books.Find(bookId);

            var sanitizer = new HtmlSanitizer();
            var sanitized = sanitizer.Sanitize(Book.Description);
            HtmlDesc = new HtmlString(sanitized);
        }
    }
}
