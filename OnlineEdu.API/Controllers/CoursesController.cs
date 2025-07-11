﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.Business.Abstract;
using OnlineEdu.DTO.DTOs.AboutDtos;
using OnlineEdu.DTO.DTOs.CourseDtos;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Controllers
{
    [Authorize(Roles = "Admin,Teacher,Student")]
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController(ICourseService _courseService, IMapper _mapper) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            var values = _courseService.TGetAllCoursesWithCategories();
            var courses = _mapper.Map<List<ResultCourseDto>>(values);
            return Ok(courses);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _courseService.TGetById(id);
            return Ok(value);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _courseService.TDelete(id);
            return Ok("Kurs Alanı Silindi");
        }

        [HttpPost]
        public IActionResult Create(CreateCourseDto createCourseDto)
        {
            var newValue = _mapper.Map<Course>(createCourseDto);
            _courseService.TCreate(newValue);
            return Ok("Yeni Kurs Alanı Oluşturuldu");
        }

        [HttpPut]
        public IActionResult Update(UpdateCourseDto updateCourseDto)

        {
            var value = _mapper.Map<Course>(updateCourseDto);
            _courseService.TUpdate(value);
            return Ok("Kurs Alanı Güncellendi");
        }

        [HttpGet("ShowOnHome/{id}")]
        public IActionResult ShowOnHome(int id)
        {
            _courseService.TShowOnHome(id);
            return Ok("Kurs Alanı Anasayfada Gösterilecek");
        }

        [HttpGet("DontShowOnHome/{id}")]
        public IActionResult DontShowOnHome(int id)
        {
            _courseService.TDontShowOnHome(id);
            return Ok("Kurs Alanı Anasayfada Gösterilmiyor");
        }

        [AllowAnonymous]
        [HttpGet("GetActiveCourses")]
        public IActionResult GetActiveCourses()
        {
            var values = _courseService.TGetFilteredList(x => x.IsShown == true);
            return Ok(values);
        }


        [HttpGet("GetCoursesByTeacherId/{id}")]
        public IActionResult GetCoursesByTeacherId(int id)
        {
            var values = _courseService.TGetCourseByTeacherId(id);
            var mappedValues = _mapper.Map<List<ResultCourseDto>>(values);
            return Ok(values);
        }

        [AllowAnonymous]
        [HttpGet("GetCourseCount")]
        public IActionResult GetCourseCount()
        {
            var courseCount = _courseService.TCount();
            return Ok(courseCount);
        }

        [AllowAnonymous]
        [HttpGet("GetCoursesByCategoryId/{id}")]
        public IActionResult GetCoursesByCategoryId(int id)
        {
            var values = _courseService.TGetAllCoursesWithCategories(x => x.CourseCategoryId == id);
            return Ok(values);
        }
    }
}
