using HtmlAgilityPack;
using LearnBySpeaking.Application.Interfaces;
using LearnBySpeaking.Domain.Interfaces.EntityInterfaces;
using LearnBySpeaking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
            if ((await _topicRepository.GetAllAsync()).Any())
                return;

            List<Topic> topics = await GetTopics();
            foreach (Topic topic in topics)
                await _topicRepository.AddAsync(topic);

            await _topicRepository.CommitAsync();
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
            var doc = await GetHtmlDocument("https://wired.com" + url);

            var ps = doc.QuerySelectorAll("article p").Where(x => x.GetClasses().Any() == false);
            StringBuilder builder = new StringBuilder();
            foreach (string v in ps.Select(x => x.InnerText))
                builder.Append(HttpUtility.HtmlDecode(v));

            return builder.ToString();
        }

        private async Task<List<(string title, string url)>> GetTitles()
        {
            List<(string title, string url)> result = new List<(string, string)>();
            var doc = await GetHtmlDocument("https://wired.com");

            var lis = doc.QuerySelectorAll("li.card-component__description");
            foreach (var li in lis)
            {
                if (result.Count == 5)
                    break;

                try
                {
                    var aElement = li.QuerySelectorAll("a")[1];
                    result.Add((HttpUtility.HtmlDecode(li.InnerText), aElement.Attributes["href"].Value));
                }
                catch (Exception)
                {
                }
            }

            return result;
        }

        private async Task<HtmlDocument> GetHtmlDocument(string url)
        {
            var response = await _httpClient.GetAsync(url);
            string pageContent = await response.Content.ReadAsStringAsync();

            var doc = new HtmlDocument();
            doc.LoadHtml(pageContent);

            return doc;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}