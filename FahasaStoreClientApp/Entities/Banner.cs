﻿using System;
using System.Collections.Generic;

namespace FahasaStoreClientApp.Entities
{
    public partial class Banner
    {
        public int Id { get; set; }
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
    }
}
