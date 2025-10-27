using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SH1ProjeUygulamasi.Core.Entities;
using SH1ProjeUygulamasi.Core.Models;
using SH1ProjeUygulamasi.Data;
// jwt oturum açma kütüphaneleri
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace SH1ProjeUygulamasi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private IConfiguration _configuration;

        public AuthController(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST api/<AuthController>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel userLoginModel)
        {
            var account = await _context.Users.FirstOrDefaultAsync(u => u.IsActive && u.Email == userLoginModel.Email && u.Password == userLoginModel.Password);

            if (account is null)
            {
                return NotFound();
            }

            //Security Key'in simetriğini alıyoruz.
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            //Şifrelenmiş kimliği oluşturuyoruz.
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            // Eğer rol bazlı yapacaksak
            var claims = new List<Claim>() // Claim = hak
                        {
                            new(ClaimTypes.Email, account.Email),
                            new(ClaimTypes.Role, account.IsAdmin ? "Admin" : "User"),
                            new("UserId", account.Id.ToString())
                        };

            Token tokenInstance = new();
            tokenInstance.Expiration = DateTime.Now.AddMinutes(15); // token bitiş süresini 15 dk ayarladık

            JwtSecurityToken securityToken = new(
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                expires: DateTime.Now.AddMinutes(10), // token geçerlilik süresi
                notBefore: DateTime.Now,//Token üretildikten ne kadar süre sonra devreye girsin ayarlıyouz.
                signingCredentials: signingCredentials,
                claims: claims // yetkileri
                );

            // Token oluşturucu sınıfında bir örnek alıyoruz.
            JwtSecurityTokenHandler tokenHandler = new();

            //Token üretiyoruz.
            tokenInstance.AccessToken = tokenHandler.WriteToken(securityToken);

            //Refresh Token üretiyoruz.
            tokenInstance.RefreshToken = Guid.NewGuid().ToString();

            //Refresh token Users tablosuna işleniyor.
            account.RefreshToken = tokenInstance.RefreshToken;
            account.RefreshTokenExpireDate = tokenInstance.Expiration.AddMinutes(30);
            _context.Users.Update(account);
            await _context.SaveChangesAsync();

            return Ok(tokenInstance);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
            var account = await _context.Users.FirstOrDefaultAsync(u => u.IsActive && u.Email == user.Email);

            if (account is not null)
            {
                return Conflict(new { errMes = account.Email + " ile daha önce kayıt olunmuş!" }); // kayıtlarda çakışma durumunda kullanılabilen geri dönüş türü.
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpGet("GetUserByUserGuid/{id}"), Authorize]
        public async Task<ActionResult<User>> GetUserByUserGuid(string id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FirstOrDefaultAsync(x => x.RefreshToken == id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
