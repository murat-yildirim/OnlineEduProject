﻿using OnlineEdu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DataAccess.Abstract
{
    public interface ICourseRepository : IRepository<Course>
    {
        List<Course> GetCoursesByTeacherId(int id);
        List<Course> GetAllCoursesWithCategories();
        List<Course> GetAllCoursesWithCategories(Expression<Func<Course,bool>> filter=null);
        void ShowOnHome(int id);
        void DontShowOnHome(int id);


    }
}
