using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Data.Models
{
    public class Book
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        [Range(1000, 2021)]
        public int PublishedYear { get; set; }

        public BookState State { get; set; }

        public DateTime TimeCreated { get; set; }


    }

    public enum BookState
    {
        Available = 0,
        Checkedout = 1,
        Reserved = 2
    }
}
