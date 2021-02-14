using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrialChecker;

namespace caGenerateLicense
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             1. Сгенерировать ключи
             2. Скопировать private.key в качестве значения переменной privateKey
                Сгенерировать файл лицензии
             */
            string mode = "1";
            if (mode == "1")
            {
                Console.WriteLine("Key generation");
                RsaUtils rsa = new RsaUtils();
                var privateKey = rsa.GetKeyInformation(true);
                var publicKey = rsa.GetKeyInformation(false);

                System.IO.File.WriteAllText("private.key", privateKey);
                System.IO.File.WriteAllText("public.key", publicKey);
            }
            else if (mode == "2")
            {
                Console.WriteLine("License generation");
                var validTo = DateTime.Today.AddDays(15);
                var privateKey = @"<RSAKeyValue><Modulus>6cDwthHizyN2oy7GVLDJj8BULOyZcNmhT8DUOtnolKl1JXyboMTtYSj8RTItDYuA9erFmjHxfXs77gjx4AZovb5tn/Vuv2WZqtRfGnmFS/BaioHR7rLeHpEQ5Fy/bSZZVCiK0qCJ1fACkb5myKxktHF33t884OgYeIafu8C/+JE=</Modulus><Exponent>AQAB</Exponent><P>9YjXPha1uIGAquhNNfO28+5HPpYoJ25unTsJDy8RB0e6B4shWSrQWoFHGfMvwKVud3bIGhak4M1BDp2oYN/zdw==</P><Q>87eNW6WEmHcZs0ZoVk7UPFBzMbz7PFkRkBFveTEGILDDxPm95JDZoe2xJH7BQCNmDouik81JMWL8pXjQqzkmNw==</Q><DP>5n/0MOF47yED/W3fLgsBcbX7RBIK2Bmf1hMZyhtRTRhU18gRInhC6PP8H6xg4+Vp8tSbvYIMfs2xiVsQOwGCuQ==</DP><DQ>wcYf+00Htt2DLpn9XRoB/w6L+HDkZGAA6cYjFn2W/Kjujo9unutYTjoZTQYHCsLaYxg8pD1lQ4ILlFkRuAGRUw==</DQ><InverseQ>lpU5H7FE5Dq38qXBK3YemSFHs9c56RHJpA+IPWk+vZwJ90RU1y2AcdW+XCYRIoK+gJx6CneSOjaHuwrXHH7r5A==</InverseQ><D>lxdAP99VxF/+2kFsrudKkaFWh2czUw9Ixl00kQmy3QyfjOh7S7KeYTP1w+BH4L5wUAQ9T4rCtkIGnbioZw6defOz9e8yd/S6mwJaC5KUmDRUhewZEd8AFscT+kLekJ4JB+WZbxVJgH1DxW9A2nqxGU5/r+gJv4TmQ8c8M6LDTcU=</D></RSAKeyValue>";

                RsaUtils rsa = new RsaUtils();
                rsa.SetKeys(privateKey);

                var validToStr = validTo.ToString("yyyyMMdd");
                var sign = rsa.SignData(validToStr);

                System.IO.File.WriteAllText("license.lic", $"{validToStr}{sign}");
            }
        }
    }
}
