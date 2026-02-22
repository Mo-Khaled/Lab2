using Lab2.Interfaces;
using Lab2.Models;

namespace Lab2.Services;

public class CourseService : ICourseService
{
  private static readonly List<Course> courses = new()
    {
        new Course { Id = 1, Title = "Web Engineering", CreditHours = 3 },
        new Course { Id = 2, Title = "Operating Systems", CreditHours = 3 },
        new Course { Id = 3, Title = "Computer Networks", CreditHours = 3 }
    };

  private static int nextId = 4;

  public IEnumerable<Course> GetAllCourses()
  {
    return courses;
  }

  public Course? GetCourseById(int id)
  {
    return courses.FirstOrDefault(c => c.Id == id);
  }

  public Course AddCourse(Course course)
  {
    course.Id = nextId++;
    courses.Add(course);
    return course;
  }
}