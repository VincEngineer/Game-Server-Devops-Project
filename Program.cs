

using GameServer_SRO_Automation;
using System.Diagnostics;
using System.IO.Compression;
using static GameServer_SRO_Automation.IP_Patch;
/*
 This script is being made to configure all the files by inserting the Publish IP address, SqlHost Name, Database Name, and database password.

 */
namespace GameserverAutomation
{
    class ServerAutomation
    {
        public static void Main(string[] args)
        {


            
            

            Console.ResetColor();
            Console.Title = "Game Server Installation V1.0";
            Console.WriteLine("------------------------------------------------------------------------------------------|");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("                                    Made By: Vincenzo Bonaiuto                             ");
            Console.ResetColor();
            Console.WriteLine("------------------------------------------------------------------------------------------|\n");
            Console.WriteLine("|-----------------------------------------------------------------------------------------|");
            Console.WriteLine("|--------------------------------------###Version 1.0###----------------------------------|");
            Console.WriteLine("|-----------------------------------------------------------------------------------------|");
            Console.WriteLine("|------------------------------PRESS (ENTER) TO START THE SCRIPT--------------------------|");

            //Gathering User Input:
            Console.Write("Enter your Public IP address:");
            var PublicIP = Console.ReadLine();
            Console.Write("Enter your sqlHost, e.g 'MY-MACHINE-NAME\\MSSQLSERVER':");
            var SqlHost_UserInput = Console.ReadLine();
            Console.Write("Enter your SQL Password: ");
            string? SqlPassword_UserInput = Global.ReadPassword();
            Console.Write("PLease, Enter your Account Database, e.g 'SRO_VT_ACCOUNT':");
            var AccountDatabase_UserInput = Console.ReadLine();
            Console.Write("PLease, Enter your SHARD Database, e.g 'SRO_VT_SHARD':");
            var ShardDatabase_UserInput = Console.ReadLine();

            Console.Write("Would you Like to execute the PowerShell Script?\n" +
                "It will install the following features:\n" +
                "1. Exclution path for each module in Windows Defender\n" +
                "2. Exclution Process for each module in windows defender\n" +
                "3. Open Firewall Ports:15779, 15881, 15884\n" +
                "4. IIS and Features\n" +
                "5. Turn off DEP execution\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Press yes or no (yes/no)");
            var ExecuteShellScript = Console.ReadLine();
            Console.ResetColor();
            


            if (ExecuteShellScript == "yes")
            {
              
                PowerShellScriptFile("GameServerScript.ps1");


                while (true)
                {
                    try
                    {

                        //check if aspnet folder exist
                        if (Directory.Exists(Global.To_wwwroot_aspnet_clientFolder))
                        {
                            Thread.Sleep(2000);
                            //Unziping Modules before patching.
                            string ZipedModules = Global.To_GameServer_FileFolder + "Dont Delete This File -Modules-.zip";
                            ZipFile.ExtractToDirectory(ZipedModules, Global.To_GameServer_FileFolder, true);
                            Thread.Sleep(2000);

                            //Patching Modules
                            Console.WriteLine($"Patching IP:{PublicIP} Into Module AgentServer.exe");
                            IP_Patching_AgentServer(PublicIP);
                            Console.WriteLine($"Patching IP:{PublicIP} Into Module MachineManager.exe");
                            IP_Patching_MachineManager(PublicIP);
                            Console.WriteLine($"Patching IP:{PublicIP} Into Module SR_GameServer.exe");
                            IP_Patching_SR_GameServer(PublicIP);

                            //Replacing text
                            ReplacingText(PublicIP, SqlHost_UserInput, SqlPassword_UserInput, AccountDatabase_UserInput, ShardDatabase_UserInput);

                            //Moving Files
                            MovingFiles();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("[+] Installation Successfully completed");
                            Console.ResetColor();
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"Waiting for IIS installation to install the folder: {Global.To_wwwrootFolder}");
                            Console.ResetColor();
                            Thread.Sleep(1000);
                        }
                    }
                    catch (Exception CallingMethodErros)
                    {

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(" [-] The process failed when calling method: {0}\n", CallingMethodErros.ToString());
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(CallingMethodErros.StackTrace);
                        Console.ResetColor();
                    }
                }

            }
            else if (ExecuteShellScript == "no")
            {

                try
                {

                    //Unziping Modules before patching.
                    string ZipedModules = Global.To_GameServer_FileFolder + "Dont Delete This File -Modules-.zip";
                    ZipFile.ExtractToDirectory(ZipedModules, Global.To_GameServer_FileFolder, true);
                    Thread.Sleep(2000);

                    //Patching Modules
                    Console.WriteLine($"Patching IP:{PublicIP} Into Module AgentServer.exe");
                    IP_Patching_AgentServer(PublicIP);
                    Console.WriteLine($"Patching IP:{PublicIP} Into Module MachineManager.exe");
                    IP_Patching_MachineManager(PublicIP);
                    Console.WriteLine($"Patching IP:{PublicIP} Into Module SR_GameServer.exe");
                    IP_Patching_SR_GameServer(PublicIP);

                    //Replacing text
                    ReplacingText(PublicIP, SqlHost_UserInput, SqlPassword_UserInput, AccountDatabase_UserInput, ShardDatabase_UserInput);

                    //Moving Files
                    MovingFiles();                   

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[+] Installation Successfully completed");
                    Console.ResetColor();
                }
                catch (Exception CallingMethodErrosNoShell)
                {

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" [-] The process failed when calling method: {0}\n", CallingMethodErrosNoShell.ToString());
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(CallingMethodErrosNoShell.StackTrace);
                    Console.ResetColor();

                }


            }

