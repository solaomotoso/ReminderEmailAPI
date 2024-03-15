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

public class ReminderTypeRepository: BaseRepository<ReminderType, SqlConnection>
    {
        
        //Setting cstring=new Setting();
        Setting sett=new Setting();
        public ReminderTypeRepository(Setting sett) : base(sett.ConString)
        {
            this.sett=sett;
            DbSettingMapper.Add<SqlConnection>(new SqlServerDbSetting(), true);
            DbHelperMapper.Add<SqlConnection>(new SqlServerDbHelper(), true);
            StatementBuilderMapper.Add<SqlConnection>(new SqlServerStatementBuilder(new SqlServerDbSetting()),true);

        }
        public  void insertReminderType(ReminderType remType)
        {
            //UserRepository usrrepository = new UserRepository(cstring.ConString);
            this.Insert(remType);
        }
        public  void updateReminderType(ReminderType remType)
        {
           
            this.Update(remType);
        }
        public int deleteReminderType(ReminderType remType)
        {
           
            int id = this.Delete<ReminderType>(remType);
            return id;
        }
        public List<ReminderType> GetReminderTypes()
        {  
          
           var bids= new List<ReminderType>();
           using (var connection = new SqlConnection(sett.ConString))
            {
                bids = connection.QueryAll<ReminderType>().ToList();
                /* Do the stuffs for the people here */
            }
          return bids;
        }
        public ReminderType GetRemType(int id)
        {  
          
           var remtype= new ReminderType();
           using (var connection = new SqlConnection(sett.ConString))
            {
                
               remtype = connection.Query<ReminderType>(id).FirstOrDefault();
            }
          return remtype;
        }
        // public Bid GetBidder(string username)
        // {  
          
        //    var bidder= new Bidder();
        //    using (var connection = new SqlConnection(sett.ConString))
        //     {
        //        bidder = connection.Query<Bidder>(e=>e.username==username).FirstOrDefault();
        //     }
        //   return bidder;
        // }
         
    }