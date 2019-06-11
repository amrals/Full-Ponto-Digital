using Full_Ponto_Digital.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Full_Ponto_Digital.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioRepository usuarioRepositorio = new UsuarioRepository();
        private string SESSION_EMAIL = "_EMAIL";
        private string SESSION_CLIENTE = "_NOME";

        [HttpPost]
        public IActionResult Login(IFormCollection form)
        {
            var usuario = form["email"];
            var senha = form["senha"];
            var cliente = usuarioRepositorio.ObterPor(usuario);
            if (cliente != null && cliente.Email.Equals(usuario) && cliente.Senha.Equals(senha))
            {
                HttpContext.Session.SetString(SESSION_EMAIL, usuario);
                HttpContext.Session.SetString(SESSION_CLIENTE, cliente.Nome);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SESSION_EMAIL);
            HttpContext.Session.Remove(SESSION_CLIENTE);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}