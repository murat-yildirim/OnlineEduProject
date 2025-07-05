using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.Entity.Entities;
using OnlineEdu.WebUI.DTOs.TeacherSocialDtos;
using OnlineEdu.WebUI.DTOs.TeacherSocialDtos;
using OnlineEdu.WebUI.Helpers;
using OnlineEdu.WebUI.Services.TokenServices;

namespace OnlineEdu.WebUI.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    public class MySocialMediaController : Controller
    {
        private readonly HttpClient _client;
        private readonly ITokenService _tokenService;

        public MySocialMediaController(IHttpClientFactory clientFactory, ITokenService tokenService)
        {
            _client = clientFactory.CreateClient("EduClient");
            _tokenService = tokenService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = _tokenService.GetUserId;
            var values = await _client.GetFromJsonAsync<List<ResultTeacherSocialDto>>("teachersocials/byTeacherId/" + userId);
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
            var userId = _tokenService.GetUserId;
            createTeacherSocialDto.TeacherId =  userId;
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
