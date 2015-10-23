using Awesm.DataAccess;
using Awesm.Domain;
using Awesm.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Awesm.Controllers
{
    [RoutePrefix("api/food")]
    public class FoodDescriptionController : ApiController
    {
        private IFoodDescriptionRepository _repo;
        private IWorkUnitFactory _factory;

        public FoodDescriptionController()
        {
            _repo = new FoodDescriptionRepo();
            _factory = new WorkUnitFactory();
        }


        [Route("")]
        [HttpGet]
        // GET api/<controller>
        public async Task<IEnumerable<FoodDescription>> Get(string pattern = null)
        {
            var user = HttpContextFactory.Current.User;

            if (user == null)
            {
                throw new InvalidOperationException("No user context.");
            }

            using (var workUnit = _factory.CreateWorkUnit(user))
            {
                var list = await _repo.QueryFoodDescriptionsAsync(workUnit, pattern);
                return list;
            }
        }

        [Route("{id}")]
        [HttpPost]
        public async Task<IHttpActionResult> Update([FromBody] FoodDescription description,
            [FromUri] string id)
        {
            if (description == null || description.Id != id)
            {
                return BadRequest();
            }

            try
            {
                using (var workUnit = _factory.CreateWorkUnit(HttpContextFactory.Current.User))
                {
                    var result = await _repo.UpdateOrAddFoodDescriptionAsync(workUnit, description);
                    await workUnit.CommitAsync();
                    return Ok(result);
                }
            }
            catch
            {
                return InternalServerError();
            }
        }

        [Route("")]
        [HttpPut]
        public async Task<IHttpActionResult> Insert([FromBody] FoodDescription description)
        {
            if (description == null || string.IsNullOrWhiteSpace(description.Id))
            {
                return BadRequest();
            }

            try
            {
                using (var workUnit = _factory.CreateWorkUnit(HttpContextFactory.Current.User))
                {
                    var result = await _repo.UpdateOrAddFoodDescriptionAsync(workUnit, description);
                    await workUnit.CommitAsync();
                    return Ok(result);
                }
            }
            catch
            {
                return InternalServerError();
            }
        }


    }
}