using NewsAPI.Models;

namespace NewsApp.Models
{
    public class SavedArticles
    {
        public string UserId { get; set; }
        public List<Article> Articles { get; set; } = new List<Article>();
    }
}
