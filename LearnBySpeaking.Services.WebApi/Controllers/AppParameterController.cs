using LearnBySpeaking.Application.Interfaces;
using LearnBySpeaking.Application.ViewModels;
using LearnBySpeaking.Domain.Events;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LearnBySpeaking.Services.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AppParameterController : BaseApiController<AppParameterViewModel, AppParameterEventModel>
    {
        private readonly IAppParameterAppService _appParameterAppService;

        public AppParameterController(IAppParameterAppService appParameterAppService)
            : base(appParameterAppService)
        {
            _appParameterAppService = appParameterAppService;
        }

        [HttpPost("CreateBundleAsync")]
        public async Task<IActionResult> CreateBundleAsync(bool throwException)
        {
            await _appParameterAppService.CreateBundleAsync(throwException);

            return Ok();
        }

        [HttpGet("GetParameterByName/{paramName}")]
        public async Task<IActionResult> GetParameterByName(string paramName)
        {
            AppParameterViewModel result = await _appParameterAppService.GetByNameAsync(paramName);

            return Json(result);
        }

        [HttpGet("ThrowException")]
        public IActionResult ThrowException()
        {
            throw new Exception("this exception has been throw for testing the NLog",
                new Exception("Inner Exception tested"));
        }
    }
}