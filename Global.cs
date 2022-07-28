namespace GameServer_SRO_Automation
{
    internal class Global
    {

        //######Setting UP Destination Folders.           
        public static string MainDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static string? MainRootPath = Path.GetPathRoot(Environment.SystemDirectory);
        public static string To_Certification_iniFolder = MainDirectory + @"vSRO_Certification\ini\";
        public static string To_GameServer_FileFolder = MainDirectory + @"vSRO_Server\";
        public static string To_SMCFolder = MainDirectory + @"vSRO_SMC\";
        public static string To_CertificationFolder = MainDirectory + @"vSRO_Certification\";
        public static string To_wwwrootFolder = MainRootPath + @"inetpub\wwwroot\";
        public static string To_wwwroot_aspnet_clientFolder = MainRootPath + @"inetpub\wwwroot\aspnet_client";

        //Replacing Text, Reading all the text from Array and overwriting the necessary Files.
        //public static string[] ServerFiles = new string[11] { "server.cfg", "ServiceManager.cfg", "smc_updater.cfg", "srGlobalService.ini", "srNodeType.ini", "srShard.ini", "settings.ini", "DBConnect.asp", "GetTotalSilk.asp", "PurchaseSilk.asp", "RefundSilk.asp" };
        public static string[] ServerFiles = new string[18] { "srGlobalService.ini", "srShard.ini", "srNodeType.ini", "server.cfg", "smc_updater.cfg", "ServiceManager.cfg", "DBConnect.asp", "GetTotalSilk.asp", "PurchaseSilk.asp", "RefundSilk.asp", "billing_serverstate.asp", "billing_silkconsume.asp", "billing_silkdatacall.asp", "Class_MD5.asp", "Function.asp", "index.aspx", "web.config", "aspnet_client" };

        //reading password and printing them in astherisc *
        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // remove one character from the list of password characters
                        password = password.Substring(0, password.Length - 1);
                        // get the location of the cursor
                        int pos = Console.CursorLeft;
                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // replace it with space
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            // add a new line because user pressed enter at the end of their password
            Console.WriteLine();
            return password;
        }



        

       

    }
}
