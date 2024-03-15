using System;
using System.Text;


public class ReminderDetails
{
    public int id { get; set; }
    public int Doctorid { get; set; }
    public DoctorDetails Doctor { get; set; }=new DoctorDetails();
    public int Active { get; set; }
    public DateTime DueDate { get; set; }
    public int ReminderTypeID { get; set; }
    public ReminderType remType{get;set;}=new ReminderType();
    public DateTime DateCreated { get; set; }

    

}
