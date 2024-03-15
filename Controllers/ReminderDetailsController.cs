using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("[controller]")]
public class ReminderDetailsController : ControllerBase
{

    ReminderDetailsRepository _repReminderDetails;
    ReminderTypeRepository _remTypeRep;
    DoctorDetailsRepository _docDetailsrep;
     private EmailConfiguration _emailConfig;
    int idvalue=0;
   
    public ReminderDetailsController(ReminderDetailsRepository _repdetailsrep,DoctorDetailsRepository docdetRep,ReminderTypeRepository remType,EmailConfiguration emailConfig)
    {
        this._repReminderDetails = _repdetailsrep;
        this._emailConfig=emailConfig;
        this._remTypeRep=remType;
        this._docDetailsrep=docdetRep;
    }
    // GET: api/Cities
    [HttpGet]
    public async Task<ActionResult> GetReminderDetails()
    {
        return new OkObjectResult(_repReminderDetails.GetRemDetails());
    }
    // GET: api/Cities/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DoctorDetails>> GetReminderDetails(int id)
    {
        return new OkObjectResult(_repReminderDetails.GetRemDetails(id));

    }

    [HttpPost("{id}")]
    public async Task<IActionResult> PutReminderDetail(int id, ReminderDetails remDetails)
    {
        if (id != remDetails.id)
        {
            return BadRequest();
        }

        _repReminderDetails.updateRemDetails(remDetails);
        return new OkObjectResult(remDetails);
    }
    [HttpPost]
    public async Task<ActionResult<DoctorDetails>> PostReminderDetails(ReminderDetails remDetails)
    {

        if (remDetails != null)
        {
            _repReminderDetails.insertRemDetails(remDetails);
        }
        return Ok(remDetails);

    }
    [HttpPost("email")]
     public async Task<IActionResult> PostReminderEmail()
    {
            List<ReminderDetails> remDet=new List<ReminderDetails>();
            remDet=_repReminderDetails.loaddueReminder();
            foreach(ReminderDetails rd in remDet)
            {
                rd.remType=_remTypeRep.GetRemType(rd.ReminderTypeID);
                rd.Doctor=_docDetailsrep.GetDocDetails(rd.Doctorid);
                EmailSender _emailSender = new EmailSender(this._emailConfig);
                Email em = new Email();
                string logourl = "";
                string salutation = "Dear " + "Medical Admin Team" + ",";
                string emailcontent = "Eligibility of: " + rd.Doctor.Designation + " Dr." + rd.Doctor.FirstName + " is due for renewal on "+ rd.DueDate.ToString("dddd, dd MMMM yyyy")+"."+ " Test Email. ";
                string narration1 = " ";
                string econtent = em.HtmlMail("Eligibility Renewal","", salutation, emailcontent, narration1, logourl);
                var message = new Message(new string[] {"Medicaladminteam@evercare.ng","olusola.omotoso@evercare.ng"}, "Doctors Eligibility Renewal", econtent);
                await _emailSender.SendEmailAsync(message);
            }
            
         return Ok();

     }
    
    // DELETE: api/Cities/5
    [HttpPost("deletedocdetails")]
    public async Task<IActionResult> DeleteReminderDetails([FromBody] ReminderDetails reminderDetails)
    {
        idvalue = _repReminderDetails.deleteRemDetails(reminderDetails);
        return Ok(reminderDetails);
    }
   

}