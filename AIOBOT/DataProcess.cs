using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AIOBOT
{
    class DataProcess
    {
        private static SQLiteConnection sqlite_conn;

        // Connect Sqlite database.(create database)
        public void CreateConnection(string db_name)
        {
            string db_create = String.Concat("Data Source=", db_name, ";");
            sqlite_conn = new SQLiteConnection(db_create);
            try
            {
                sqlite_conn.Open();
                Status.DbLocked = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //return sqlite_conn;
        }

        //add task
        public void InsertData_task(BotTask bottask)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = String.Concat("INSERT INTO tasks VALUES (", '"', bottask.id, '"', ",", '"', bottask.store, '"', ", ", '"', bottask.profilename, '"', ", ", '"', bottask.product_url, '"', ", ", '"', bottask.size, '"', ",", '"', bottask.activeProxy, '"', ",", bottask.cardtype, ",", '"', bottask.wish, '"', ",", '"', bottask.token, '"', ",", '"',bottask.sku, '"', ",", '"',bottask.variant, '"', ",", '"',bottask.product_id, '"', ",", '"',bottask.image_url, '"', ");"); ;
            sqlite_cmd.ExecuteNonQuery();
        }       

        public void RemoveData_task()
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM tasks";
            sqlite_cmd.ExecuteNonQuery();

        }
        public List<BotTask> Read_tasks()
        {
            List<BotTask> result = new List<BotTask>();
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM tasks";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                BotTask temp = new BotTask();
                temp.id = sqlite_datareader.GetString(0);
                temp.store = sqlite_datareader.GetString(1);
                temp.profilename = sqlite_datareader.GetString(2);
                temp.product_url = sqlite_datareader.GetString(3);
                temp.size = sqlite_datareader.GetString(4);            
                temp.activeProxy = sqlite_datareader.GetString(5);
                temp.cardtype = sqlite_datareader.GetInt32(6);
                temp.wish = sqlite_datareader.GetString(7);
                temp.token = sqlite_datareader.GetString(8);
                temp.sku = sqlite_datareader.GetString(9);
                temp.variant = sqlite_datareader.GetString(10);
                temp.product_id = sqlite_datareader.GetString(11);
                temp.image_url = sqlite_datareader.GetString(12);
                result.Add(temp);
            }
            return result;
        }

        //Close Database
        public void Endprocess()
        {
            sqlite_conn.Close();
            Status.DbLocked = false;
        }

        public void InsertData_profile(Profile profile)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            string command = "INSERT INTO profile_all VALUES (";
            sqlite_cmd.CommandText = string.Concat(command, '"', profile.email, '"', ", ", '"', profile.password, '"', ", ", '"', profile.cardNumber, '"', ", ", '"', profile.cardName, '"', ", ", '"', profile.cardCVV, '"', ", ", '"', profile.cardMonth, '"', ", ", '"', profile.cardYear, '"', ", ", '"', profile.profilename, '"', ",", '"', profile.cardType, '"',",",'"',profile.store,'"', ");");
            sqlite_cmd.ExecuteNonQuery();
        }

        public void InsertData_profile_MCT(ProfileMCT profile)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            string command = "INSERT INTO profile_mct VALUES (";
            sqlite_cmd.CommandText = string.Concat(command, '"', profile.email, '"', ", ", '"', profile.firstname, '"', ", ", '"', profile.lastname, '"', ", ", '"', profile.province, '"', ", ", '"', profile.city, '"', ", ", '"', profile.address1, '"', ", ", '"', profile.address2, '"', ", ", '"', profile.phone, '"', ",", '"',profile.cardType,'"',",",'"',profile.cardNumber,'"',",",'"',profile.cardName,'"',",",'"',profile.cardCVV,'"',",",'"',profile.cardMonth,'"',",",'"',profile.cardYear,'"',",",'"', profile.postalcode,'"',",",'"', profile.profilename, '"', ");");
            sqlite_cmd.ExecuteNonQuery();
        }

        public void InsertData_proxy_group(string id, string name, string proxy)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = String.Concat("INSERT INTO proxy_group", " VALUES (", '"', id, '"', ",", '"', name, '"', ",", '"', proxy, '"', ");");
            sqlite_cmd.ExecuteNonQuery();
        }

        public void Update_proxy_group(string id, string name, string proxy)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = String.Concat("UPDATE proxy_group", " SET name=", '"', name, '"', ",", "proxy=", '"', proxy, '"', " WHERE id=", '"', id, '"', ";");
            sqlite_cmd.ExecuteNonQuery();
        }

        //Remove user account
        public void RemoveData_profile()
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM profile_all";
            sqlite_cmd.ExecuteNonQuery();
        }

        public void RemoveData_profile_mct()
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM profile_mct";
            sqlite_cmd.ExecuteNonQuery();
        }      

        public void RemoveData_proxy_group(string id)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            if (id != "")
            {
                sqlite_cmd.CommandText = String.Concat("DELETE FROM proxy_group where id=", '"', id, '"', ";");
            }
            else
            {
                sqlite_cmd.CommandText = "DELETE FROM proxy_group";
            }
            sqlite_cmd.ExecuteNonQuery();
        }

        //Remove all task

        public void RemoveData_captcha(string api)
        {
            if (api == "")
            {
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = String.Concat("DELETE FROM captcha");
                sqlite_cmd.ExecuteNonQuery();
            }
            else
            {
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = String.Concat("DELETE FROM captcha WHERE ", "api=", '"', api, '"', ";");
                sqlite_cmd.ExecuteNonQuery();
            }
        }

        public void Copy_task(string id)
        {
            List<BotTask> datas = Read_tasks();
            BotTask new_data = new BotTask();
            foreach (var temp in datas)
            {
                if (temp.id == id)
                {
                    new_data = temp;
                    break;
                }
            }
            Random GenID = new Random();
            new_data.id = GenID.Next(10000, 99999).ToString();
            InsertData_task(new_data);
        }
        ////Read Table
        ///
        public List<string> Read_proxy_group_name()
        {
            List<string> result = new List<string>();
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM proxy_group";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                result.Add(sqlite_datareader.GetString(1));
            }
            return result;
        }

        public List<Profile> Read_profile()
        {
            List<Profile> result = new List<Profile>();
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM profile_all";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            int index = 0;
            while (sqlite_datareader.Read())
            {
                Profile profile = new Profile();              
                profile.email = sqlite_datareader.GetString(0);
                profile.password = sqlite_datareader.GetString(1);
                profile.cardNumber = sqlite_datareader.GetString(2);
                profile.cardName = sqlite_datareader.GetString(3);
                profile.cardCVV = sqlite_datareader.GetString(4);
                profile.cardMonth = sqlite_datareader.GetString(5);
                profile.cardYear = sqlite_datareader.GetString(6);
                profile.profilename = sqlite_datareader.GetString(7);
                profile.cardType = sqlite_datareader.GetString(8);
                profile.store = sqlite_datareader.GetString(9);
                result.Add(profile);
                index++;
            }
            return result;
        }
    
        public List<ProfileMCT> Read_profile_MCT()
        {
            List<ProfileMCT> result = new List<ProfileMCT>();
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM profile_mct";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            int index = 0;
            while (sqlite_datareader.Read())
            {
                ProfileMCT profile = new ProfileMCT();            
                profile.email = sqlite_datareader.GetString(0);                
                profile.firstname= sqlite_datareader.GetString(1);
                profile.lastname = sqlite_datareader.GetString(2);
                profile.province = sqlite_datareader.GetString(3);
                profile.city = sqlite_datareader.GetString(4);
                profile.address1 = sqlite_datareader.GetString(5);
                profile.address2 = sqlite_datareader.GetString(6);
                profile.phone = sqlite_datareader.GetString(7);
                profile.cardType = sqlite_datareader.GetString(8);
                profile.cardNumber = sqlite_datareader.GetString(9);         
                profile.cardName = sqlite_datareader.GetString(10);
                profile.cardCVV = sqlite_datareader.GetString(11);
                profile.cardMonth = sqlite_datareader.GetString(12);
                profile.cardYear = sqlite_datareader.GetString(13);
                profile.postalcode = sqlite_datareader.GetString(14);
                profile.profilename = sqlite_datareader.GetString(15);                               
                result.Add(profile);
                index++;
            }
            return result;
        }
        public Profile Read_profile(string profilename)
        {
            try
            {
                SQLiteDataReader sqlite_datareader;
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = string.Concat("SELECT * FROM profile_all WHERE profilename=", '"', profilename, '"');
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                sqlite_datareader.Read();
                Profile profile = new Profile();                
                profile.email = sqlite_datareader.GetString(0);
                profile.password = sqlite_datareader.GetString(1);
                profile.cardNumber = sqlite_datareader.GetString(2);
                profile.cardName = sqlite_datareader.GetString(3);
                profile.cardCVV = sqlite_datareader.GetString(4);
                profile.cardMonth = sqlite_datareader.GetString(5);
                profile.cardYear = sqlite_datareader.GetString(6);
                profile.profilename = sqlite_datareader.GetString(7);
                profile.cardType = sqlite_datareader.GetString(8);
                profile.store = sqlite_datareader.GetString(9);
                return profile;
            }
            catch
            {
                return new Profile();
            }
        }

        public List<ProxyGroup> ReadData_proxy_group()
        {
            List<ProxyGroup> result = new List<ProxyGroup>();
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM proxy_group";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string get_id = sqlite_datareader.GetString(0);
                string name = sqlite_datareader.GetString(1);
                string proxy_text = sqlite_datareader.GetString(2);
                ProxyGroup result_one = new ProxyGroup();
                result_one.group_id = get_id;
                result_one.group_name = name;
                result_one.proxylist = new List<MyProxy>();
                string[] temps = proxy_text.Split(';');
                foreach (string temp in temps)
                {
                    try
                    {
                        MyProxy myProxy = new MyProxy();
                        myProxy.url = temp.Split(':')[0];
                        myProxy.port = temp.Split(':')[1];
                        myProxy.user = temp.Split(':')[2];
                        myProxy.pass = temp.Split(':')[3];
                        result_one.proxylist.Add(myProxy);
                    }
                    catch
                    {

                    }
                }
                result_one.group_count = result_one.proxylist.Count;
                result.Add(result_one);
            }
            return result;
        }

        public List<string> getProfilelist()
        {
            List<string> result = new List<string>();
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM profile";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            int index = 0;
            while (sqlite_datareader.Read())
            {
                string profilename = sqlite_datareader.GetString(16);
                result.Add(profilename);
                index++;
            }
            return result;
        }
    }
}
