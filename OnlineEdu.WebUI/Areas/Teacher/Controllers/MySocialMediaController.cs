using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.Entity.Entities;
using OnlineEdu.WebUI.DTOs.TeacherSocialDtos;
using OnlineEdu.WebUI.DTOs.TeacherSocialDtos;
using OnlineEdu.WebUI.Helpers;

namespace OnlineEdu.WebUI.Areas.Teacher.Controllers
{
    public class MyTeacherSocialController(UserManager<AppUser> _userManager) : Controller
    {
        private readonly HttpClient _client = HttpClientInstance.CreateClient();
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = await _client.GetFromJsonAsync<List<ResultTeacherSocialDto>>("teachersocials/byTeacherId/" + user.Id);
            return View(values);
        }

        public async Task<IActionResult> DeleteTeacherSocial(int id)
        {
            await _client.DeleteAsync($"teachersocials/" + id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult CreateTeacherSocial()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacherSocial(CreateTeacherSocialDto createTeacherSocialDto)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            createTeacherSocialDto.TeacherId = user.Id;
            await _client.PostAsJsonAsync("teachersocials", createTeacherSocialDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTeacherSocial(int id)
        {
            var values = await _client.GetFromJsonAsync<UpdateTeacherSocialDto>($"teachersocials/" + id);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTeacherSocial(UpdateTeacherSocialDto updateTeacherSocialDto)
        {
            
            await _client.PutAsJsonAsync("teachersocials", updateTeacherSocialDto);
            return RedirectToAction(nameof(Index));
        }
    }
}
