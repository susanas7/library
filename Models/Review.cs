namespace Library.Models
{
    public class Review
    {
       
            public int Id { get; set; }
            public int BookId { get; set; }
            public int UserId { get; set; }
            public int Rating { get; set; } = 0;
            public string Comment { get; set; } = string.Empty;
            public DateTime Date { get; set; } = DateTime.Now;
            public Book? Book { get; set; }
            public User? User { get; set; }
        
    }
}
