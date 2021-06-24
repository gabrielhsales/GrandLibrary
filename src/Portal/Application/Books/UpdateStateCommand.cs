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
    public class UpdateStateCommand
    {
        public class Request : IRequest<OperationResult<Response>>
        {
            public Request(int bookId, BookState state)
            {
                Id = bookId;
                State = state;
            }
            public int Id { get; init; }
            public BookState State { get; init; }
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

                var entity = _db.Books.Update(book);
                entity.Property(b => b.TimeCreated).IsModified = false;
                entity.Property(b => b.PublishedYear).IsModified = false;
                entity.Property(b => b.Name).IsModified = false;
                entity.Property(b => b.Description).IsModified = false;

                var changeCount = await _db.SaveChangesAsync();

                if (changeCount > 0)
                {
                    return OperationResult<Response>.BuildSuccess(new Response { BookId = book.Id });
                }
                return OperationResult<Response>.BuildFailure("State update failed");
            }
        }

        public class Response
        {
            public int BookId { get; set; }
        }
    }
}
