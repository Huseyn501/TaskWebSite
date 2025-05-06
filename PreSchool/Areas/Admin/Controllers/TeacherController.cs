using Microsoft.AspNetCore.Mvc;
using PreSchool.DAL;
using PreSchool.Helper;

namespace PreSchool.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeacherController : Controller
    {
        AppDbContext _context;
        private readonly IWebHostEnvironment environment;

        public TeacherController(AppDbContext context,IWebHostEnvironment environment)
        {
            _context = context;
            this.environment = environment;
        }
        public IActionResult Index()
        {
            var teachers = _context.Teachers.ToList();
            return View(teachers);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return View(teacher);
            }
            if(!teacher.Image.ContentType.Contains("image"))
            {
                ModelState.AddModelError("Image", "Please select a valid image file.");
                return View(teacher);
            }
            if (teacher.Image.Length > 1048576)
            {
                ModelState.AddModelError("Image", "File size must be less than 1MB.");
                return View(teacher);
            }

            teacher.imageUrl = teacher.Image.CreateFile("Upload", environment.WebRootPath);

            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var teacher = _context.Teachers.FirstOrDefault(x => x.Id == id);
            if (teacher == null) return NotFound();
            string path = Path.Combine(environment.WebRootPath, "Upload", teacher.imageUrl);
            if(System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Teachers.Remove(teacher);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var teacher = _context.Teachers.FirstOrDefault(x => x.Id == id);
            if (teacher == null) return NotFound();
            return View(teacher);
        }
        [HttpPost]
        public IActionResult Update(Teacher teacher)
        {
            var dbTeacher = _context.Teachers.FirstOrDefault(x => x.Id == teacher.Id);
            if (dbTeacher == null) return NotFound();
            if (!ModelState.IsValid)
            {
                return View(teacher);
            }
            if (!teacher.Image.ContentType.Contains("image"))
            {
                ModelState.AddModelError("Image", "Please select a valid image file.");
                return View(teacher);
            }
            if (teacher.Image.Length > 1048576)
            {
                ModelState.AddModelError("Image", "File size must be less than 1MB.");
                return View(teacher);
            }
            dbTeacher.Name = teacher.Name;
            dbTeacher.Position = teacher.Position;
            dbTeacher.imageUrl = teacher.Image.CreateFile("Upload", environment.WebRootPath);
            _context.Teachers.Update(dbTeacher);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
