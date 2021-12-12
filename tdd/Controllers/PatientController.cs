using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TDD.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace tdd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : Controller
    {
        [HttpPost]
        public IActionResult AddPatient([FromBody] Patient Patient)
        {
            // TODO: Insert the patient into db
            return Created("/patient/1", Patient);
        }
    }
}
