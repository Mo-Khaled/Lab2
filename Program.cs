using Lab2.Interfaces;
using Lab2.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseService, CourseService>();

var app = builder.Build();

app.MapControllers();

app.Run();
