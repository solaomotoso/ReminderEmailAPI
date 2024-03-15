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

public class ReminderDetailsRepository: BaseRepository<ReminderDetails, SqlConnection>
    {
        
        //Setting cstring=new Setting();
        Setting sett=new Setting();
        
        public ReminderDetailsRepository(Setting sett) : base(sett.ConString)
        {
            this.sett=sett;
            DbSettingMapper.Add<SqlConnection>(new SqlServerDbSetting(), true);
            DbHelperMapper.Add<SqlConnection>(new SqlServerDbHelper(), true);
            StatementBuilderMapper.Add<SqlConnection>(new SqlServerStatementBuilder(new SqlServerDbSetting()),true);


        }
        public  void insertRemDetails(ReminderDetails remDetails)
        {
            //UserRepository usrrepository = new UserRepository(cstring.ConString);
            this.Insert(remDetails);
        }

        public List<ReminderDetails> loaddueReminder()
        {
            List<ReminderDetails>dueReminder=new List<ReminderDetails>();
            using (var connection = new SqlConnection(sett.ConString))
            {
                dueReminder = connection.ExecuteQuery<ReminderDetails>("[dbo].[usp_getDueReminder]",
                commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            return dueReminder;
        }
        public  void updateRemDetails(ReminderDetails remDetails)
        {
           
            this.Update(remDetails);
        }
        public int deleteRemDetails(ReminderDetails remDetails)
        {
           
            int id = this.Delete<ReminderDetails>(remDetails);
            return id;
        }
        public List<ReminderDetails> GetRemDetails()
        {  
          
           var remDetails= new List<ReminderDetails>();
           using (var connection = new SqlConnection(sett.ConString))
            {
                remDetails = connection.QueryAll<ReminderDetails>().ToList();
                /* Do the stuffs for the people here */
            }
          return remDetails;
        }
        public ReminderDetails GetRemDetails(int id)
        {  
          
           var remDetails= new ReminderDetails();
           using (var connection = new SqlConnection(sett.ConString))
            {
                
               remDetails = connection.Query<ReminderDetails>(id).FirstOrDefault();
            }
          return remDetails;
        }
       
         
    }