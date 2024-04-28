namespace FahasaStoreClientApp.Models
{
    public class Review
    {
        public Review(int reviewId, int bookId, int userId, int rating, string? comment, DateTime reviewDate, string book, string user)
        {
            ReviewId = reviewId;
            BookId = bookId;
            UserId = userId;
            Rating = rating;
            Comment = comment;
            ReviewDate = reviewDate;
            Book = book;
            User = user;
        }

        public int ReviewId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public string Book { get; set; }
        public string User { get; set; }
    }
}
