﻿using OnlineEdu.WebUI.DTOs.BlogCategoryDtos;
using OnlineEdu.WebUI.DTOs.UserDtos;

namespace OnlineEdu.WebUI.DTOs.BlogDtos
{
    public class ResultBlogDto
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime BlogDate { get; set; }
        public int BlogCategoryId { get; set; }

        public ResultBlogCategoryDto BlogCategory { get; set; }
        public int WriterId { get; set; }

        public ResultUserDto Writer { get; set; }

    }
}
