using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            string Cin7UsreName = "RevampcoUS";
            string Cin7ApiKey = "ef08b156c82740e191f0254b26e30c87";
            string CRMUserName = "ahmed.mohy2+1551@gmail.com";
            string CRMApiKey = "Fjxr7lNEIGu7xzo1fkKCnwuwNTJhQaqoSpAVOEz3";

            string CIN7_EMail = "smeligy@revampco.com";

            CIN7Integration.StartUp.StartSync(CIN7_EMail,Cin7UsreName, Cin7ApiKey, CRMApiKey, CRMUserName, DateTime.Now.AddYears(-1) , "WooCommerce");

        }
    }
}
