using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductMVC.Models;
using PagedList;
using Microsoft.EntityFrameworkCore;

namespace ProductMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ConnectionDBClass _context;

        public HomeController(ConnectionDBClass context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder, int pageNumber = 1)
        {
            var product = _context.Products.AsQueryable();
            ViewData["IDOrder"] = string.IsNullOrEmpty(sortOrder) ? "productid_desc" : "";
            switch (sortOrder)
            {
                case "productid_desc": product.OrderByDescending(a => a.ProductID);
                    break;
                default:
                    product = product.OrderBy(a => a.ProductID);
                    break;
            }

            //return View(await Paging<ProductClass>.CreateAsync(_context.Products, pageNumber, 20));
            return View(await Paging<ProductClass>.CreateAsync(product.AsNoTracking(), pageNumber, 20));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,ProductName")] ProductClass productClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productClass);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productClass = await _context.Products.FindAsync(id);
            if (productClass == null)
            {
                return NotFound();
            }
            return View(productClass);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,ProductName")] ProductClass productClass)
        {
            if (id != productClass.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductClassExists(productClass.ProductID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productClass);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productClass = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (productClass == null)
            {
                return NotFound();
            }

            return View(productClass);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productClass = await _context.Products.FindAsync(id);
            _context.Products.Remove(productClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductClassExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}
