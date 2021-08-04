using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Waffle.Inc.Domain;
using Waffle.Inc.Models;
using Waffle.Inc.Services;
using Waffle.Inc.Settings;

namespace Waffle.Inc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private FuncionariosService _funcionariosService;
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;

        public LoginController(FuncionariosService funcionariosService,
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations)
        {
            _funcionariosService = funcionariosService;
            _tokenConfigurations = tokenConfigurations;
            _signingConfigurations = signingConfigurations;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Post([FromBody] LoginRequestModel loginRequestModel)
        {
            var usuario = _funcionariosService.ObterFuncionariosPorCredenciais(loginRequestModel);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(DoLogin(usuario));
        }

        private LoginResponseModel DoLogin(Funcionario funcionario)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(funcionario.Id.ToString(), "Id"),
                new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, funcionario.Email),
                    new Claim(ClaimTypes.Role, funcionario.Role)
                }
            );

            var response = new LoginResponseModel(true, _tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = response.DataCriacao,
                Expires = response.DataExpiracao
            });

            response.SetRole(funcionario.Role);
            response.SetToken(handler.WriteToken(securityToken));

            return response;
        }
    }
}
