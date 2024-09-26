using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

class Server
{
    static void Main(string[] args)
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 11111);
        listener.Start();
        Console.WriteLine("Menunggu koneksi dari client...");

        try
        {
            using (TcpClient client = listener.AcceptTcpClient())
            {
                Console.WriteLine("Client terhubung.");
                using (NetworkStream ns = client.GetStream())
                {
                    using (FileStream fs = new FileStream("file_diterima.txt", FileMode.Create, FileAccess.Write))
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRead;
                        while ((bytesRead = ns.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            fs.Write(buffer, 0, bytesRead);
                        }
                    }
                    Console.WriteLine("File diterima dan disimpan sebagai file_diterima.txt");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
        finally
        {
            listener.Stop();
        }
    }
}
