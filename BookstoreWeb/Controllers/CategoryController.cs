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
        [Route("Category/Home")]
        [Route("Category/Index")]
        [Route("Category")]
        [Route("Categories")]
        public IActionResult Index()
        {
            var categoriesViewList = _db.Categories.Select(c => new CategoryOutputDto
            {
                Id = c.Id,
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
            if (_db.Categories.Any(c => c.DisplayOrder.ToString() == categoryInputDto.DisplayOrder))
            {
                ModelState.AddModelError("DisplayOrder", "A category with this display order already exists.");
            }

            if (_db.Categories.Any(c => c.Name == categoryInputDto.Name))
            {
                ModelState.AddModelError("Name", "A category with this name already exists.");
            }

            if (ModelState.IsValid)
            {
                Category categoryInput = new Category()
                {
                    Name = categoryInputDto.Name,
                    DisplayOrder = int.Parse(categoryInputDto.DisplayOrder)
                };

                _db.Categories.Add(categoryInput);
                _db.SaveChanges();
                TempData["success"] = $"Category {categoryInput.Name} added.";

                return RedirectToAction("Index");
            }

            return View(categoryInputDto);
        }

        public IActionResult Edit(int? id)
        {
            var categoryToEdit = _db.Categories.FirstOrDefault(c => c.Id == id);

            if (categoryToEdit == null)
            {
                return NotFound();
            }

            return View(categoryToEdit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category categoryToEdit)
        {

            if (categoryToEdit == null)
            {
                return NotFound();
            }

            if (_db.Categories.Any(c => c.DisplayOrder == categoryToEdit.DisplayOrder))
            {
                ModelState.AddModelError("DisplayOrder", "A category with this display order already exists.");
            }

            if (_db.Categories.Any(c=>c.Name == categoryToEdit.Name))
            {
                ModelState.AddModelError("Name", "A category with that name already exists.");
            }

            if (ModelState.IsValid)
            {
                string oldName = _db.Categories.FirstOrDefault(c=> c.Id == categoryToEdit.Id).Name;

                _db.ChangeTracker.Clear();

                _db.Categories.Update(categoryToEdit);
                _db.SaveChanges();

                TempData["success"] = $"Category {oldName} renamed to {categoryToEdit.Name}.";

                return RedirectToAction("Index");
            }

            return View(categoryToEdit);

        }

        public IActionResult Delete(int? id)
        {
            var categoryToDelete = _db.Categories.FirstOrDefault(c => c.Id == id);

            if (categoryToDelete == null)
            {
                return NotFound();
            }

            return View(categoryToDelete);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category categoryToDelete)
        {
            _db.Categories.Remove(categoryToDelete);
            _db.SaveChanges();

            TempData["success"] = $"Category {categoryToDelete.Name} deleted.";

            return RedirectToAction("Index");
        }
    }
}
