using System;
using System.Collections.Generic;
using System.IO;
using Full_Ponto_Digital.Models;

namespace Full_Ponto_Digital.Repository
{
    public class UsuarioRepository 
    {
        public static uint CONT = 0;
        private const string PATH = "DataBase/Cliente.csv";
        private const string PATH_INDEX = "DataBase/Cliente_Id.csv";
        private List<Usuario> clientes = new List<Usuario> ();
        
        public UsuarioRepository()
        {
            if (!File.Exists(PATH_INDEX)){
                File.Create(PATH_INDEX).Close();
            }

            var ultimoIndice = File.ReadAllText(PATH_INDEX);
            uint indice = 0;
            uint.TryParse(ultimoIndice, out indice);
            CONT = indice;
        }

        public bool Inserir (Usuario cliente) {
            CONT++;
            File.WriteAllText(PATH_INDEX, CONT.ToString());

            string linha = PrepararRegistroCSV (cliente);
            File.AppendAllText (PATH, linha);

            return true;
        }

        public bool Atualizar (Usuario cliente) {
            var clientesRecuperados = ObterRegistrosCSV (PATH);
            var clienteString = PrepararRegistroCSV (cliente);
            var linhaCliente = -1;
            var resultado = false;

            for (int i = 0; i < clientesRecuperados.Length; i++) {
                if (clienteString.Equals (clientesRecuperados[i])) {
                    linhaCliente = i;
                    resultado = true;
                }
            }
            if (linhaCliente >= 0) {
                clientesRecuperados[linhaCliente] = clienteString;
                File.WriteAllLines (PATH, clientesRecuperados);
            }

            return resultado;

        }

        public bool Apagar (ulong id) {

            var clientesRecuperados = ObterRegistrosCSV (PATH);
            var linhaCliente = -1;
            var resultado = false;

            for (int i = 0; i < clientesRecuperados.Length; i++) {
                if (id.Equals (clientesRecuperados[i])) {
                    linhaCliente = i;
                    resultado = true;
                }
            }

            if (linhaCliente >= 0) {
                clientesRecuperados[linhaCliente] = "";
                try {
                    File.WriteAllLines (PATH, clientesRecuperados);

                } catch(DirectoryNotFoundException dnfe) {
                    System.Console.WriteLine("Diretório não encontrado. Favor verificar.");
                } catch (PathTooLongException ptle) {
                    System.Console.WriteLine("Nome do arquivo é muito grande.");
                }
            }

            return resultado;
        }

        public Usuario ObterPor (ulong id) {

            foreach (var item in ObterRegistrosCSV (PATH)) {
                if (id.Equals (ExtrairCampo (id.ToString(), item))) {
                    return ConverterEmObjeto (item);
                }
            }
            return null;
        }

        public Usuario ObterPor (string email) {

            foreach (var item in ObterRegistrosCSV (PATH)) 
            {
                if (email.Equals (ExtrairCampo ("email", item))) 
                {
                    return ConverterEmObjeto (item);
                }
            }
            return null;
        }

        public List<Usuario> ListarTodos () {
            var linhas = ObterRegistrosCSV (PATH);
            foreach (var item in linhas) {

                Usuario cliente = ConverterEmObjeto (item);

                this.clientes.Add (cliente);
            }
            return this.clientes;
        }

        private Usuario ConverterEmObjeto (string registro) {

            Usuario cliente = new Usuario();
            System.Console.WriteLine("REGISTRO:" + registro);
            cliente.Nome = ExtrairCampo("nome", registro);
            cliente.Email = ExtrairCampo("email", registro);
            cliente.Senha = ExtrairCampo("senha", registro);
            cliente.DataNascimento = DateTime.Parse(ExtrairCampo("data", registro));

            return cliente;
        }

        private string PrepararRegistroCSV (Usuario cliente) {
            return $"id={CONT};nome={cliente.Nome};email={cliente.Email};senha={cliente.Senha};data={cliente.DataNascimento};{cliente.UrlFoto}\n";
        }
        protected string[] ObterRegistrosCSV (string PATH) {
            return File.ReadAllLines (PATH);
        }

        protected string ExtrairCampo (string nomeCampo, string linha) {
            var chave = nomeCampo;
            var indiceChave = linha.IndexOf(chave);
            var indiceTerminal = linha.IndexOf(";", indiceChave);
            var valor = "";
            
            if (indiceTerminal != -1) {
                valor = linha.Substring(indiceChave, indiceTerminal - indiceChave);
            } else {
                valor = linha.Substring(indiceChave);
            }
            
            
            System.Console.WriteLine($"Campo[{nomeCampo}] e valor {valor}");
            return valor.Replace(nomeCampo + "=", "");
        }
    }
}