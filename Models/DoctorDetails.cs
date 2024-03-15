using System;
using System.Text;
using MimeKit.Cryptography;


public class DoctorDetails
{
    public int id { get; set; }
    public int Departmentid { get; set; }
    public Department department { get; set; }
    public Specialty specialty { get; set; }
    public int Specialtyid { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Designation { get; set; }
    public int TitleId { get; set; }
    public string MobileNo { get; set; }
    public string Email { get; set; }
    public int EmployeeId { get; set; }
    public bool Active {get;set;}

}
