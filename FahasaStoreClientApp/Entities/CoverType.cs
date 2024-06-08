﻿using System;
using System.Collections.Generic;

namespace FahasaStoreClientApp.Entities
{
    public partial class CoverType
    {
        public CoverType()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
