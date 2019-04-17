using myCao.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace myCao.RESTHelper
{
    public class WebSearchService
    {
        private const string baseUrl = "https://www.googleapis.com/customsearch/v1";
        private const string key = "?key=AIzaSyCN-GugsvmPjgTOIU34iNweMeqnaGs9PQ4";
        private const string apiID = "&cx=012637256529080974568:zgqauhoq1gu";
        private HttpClient _client;

        public WebSearchService()
        {
            _client = new HttpClient();
        }

        public async Task<Website> CourseLink(Course course)
        {
            string baseLink = baseUrl + key + apiID+"&q=";
            List<string> terms = SplitTitle(course.CourseTitle);

            for(int i = -1;i<terms.Count;i++)
            {
                if (i == -1)
                {
                    baseLink += course.CourseCode;
                }
                else
                {
                    baseLink += "+" + terms[i];
                }
            }

            var response = await _client.GetStringAsync(baseLink);

            var websearch = JsonConvert.DeserializeObject<WebSearch>(response);
            

            return websearch.items[0];
        }

        private List<string> SplitTitle(string courseTitle)
        {
            string[] arr = courseTitle.Split(null);
            List<string> terms = new List<string>();
            for(int i=0;i<arr.Length;i++)
            {
                if(arr[i] != "(Portfolio)")
                {
                    terms.Add(arr[i]);
                }
            }
            return terms;
        }
    }
}
