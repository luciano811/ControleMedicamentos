using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.ModuloPaciente;
using ControleMedicamentos.ConsoleApp.ModuloMedicamento;



namespace ControleMedicamentos.ConsoleApp.ModuloRequisicao
{
    public class Requisicao : EntidadeBase
    {
        private string _titulo;
        private string _descricao;
        private DateTime _dataAbertura;
        public Medicamento medicamento;
        public Paciente paciente;


        public Requisicao(string titulo, string descricao, DateTime dataAbertura)
        {
            _titulo = titulo;
            _descricao = descricao;
            _dataAbertura = dataAbertura;
        }

        public string Titulo { get => _titulo; }
        public string Descricao { get => _descricao; }
        public DateTime DataAbertura { get => _dataAbertura; }

       

        public override string ToString()
        {
            return "Dados da Requisição\nId: " + id + Environment.NewLine +
                "Titulo: " + Titulo + Environment.NewLine +
                "Descrição: " + Descricao + Environment.NewLine +
                "Data da abertura: " + DataAbertura + Environment.NewLine;
        }
    }
}
