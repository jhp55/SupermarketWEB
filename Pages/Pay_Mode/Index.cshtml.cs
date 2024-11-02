using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Pay_Mode
{
    public class IndexModel : PageModel
    {
        private readonly SumpermarketContext _context;

        public IndexModel(SumpermarketContext context)
        {
            _context = context;
        }

        public IList<PayMode> PayModes { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.PayModes != null)
            {
                PayModes = await _context.PayModes.ToListAsync();
            }
        }
    }
}
