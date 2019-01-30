using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolNet_JWT_Identity.Context;
using SchoolNet_JWT_Identity.Entities;

namespace SchoolNet_JWT_Identity.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentClassController : ControllerBase
    {
        private readonly SchoolNetContext _context;

        public StudentClassController(SchoolNetContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _context.StudentClasses.Include(sc => sc.Course)
                                                        .Include(sc => sc.Student)
                                                        .Include(sc => sc.Teacher)
                                                        .ToListAsync();
            return Ok(courses);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var course = await _context.StudentClasses.Include(sc => sc.Course)
                                                      .Include(sc => sc.Student)
                                                      .Include(sc => sc.Teacher)
                                                      .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                return NotFound();

            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentClass studentClass)
        {
            await _context.StudentClasses.AddAsync(studentClass);

            await _context.SaveChangesAsync();

            return Ok(studentClass);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!await _context.StudentClasses.AnyAsync(c => c.Id == id)) return NotFound();

            var entity = new StudentClass() { Id = id };
            _context.StudentClasses.Attach(entity);

            _context.StudentClasses.Remove(entity);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}