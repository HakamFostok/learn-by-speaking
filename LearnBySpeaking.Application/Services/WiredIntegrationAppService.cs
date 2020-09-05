using LearnBySpeaking.Application.Interfaces;
using LearnBySpeaking.Domain.Interfaces.EntityInterfaces;
using LearnBySpeaking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LearnBySpeaking.Application.Services
{
    public class WiredIntegrationAppService : IWiredIntegrationAppService
    {
        private readonly HttpClient _httpClient;
        private readonly ITopicRepository _topicRepository;

        public WiredIntegrationAppService(HttpClient httpClient, ITopicRepository topicRepository)
        {
            _httpClient = httpClient;
            _topicRepository = topicRepository;
        }

        public async Task Integration()
        {
            List<Topic> topics = await GetTopics();
            foreach (Topic topic in topics)
                await _topicRepository.AddAsync(topic);

            //await _topicRepository.SaveChangesAsync() ;
        }

        private async Task<List<Topic>> GetTopics()
        {
            var articles = await GetTitles();
            List<Topic> result = new List<Topic>();
            foreach ((string title, string url) in articles)
            {
                string content = await GetContent(url);

                Topic topic = new Topic
                {
                    Title = title,
                    Content = content
                };

                result.Add(topic);
            }

            return result;
        }

        private async Task<string> GetContent(string url)
        {
            throw new NotImplementedException();
        }

        private async Task<List<(string title, string url)>> GetTitles()
        {
            List<(string, string)> result = new List<(string, string)>();
            var response = await _httpClient.GetAsync("https://wired.com");
            string pageContent = await response.Content.ReadAsStringAsync();

            return result;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}