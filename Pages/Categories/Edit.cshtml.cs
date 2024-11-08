using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Categories
{
	[Authorize]
	public class EditModel : PageModel
    {
		private readonly SumpermarketContext _context;

		public EditModel(SumpermarketContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Category Category { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null || _context.Categories == null)
			{
				return NotFound();
			}

			var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
			if (category == null)
			{
				return NotFound();
			}
			
			Category = category;
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int? id)
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.Attach(Category).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException) 
			{
				if (!CategoryExists(Category.Id)) 
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
			return RedirectToPage("./Index");
		}

		private bool CategoryExists(int id)
		{
			return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
