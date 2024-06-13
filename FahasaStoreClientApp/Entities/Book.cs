﻿using System;
using System.Collections.Generic;

namespace FahasaStoreClientApp.Entities
{
    public partial class Book
    {
        public Book()
        {
            BookPartners = new HashSet<BookPartner>();
            CartItems = new HashSet<CartItem>();
            Favourites = new HashSet<Favourite>();
            FlashSaleBooks = new HashSet<FlashSaleBook>();
            OrderItems = new HashSet<OrderItem>();
            PosterImages = new HashSet<PosterImage>();
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public int SubcategoryId { get; set; }
        public int AuthorId { get; set; }
        public int CoverTypeId { get; set; }
        public int DimensionId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Price { get; set; }
        public int DiscountPercentage { get; set; }
        public int Quantity { get; set; }
        public double? Weight { get; set; }
        public int? PageCount { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Author Author { get; set; } = null!;
        public virtual CoverType CoverType { get; set; } = null!;
        public virtual Dimension Dimension { get; set; } = null!;
        public virtual Subcategory Subcategory { get; set; } = null!;
        public virtual ICollection<BookPartner> BookPartners { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<FlashSaleBook> FlashSaleBooks { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<PosterImage> PosterImages { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
