using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Dto;
using MongoDB.Driver;
using SnackBarSupport.DataService.IDataService;
using SnackBarSupport.DAL;

namespace SnackBarSupport.DataService
{
    public class IngredientsService : IIngredientsService
    {
        protected MongoDbContext MongoDbContext;
        private IRestaurantsService _restaurantsService;
        private IRestaurantsService RestaurantsService
        {
            get { return _restaurantsService ?? (_restaurantsService = new RestaurantsService()); }
        }

        protected IngredientsService(MongoDbContext context)
        {
            MongoDbContext = context;
        }

        public IngredientsService()
        {
            MongoDbContext = new MongoDbContext();
        }

        public async Task AddAsync(IngredientDto entity)
        {
            await MongoDbContext
                .GetCollection<IngredientDto>()
                .InsertOneAsync(entity);

            await AddIngredientInRestaurants(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await MongoDbContext
                .GetCollection<IngredientDto>()
                .DeleteOneAsync(i => i.Id == id);

            await DeleteIngredientInRestaurants(id);
        }

        public async Task UpdateAsync(IngredientDto entity)
        {
            await MongoDbContext
                .GetCollection<IngredientDto>()
                .ReplaceOneAsync(i => i.Id == entity.Id, entity);

            await UpdateIngredientInRestaurants(entity);
        }

        public async Task<List<IngredientDto>> GetAllAsync()
        {
            var result = await MongoDbContext.GetCollection<IngredientDto>()
                .FindAsync(_ => true);

            return result.ToList();
        }

        public async Task<IngredientDto> GetAsync(string id)
        {
            return await MongoDbContext.GetCollection<IngredientDto>()
                .Find(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        private async Task AddIngredientInRestaurants(IngredientDto ingredient)
        {
            var restaurants = await RestaurantsService.GetAllAsync();
            foreach (var restaurantDto in restaurants)
            {
                restaurantDto.IngredientsCountDictionary.Add(ingredient, 0);
                await RestaurantsService.UpdateAsync(restaurantDto);
            }
        }

        private async Task DeleteIngredientInRestaurants(string ingredientId)
        {
            var restaurants = await RestaurantsService.GetAllAsync();
            var ingredient = new IngredientDto() {Id = ingredientId};

            foreach (var restaurantDto in restaurants)
            {
                if (!restaurantDto.IngredientsCountDictionary.ContainsKey(ingredient)) continue;

                restaurantDto.IngredientsCountDictionary.Remove(ingredient);
                await RestaurantsService.UpdateAsync(restaurantDto);
            }
        }
        private async Task UpdateIngredientInRestaurants(IngredientDto ingredient)
        {
            var restaurants = await RestaurantsService.GetAllAsync();
            foreach (var restaurantDto in restaurants)
            {
                var keys = restaurantDto.IngredientsCountDictionary.Keys;
                var chosenKey = keys.SingleOrDefault(k => k.Equals(ingredient));
                if (chosenKey == null || chosenKey.Name == ingredient.Name) continue;

                chosenKey.Name = ingredient.Name;
                await RestaurantsService.UpdateAsync(restaurantDto);
            }
        }
    }
}