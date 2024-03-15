using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("[controller]")]
public class DoctorDetailsController : ControllerBase
{

    DoctorDetailsRepository _repDocDet;
    int idvalue=0;
   
    public DoctorDetailsController(DoctorDetailsRepository _repdocdet)
    {
        this._repDocDet = _repdocdet;
    }
    // GET: api/Cities
    [HttpGet]
    public async Task<ActionResult> GetDoctorDetails()
    {
        return new OkObjectResult(_repDocDet.GetDocDetails());
    }
    // GET: api/Cities/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DoctorDetails>> GetDoctorDetail(int id)
    {
        return new OkObjectResult(_repDocDet.GetDocDetails(id));

    }

    [HttpPost("{id}")]
    public async Task<IActionResult> PutDoctorDetail(int id, DoctorDetails docdetails)
    {
        if (id != docdetails.id)
        {
            return BadRequest();
        }

        _repDocDet.updateDocDetails(docdetails);
        return new OkObjectResult(docdetails);
    }
    [HttpPost]
    public async Task<ActionResult<DoctorDetails>> PostBid(DoctorDetails docdetails)
    {

        if (docdetails != null)
        {
            _repDocDet.insertDoctorDetails(docdetails);
        }
        return Ok(docdetails);

    }
    
    // DELETE: api/Cities/5
    [HttpPost("deletedocdetails")]
    public async Task<IActionResult> DeleteDoctorDetails([FromBody] DoctorDetails doctorDetails)
    {
        idvalue = _repDocDet.deleteDocDetails(doctorDetails);
        return Ok(doctorDetails);
    }
   

}