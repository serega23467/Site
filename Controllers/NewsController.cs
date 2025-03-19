using Microsoft.AspNetCore.Mvc;
using Site.Models;
using Site.wwwroot.db;
using static System.Net.Mime.MediaTypeNames;
namespace Site.Controllers
{
    public class NewsController : Controller
    {
        public WeatherService ws { get; set; }
        public NewsController()
        {
            ws = new WeatherService();
        }
        public IActionResult Index(string category)
        {
            List<NewsItemModel> newsItems;
            List<CategoryModel> categories;
            using (ApplicationContext db = new ApplicationContext())
            {
                categories = db.Categories.ToList();
                newsItems = db.News.ToList();
            }
            if(category == "Все" || string.IsNullOrEmpty(category))
            {
                return View(new NewsPageModel { NewsItems = newsItems, Categories = categories });
            }
            else if (int.TryParse(category, out int categoryId))
            {
                var filteredNews = newsItems.FindAll(n => n.Category == categoryId);
                return View(new NewsPageModel { NewsItems = filteredNews, Categories = categories });
            }
            else
            {
                return View();
            }
        }
        public IActionResult Details(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var newsItem = db.News.FirstOrDefault(n=>n.Id == id);

                if (newsItem == null)
                {
                    return NotFound();
                }
                return View(newsItem);
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Weather(string city = "Москва")
        {
            string prova = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid=120229cbb056acf4d2737648db0d9866&units=metric";
            WeatherData results = ws.GetWeatherData(prova).Result;
            results.Title = city;
            return View(results);
        }
        [HttpPost]
        public IActionResult WeatherPost(string city)
        {
            string prova = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid=120229cbb056acf4d2737648db0d9866&units=metric";
            WeatherData results = ws.GetWeatherData(prova).Result;
            if(results==null)
            {
                return View("Weather", null);
            }
            results.Title = city;
            return View("Weather",results);
        }
    }
}
