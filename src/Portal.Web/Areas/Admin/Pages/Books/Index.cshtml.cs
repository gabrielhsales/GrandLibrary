using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portal.Data.Models;
using Portal.Modules.Books;

namespace Portal.Web.Areas.Admin.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IList<Book> Books { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var result = await _mediator.Send(new GetQuery.Request());
            Books = result.Result.Books;
            return Page();
        }
    }
}
