using Microsoft.AspNetCore.Mvc;
using Lab2.Interfaces;
using Lab2.Models;

namespace Lab2.Controllers;

[Route("courses")]
[ApiController]
public class CourseController : ControllerBase
{
  private readonly ICourseService courseService;

  public CourseController(ICourseService courseService)
  {
    this.courseService = courseService;
  }

  // 1) GET /courses
  [HttpGet]
  public IActionResult GetAllCourses()
  {
    var courses = courseService.GetAllCourses();
    return Ok(courses);
  }

  // 2) GET /courses/5
  [HttpGet("{id}")]
  public IActionResult GetCourseById(int id)
  {
    var course = courseService.GetCourseById(id);
    if (course == null) return NotFound();
    return Ok(course);
  }

  // 3) POST /courses
  [HttpPost]
  public IActionResult AddCourse([FromBody] Course course)
  {
    var created = courseService.AddCourse(course);
    return Ok(created);
  }
}
