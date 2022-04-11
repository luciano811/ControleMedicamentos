using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;

namespace ControleMedicamentos.ConsoleApp.ModuloMedicamento
{
    public class TelaCadastroMedicamento : TelaBase, ITelaCadastravel
    {
        private readonly RepositorioMedicamento _repositorioMedicamento;
        private readonly Notificador _notificador;

        public TelaCadastroMedicamento(RepositorioMedicamento repositorioMedicamento, Notificador notificador)
            : base("Cadastro de Medicamentoes")
        {
            _repositorioMedicamento = repositorioMedicamento;
            _notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Medicamento");

            Medicamento novoMedicamento = ObterMedicamento();

            _repositorioMedicamento.Inserir(novoMedicamento);

            _notificador.ApresentarMensagem("Medicamento cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Medicamento");

            bool temMedicamentoesCadastrados = VisualizarRegistros("Pesquisando");

            if (temMedicamentoesCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum Medicamento cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroMedicamento = ObterNumeroRegistro();

            Medicamento MedicamentoAtualizado = ObterMedicamento();

            bool conseguiuEditar = _repositorioMedicamento.Editar(numeroMedicamento, MedicamentoAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Medicamento editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Medicamento");

            bool temMedicamentoesRegistrados = VisualizarRegistros("Pesquisando");

            if (temMedicamentoesRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum Medicamento cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroMedicamento = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioMedicamento.Excluir(numeroMedicamento);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Medicamento excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Medicamentoes");

            List<Medicamento> medicamentoes = _repositorioMedicamento.SelecionarTodos();

            if (medicamentoes.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum medicamento disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Medicamento medicamento in medicamentoes)
            {
                if (medicamento.Quantidade < 5)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("O medicamento abaixo está em baixa quantidade! (Menos que 5 caixas)");
                    Console.ResetColor();
                }                  

                Console.WriteLine(medicamento.ToString());
            }
            Console.ReadLine();

            return true;
        }

        //public void VisualizarMedicamentosEmFalta()
        //{
        //    MostrarTitulo("Exibindo Medicamentos com pouca quantidade");

        //    bool temMedicamentoesRegistrados = VisualizarRegistros("Pesquisando");

        //    if (temMedicamentoesRegistrados == false)
        //    {
        //        _notificador.ApresentarMensagem("Nenhum Medicamento encontrado - isto é, todos estão em falta hehehe.", TipoMensagem.Erro);
        //        return;
        //    }

        //    int numeroMedicamento = ObterNumeroRegistro();

        //    bool conseguiuExcluir = _repositorioMedicamento.Excluir(numeroMedicamento);

        //    if (!conseguiuExcluir)
        //        _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
        //    else
        //        _notificador.ApresentarMensagem("Medicamento excluído com sucesso!", TipoMensagem.Sucesso);
        //}

        private Medicamento ObterMedicamento()
        {
            Console.WriteLine("Digite o nome do medicamento: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite a descrição do medicamento: ");
            string descricao = Console.ReadLine();

            Console.WriteLine("Digite a quantidade de medicamento: ");
            int quantidade = Convert.ToInt32(Console.ReadLine());

            

            return new Medicamento(nome, descricao, quantidade);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do medicamento em questão: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioMedicamento.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do medicamento não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
