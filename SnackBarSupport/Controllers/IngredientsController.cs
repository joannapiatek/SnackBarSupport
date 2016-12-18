﻿using System.Web.Http;
using SnackBarSupport.DAL;
using SnackBarSupport.Dto.Dto;

namespace SnackBarSupport.Controllers
{
    [RoutePrefix("api/ingredients")]
    public class IngredientsController : GenericRepository<Ingredient>
    {
    }
}
