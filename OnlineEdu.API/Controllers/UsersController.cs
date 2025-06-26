using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.Business.Abstract;
using OnlineEdu.DTO.DTOs.UserDtos;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, IJwtService _jwtService) : ControllerBase
    {

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest("Bu Email Sistemde Kayıtlı Değil");
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if(!result.Succeeded)
            {
                return BadRequest("Kullanıcı Adı veya Şifre Hatalı");
            }

            var token = await _jwtService.CreateTokenAsync(user);
            return Ok(token);
        }
    }
}
