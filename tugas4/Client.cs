using System;
using System.IO;
using System.Net.Sockets;

class Client
{
    static void Main(string[] args)
    {
        Console.Write("Masukkan path file yang akan dikirim: ");
        string filePath = Console.ReadLine();

        if (File.Exists(filePath))
        {
            try
            {
                using (TcpClient client = new TcpClient("127.0.0.1", 11111))
                {
                    using (NetworkStream ns = client.GetStream())
                    {
                        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        {
                            byte[] buffer = new byte[1024];
                            int bytesRead;
                            while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                ns.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                }
                Console.WriteLine("File berhasil dikirim.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
        else
        {
            Console.WriteLine("File tidak ditemukan.");
        }
    }
}
