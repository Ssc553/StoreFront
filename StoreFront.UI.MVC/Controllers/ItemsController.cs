using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;
using Microsoft.AspNetCore.Authorization;
using System.Drawing;//added for file upload

//using X.PagedList;

namespace StoreFront.UI.MVC.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ScottsStoreContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ItemsController(ScottsStoreContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            var scottsStoreContext = _context.Items.Include(i => i.ItemCategory).Include(i => i.ItemType).Include(i => i.Server);
            return View(await scottsStoreContext.ToListAsync());


        }
		public async Task<IActionResult> TiledIndex()
		{
			var scottsStoreContext = _context.Items.Include(i => i.ItemCategory).Include(i => i.ItemType).Include(i => i.Server);
			return View(await scottsStoreContext.ToListAsync());
		}

		// GET: Items/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.ItemCategory)
                .Include(i => i.ItemType)
                .Include(i => i.Server)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["ItemCategoryId"] = new SelectList(_context.Categories, "ItemCategoryId", "CategoryName");
            ViewData["ItemTypeId"] = new SelectList(_context.ItemTypes, "ItemTypeId", "ItemTypeName");
            ViewData["ServerId"] = new SelectList(_context.ServerModes, "ServerId", "ModeName");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemTypeId,Name,RequiredLevel,MaxStackSize,IsUnique,IsSetItem,IsSocketed,Rarity,ItemCategoryId,ServerId,ItemsInStock,Price,ItemImage")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemCategoryId"] = new SelectList(_context.Categories, "ItemCategoryId", "CategoryName", item.ItemCategoryId);
            ViewData["ItemTypeId"] = new SelectList(_context.ItemTypes, "ItemTypeId", "ItemTypeName", item.ItemTypeId);
            ViewData["ServerId"] = new SelectList(_context.ServerModes, "ServerId", "ModeName", item.ServerId);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["ItemCategoryId"] = new SelectList(_context.Categories, "ItemCategoryId", "CategoryName", item.ItemCategoryId);
            ViewData["ItemTypeId"] = new SelectList(_context.ItemTypes, "ItemTypeId", "ItemTypeName", item.ItemTypeId);
            ViewData["ServerId"] = new SelectList(_context.ServerModes, "ServerId", "ModeName", item.ServerId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemTypeId,Name,RequiredLevel,MaxStackSize,IsUnique,IsSetItem,IsSocketed,Rarity,ItemCategoryId,ServerId,ItemsInStock,Price")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
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
            ViewData["ItemCategoryId"] = new SelectList(_context.Categories, "ItemCategoryId", "CategoryName", item.ItemCategoryId);
            ViewData["ItemTypeId"] = new SelectList(_context.ItemTypes, "ItemTypeId", "ItemTypeName", item.ItemTypeId);
            ViewData["ServerId"] = new SelectList(_context.ServerModes, "ServerId", "ModeName", item.ServerId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.ItemCategory)
                .Include(i => i.ItemType)
                .Include(i => i.Server)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'ScottsStoreContext.Items'  is null.");
            }
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
          return (_context.Items?.Any(e => e.ItemId == id)).GetValueOrDefault();
        }
    }
}
