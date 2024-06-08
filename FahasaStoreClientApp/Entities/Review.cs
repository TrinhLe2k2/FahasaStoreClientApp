﻿using System;
using System.Collections.Generic;

namespace FahasaStoreClientApp.Entities
{
    public partial class Review
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; } = null!;
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual AspNetUser User { get; set; } = null!;
    }
}
