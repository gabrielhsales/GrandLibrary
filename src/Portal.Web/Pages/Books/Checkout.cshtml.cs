using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portal.Modules.Books;

namespace Portal.Web.Pages.Books
{
    public class CheckoutModel : PageModel
    {
        public async Task OnGet(int bookId)
        {
            var response = await _mediator.Send(new GetQuery.Request());
            var book = response.Result.Books.FirstOrDefault(b => b.Id == bookId);

            Id = book.Id;
            Name = book.Name;
            PublishedYear = book.PublishedYear;
            Description = book.Description;
        }

        private readonly IMediator _mediator;

        public CheckoutModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public int Id { get; set; }

        public string Name { get; set; }
        public int PublishedYear { get; set; }
        public string Description { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var req = new UpdateStateCommand.Request(Id, Data.Models.BookState.Checkedout);
                
                var result = await _mediator.Send(req);

                if (result.Success)
                {
                    return RedirectToPage("/index");
                }

                return Page();
            }

            return Page();
        }
    }
}
