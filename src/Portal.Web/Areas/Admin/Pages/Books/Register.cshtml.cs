using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portal.Modules.Books;

namespace Portal.Web.Areas.Admin.Pages.Books
{
    [BindProperties]
    public class RegisterModel : PageModel
    {

        private readonly IMediator _mediator;

        public RegisterModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int PublishedYear { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var req = new RegisterCommand.Request(Name, PublishedYear);
                req.Description = Description;
                var result = await _mediator.Send(req);
                if (result.Success)
                {
                    return RedirectToPage("./index");
                }
            }

            return Page();
        }
    }
}
