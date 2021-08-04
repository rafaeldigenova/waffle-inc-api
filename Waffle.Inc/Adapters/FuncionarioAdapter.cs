using Waffle.Inc.Contracts.Models;
using Waffle.Inc.Domain;

namespace Waffle.Inc.Adapters
{
    public static class FuncionarioAdapter
    {
        public static Funcionario ToEntity(this FuncionarioModel funcionarioViewModel)
        {
            return new Funcionario()
            {
                Id = funcionarioViewModel.Id ?? 0,
                Email = funcionarioViewModel.Email,
                LiderId = funcionarioViewModel.LiderId,
                LiderNome = funcionarioViewModel.LiderNome,
                Nome = funcionarioViewModel.Nome,
                Sobrenome = funcionarioViewModel.Sobrenome,
                Senha = funcionarioViewModel.Senha,
                NumeroChapa = funcionarioViewModel.NumeroChapa,
                Role = funcionarioViewModel.Role,
                Telefones = funcionarioViewModel.Telefones.ToListOfEntities()
            };
        }

        public static FuncionarioModel ToModel(this Funcionario funcionario)
        {
            return new FuncionarioModel()
            {
                Id = funcionario.Id,
                Email = funcionario.Email,
                LiderId = funcionario.LiderId,
                LiderNome = funcionario.LiderNome,
                Nome = funcionario.Nome,
                Sobrenome = funcionario.Sobrenome,
                Senha = funcionario.Senha,
                NumeroChapa = funcionario.NumeroChapa,
                Role = funcionario.Role,
                Telefones = funcionario.Telefones.ToListOfModels()
            };
        }

        public static Funcionario Update(this Funcionario funcionario, FuncionarioModel funcionarioModel)
        {
            funcionario.Email = funcionarioModel.Email;
            funcionario.Nome = funcionarioModel.Nome;
            funcionario.Sobrenome = funcionarioModel.Sobrenome;
            funcionario.Role = funcionarioModel.Role;
            funcionario.NumeroChapa = funcionarioModel.NumeroChapa;
            funcionario.Telefones = funcionarioModel.Telefones.ToListOfEntities();

            return funcionario;
        }
    }
}
