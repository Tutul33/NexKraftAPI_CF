using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Utility
{
    public static class CommonExt
    {
        public static string Encryptdata(string inputString)
        {
            string strmsg = string.Empty;
            try
            {
                byte[] encode = new byte[inputString.Length];
                encode = Encoding.UTF8.GetBytes(inputString);
                strmsg = Convert.ToBase64String(encode);
            }
            catch (Exception) { }

            return strmsg;
        }

        public static string Decryptdata(string inputString)
        {
            string decryptpwd = string.Empty;
            try
            {
                UTF8Encoding encodepwd = new UTF8Encoding();
                Decoder Decode = encodepwd.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(inputString);
                int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                decryptpwd = new String(decoded_char);
            }
            catch (Exception) { }
            return decryptpwd;
        }
        public static bool IsEmptyOrInvalid(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return true;
            }

            var jwtToken = new JwtSecurityToken(token);
            return (jwtToken.ValidTo < DateTime.UtcNow);
        }
    }
}
