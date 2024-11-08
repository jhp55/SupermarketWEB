using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Pay_Mode
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
		public PayMode PayMode { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null || _context.PayModes == null)
			{
				return NotFound();
			}

			var payMode = await _context.PayModes.FirstOrDefaultAsync(m => m.Id == id);
			if (payMode == null)
			{
				return NotFound();
			}

			PayMode = payMode;
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int? id)
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.Attach(PayMode).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!PayModeExists(PayMode.Id))
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

		private bool PayModeExists(int id)
		{
			return (_context.PayModes?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
