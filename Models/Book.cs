namespace Library.Models
{
    public class Book
    {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public int Year { get; set; }
            public string CoverImage { get; set; }

            public List<Review> Reviews { get; set; }

            public double AverageRating
            {
                get
                {
                    if (Reviews != null && Reviews.Any())
                    {
                        return Reviews.Average(review => review.Rating);
                    }
                    return 0;
                }
            }

            public Book()
            {
                Title = string.Empty;
                Author = string.Empty;
                CoverImage = string.Empty;

                // Inicializar la lista de reseñas
                Reviews = new List<Review>();
            }
        
    }
}
