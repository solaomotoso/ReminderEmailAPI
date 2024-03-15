using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("[controller]")]
public class TitleMasterController : ControllerBase
{

    TitleMasterRepository _repTitleMaster;
    int idvalue=0;
   
    public TitleMasterController(TitleMasterRepository _reptitlemaster)
    {
        this._repTitleMaster = _reptitlemaster;
    }
    // GET: api/Cities
    [HttpGet]
    public async Task<ActionResult> GetTitleMaster()
    {
        return new OkObjectResult(_repTitleMaster.GetTitMasters());
    }
    // GET: api/Cities/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DoctorDetails>> GetSpecialty(int id)
    {
        return new OkObjectResult(_repTitleMaster.GetTitMaster(id));

    }

    [HttpPost("{id}")]
    public async Task<IActionResult> PutTitleMaster(int id, TitleMaster titlemaster)
    {
        if (id != titlemaster.id)
        {
            return BadRequest();
        }

        _repTitleMaster.updateTitMaster(titlemaster);
        return new OkObjectResult(titlemaster);
    }
    [HttpPost]
    public async Task<ActionResult<TitleMaster>> PostTitleMaster(TitleMaster titlemaster)
    {

        if (titlemaster != null)
        {
            _repTitleMaster.insertTitleMaster(titlemaster);
        }
        return Ok(titlemaster);

    }
    
    // DELETE: api/Cities/5
    [HttpPost("deletetitlemaster")]
    public async Task<IActionResult> deleteTitleMaster([FromBody] TitleMaster titlemaster)
    {
        idvalue = _repTitleMaster.deleteTit(titlemaster);
        return Ok(titlemaster);
    }
   

}