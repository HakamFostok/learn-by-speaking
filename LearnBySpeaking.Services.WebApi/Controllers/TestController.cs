using LearnBySpeaking.Application.Interfaces;
using LearnBySpeaking.Application.ViewModels;
using LearnBySpeaking.Domain.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnBySpeaking.Services.WebApi.Controllers
{
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
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListTests()
        {
            List<TestViewModel> models = new List<TestViewModel>
            {
                new TestViewModel
                {
                    Topic = new TopicViewModel
                    {
                        Title = "Title 1"
                    },
                    CreatedTime = DateTime.Now,
                    Id = 1
                },
                new TestViewModel
                {
                    Topic = new TopicViewModel
                    {
                        Title = "Title 2"
                    },
                    CreatedTime = DateTime.Now,
                    Id = 2
                }
            };
            var result = await _testAppService.GetAllAsync();
            var list = result.ToList();
            return View(models);
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EvaluateTest(EvaluateTestViewModel model)
        {
            return View();
        }

        public IActionResult Error(string message)
        {
            return View(new ErrorViewModel { Message = message });
        }
    }

}
