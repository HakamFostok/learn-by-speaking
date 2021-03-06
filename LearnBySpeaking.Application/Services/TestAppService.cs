﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using LearnBySpeaking.Application.Interfaces;
using LearnBySpeaking.Application.ViewModels;
using LearnBySpeaking.Domain.Interfaces.EntityInterfaces;
using LearnBySpeaking.Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearnBySpeaking.Application.Services
{
    public class TestAppService : ITestAppService
    {
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;

        public TestAppService(IMapper mapper,
                              ITestRepository testRepository)
        {
            _mapper = mapper;
            _testRepository = testRepository;
        }

        public async Task CreateTest(CreateTestViewModel model)
        {
            Test test = new Test
            {
                TopicId = model.TopicId,
                CreatedTime = DateTime.Now,
                Questions = model.Questions.Select(q => new Question
                {
                    Text = q.Text,
                    CorrectAnswer = q.CorrectAnswer,
                    Answers = q.Answers.Select(a => new Answer
                    {
                        AnswerLetter = a.AnswerLetter,
                        Text = a.Text,
                    }).ToList()
                }).ToList()
            };

            await _testRepository.AddAsync(test);
            await _testRepository.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _testRepository.RemoveAsync(id);
            await _testRepository.CommitAsync();
        }

        public async Task<IQueryable<TestViewModel>> GetAllAsync()
        {
            var result = await _testRepository.GetAllAsync();
            return result.ProjectTo<TestViewModel>(_mapper.ConfigurationProvider);
        }

        public async Task<TestViewModel> GetByIdAsync(int id)
        {
            var result = await _testRepository.GetByIdAsync(id);
            return _mapper.Map<TestViewModel>(result);
        }

        public async Task<EvaluateTest> TakeTest(EvaluateTest model)
        {
            var test = await _testRepository.GetByIdAsync(model.Id);

            foreach (var userQuestion in model.Questions)
            {
                var testQuestion = test.Questions.FirstOrDefault(x => x.Id == userQuestion.Id);

                var correctAnswer = testQuestion.Answers.FirstOrDefault(x1 => x1.AnswerLetter == testQuestion.CorrectAnswer);
                var userAnswer = userQuestion.Answers.FirstOrDefault(x1 => x1.Selected);
                if (correctAnswer.Id != userAnswer.Id)
                    userAnswer.Wrong = true;
            }

            return model;
        }
    }
}