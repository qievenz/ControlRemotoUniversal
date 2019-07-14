using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ControlRemotoUniversal.Models;
using Microsoft.Extensions.Configuration;
using System.Collections;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace ControlRemotoUniversal.Controllers
{
    public class HomeController : Controller
    {
        readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
 
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult check(string button)
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                if (button.Split(" ").Length == 2)
                {
                    string dispositivo = button.Split(" ")[0];
                    string accion = button.Split(" ")[1];

                    // Enviar señal
                    //irsend SEND_ONCE <DISPOSITIVO> <ACCION>
                    string parametro = "SEND_ONCE " + dispositivo + " " + accion;    
                    run_cmd("irsend", parametro);

                    if (dispositivo == "WW_RCH-3218ND")
                    {
                        // Guardar temperatura
                        //python DHT11.py <DISPOSITIVO> <ACCION>
                        //sudo runuser -l www-data -c "/usr/bin/python /var/www/ControlRemotoUniversal/DHT11.py WW_RCH-3218ND AUTO_OFF" -s /bin/sh
                        //parametro = "/usr/bin/python /var/www/ControlRemotoUniversal/DHT11.py " + dispositivo + " " + accion;
                        string python = _configuration.GetSection("PythonConfig").GetSection("Python").Value;
                        string dht12 = _configuration.GetSection("PythonConfig").GetSection("DHT12").Value;
                        parametro = python + " " + dht12 + " " + dispositivo + " " + accion;
                        
                        Console.WriteLine(parametro);
                        run_cmd("sudo", parametro);
                    }
                }           
            }
            catch (Exception e)
            {
                Console.WriteLine("check: " + button + ", Exception Handler: {0}", e.ToString());
            }
            return PartialView("Index");
        }

        public string run_cmd(string exe, string args)
        {
            Process process = new System.Diagnostics.Process();
            process.StartInfo = new ProcessStartInfo(exe,  args){
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true
            };

            process.Start();
            string DHT11stderr = process.StandardError.ReadToEnd();
            Console.WriteLine(DHT11stderr);
            string DHT11result = process.StandardOutput.ReadToEnd();
            Console.WriteLine(DHT11result);
            return DHT11result;
        }

    }
}
