﻿using System;
using System.Collections.Generic;

namespace FahasaStoreClientApp.Entities
{
    public partial class Subcategory
    {
        public Subcategory()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<Book> Books { get; set; }
    }
}
