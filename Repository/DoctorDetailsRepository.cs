using RepoDb;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.Extensions.Configuration;
using RepoDb.DbHelpers;
using RepoDb.DbSettings;
using RepoDb.Enumerations;
using RepoDb.StatementBuilders;

public class DoctorDetailsRepository: BaseRepository<DoctorDetails, SqlConnection>
    {
        
        //Setting cstring=new Setting();
        Setting sett=new Setting();
        public DoctorDetailsRepository(Setting sett) : base(sett.ConString)
        {
            this.sett=sett;
            DbSettingMapper.Add<SqlConnection>(new SqlServerDbSetting(), true);
            DbHelperMapper.Add<SqlConnection>(new SqlServerDbHelper(), true);
            StatementBuilderMapper.Add<SqlConnection>(new SqlServerStatementBuilder(new SqlServerDbSetting()),true);

        }
        public  void insertDoctorDetails(DoctorDetails docDetails)
        {
            //UserRepository usrrepository = new UserRepository(cstring.ConString);
            this.Insert(docDetails);
        }
        public  void updateDocDetails(DoctorDetails docDetails)
        {
           
            this.Update(docDetails);
        }
        public int deleteDocDetails(DoctorDetails docDetails)
        {
           
            int id = this.Delete<DoctorDetails>(docDetails);
            return id;
        }
        public List<DoctorDetails> GetDocDetails()
        {  
           var docDetails= new List<DoctorDetails>();
           using (var connection = new SqlConnection(sett.ConString))
            {
                docDetails = connection.ExecuteQuery<DoctorDetails>("[dbo].[usp_getDoctorsDetails]",
                commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
          return docDetails;
        }
        public DoctorDetails GetDocDetails(int id)
        {  
          
           var docDetails= new DoctorDetails();
           using (var connection = new SqlConnection(sett.ConString))
            {
                
               docDetails = connection.Query<DoctorDetails>(id).FirstOrDefault();
            }
          return docDetails;
        }
       
         
    }