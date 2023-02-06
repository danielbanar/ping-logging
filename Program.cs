using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
namespace Ping
{
    class Program
    {
        static string domain = "google.com";
        static bool online = true;
        static bool lastOnline = true;
        static int timeout = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("[Start]   " + DateTime.Now.ToString("F"));
            while (true)
            {
                System.Net.NetworkInformation.Ping myrequest = new System.Net.NetworkInformation.Ping();
                myrequest.SendAsync(domain, null);
                myrequest.PingCompleted += pingcompleted;
                if (lastOnline != online)//zmena
                {
                    if(timeout >= 5 || !online)
                    {
                        Console.WriteLine("[" + DateTime.Now.ToString("F") + "]   Online: " + online);
                        Write();
                    }
                }
                    if (online)
                        timeout = 0;
                    else if (!online)
                        timeout++;
                lastOnline = online;
                Thread.Sleep(1000);
            }
        }
        static void pingcompleted(object source, System.Net.NetworkInformation.PingCompletedEventArgs e)
        {
            if (e.Cancelled)
                online = false;
            try
            {
                online = e.Reply.Status.ToString() == "Success";
            }
            catch
            {
                online = false;
            }

        }
        static void Write()
        {
            File.AppendAllText(@"D:\Log.txt", "[" + DateTime.Now.ToString("F") + "]   Online: " + online.ToString() + "\n");
        }
    }
}
