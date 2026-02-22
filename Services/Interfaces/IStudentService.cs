using Lab2.Models;

namespace Lab2.Services.Interfaces;

public interface IStudentService
{
  IReadOnlyList<Student> GetAll();
  Student? GetById(int id);
  Student Add(Student student);
}