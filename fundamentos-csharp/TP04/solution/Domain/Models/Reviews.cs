namespace Domain.Models
{
    public class Reviews
    {
        public Reviews(string review, int score, int movieId)
        {
            Review = review;
            Score = score;
            MovieId = movieId;

        }
        public int Id { get; set; }
        public string Review { get; set; }
        public int Score { get; set; }
        public int MovieId { get; set; }
    }
}
