﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.Business.Abstract;
using OnlineEdu.DTO.DTOs.CourseVideoDtos;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Controllers
{
    [Authorize(Roles = "Admin,Teacher,Student")]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseVideosController(IGenericService<CourseVideo> _courseService, IMapper _mapper) : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            var values = _courseService.TGetList();
            return Ok(values);
        }

        [HttpGet("GetCourseVideosByCourseId/{id}")]
        public IActionResult GetCourseVideosByCourseId(int id)
        {
            var values = _courseService.TGetFilteredList(X=>X.CourseId==id);
            return Ok(values);
        }

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
            return Ok("Kurs Video Alanı Silindi");
        }

        [HttpPost]
        public IActionResult Create(CreateCourseVideoDto createCourseVideoDto)
        {
            var newValue = _mapper.Map<CourseVideo>(createCourseVideoDto);
            _courseService.TCreate(newValue);
            return Ok("Yeni Kurs Video Alanı Oluşturuldu");
        }

        [HttpPut]
        public IActionResult Update(UpdateCourseVideoDto updateCourseVideoDto)

        {
            var value = _mapper.Map<CourseVideo>(updateCourseVideoDto);
            _courseService.TUpdate(value);
            return Ok("Kurs Video Alanı Güncellendi");
        }
    }
}
