
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sprint16.Models;
using Sprint16.Data;

public class SupermarketsController : Controller
{
    private readonly ShoppingContext _context;

    public SupermarketsController(ShoppingContext context)
    {
        _context = context;
    }

    // GET: SUPERMARKETS
    public async Task<IActionResult> Index(int page = 0)   
    {
        ViewBag.Page = page;
        return View(await _context.Supermarkets.ToListAsync());
    }

    // GET: SUPERMARKETS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var supermarket = await _context.Supermarkets
            .FirstOrDefaultAsync(m => m.Id == id);
        if (supermarket == null)
        {
            return NotFound();
        }

        return View(supermarket);
    }

    // GET: SUPERMARKETS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: SUPERMARKETS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Address")] Supermarket supermarket)
    {
        if (ModelState.IsValid)
        {
            _context.Add(supermarket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(supermarket);
    }

    // GET: SUPERMARKETS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var supermarket = await _context.Supermarkets.FindAsync(id);
        if (supermarket == null)
        {
            return NotFound();
        }
        return View(supermarket);
    }

    // POST: SUPERMARKETS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Address")] Supermarket supermarket)
    {
        if (id != supermarket.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(supermarket);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupermarketExists(supermarket.Id))
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
        return View(supermarket);
    }

    // GET: SUPERMARKETS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var supermarket = await _context.Supermarkets
            .FirstOrDefaultAsync(m => m.Id == id);
        if (supermarket == null)
        {
            return NotFound();
        }

        return View(supermarket);
    }

    // POST: SUPERMARKETS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var supermarket = await _context.Supermarkets.FindAsync(id);
        if (supermarket != null)
        {
            _context.Supermarkets.Remove(supermarket);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SupermarketExists(int? id)
    {
        return _context.Supermarkets.Any(e => e.Id == id);
    }
}
