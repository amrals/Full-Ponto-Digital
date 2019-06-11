using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Full_Ponto_Digital.Models;
using Full_Ponto_Digital.ViewModels;
using Full_Ponto_Digital.Repository;
using Microsoft.AspNetCore.Http;

namespace Full_Ponto_Digital.Controllers
{
    public class HomeController : Controller
    {
        PlanoRepository planoRepositorio = new PlanoRepository();
        private string SESSION_EMAIL = "_EMAIL";
        private string SESSION_CLIENTE = "_NOME";
        public IActionResult Index()
        {
            var planos = planoRepositorio.Listar();

            PlanoViewModel plano = new PlanoViewModel();
            plano.Planos = planos;

            ViewData["User"] = HttpContext.Session.GetString(SESSION_CLIENTE);
            ViewData["NomeView"] = "Home";
            return View(plano);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
    }
}
