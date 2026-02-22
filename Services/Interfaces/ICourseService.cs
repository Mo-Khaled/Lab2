using Lab2.Models;

namespace Lab2.Services.Interfaces;

public interface ICourseService
{
  IReadOnlyList<Course> GetAll();
  Course? GetById(int id);
  Course Add(Course course);
}