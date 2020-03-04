using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Project.Class
{
    public class DbConnection
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlContext"].ConnectionString);
        public bool login(string username, string password)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT*FROM USERS WHERE USERNAME=@USERNAME AND PASSWORD=@PASSWORD AND STATUS=1", con);
            dataAdapter.SelectCommand.Parameters.AddWithValue("@USERNAME", username);
            dataAdapter.SelectCommand.Parameters.AddWithValue("@PASSWORD", MD5Hash(password));
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count == 1)
                return true;
            else
                return false;
        }

        public string MD5Hash(string val)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytes = ASCIIEncoding.Default.GetBytes(val);
            byte[] encoded = md5.ComputeHash(bytes);
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < encoded.Length; i++)
                strBuilder.Append(encoded[i].ToString("x2"));
            return strBuilder.ToString();
        }
    }
}