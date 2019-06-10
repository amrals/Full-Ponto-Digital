using System;
using Full_Ponto_Digital.Models;
using Full_Ponto_Digital.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Full_Ponto_Digital.Controllers
{
    public class CadastroController : Controller
    {
        private UsuarioRepository clienteRepositorio = new UsuarioRepository();
        public IActionResult Index()
        {
            ViewData["NomeView"] = "Cadastro";
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar (IFormCollection form)
        {
            Usuario cliente = new Usuario();
            cliente.Nome = form["nome"];
            cliente.Senha = form["senha"];
            cliente.Email = form["email"];
            cliente.DataNascimento = DateTime.Parse(form["data"]);

            clienteRepositorio.Inserir(cliente);

            return RedirectToAction("Index", "Home");
        }
    }
}