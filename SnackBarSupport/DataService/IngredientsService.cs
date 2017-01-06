using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Dto;
using SnackBarSupport.DataService.IDataService;

namespace SnackBarSupport.DataService
{
    public class IngredientsService : IIngredientsService
    {
        public Task Add(Ingredient ingredient)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Ingredient> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ingredient>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Update(Ingredient ingredient)
        {
            throw new NotImplementedException();
        }
    }
}