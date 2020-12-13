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
            string CRMUserName = "mohamed.elfawy+cin@gmail.com";
            string CRMApiKey = "DWDPV3MKI1EglnbphFWwONjDtM6pBMdcPlshurLm";
            string CRMAccountId = "5fbb00f0d40dd72644bcaf14";
            CIN7Integration.StartUp.StartSync(Cin7UsreName, Cin7ApiKey, CRMApiKey, CRMAccountId, CRMUserName, DateTime.Now.AddYears(-1));

        }
    }
}
