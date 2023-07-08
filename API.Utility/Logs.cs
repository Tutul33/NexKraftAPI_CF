using API.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace API.Utility
{
    public class Logs
    {
        private const string FILE_NAME = "\\systemLog\\LogTextFile.txt";
        public static void WriteLogFile(string message)
        {
            FileStream fs = null;
            string configPath = StaticInfos.WebRootPath + FILE_NAME;
            if (!File.Exists(configPath))
            {
                using (fs = File.Create(configPath))
                {
                }
            }

            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    StreamWriter streamWriter = new StreamWriter(configPath, true);
                    streamWriter.WriteLine(message);
                    streamWriter.Close();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }       
    }
}
