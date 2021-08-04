using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Waffle.Inc.Domain;

namespace Waffle.Inc.Repositories
{
    public class FuncionariosRepository
    {
        public WaffleIncDbContext _waffleIncDbContext { get; set; }

        public FuncionariosRepository(WaffleIncDbContext waffleIncDbContext)
        {
            _waffleIncDbContext = waffleIncDbContext;
        }

        public virtual bool ValidarDisponibilidadeNumeroChapa(int id, string numeroChapa)
        {
            return !_waffleIncDbContext.Funcionarios.Any(funcionario =>
                funcionario.NumeroChapa == numeroChapa &&
                (id == 0 || funcionario.Id != id));
        }

        public virtual bool ValidarDisponibilidadeEmail(int id, string email)
        {
            return !_waffleIncDbContext.Funcionarios.Any(funcionario => 
                funcionario.Email == email &&
                (id == 0 || funcionario.Id != id));
        }

        public virtual Funcionario ObterFuncionariosPorCredenciais(string email, string senha)
        {
            return _waffleIncDbContext.Funcionarios.FirstOrDefault(funcionario => 
                funcionario.Email == email && funcionario.Senha == senha);
        }

        public virtual bool RemoverFuncionario(Funcionario funcionario)
        {
            _waffleIncDbContext.Funcionarios.Remove(funcionario);
            _waffleIncDbContext.SaveChanges();

            return true;
        }

        public virtual Funcionario ObterFuncionarioPorId(int id)
        {
            return _waffleIncDbContext.Funcionarios
                .Include(funcionario => funcionario.Telefones)
                .FirstOrDefault(funcionario => funcionario.Id == id);
        }

        public virtual bool VerificarSeFuncionarioEhLider(int id)
        {
            return _waffleIncDbContext.Funcionarios.Any(funcionario => funcionario.LiderId == id);
        }

        public virtual bool VerificarExistenciaDoUsuario(int id)
        {
            return _waffleIncDbContext.Funcionarios.Any(funcionario => funcionario.Id == id);
        }

        public virtual IEnumerable<Funcionario> ListarFuncionarios()
        {
            return _waffleIncDbContext.Funcionarios
                .Include(funcionario => funcionario.Telefones)
                .ToList();
        }

        public virtual Funcionario CadastrarFuncionario(Funcionario funcionario)
        {
            _waffleIncDbContext.Funcionarios.Add(funcionario);
            _waffleIncDbContext.SaveChanges();
            return funcionario;
        }

        public virtual Funcionario AtualizarFuncionario(Funcionario funcionario)
        {
            var telefones = _waffleIncDbContext.Telefones.Where(telefone => telefone.FuncionarioId == funcionario.Id);
            _waffleIncDbContext.Telefones.RemoveRange(telefones);

            _waffleIncDbContext.Funcionarios.Update(funcionario);
            _waffleIncDbContext.SaveChanges();
            return funcionario;
        }
    }
}
