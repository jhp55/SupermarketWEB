using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; // Aseg�rate de tener esta l�nea
using SupermarketWEB.Data;
using SupermarketWEB.Models;
using System.Security.Claims;

namespace SupermarketWEB.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SumpermarketContext _context;

        public LoginModel(SumpermarketContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // Aseg�rate de que la colecci�n de usuarios est� bien nombrada como 'Users'
            var user = await _context.User
                .FirstOrDefaultAsync(u => u.Email == User.Email && u.Password == User.Password);

            if (user != null)
            {
                // Usuario encontrado, crea los claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Email, user.Email),
                };

                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                // Autentica al usuario con una cookie
                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
                return RedirectToPage("/Index");
            }

            // Si no se encuentran coincidencias en la base de datos, muestra un mensaje de error
            ModelState.AddModelError(string.Empty, "Credenciales inv�lidas.");
            return Page();
        }
    }
}
