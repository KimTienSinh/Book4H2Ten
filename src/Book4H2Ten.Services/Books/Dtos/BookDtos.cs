using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Services.Books.Dtos
{
    public class BookDtos
    {
        public string BookName { get; set; }

        public string Description { get; set; }

        public DateTime PublishDate { get; set; }

        public byte Image { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }
        public string AuthorName { get; set; }

        public int Status { get; set; }
    }
}
