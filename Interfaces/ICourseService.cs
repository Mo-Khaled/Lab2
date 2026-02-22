using Lab2.Models;

namespace Lab2.Interfaces;

public interface ICourseService
{
  IEnumerable<Course> GetAllCourses();
  Course? GetCourseById(int id);
  Course AddCourse(Course course);
}
