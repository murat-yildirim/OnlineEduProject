﻿using Microsoft.EntityFrameworkCore;
using OnlineEdu.DataAccess.Abstract;
using OnlineEdu.DataAccess.Context;
using OnlineEdu.DataAccess.Repositories;
using OnlineEdu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DataAccess.Concrete
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        public BlogRepository(OnlineEduContext _context) : base(_context)
        {
           
        }

        public List<Blog> GetBlogsWithCategories()
        {
            return _context.Blogs.Include("BlogCategory").ToList();
        }

        public List<Blog> GetBlogsWithCategoriesByWriterId(int id)
        {
           return _context.Blogs.Include(x=>x.BlogCategory).Where(x => x.WriterId == id).ToList();
        }
    }
}
