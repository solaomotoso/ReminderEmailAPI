using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("[controller]")]
public class ReminderTypeController : ControllerBase
{

    ReminderTypeRepository _repReminderType;
    int idvalue=0;
   
    public ReminderTypeController(ReminderTypeRepository _repreminderType)
    {
        this._repReminderType = _repreminderType;
    }
    // GET: api/Cities
    [HttpGet]
    public async Task<ActionResult> GetReminderType()
    {
        return new OkObjectResult(_repReminderType.GetReminderTypes());
    }
    // GET: api/Cities/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DoctorDetails>> GetReminderTypes(int id)
    {
        return new OkObjectResult(_repReminderType.GetRemType(id));

    }

    [HttpPost("{id}")]
    public async Task<IActionResult> PutReminderType(int id, ReminderType remType)
    {
        if (id != remType.id)
        {
            return BadRequest();
        }

        _repReminderType.updateReminderType(remType);
        return new OkObjectResult(remType);
    }
    [HttpPost]
    public async Task<ActionResult<DoctorDetails>> PostReminderType(ReminderType remType)
    {

        if (remType != null)
        {
            _repReminderType.insertReminderType(remType);
        }
        return Ok(remType);

    }
    
    // DELETE: api/Cities/5
    [HttpPost("deletedocdetails")]
    public async Task<IActionResult> deleteReminderType([FromBody] ReminderType reminderType)
    {
        idvalue = _repReminderType.deleteReminderType(reminderType);
        return Ok(reminderType);
    }
   

}