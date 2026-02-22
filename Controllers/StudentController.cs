using Microsoft.AspNetCore.Mvc;
using Lab2.Interfaces;
using Lab2.Models;

namespace Lab2.Controllers;

[Route("students")]
[ApiController]
public class StudentController : ControllerBase
{
  private readonly IStudentService studentService;

  public StudentController(IStudentService studentService)
  {
    this.studentService = studentService;
  }

  // 1) GET /students
  [HttpGet]
  public IActionResult GetAllStudents()
  {
    var students = studentService.GetAllStudents();
    return Ok(students);
  }

  // 2) GET /students/5
  [HttpGet("{id}")]
  public IActionResult GetStudentById(int id)
  {
    var student = studentService.GetStudentById(id);
    if (student == null) return NotFound();
    return Ok(student);
  }

  // 3) POST /students
  [HttpPost]
  public IActionResult AddStudent([FromBody] Student student)
  {
    var created = studentService.AddStudent(student);
    return Ok(created);
  }
}
