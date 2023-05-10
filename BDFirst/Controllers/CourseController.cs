using BDFirst.Context;
using BDFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BDFirst.Controllers
{
    public class CourseController : Controller
    {
        private readonly Student_Course_UniversityContext _context;
        public CourseController(Student_Course_UniversityContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            return View();
        }
        public async Task<bool> SaveCourse(string enroll, string courseType)
        {
            int Enroll = int.Parse(enroll);
            try
            {
                // find enroll in sc relation table
                SCRelation _SCRelation = await _context.SCRelations.FirstOrDefaultAsync(m => m.FEnrollId == Enroll);
                if (_SCRelation != null)
                {
                    // put data in course type and same _SCRelation.FCourseId in course.CourseId
                    Course course = new Course();
                    course.CourseName = courseType;
                    course.CourseId = _SCRelation.Id;
                    _context.Courses.Add(course);
                    _context.SaveChanges();

                    // put random id in sc relation table where enroll is present
                    _SCRelation.FCourseId = course.CourseId;
                    _context.SaveChanges();

                }
            }
            catch (Exception ex) {
                var er = ex.Message;
            }
            return true;
        }
    }
}
