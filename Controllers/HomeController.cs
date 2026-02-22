using Microsoft.AspNetCore.Mvc;

namespace Lab2.Controllers;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
  [HttpGet]
  public IActionResult Index()
  {
    return Ok(new
    {
      message = "Hello! Welcome to the Lab2 API",
      endpoints = new
      {
        students = "Go to /students to view all students",
        courses = "Go to /courses to view all courses"
      }
    });
  }
}
