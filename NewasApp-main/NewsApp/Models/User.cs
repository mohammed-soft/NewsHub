using NewsAPI.Models;

namespace NewsApp.Models
{
    public class UserData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Article> SavedArticles { get; set; }
    }
}
