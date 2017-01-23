using System;
using System.Threading;
using Microsoft.Owin.Hosting;

namespace Api
{
    internal class Program
    {
        private const string BaseAddress = "http://*:5000";

        private static void Main(string[] args)
        {
            using (WebApp.Start<Startup>(url: BaseAddress))
            {
                Console.WriteLine("Application started...");
                var readVal = Console.ReadLine();
                while (string.IsNullOrEmpty(readVal))
                {
                    Thread.Sleep(5000);
                    readVal = Console.ReadLine();
                }
            }
        }
    }
}
