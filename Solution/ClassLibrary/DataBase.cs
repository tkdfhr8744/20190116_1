using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClassLibrary
{
    public class DataBase
    {
        public MySqlConnection GetConnection()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection();

                string path = "/public/DBInfo.json";
                string result = new StreamReader(File.OpenRead(path)).ReadToEnd();
                JObject jo = JsonConvert.DeserializeObject<JObject>(result); // 스트링형식을 제인스화 오브젝트
                Hashtable map = new Hashtable();
                foreach (JProperty col in jo.Properties())
                {
                    Console.WriteLine("{0} : {1}", col.Name, col.Value);
                    map.Add(col.Name, col.Value);
                }
                string strConn = string.Format("server={0};user={1};password={2};database={3};", map["server"], map["user"], map["password"], map["database"]);
                conn.ConnectionString = strConn;
                conn.Open();
                return conn;
                /*
                FileStream fs = File.OpenRead(path);
                byte[] b = new byte[1024];
                FileStream sr = new StreamReader(fs);
                //UTF8Encoding temp = new UTF8Encoding(true);
                */
                /*
                    while (fs.Read(b, 0, b.Length) > 0)
                {
                    result = temp.GetString(b);
                }*/
                //Console.WriteLine(result);
                //fs.Close();


            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
