using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;

namespace ControleMedicamentos.ConsoleApp.ModuloMedicamento
{
    public class Medicamento : EntidadeBase
    {
        public Medicamento(string nome, string descricao, int quantidade)
        {
            Nome = nome;
            Descricao = descricao;
            Quantidade = quantidade;
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
       

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Nome: " + Nome + Environment.NewLine +
                "Descricao: " + Descricao + Environment.NewLine +
                "Quantidade: " + Quantidade + Environment.NewLine;
        }

    }
}