            else
            {
                Console.WriteLine("You Typed the wrong word, please Make Sure you type yes or no");
                Console.ReadKey();
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------------------------------PLEASE, RESTART THE COMPUTER---------------------------------------------------");
            Console.ResetColor();
            
            
            System.Console.ReadKey();




        }

        private static void ReplacingText(string? PublicIP, string? SqlHost_UserInput, string? SqlPassword_UserInput, string? AccountDatabase_UserInput, string? ShardDatabase_UserInput)
        {
            try
            {


                string ZipedFile = "Dont Delete This File -Configuration-.zip";
                ZipFile.ExtractToDirectory(ZipedFile, Global.MainDirectory, true);

                string[] FilesToModify = new string[11] { "server.cfg", "ServiceManager.cfg", "smc_updater.cfg", "srGlobalService.ini", "srNodeType.ini", "srShard.ini", "settings.ini", "DBConnect.asp", "GetTotalSilk.asp", "PurchaseSilk.asp", "RefundSilk.asp" };

                foreach (string FileToModify in FilesToModify)
                {
                    //Reading Files...
                    string WillBe_a = File.ReadAllText(FileToModify);

                    //Replacing SQL host
                    WillBe_a = WillBe_a.Replace("MSSQLSERVER", SqlHost_UserInput);
                    File.WriteAllText(FileToModify, WillBe_a);

                    //Replacing SQL password
                    WillBe_a = WillBe_a.Replace("PASSWORD", SqlPassword_UserInput);
                    File.WriteAllText(FileToModify, WillBe_a);

                    //Replacing SQL Account
                    WillBe_a = WillBe_a.Replace("SRO_VT_ACCOUNT", AccountDatabase_UserInput);
                    File.WriteAllText(FileToModify, WillBe_a);

                    //Replacing SQL Shard
                    WillBe_a = WillBe_a.Replace("SRO_VT_SHARD", ShardDatabase_UserInput);
                    File.WriteAllText(FileToModify, WillBe_a);

                    //Replacing IP 
                    WillBe_a = WillBe_a.Replace("127.1.1.1", PublicIP);
                    File.WriteAllText(FileToModify, WillBe_a);

                    //Replacing Path to execute the modules from the Server Manager
                    WillBe_a = WillBe_a.Replace("Directory to Replace for module:", Global.To_GameServer_FileFolder);
                    File.WriteAllText(FileToModify, WillBe_a);
                    WillBe_a = WillBe_a.Replace("Directory to Replace for SMC:", Global.To_SMCFolder);
                    File.WriteAllText(FileToModify, WillBe_a);
                    WillBe_a = WillBe_a.Replace("Directory to Replace for Certification:", Global.To_CertificationFolder);
                    File.WriteAllText(FileToModify, WillBe_a);



                }
            }
            catch (Exception ErrorFrom_ReplacingText)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" [-] The process failed: {0}", ErrorFrom_ReplacingText.ToString());
                Console.ResetColor();
            }
        }


        public static void MovingFiles()
        {

            try
            {
                if (!File.Exists(Global.MainDirectory))
                {

                    while (true)
                    {

                        //Moving Files to \vSRO_Certification

                        File.Copy(Global.ServerFiles[0], $"{Global.To_Certification_iniFolder}{Global.ServerFiles[0]}", true);
                        File.Copy(Global.ServerFiles[1], $"{Global.To_Certification_iniFolder}{Global.ServerFiles[1]}", true);
                        File.Copy(Global.ServerFiles[2], $"{Global.To_Certification_iniFolder}{Global.ServerFiles[2]}", true);
                        Thread.Sleep(250);

                        //Moving Files to \vSRO_Server                       
                        File.Copy(Global.ServerFiles[3], $"{Global.To_GameServer_FileFolder}{Global.ServerFiles[3]}", true);


                        Thread.Sleep(250);

                        //Moving Files to \vSRO_SMC
                        File.Copy(Global.ServerFiles[4], $"{Global.To_SMCFolder}{Global.ServerFiles[4]}", true);
                        File.Copy(Global.ServerFiles[5], $"{Global.To_SMCFolder}{Global.ServerFiles[5]}", true);
                        Thread.Sleep(1750);

                        //Moving Files to \wwwroot
                        File.Copy(Global.ServerFiles[6], $"{Global.To_wwwrootFolder}{Global.ServerFiles[6]}", true);
                        File.Copy(Global.ServerFiles[7], $"{Global.To_wwwrootFolder}{Global.ServerFiles[7]}", true);
                        File.Copy(Global.ServerFiles[8], $"{Global.To_wwwrootFolder}{Global.ServerFiles[8]}", true);
                        File.Copy(Global.ServerFiles[9], $"{Global.To_wwwrootFolder}{Global.ServerFiles[9]}", true);
                        File.Copy(Global.ServerFiles[10], $"{Global.To_wwwrootFolder}{Global.ServerFiles[10]}", true);
                        File.Copy(Global.ServerFiles[11], $"{Global.To_wwwrootFolder}{Global.ServerFiles[11]}", true);
                        File.Copy(Global.ServerFiles[12], $"{Global.To_wwwrootFolder}{Global.ServerFiles[12]}", true);
                        File.Copy(Global.ServerFiles[13], $"{Global.To_wwwrootFolder}{Global.ServerFiles[13]}", true);
                        File.Copy(Global.ServerFiles[14], $"{Global.To_wwwrootFolder}{Global.ServerFiles[14]}", true);
                        File.Copy(Global.ServerFiles[15], $"{Global.To_wwwrootFolder}{Global.ServerFiles[15]}", true);
                        File.Copy(Global.ServerFiles[16], $"{Global.To_wwwrootFolder}{Global.ServerFiles[16]}", true);

                        //check if aspnet folder exist
                        if (Directory.Exists(Global.To_wwwroot_aspnet_clientFolder))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"Deleting old directory: {Global.To_wwwroot_aspnet_clientFolder} ");
                            Console.ResetColor();
                            Directory.Delete(Global.To_wwwroot_aspnet_clientFolder, true);
                            Thread.Sleep(1000);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(Global.ServerFiles[17] + $" Was successfully moved to {Global.To_wwwrootFolder}");
                            Console.ResetColor();
                            Directory.Move(Global.ServerFiles[17], $"{Global.To_wwwrootFolder}{Global.ServerFiles[17]}");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(Global.ServerFiles[17] + $"Was successfully moved to {Global.To_wwwrootFolder}");
                            Console.ResetColor();
                            Directory.Move(Global.ServerFiles[17], $"{Global.To_wwwrootFolder}{Global.ServerFiles[17]}");
                        }




                        //>>>>>>>>>>>>>>>>>>COMPILER of the Certification Server(IMPORTANT FOR MODULES TO WORK)<<<<<<<<<<<<<<<<<<<<<<<<
                        ProcessStartInfo processInfo = new();
                        Process process = new()
                        {
                            StartInfo = processInfo
                        };
                        process.StartInfo.WorkingDirectory = Global.To_CertificationFolder;
                        //opening powershell in the background
                        processInfo.FileName = @"powershell.exe";
                        //executing compiler, to compile the ini folder
                        processInfo.Arguments = @"& .\compile";
                        processInfo.UseShellExecute = false;
                        processInfo.CreateNoWindow = true;
                        process.Start();

                        //deleting all unneeded files after the installation is done. eventhough the printing say 'Restoring File'
                        foreach (string FileToDelete in Global.ServerFiles)
                        {

                            if (File.Exists(FileToDelete))
                            {

                                File.Delete(FileToDelete);
                                Console.WriteLine("Restoring File " + FileToDelete + "\n");

                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Files not found, or there is no more files to clean");
                                Console.ResetColor();
                            }
                        }

                        break;

                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" [+] Files successfully Loaded\n");
                    Console.ResetColor();
                }
                //Error Message
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(" [-] Please, Check the main folder. Detecting missing file\n");
                    Console.ResetColor();
                }
            }
            //Error Message:"Process failed"
            catch (Exception ErrorFrom_MovingFiles)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" [-] The process failed: {0}", ErrorFrom_MovingFiles.ToString());
                Console.ResetColor();
            }

        }


        public static void PowerShellScriptFile(string ScriptFileName)
        {

            try
            {
                //execute powershell cmdlets or scripts using command arguments as process
                ProcessStartInfo processInfo = new()
                {
                    //opening powershell
                    FileName = @"powershell.exe",
                    //scripting commands               
                    Arguments = @$"& powershell .\{ScriptFileName}",
                    UseShellExecute = true,
                    CreateNoWindow = false
                };
                Process process = new()
                {
                    StartInfo = processInfo
                };
                process.Start();
            }
            catch (Exception PowershellScriptError)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" [-] The process failed: {0}", PowershellScriptError.ToString());
                Console.WriteLine(PowershellScriptError.StackTrace);
                Console.ResetColor();
            }



        }

        public static void PowershellCommands(string Command)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            ProcessStartInfo processInfo = processStartInfo;
            processInfo.FileName = "powershell.exe";
            processInfo.Arguments = $@"& {Command}";
            processInfo.UseShellExecute = true;
            processInfo.CreateNoWindow = true;

            Process process = new()
            {
                StartInfo = processInfo
            };
            process.Start();


            Console.Read();
        }

    }

}




