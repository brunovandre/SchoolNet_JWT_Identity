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
    public class CoursesController : ControllerBase
    {
        private readonly SchoolNetContext _context;

        public CoursesController(SchoolNetContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _context.Courses.ToListAsync();
            return Ok(courses);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if (course == null)
                return NotFound();

            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Course course)
        {
            await _context.Courses.AddAsync(course);

            await _context.SaveChangesAsync();

            return Ok(course);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromBody] Course course, int id)
        {
            if (!await _context.Courses.AnyAsync(c => c.Id == id)) return NotFound();

            _context.Entry(course).State = EntityState.Modified;

            _context.Courses.Update(course);

            await _context.SaveChangesAsync();

            return Ok(course);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!await _context.Courses.AnyAsync(c => c.Id == id)) return NotFound();

            var entity = new Course() { Id = id };
            _context.Courses.Attach(entity);

            _context.Courses.Remove(entity);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}