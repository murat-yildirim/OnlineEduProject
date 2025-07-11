﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineEdu.WebUI.DTOs.CourseDtos;
using OnlineEdu.WebUI.DTOs.CourseRegisterDtos;
using OnlineEdu.WebUI.DTOs.CourseVideoDtos;
using OnlineEdu.WebUI.Services.TokenServices;

namespace OnlineEdu.WebUI.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
    [Area("Student")]
    public class CourseRegisterController : Controller
    {
        private readonly HttpClient _client;
        private readonly ITokenService _tokenService;

        public CourseRegisterController(IHttpClientFactory clientFactory, ITokenService tokenService)
        {
            _client = clientFactory.CreateClient("EduClient");
            _tokenService = tokenService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = _tokenService.GetUserId;

            var values = await _client.GetFromJsonAsync<List<ResultCourseRegisterDto>>("courseRegisters/GetMyCourses/" + userId);

            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> RegisterCourse()
        {
            var courseList = await _client.GetFromJsonAsync<List<ResultCourseDto>>("courses");

            List<SelectListItem> courses = (from x in courseList
                                            select new SelectListItem
                                            {
                                                Text = x.CourseName,
                                                Value = x.CourseId.ToString()
                                            }).ToList();
            ViewBag.courses = courses;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCourse(CreateCourseRegisterDto model)
        {
            var courseList = await _client.GetFromJsonAsync<List<ResultCourseDto>>("courses");

            List<SelectListItem> courses = (from x in courseList
                                            select new SelectListItem
                                            {
                                                Text = x.CourseName,
                                                Value = x.CourseId.ToString()
                                            }).ToList();
            ViewBag.courses = courses;
            var userId = _tokenService.GetUserId;
            model.AppUserId = userId;

            var result = await _client.PostAsJsonAsync("courseRegisters", model);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> CourseVideos(int id)
        {
            var values = await _client.GetFromJsonAsync<List<ResultCourseVideoDto>>("courseVideos/GetCourseVideosByCourseId/" + id);

            ViewBag.courseName = values.Select(x => x.Course.CourseName).FirstOrDefault();
            return View(values);

        }
    }
}
