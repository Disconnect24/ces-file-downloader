using System;
using System.IO;
using System.Net.Http;

namespace CES_Downloader
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0) Console.WriteLine("No args.");
            else if (args[0] == "all")
            {
                if (!Directory.Exists("files")) Directory.CreateDirectory("files");
                else
                {
                    DirectoryInfo tempdir = new DirectoryInfo("files");
                    foreach (FileInfo file in tempdir.GetFiles()) file.Delete();
                }
                int count = 0;
                while (count <= 255)
                {
                    StreamWriter stream = File.CreateText($"files\\{count}.ces");
                    stream.Write(GetData(count)); stream.Close(); count++;
                }
            }
            else if (int.TryParse(args[0], out int id))
            {
                if (File.Exists($"{id}.ces")) File.Delete($"{id}.ces");
                StreamWriter stream = File.CreateText($"{id}.ces");
                stream.Write(GetData(id)); stream.Close();
            }
            else Console.WriteLine("Invalid args.");
        }

        static byte[] GetData(int id) => new HttpClient().GetByteArrayAsync($"http://miicontest.wapp.wii.com/first/{id}.ces").GetAwaiter().GetResult();
    }
}
