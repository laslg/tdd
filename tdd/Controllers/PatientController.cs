using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TDD;
using TDD.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace tdd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : Controller
    {
        private readonly DataContext context;

        public PatientController(DataContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddPatientAsync([FromBody] Patient patient)
        {
            var existing = context.Patients.FirstOrDefault(p => p.PhoneNumber == patient.PhoneNumber);
            if (existing != null)
            {
                return BadRequest();
            }

            context.Patients.Add(patient);
            await context.SaveChangesAsync();

            return Created($"/patient/{patient.Id}", patient);
        }
    }
}
