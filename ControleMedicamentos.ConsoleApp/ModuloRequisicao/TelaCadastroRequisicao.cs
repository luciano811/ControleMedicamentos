using System;
using System.Collections.Generic;
using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleMedicamentos.ConsoleApp.ModuloPaciente;




namespace ControleMedicamentos.ConsoleApp.ModuloRequisicao
{
    public class TelaCadastroRequisicao : TelaBase, ITelaCadastravel
    {
        private RepositorioMedicamento repositorioMedicamento;
        private TelaCadastroMedicamento telaCadastroMedicamento;

        private RepositorioPaciente repositorioPaciente;
        private TelaCadastroPaciente telaCadastroPaciente;

        private RepositorioRequisicao repositorioRequisicao;
        private Notificador notificador;

        public TelaCadastroRequisicao(RepositorioRequisicao repositorioRequisicao, RepositorioMedicamento repositorioMedicamento,
                                      TelaCadastroMedicamento telaCadastroMedicamento, TelaCadastroPaciente telaCadastroPaciente, RepositorioPaciente repositorioPaciente, Notificador notificador) : base ("Cadastro de Requisicoes")
        {
            this.repositorioMedicamento = repositorioMedicamento;
            this.telaCadastroMedicamento = telaCadastroMedicamento;
            this.repositorioRequisicao = repositorioRequisicao;
            this.telaCadastroPaciente = telaCadastroPaciente;
            this.repositorioPaciente = repositorioPaciente;

            this.notificador = notificador;

        }
                

        public void Inserir()
        {
             MostrarTitulo("Inserindo nova Requisição ---- Aperte qualquer tecla para avançar as exibições\n\n\nMedicamentos Cadastrados:");

            Medicamento medicamentoSelecionado = ObterMedicamento();
            Console.WriteLine("\nPacientes Cadastrados: ");
            Paciente pacienteSelecionado = ObterPaciente();

            if (medicamentoSelecionado == null)
            {
                notificador.ApresentarMensagem("Cadastre um medicamento antes de cadastrar Requisicoes!", TipoMensagem.Atencao);
                return;
            }

            if (pacienteSelecionado == null)
            {
                notificador.ApresentarMensagem("Cadastre um paciente antes de cadastrar Requisicoes!", TipoMensagem.Atencao);
                return;
            }

            Requisicao novoRequisicao = ObterRequisicao();

            novoRequisicao.medicamento = medicamentoSelecionado;
            novoRequisicao.paciente = pacienteSelecionado;

            repositorioRequisicao.Inserir(novoRequisicao);

            notificador.ApresentarMensagem("Requisicao inserido com sucesso", TipoMensagem.Sucesso);
        }
         
        public void Editar()
        {
            MostrarTitulo("Editando Requisicao");

            bool temRequisicoesCadastrados = VisualizarRegistros("Pesquisando");

            if (temRequisicoesCadastrados == false)
            {
                notificador.ApresentarMensagem("Nenhum Requisicao cadastrado para poder editar", TipoMensagem.Atencao);
                return;
            }

            int numeroRequisicao = ObterNumeroRequisicao();

            Console.WriteLine();

            Medicamento medicamentoSelecionado = ObterMedicamento();

            Requisicao requisicaoAtualizado = ObterRequisicao();

            requisicaoAtualizado.medicamento = medicamentoSelecionado;

            //repositorioRequisicao.Editar(requisicaoAtualizado, numeroRequisicao);

            notificador.ApresentarMensagem("Requisicao editado com sucesso", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Requisicao");

            bool temRequisicoesCadastrados = VisualizarRegistros("Pesquisando");

            if (temRequisicoesCadastrados == false)
            {
                notificador.ApresentarMensagem(
                    "Nenhum Requisicao cadastrado para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int numeroRequisicao = ObterNumeroRequisicao();

            repositorioRequisicao.Excluir(numeroRequisicao);

            notificador.ApresentarMensagem("Requisicao excluído com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Requisicoes");

            List<Requisicao> requisicoes = repositorioRequisicao.SelecionarTodos();

            if (requisicoes.Count == 0)
                return false;

            for (int i = 0; i < requisicoes.Count; i++)
            {
                Requisicao requisicao = (Requisicao)requisicoes[i];

                Console.WriteLine("\nID: " + requisicao.id);
                Console.WriteLine("Título: " + requisicao.Titulo);
                Console.WriteLine("medicamento: " + requisicao.medicamento);
                Console.WriteLine("Data de Abertura: " + requisicao.DataAbertura);
                Console.WriteLine("Nr Dias requisicoes em Aberto: " + (DateTime.Now - requisicao.DataAbertura).Days);
            }

            Console.ReadKey();

            return true;
        }

        #region Métodos privados

        private Medicamento ObterMedicamento()
        {
            bool temMedicamentosDisponiveis = telaCadastroMedicamento.VisualizarRegistros("");

            if (!temMedicamentosDisponiveis)
            {
                notificador.ApresentarMensagem("Não há nenhum Medicamento disponível para cadastrar requisicoes", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o ID do Medicamento que irá inserir na Requisição: ");
            int numIdRequisicao = Convert.ToInt32(Console.ReadLine());

            Medicamento medicamentoSelecionado = (Medicamento)repositorioMedicamento.SelecionarRegistro(numIdRequisicao);

            return medicamentoSelecionado;
        }

        private Paciente ObterPaciente()
        {
            
            bool temPacientesDisponiveis = telaCadastroPaciente.VisualizarRegistros("");

            if (!temPacientesDisponiveis)
            {
                notificador.ApresentarMensagem("Não há nenhum Paciente disponível para cadastrar requisicoes", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o ID do Paciente que irá inserir na Requisição: ");
            int numIdPaciente = Convert.ToInt32(Console.ReadLine());

            Paciente pacienteSelecionado = (Paciente)repositorioPaciente.SelecionarRegistro(numIdPaciente);

            return pacienteSelecionado;
        }

        private Requisicao ObterRequisicao()
        {
            Console.Write("Digite o título da Requisição: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite a descrição: ");
            string descricao = Console.ReadLine();

            DateTime dataAbertura = DateTime.Now;
            Console.Write("Data de abertura: " + dataAbertura);

            Requisicao novoRequisicao = new Requisicao(titulo, descricao, dataAbertura);

            return novoRequisicao;
        }

        private int ObterNumeroRequisicao()
        {
            int numeroRequisicao;
            bool numeroRequisicaoEncontrado;

            do
            {
                Console.WriteLine("Digite o número do Requisicao que deseja editar: ");
                numeroRequisicao = int.Parse(Console.ReadLine());

                numeroRequisicaoEncontrado = repositorioRequisicao.ExisteRegistro(numeroRequisicao);

                if (!numeroRequisicaoEncontrado)
                    notificador.ApresentarMensagem("Número de Requisicao não encontrado, digite novamente", TipoMensagem.Atencao);
            } while (!numeroRequisicaoEncontrado);

            return numeroRequisicao;
        }

        #endregion

    }
}
