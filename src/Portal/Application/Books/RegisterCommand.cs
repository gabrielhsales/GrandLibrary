using Mapster;
using MediatR;
using Portal.Application.Common;
using Portal.Data.Models;
using Portal.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Portal.Modules.Books
{
    public class RegisterCommand
    {
        public class Request : IRequest<OperationResult<Response>>
        {

            public Request(string name, int publishedYear)
            {
                Name = name;
                PublishedYear = publishedYear;
            }

            public string Name { get; init; }
            public int PublishedYear { get; init; }
            public string Description { get; set; }
        }

        public class Handler : IRequestHandler<Request, OperationResult<Response>>
        {

            private readonly ApplicationDbContext _db;

            public Handler(ApplicationDbContext db)
            {
                _db = db;
            }

            public async Task<OperationResult<Response>> Handle(Request request, CancellationToken cancellationToken)
            {
                var book = request.Adapt<Book>();
                _db.Books.Add(book);

                try
                {
                    await _db.SaveChangesAsync(cancellationToken);
                    return OperationResult<Response>.BuildSuccess(new Response { BookId = book.Id });
                }
                catch (Exception ex)
                {

                    return OperationResult<Response>.BuildFailure(ex, "Book Register failed");
                }

            }
        }

        public class Response
        {
            public int BookId { get; set; }
        }

    }
}
