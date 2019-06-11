using Full_Ponto_Digital.Repository;
using Full_Ponto_Digital.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Full_Ponto_Digital.Controllers
{
    public class PlanoController : Controller
    {
        PlanoRepository planoRepositorio = new PlanoRepository();

        [HttpGet]
        public IActionResult Index()
        {
            var planos = planoRepositorio.Listar();

            PlanoViewModel plano = new PlanoViewModel();
            plano.Planos = planos;

            return View(plano);  
        }
    }
}