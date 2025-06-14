using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
namespace SimulFactory.Core.Util
{
    public class GoogleUtil
    {
        public static async Task<bool> CheckTokenAsync(string jwt)
        {
            try
            {
                GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(jwt);
                return payload.EmailVerified;
            }
            catch(Exception e)
            {
                // 실패시
                Console.WriteLine(e.Message);
                return false;
            }
        }

    }
}
