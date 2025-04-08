using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAliona.Data;
using WebAliona.Models;


namespace WebAliona.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppAlionaContext _context;
        private readonly DataBaseManager _dataBaseManager;

        public HomeController(ILogger<HomeController> logger,
            AppAlionaContext context)
        {
            _logger = logger;
            _context = context;
            _dataBaseManager = new DataBaseManager();
        }

        public async Task<IActionResult> Index()
        {
            
            
            
            if (!_context.News.ToList().Any())
            {
                await _dataBaseManager.AddNewsAsync(_context, 20);
            }
            
            return View(await _context.News.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string title, string slug, string summary, string content, IFormFile photo)
        {
            string photo_name="";
            if (photo != null && photo.Length > 0)
            {
                var fileName = Path.GetFileName(photo.FileName);
                var path = Path.Combine("~wwwroot/img/", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }
                
                photo_name = fileName;
            }

            New n = new New()
            {
                title = title,
                slug = slug,
                summary = summary,
                content = content,
                photo = photo_name
            };
            
            await _context.News.AddAsync(n);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}