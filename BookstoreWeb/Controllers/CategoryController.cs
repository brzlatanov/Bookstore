using BookstoreWeb.Data;
using BookstoreWeb.DTO;
using BookstoreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookstoreWeb.Controllers
{
    public class CategoryController : MainController
    {
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var categoriesViewList = _db.Categories.Select(c=> new CategoryOutputDto
            {
                Name = c.Name,
                DisplayOrder = c.DisplayOrder
            }).ToList();
                
            return View(categoriesViewList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryInputDto categoryInputDto)
        {
            Category categoryInput = new Category()
            {
                Name = categoryInputDto.Name,
                DisplayOrder = int.Parse(categoryInputDto.DisplayOrder)
            };

            _db.Categories.Add(categoryInput);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
