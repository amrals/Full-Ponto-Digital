using System;
using Microsoft.AspNetCore.Http;

namespace Full_Ponto_Digital.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome {get;set;}
        public string Email {get;set;}
        public string Senha {get;set;}
        public DateTime DataNascimento {get;set;}
        public Plano Plano {get;set;}
        public string UrlFoto { get; set; }
        public IFormFile foto {get;set;}

        public Usuario(){
            
        }

        public Usuario(string nome, string email, string senha, DateTime dataNascimento){
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
            this.DataNascimento = dataNascimento;
        }
          public Usuario(int id, string nome, string email, string senha, DateTime dataNascimento, string foto){
            this.Id = id;
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
            this.DataNascimento = dataNascimento;
            this.UrlFoto = foto;
        }
    }
}