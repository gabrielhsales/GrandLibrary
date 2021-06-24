using MediatR;
using Microsoft.EntityFrameworkCore;
using Portal.Application.Common;
using Portal.Data.Models;
using Portal.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Portal.Modules.Books
{
    public class GetQuery
    {
        public class Request : IRequest<OperationResult<Response>>
        {
            public string? Name { get; set; }
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
                return OperationResult<Response>.BuildSuccess(new Response { Books= await _db.Books.ToListAsync() });
            }
        }

        public class Response
        {
            public IList<Book>? Books { get; set; }
        }
    }
}
