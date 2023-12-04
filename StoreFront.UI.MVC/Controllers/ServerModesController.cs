using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;

namespace StoreFront.UI.MVC.Controllers
{
    public class ServerModesController : Controller
    {
        private readonly ScottsStoreContext _context;

        public ServerModesController(ScottsStoreContext context)
        {
            _context = context;
        }

        // GET: ServerModes
        public async Task<IActionResult> Index()
        {
              return _context.ServerModes != null ? 
                          View(await _context.ServerModes.ToListAsync()) :
                          Problem("Entity set 'ScottsStoreContext.ServerModes'  is null.");
        }

        // GET: ServerModes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ServerModes == null)
            {
                return NotFound();
            }

            var serverMode = await _context.ServerModes
                .FirstOrDefaultAsync(m => m.ServerId == id);
            if (serverMode == null)
            {
                return NotFound();
            }

            return View(serverMode);
        }

        // GET: ServerModes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServerModes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServerId,ServerName,ModeName")] ServerMode serverMode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serverMode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serverMode);
        }

        // GET: ServerModes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ServerModes == null)
            {
                return NotFound();
            }

            var serverMode = await _context.ServerModes.FindAsync(id);
            if (serverMode == null)
            {
                return NotFound();
            }
            return View(serverMode);
        }

        // POST: ServerModes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServerId,ServerName,ModeName")] ServerMode serverMode)
        {
            if (id != serverMode.ServerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serverMode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServerModeExists(serverMode.ServerId))
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
            return View(serverMode);
        }

        // GET: ServerModes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ServerModes == null)
            {
                return NotFound();
            }

            var serverMode = await _context.ServerModes
                .FirstOrDefaultAsync(m => m.ServerId == id);
            if (serverMode == null)
            {
                return NotFound();
            }

            return View(serverMode);
        }

        // POST: ServerModes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ServerModes == null)
            {
                return Problem("Entity set 'ScottsStoreContext.ServerModes'  is null.");
            }
            var serverMode = await _context.ServerModes.FindAsync(id);
            if (serverMode != null)
            {
                _context.ServerModes.Remove(serverMode);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServerModeExists(int id)
        {
          return (_context.ServerModes?.Any(e => e.ServerId == id)).GetValueOrDefault();
        }
    }
}
