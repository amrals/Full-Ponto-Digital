using System.Collections.Generic;
using System.IO;
using Full_Ponto_Digital.Models;

namespace Full_Ponto_Digital.Repository
{
    public class PlanoRepository
    {
        private const string PATH = "DataBase/Plano.csv";
        List<Plano> Planos = new List<Plano>();
        public List<Plano> Listar(){
            var registros = File.ReadAllLines(PATH);
            foreach (var item in registros)
            {
                var valores= item.Split(";");
                Plano plano = new Plano();
                plano.Id = int.Parse(valores[0]);
                plano.Nome = valores[1];
                plano.Preco = float.Parse(valores[2]);

                this.Planos.Add(plano);
            }
            return this.Planos;
        }
    }
}