using Data.Payloads;
using BL;
using Data.Payloads;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaBodyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FoodsController : AlphaBodyControllerBase
    {
        public FoodsController()
        {
        }

        [HttpGet]
        [Route("kinds")]
        public ActionResult<List<FoodKindPayload>> GetFoodKinds()
        {
            var kinds = FoodsManager.Instance.GetFoodKinds();

            return Ok(kinds);
        }

        [HttpGet]
        [Route("kind/{kindId:int}")]
        public ActionResult<List<FoodPayload>> GetFoodsOfKind(int kindId)
        {
            var foods = FoodsManager.Instance.GetFoodOfKind(kindId);

            return OkOrBadRequest(foods);
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<List<FoodPayload>> GetAllFoods()
        {
            var foods = FoodsManager.Instance.GetAllFoods();

            return Ok(foods);
        }

        [HttpPut]
        [Route("add")]
        public ActionResult<int> AddNewFood([FromBody] FoodPayload newFood)
        {
            var id = FoodsManager.Instance.SaveNewFood(newFood);

            return Ok(id);
        }

        [HttpDelete]
        [Route("delete/{foodId:int}")]
        public ActionResult DeleteFood(int foodId)
        {
            var sucess = FoodsManager.Instance.DeleteFood(foodId);

            return OkOrBadRequest(sucess);
        }
    }
}
