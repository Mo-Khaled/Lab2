using Lab2.Models;

namespace Lab2.Interfaces;

public interface IStudentService
{
  IEnumerable<Student> GetAllStudents();
  Student? GetStudentById(int id);
  Student AddStudent(Student student);
}
