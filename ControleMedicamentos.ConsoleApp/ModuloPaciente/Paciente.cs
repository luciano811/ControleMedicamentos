using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.ConsoleApp.ModuloPaciente
{
    public class Paciente : EntidadeBase
    {
        private readonly string _nome;
        private readonly string _cpf;

        public string Nome { get => _nome; }
        public string Cpf { get => _cpf; }


        public Paciente(string nome, string cpf)
        {
            _nome = nome;
            _cpf = cpf;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Nome: " + Nome + Environment.NewLine + 
                "Cpf: " + Cpf + Environment.NewLine; 
        }
    }
}
