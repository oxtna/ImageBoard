using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestWebApp.Data;
using TestWebApp.Models;

namespace TestWebApp.Controllers
{
    public class ImagesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly string _imgDir;

        public ImagesController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
            _imgDir = "img";
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Images.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("File")] Image image)
        {
            if (image.File != null)
            {
                // This loop should NEVER take a noticable amount of time.
                do
                {
                    image.Path = Path.Combine(_imgDir, Guid.NewGuid().ToString("N")) + Path.GetExtension(image.File.FileName);
                } while (System.IO.File.Exists(Path.Combine(_env.WebRootPath, image.Path)));

                using (var filestream = new FileStream(Path.Combine(_env.WebRootPath, image.Path), FileMode.CreateNew, FileAccess.Write))
                {
                    await image.File.CopyToAsync(filestream);
                    filestream.Flush();
                    _context.Add(image);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(image);
        }
    }
}
