using OnlineEdu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.Business.Abstract
{
    public interface ICourseService: IGenericService<Course>
    {
        List<Course> TGetCourseByTeacherId(int id);

        List<Course> TGetAllCoursesWithCategories(Expression<Func<Course, bool>> filter = null);

        List<Course> TGetAllCoursesWithCategories();

        void TShowOnHome(int id);

        void TDontShowOnHome(int id);
    }
}
