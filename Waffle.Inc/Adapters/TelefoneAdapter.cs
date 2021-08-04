using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Waffle.Inc.Contracts.Models;
using Waffle.Inc.Domain;

namespace Waffle.Inc.Adapters
{
    public static class TelefoneAdapter
    {
        public static Telefone ToEntity(this TelefoneModel telefoneViewModel)
        {
            return new Telefone()
            {
                CodigoDeArea = telefoneViewModel.CodigoDeArea,
                Numero = telefoneViewModel.Numero
            };
        }

        public static List<Telefone> ToListOfEntities(this IEnumerable<TelefoneModel> telefonesViewModels)
        {
            return telefonesViewModels.Select(telefoneViewModel => telefoneViewModel.ToEntity()).ToList();
        }

        public static TelefoneModel ToModel(this Telefone telefone)
        {
            return new TelefoneModel()
            {
                CodigoDeArea = telefone.CodigoDeArea,
                Numero = telefone.Numero
            };
        }

        public static IEnumerable<TelefoneModel> ToListOfModels(this IEnumerable<Telefone> telefones)
        {
            if (telefones == null || !telefones.Any())
            {
                return null;
            }

            return telefones.Select(telefone => telefone.ToModel());
        }
    }
}
