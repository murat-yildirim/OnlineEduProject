﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineEdu.Entity.Entities;
using OnlineEdu.WebUI.DTOs.BlogCategoryDtos;
using OnlineEdu.WebUI.DTOs.BlogDtos;
using OnlineEdu.WebUI.Helpers;

namespace OnlineEdu.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly HttpClient _client = HttpClientInstance.CreateClient();

        private readonly UserManager<AppUser> _userManager;
        public async Task CategoryDropdown()
        {
            var categoryList = await _client.GetFromJsonAsync<List<ResultBlogCategoryDto>>("blogcategories");

            List<SelectListItem> categories = (from x in categoryList
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.BlogCategoryId.ToString()
                                               }).ToList();
            ViewBag.categories = categories;
        }


        public async Task<IActionResult> Index()
        {
            var values = await _client.GetFromJsonAsync<List<ResultBlogDto>>("blogs");
            return View(values);
        }

        public async Task<IActionResult> DeleteBlog(int id)
        {
            await _client.DeleteAsync($"blogs/"+ id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CreateBlog()
        {
            await CategoryDropdown();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(CreateBlogDto createBlogDto)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            createBlogDto.WriterId = user.Id;
            await _client.PostAsJsonAsync("blogs", createBlogDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBlog(int id)
        {
            var values = await _client.GetFromJsonAsync<UpdateBlogDto>($"blogs/" + id);
            await CategoryDropdown();
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBlog(UpdateBlogDto updateBlogDto)
        {
            await _client.PutAsJsonAsync("blogs", updateBlogDto);
            return RedirectToAction(nameof(Index));
        }

    }
}
