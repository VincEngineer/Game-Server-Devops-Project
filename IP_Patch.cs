
using System.Text;

//Injecting IP for AgentServer.exe and MachineManager.exe
namespace GameServer_SRO_Automation
{
    internal class IP_Patch
    {
        public static void IP_Patching_AgentServer(string IPToSend)
        {
            string FileName = Global.MainDirectory + @"vSRO_Server\AgentServer.exe";

            if (File.Exists(FileName))
            {

                FileStream output = new(FileName, FileMode.Open);
                BinaryWriter binaryWriter = new(output);
                byte[] buffer = new byte[]
                {
                    250,
                    39
                };
                binaryWriter.Seek(213882, SeekOrigin.Begin);
                binaryWriter.Write(buffer, 0, 2);
                binaryWriter.Seek(213966, SeekOrigin.Begin);
                binaryWriter.Write(buffer, 0, 2);
                byte[] array = new byte[35];
                byte[] buffer2 = array;
                binaryWriter.Seek(796666, SeekOrigin.Begin);
                binaryWriter.Write(buffer2, 0, 35);
                byte[] bytes = Encoding.ASCII.GetBytes(IPToSend);
                binaryWriter.Seek(796666, SeekOrigin.Begin);
                binaryWriter.Write(bytes, 0, bytes.Length);
                binaryWriter.Close();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[+] IP address was Successfully injected into: {FileName}.\n");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[-] File could not be Injected ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"File 'AgentServer.exe' not Found. Make sure your Anti-virus is not deleting the modules\n" +
                     $"If this happens to you, you will need to Patch the MachineManager.exe Manually, and make sure the Anti-virus does not delete them again.\n" +
                     $"use the following zip file to recover the module:'ServerModules_backup.zip' Location:{Global.To_GameServer_FileFolder}");
                Console.ResetColor();
            }
        }

        public static void IP_Patching_MachineManager(string IPToSend)
        {
            string FileName = Global.MainDirectory + @"vSRO_Server\MachineManager.exe";

            if (File.Exists(FileName))
            {

                FileStream output = new(FileName, FileMode.Open);
                BinaryWriter binaryWriter = new(output);
                byte[] buffer = new byte[]
                {
                    96,
                    63
                };
                binaryWriter.Seek(177642, SeekOrigin.Begin);
                binaryWriter.Write(buffer, 0, 2);
                binaryWriter.Seek(177726, SeekOrigin.Begin);
                binaryWriter.Write(buffer, 0, 2);
                byte[] array = new byte[32];
                byte[] buffer2 = array;
                binaryWriter.Seek(737120, SeekOrigin.Begin);
                binaryWriter.Write(buffer2, 0, 32);
                byte[] bytes = Encoding.ASCII.GetBytes(IPToSend);
                binaryWriter.Seek(737120, SeekOrigin.Begin);
                binaryWriter.Write(bytes, 0, bytes.Length);
                binaryWriter.Close();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[+] IP address was Successfully injected into: {FileName}.\n");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[-] File could not be Injected ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"File 'MachineManager.exe' not Found. Make sure your Anti-virus is not deleting the modules\n" +
                    $"If this happens to you, you will need to Patch the MachineManager.exe Manually, and make sure the Anti-virus does not delete them again.\n" +
                    $"use the following file to recover the module:'ServerModules_backup.zip' Location:{Global.To_GameServer_FileFolder}");
                Console.ResetColor();
            }




        }

        public static void IP_Patching_SR_GameServer(string IPToSend)
        {

            string FileName = Global.MainDirectory + @"vSRO_Server\SR_GameServer.exe";

            if (File.Exists(FileName))
            {

                FileStream output = new(FileName, FileMode.Open);
                BinaryWriter binaryWriter = new(output);
                byte[] buffer6 = new byte[]
                    {
                        32,
                        142,
                        173
                    };
                binaryWriter.Seek(5465530, SeekOrigin.Begin);
                binaryWriter.Write(buffer6, 0, 3);
                binaryWriter.Seek(5465614, SeekOrigin.Begin);
                binaryWriter.Write(buffer6, 0, 3);
                byte[] array2 = new byte[32];
                byte[] buffer7 = array2;
                binaryWriter.Seek(7179808, SeekOrigin.Begin);
                binaryWriter.Write(buffer7, 0, 32);
                byte[] bytes2 = Encoding.ASCII.GetBytes(IPToSend);
                binaryWriter.Seek(7179808, SeekOrigin.Begin);
                binaryWriter.Write(bytes2, 0, bytes2.Length);
                binaryWriter.Close();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[+] IP address was Successfully injected into: {FileName}.\n");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[-] File could not be Injected ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"File 'SR_GameServer.exe' not Found. Make sure your Anti-virus is not deleting the modules\n" +
                    $"If this happens to you, you will need to Patch the SR_GameServer.exe Manually, and make sure the Anti-virus does not delete them again.\n" +
                    $"use the following file to recover the module:'ServerModules_backup.zip' Location:{Global.To_GameServer_FileFolder}");
                Console.ResetColor();
            }
        }


    }
}
