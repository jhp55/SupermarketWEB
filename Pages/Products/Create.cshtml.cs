using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Products
{
	[Authorize]
	public class CreateModel : PageModel
    {
		private readonly SumpermarketContext _context;

		public CreateModel(SumpermarketContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Product Product { get; set; } = default!;
		public SelectList Categories { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync()
		{
			Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{

			if (!ModelState.IsValid || _context.Products == null || Product == null)
			{
				return Page();
			}

			_context.Products.Add(Product);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}
