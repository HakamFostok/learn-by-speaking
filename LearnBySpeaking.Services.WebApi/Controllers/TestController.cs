﻿using LearnBySpeaking.Application.Interfaces;
using LearnBySpeaking.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearnBySpeaking.Services.WebApi.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private readonly ITopicAppService _topicAppService;
        private readonly ITestAppService _testAppService;

        public TestController(
            ITopicAppService topicAppService,
            ITestAppService testAppService)
        {
            _topicAppService = topicAppService;
            _testAppService = testAppService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateTest()
        {
            CreateTestViewModel model = new CreateTestViewModel();
            model.Topics = (await _topicAppService.GetAllAsync()).ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTest(CreateTestViewModel model)
        {
            await _testAppService.CreateTest(model);

            return RedirectToAction("ListTests", "Test");
        }

        [HttpGet]
        public async Task<IActionResult> ListTests()
        {
            var result = await _testAppService.GetAllAsync();
            var list = result.ToList();
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTest(int id)
        {
            try
            {
                await _testAppService.DeleteAsync(id);

                return RedirectToAction("ListTests", "Test");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Test", new { ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> TakeTest(int id)
        {
            var result = await _testAppService.GetByIdAsync(id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> TakeTest([FromBody] EvaluateTest model)
        {
            var result = await _testAppService.TakeTest(model);
            return Json(result);

        }

        public IActionResult Error(string message)
        {
            return View(new ErrorViewModel { Message = message });
        }
    }

}
