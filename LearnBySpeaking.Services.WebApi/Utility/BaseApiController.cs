using LearnBySpeaking.Application.Interfaces;
using LearnBySpeaking.Application.ViewModels;
using LearnBySpeaking.Domain.Core;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LearnBySpeaking.Services.WebApi
{
    public abstract class BaseApiController<TViewModel, TEventModel> : ControllerBase
        where TViewModel : BaseViewModel
        where TEventModel : Event
    {
        private readonly IBaseAppService<TViewModel, TEventModel> _baseAppService;

        protected BaseApiController(IBaseAppService<TViewModel, TEventModel> baseAppService)
        {
            _baseAppService = baseAppService;
        }

        protected JsonResult Json(object value)
        {
            return new JsonResult(value);
        }

        #region Query Operations

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            TViewModel result = await _baseAppService.GetByIdAsync(id);
            return Json(result);
        }

        #endregion

        #region CRUD operations

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] TViewModel tEntity)
        {
            await _baseAppService.CreateAsync(tEntity);
            return Ok();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] TViewModel tEntity)
        {
            await _baseAppService.UpdateAsync(tEntity);

            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _baseAppService.RemoveAsync(id);

            return Ok();
        }

        #endregion

        #region Event operations

        [HttpGet("AllEventModel")]
        public async Task<IActionResult> AllEventModel()
        {
            System.Linq.IQueryable<TEventModel> result = await _baseAppService.GetAllEventAsync();
            return Json(result);
        }

        [HttpGet("AllEventModelByRefKey/{refKey}")]
        public async Task<IActionResult> AllEventModelByRefKey(int refKey)
        {
            System.Linq.IQueryable<TEventModel> result = await _baseAppService.GetAllEventByRefKeyAsync(refKey);
            return Json(result);
        }

        [HttpGet("AllEventModelByMongoId/{mongoId}")]
        public async Task<IActionResult> AllEventModelByMongoId(string mongoId)
        {
            System.Linq.IQueryable<TEventModel> result = await _baseAppService.GetAllEventByObjectIdAsync(mongoId);
            return Json(result);
        }

        #endregion
    }
}