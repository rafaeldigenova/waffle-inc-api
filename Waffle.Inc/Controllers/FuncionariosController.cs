using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Waffle.Inc.Contracts.Models;
using Waffle.Inc.Domain;
using Waffle.Inc.Services;

namespace Waffle.Inc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class FuncionariosController : ControllerBase
    {
        private readonly ILogger<FuncionariosController> _logger;
        private readonly FuncionariosService _funcionariosService;

        public FuncionariosController(ILogger<FuncionariosController> logger,
            FuncionariosService funcionariosService)
        {
            _logger = logger;
            _funcionariosService = funcionariosService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var funcionarios = _funcionariosService.ListarFuncionarios();

            var response = new ResponseBaseModel<IEnumerable<FuncionarioModel>>(true, "", funcionarios);

            return Ok(response);
        }

        [HttpGet("email")]
        public IActionResult GetDisponibilidadeEmail(int id, string email)
        {
            var emailDisponivel = _funcionariosService.ValidarDisponibilidadeEmail(id, email);

            var response = new ResponseBaseModel<bool>(true, "", emailDisponivel);

            return Ok(response);
        }

        [HttpGet("numeroChapa")]
        public IActionResult GetDisponibilidadeNumeroChapa(int id, string numeroChapa)
        {
            var numeroChapaDisponivel = _funcionariosService.ValidarDisponibilidadeNumeroChapa(id, numeroChapa);

            var response = new ResponseBaseModel<bool>(true, "", numeroChapaDisponivel);

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Post([FromBody]FuncionarioModel funcionarioModel)
        {
            if (funcionarioModel.Id.HasValue && funcionarioModel.Id.Value != 0)
            {
                var responseBadRequest = new ResponseBaseModel<FuncionarioModel>(
                    false,
                    "Body inválido para cadastro",
                    funcionarioModel);
                return BadRequest(responseBadRequest);
            }

            if (funcionarioModel.Role != "Admin" && funcionarioModel.Role != "Funcionario")
            {
                var responseBadRequest = new ResponseBaseModel<FuncionarioModel>(
                    false,
                    "Role inválido para cadastro, por favor informe 'Admin' ou 'Funcionario'",
                    funcionarioModel);
                return BadRequest(responseBadRequest);
            }

            if (funcionarioModel.LiderId.HasValue && funcionarioModel.LiderId != 0)
            {
                var funcionarioLiderEncontrado = _funcionariosService.VerificarExistenciaDoUsuario(funcionarioModel.LiderId.Value);

                if (!funcionarioLiderEncontrado)
                {
                    var responseLiderNaoEncontrado = new ResponseBaseModel<FuncionarioModel>(
                        false,
                        $"O Líder selecionado com o Id {funcionarioModel.LiderId.Value} não foi encontrado",
                        funcionarioModel);
                    return NotFound(responseLiderNaoEncontrado);
                }
            }

            var funcionario = _funcionariosService.CadastrarFuncionario(funcionarioModel);

            var response = new ResponseBaseModel<FuncionarioModel>(
               true,
               $"Usuário cadastrado com sucesso!",
               funcionario);

            return Created($"/funcionarios/{funcionario.Id}", response);
        }
        
        [HttpPut]
        public IActionResult Put([FromBody]FuncionarioModel funcionarioModel)
        {
            if (!funcionarioModel.Id.HasValue || funcionarioModel.Id == 0 || !ModelState.IsValid 
                || funcionarioModel.Id == funcionarioModel.LiderId)
            {
                var responseBadRequest = new ResponseBaseModel<FuncionarioModel>(
                    false,
                    "Body inválido para update",
                    funcionarioModel);
                return BadRequest(responseBadRequest);
            }

            if (funcionarioModel.Role != "Admin" && funcionarioModel.Role != "Funcionario")
            {
                var responseBadRequest = new ResponseBaseModel<FuncionarioModel>(
                    false,
                    "Role inválido para cadastro, por favor informe 'Admin' ou 'Funcionario'",
                    funcionarioModel);
                return BadRequest(responseBadRequest);
            }

            var funcionarioEncontrado = _funcionariosService.VerificarExistenciaDoUsuario(funcionarioModel.Id.Value);
            
            if (!funcionarioEncontrado)
            {
                var responseNaoEncontrado = new ResponseBaseModel<FuncionarioModel>(
                    false,
                    $"Usuário com o Id {funcionarioModel.Id.Value} não encontrado",
                    funcionarioModel);
                return NotFound(responseNaoEncontrado);
            }

            if (funcionarioModel.LiderId.HasValue && funcionarioModel.LiderId != 0)
            {
                var funcionarioLiderEncontrado = _funcionariosService.VerificarExistenciaDoUsuario(funcionarioModel.LiderId.Value);

                if (!funcionarioLiderEncontrado)
                {
                    var responseLiderNaoEncontrado = new ResponseBaseModel<FuncionarioModel>(
                        false,
                        $"O Líder selecionado com o Id {funcionarioModel.LiderId.Value} não foi encontrado",
                        funcionarioModel);
                    return NotFound(responseLiderNaoEncontrado);
                }
            }

            var funcionario = _funcionariosService.AtualizarFuncionario(funcionarioModel);

            var response = new ResponseBaseModel<FuncionarioModel>(
                true,
                $"Usuário atualizado com sucesso!",
                funcionario);
            return Ok(response);
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var funcionario = _funcionariosService.ObterFuncionarioPorId(id);

            if (funcionario == null)
            {
                var responseNaoEncontrado = new ResponseBaseModel<int>(
                    false,
                    $"Funcionario com o Id {id} não encontrado",
                    id);
                return NotFound(responseNaoEncontrado);
            }

            var ehLiderDeOutroFuncionario = _funcionariosService.VerificarSeFuncionarioEhLider(id);

            if (ehLiderDeOutroFuncionario)
            {
                var responseLider = new ResponseBaseModel<int>(
                    false,
                    $"Não é possível deletar o funcionário pois o mesmo é Líder de outros ",
                    id);
                return Conflict(responseLider);
            }

            _funcionariosService.RemoverFuncionario(funcionario);
            
            var response = new ResponseBaseModel<int>(
                    true,
                    $"Funcionário deletado.",
                    id);
            return Ok(response);
        }
    }
}
