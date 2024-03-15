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

public class TitleMasterRepository: BaseRepository<TitleMaster, SqlConnection>
    {
        
        //Setting cstring=new Setting();
        Setting sett=new Setting();
        public TitleMasterRepository(Setting sett) : base(sett.ConString)
        {
            this.sett=sett;
            DbSettingMapper.Add<SqlConnection>(new SqlServerDbSetting(), true);
            DbHelperMapper.Add<SqlConnection>(new SqlServerDbHelper(), true);
            StatementBuilderMapper.Add<SqlConnection>(new SqlServerStatementBuilder(new SqlServerDbSetting()),true);

        }
        public  void insertTitleMaster(TitleMaster titMaster)
        {
            //UserRepository usrrepository = new UserRepository(cstring.ConString);
            this.Insert(titMaster);
        }
        public  void updateTitMaster(TitleMaster titMaster)
        {
           
            this.Update(titMaster);
        }
        public int deleteTit(TitleMaster titMaster)
        {
           
            int id = this.Delete<TitleMaster>(titMaster);
            return id;
        }
        public List<TitleMaster> GetTitMasters()
        {  
          
           var titMasters= new List<TitleMaster>();
           using (var connection = new SqlConnection(sett.ConString))
            {
                titMasters = connection.QueryAll<TitleMaster>().ToList();
                /* Do the stuffs for the people here */
            }
          return titMasters;
        }
        public TitleMaster GetTitMaster(int id)
        {  
          
           var titMaster= new TitleMaster();
           using (var connection = new SqlConnection(sett.ConString))
            {
                
               titMaster = connection.Query<TitleMaster>(id).FirstOrDefault();
            }
          return titMaster;
        }
       
         
    }