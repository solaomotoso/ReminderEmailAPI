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

public class SpecialtyRepository: BaseRepository<Specialty, SqlConnection>
    {
        
        //Setting cstring=new Setting();
        Setting sett=new Setting();
        public SpecialtyRepository(Setting sett) : base(sett.ConString)
        {
            this.sett=sett;
            DbSettingMapper.Add<SqlConnection>(new SqlServerDbSetting(), true);
            DbHelperMapper.Add<SqlConnection>(new SqlServerDbHelper(), true);
            StatementBuilderMapper.Add<SqlConnection>(new SqlServerStatementBuilder(new SqlServerDbSetting()),true);

        }
        public  void insertSpecialty(Specialty special)
        {
            //UserRepository usrrepository = new UserRepository(cstring.ConString);
            this.Insert(special);
        }
        public  void updateSpecialty(Specialty special)
        {
           
            this.Update(special);
        }
        public int deleteSpecialty(Specialty special)
        {
           
            int id = this.Delete<Specialty>(special);
            return id;
        }
        public List<Specialty> GetSpecialties()
        {  
          
           var specialties= new List<Specialty>();
           using (var connection = new SqlConnection(sett.ConString))
            {
                specialties = connection.QueryAll<Specialty>().ToList();
                /* Do the stuffs for the people here */
            }
          return specialties;
        }
        public Specialty GetSpecialty(int id)
        {  
          
           var special= new Specialty();
           using (var connection = new SqlConnection(sett.ConString))
            {
                
               special = connection.Query<Specialty>(id).FirstOrDefault();
            }
          return special;
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