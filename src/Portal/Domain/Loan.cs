using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Domain
{
    public class Loan
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public int BookId { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
    }
}
