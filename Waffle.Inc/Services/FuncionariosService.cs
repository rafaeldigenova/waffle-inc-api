using System.Collections.Generic;
using System.Linq;
using Waffle.Inc.Adapters;
using Waffle.Inc.Contracts.Models;
using Waffle.Inc.Domain;
using Waffle.Inc.Models;
using Waffle.Inc.Repositories;

namespace Waffle.Inc.Services
{
    public class FuncionariosService
    {
        public FuncionariosRepository _funcionariosRepository { get; set; }
        public CryptoService _cryptoService { get; set; }

        public FuncionariosService(FuncionariosRepository funcionariosRepository,
            CryptoService cryptoService)
        {
            _funcionariosRepository = funcionariosRepository;
            _cryptoService = cryptoService;
        }

        public virtual bool ValidarDisponibilidadeNumeroChapa(int? id, string numeroChapa)
        {
            return _funcionariosRepository.ValidarDisponibilidadeNumeroChapa(id ?? 0, numeroChapa);
        }

        public virtual bool ValidarDisponibilidadeEmail(int? id, string email)
        {
            return _funcionariosRepository.ValidarDisponibilidadeEmail(id ?? 0, email);
        }

        public virtual Funcionario ObterFuncionariosPorCredenciais(LoginRequestModel loginViewModel)
        {
            var senhaCryptografada = _cryptoService.Encrypt(loginViewModel.Senha);
            return _funcionariosRepository.ObterFuncionariosPorCredenciais(loginViewModel.Email, senhaCryptografada);
        }

        public virtual Funcionario ObterFuncionarioPorId(int id)
        {
            return _funcionariosRepository.ObterFuncionarioPorId(id);
        }

        public virtual bool VerificarExistenciaDoUsuario(int id)
        {
            return _funcionariosRepository.VerificarExistenciaDoUsuario(id);
        }

        public virtual bool RemoverFuncionario(Funcionario funcionario)
        {
            return _funcionariosRepository.RemoverFuncionario(funcionario);
        }

        public virtual bool VerificarSeFuncionarioEhLider(int id)
        {
            return _funcionariosRepository.VerificarSeFuncionarioEhLider(id);
        }

        public virtual IEnumerable<FuncionarioModel> ListarFuncionarios()
        {
            var funcionarios = _funcionariosRepository.ListarFuncionarios();
            return funcionarios.Select(funcionario => funcionario.ToModel());
        }

        public virtual FuncionarioModel CadastrarFuncionario(FuncionarioModel funcionarioModel)
        {
            var funcionarioEntity = funcionarioModel.ToEntity();
            funcionarioEntity.Senha = _cryptoService.Encrypt(funcionarioEntity.Senha);
            var funcionario = _funcionariosRepository.CadastrarFuncionario(funcionarioEntity);
            return funcionario.ToModel();
        }

        public virtual FuncionarioModel AtualizarFuncionario(FuncionarioModel funcionarioModel)
        {
            var funcionarioEntity = _funcionariosRepository.ObterFuncionarioPorId(funcionarioModel.Id.Value);
            funcionarioEntity.Update(funcionarioModel);

            funcionarioEntity.Senha = _cryptoService.Encrypt(funcionarioEntity.Senha);

            if (funcionarioModel.LiderId.HasValue && funcionarioModel.LiderId != 0)
            {
                var funcionarioLiderEntity = _funcionariosRepository.ObterFuncionarioPorId(funcionarioModel.LiderId.Value);
                funcionarioEntity.LiderId = funcionarioModel.LiderId;
                funcionarioEntity.LiderNome = funcionarioLiderEntity.Nome;
            }

            var funcionario = _funcionariosRepository.AtualizarFuncionario(funcionarioEntity);
            return funcionario.ToModel();
        }
    }
}
