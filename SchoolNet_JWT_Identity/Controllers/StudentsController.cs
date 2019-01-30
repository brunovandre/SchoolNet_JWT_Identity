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
    public class StudentsController : ControllerBase
    {
        private readonly SchoolNetContext _context;

        public StudentsController(SchoolNetContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(students);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(c => c.Id == id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            await _context.Students.AddAsync(student);

            await _context.SaveChangesAsync();

            return Ok(student);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromBody] Student student, int id)
        {
            if (!await _context.Students.AnyAsync(c => c.Id == id)) return NotFound();

            _context.Entry(student).State = EntityState.Modified;

            _context.Students.Update(student);

            await _context.SaveChangesAsync();

            return Ok(student);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!await _context.Students.AnyAsync(c => c.Id == id)) return NotFound();

            var entity = new Student() { Id = id };
            _context.Students.Attach(entity);

            _context.Students.Remove(entity);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}