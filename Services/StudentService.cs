using Lab2.Interfaces;
using Lab2.Models;

namespace Lab2.Services;

public class StudentService : IStudentService
{
  private static readonly List<Student> students = new()
    {
        new Student { Id = 1, Name = "Mohamed", Level = 4 },
        new Student { Id = 2, Name = "Shahd", Level = 3 }
    };

  private static int nextId = 4;

  public IEnumerable<Student> GetAllStudents()
  {
    return students;
  }

  public Student? GetStudentById(int id)
  {
    return students.FirstOrDefault(s => s.Id == id);
  }

  public Student AddStudent(Student student)
  {
    student.Id = nextId++;
    students.Add(student);
    return student;
  }
}