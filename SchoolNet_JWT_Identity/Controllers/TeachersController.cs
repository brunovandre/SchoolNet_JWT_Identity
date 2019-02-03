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
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly SchoolNetContext _context;

        public TeachersController(SchoolNetContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _context.Teachers.ToListAsync();
            return Ok(teachers);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(c => c.Id == id);
            if (teacher == null)
                return NotFound();

            return Ok(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Teacher teacher)
        {
            await _context.Teachers.AddAsync(teacher);

            await _context.SaveChangesAsync();

            return Ok(teacher);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromBody] Teacher teacher, int id)
        {
            if (!await _context.Teachers.AnyAsync(c => c.Id == id)) return NotFound();

            _context.Entry(teacher).State = EntityState.Modified;

            _context.Teachers.Update(teacher);

            await _context.SaveChangesAsync();

            return Ok(teacher);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!await _context.Teachers.AnyAsync(c => c.Id == id)) return NotFound();

            var entity = new Teacher() { Id = id };
            _context.Teachers.Attach(entity);

            _context.Teachers.Remove(entity);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}