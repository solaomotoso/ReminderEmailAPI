using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("[controller]")]
public class SpecialtyController : ControllerBase
{

    SpecialtyRepository _repSpecialty;
    int idvalue=0;
   
    public SpecialtyController(SpecialtyRepository _repSpecialty)
    {
        this._repSpecialty = _repSpecialty;
    }
    // GET: api/Cities
    [HttpGet]
    public async Task<ActionResult> GetSpecialty()
    {
        return new OkObjectResult(_repSpecialty.GetSpecialties());
    }
    // GET: api/Cities/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DoctorDetails>> GetSpecialty(int id)
    {
        return new OkObjectResult(_repSpecialty.GetSpecialty(id));

    }

    [HttpPost("{id}")]
    public async Task<IActionResult> PutSpecialty(int id, Specialty spec)
    {
        if (id != spec.id)
        {
            return BadRequest();
        }

        _repSpecialty.updateSpecialty(spec);
        return new OkObjectResult(spec);
    }
    [HttpPost]
    public async Task<ActionResult<Specialty>> PostReminderType(Specialty spec)
    {

        if (spec != null)
        {
            _repSpecialty.insertSpecialty(spec);
        }
        return Ok(spec);

    }
    
    // DELETE: api/Cities/5
    [HttpPost("deletedocdetails")]
    public async Task<IActionResult> deleteSpecialty([FromBody] Specialty spec)
    {
        idvalue = _repSpecialty.deleteSpecialty(spec);
        return Ok(spec);
    }
   

}