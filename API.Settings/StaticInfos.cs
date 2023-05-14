using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Settings
{
    public static class StaticInfos
    {
        public static string MsSqlConnectionString;
        public static string MySqlConnectionString;
        public static string PostgreSqlConnectionString;
        public static bool IsMsSQL;
        public static bool IsMySQL;
        public static bool IsPostgreSQL;
        //JWT
        public static string JwtKey;
        public static string JwtIssuer;
        public static string JwtAudience;
        public static int JwtKeyExpireIn;
        //EMAIL
        public static string SenderId= "tutul@nexkraft.com";
        public static string SenderHost= "ssl://smtp.titan.email";
        public static string SenderPassword= "Nexkraft2023#";
        public static string SenderPort= "587";
        public static bool EnableSSL= false;

        //Role
        public enum RoleType
        {
            SuperAdmin=1,
            Admin=2,
            User = 3
        }
        //RootPath
        public static string WebRootPath="";
        public static string ContentRootPath = "";
        public static string FileUploadPath = "";
    }
}
