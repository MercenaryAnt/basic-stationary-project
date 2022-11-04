using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using Microsoft.Win32;

namespace LisansUretici
{
    class LisansIslemleri
    {

        public string YazilimKodu()
        {
            string HDDSeri = HDDSeriNo();
            string CPUSeri = CPUSeriNo();
            string HddCpuSeriNo = HDDSeri + CPUSeri;
            HddCpuSeriNo = HddCpuSeriNo.ToUpper();
            string kod = "";
            foreach (var item in HddCpuSeriNo)
            {
                if (item == '0')
                {
                    kod += 'A';
                }
                else if (item == 'C')
                {
                    kod += 'Z';
                }
                else if (item == 'W')
                {
                    kod += 'U';
                }
                else if (item == 'H')
                {
                    kod += 'L';
                }
                else if (item == 'O')
                {
                    kod += 'T';
                }
                else if (item == 'K')
                {
                    kod += 'I';
                }
                else if (item == 'C')
                {
                    kod += 'Z';
                }
                else if (item == '3')
                {
                    kod += 'B';
                }
                else if (item == '5')
                {
                    kod += 'Y';
                }
                else if (item == '6')
                {
                    kod += 'P';
                }
                else if (item == '8')
                {
                    kod += 'F';
                }
            }
            string yazilimKodu = Md5Sifrele(kod).ToUpper();
            return yazilimKodu;
        }

        public string LisansKodu(string yazilimKodu)
        {
            yazilimKodu = Md5Sifrele(yazilimKodu);
            string sonKod = yazilimKodu.Substring(0, 30) + "-" + "Kırtasiye Uygulaması";

            string lisansKodu1 = Md5Sifrele(sonKod);
            string lisansKodu2 = Md5Sifrele(lisansKodu1);

            string SonLisansKodu = lisansKodu2.Substring(1, 3) + lisansKodu2.Substring(8, 6) + lisansKodu2.Substring(20, 4) + lisansKodu2.Substring(28, 4) + lisansKodu2.Substring(16, 3);

            SonLisansKodu = SonLisansKodu.ToUpper();

            SonLisansKodu = SonLisansKodu.Insert(15, "-");
            SonLisansKodu = SonLisansKodu.Insert(10, "-");
            SonLisansKodu = SonLisansKodu.Insert(5, "-");

            return SonLisansKodu;
        }

        public string HDDSeriNo()
        {
            return HDDSeriNoCek();
        }

        public string CPUSeriNo()
        {
            return CPUSeriNoCek();
        }

        public string Md5Sifrele(string metin)
        {
            return CreateMD5(metin);
        }

        public static String CPUSeriNoCek()
        {
            String processorID = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * FROM WIN32_Processor");
            ManagementObjectCollection mObject = searcher.Get();

            foreach (ManagementObject obj in mObject)
            {
                processorID = obj["ProcessorId"].ToString();
            }

            return processorID;
        }


        public static string HDDSeriNoCek()
        {
            List<string> serials = new List<string>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
            ManagementObjectCollection disks = searcher.Get();
            foreach (ManagementObject disk in disks)
            {
                if (disk["SerialNumber"] == null)
                    serials.Add("");
                else
                    serials.Add(disk["SerialNumber"].ToString());
            }
            string HDDserialNo = "";
            // List<string> serialsList = HDDSeriNoCek();
            foreach (string s in serials)
            {
                HDDserialNo = HDDserialNo + s.Trim();
            }
            HDDserialNo = HDDserialNo.TrimStart(); //Baştaki Boşluğu Kaldırıyoruz.
            return HDDserialNo;
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString().ToLower();
            }
        }

    }
}
