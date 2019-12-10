using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using GBIS.Models;
using Microsoft.AspNetCore.Hosting;

namespace GBIS.Services
{
    public class JsonFileStadiumService
    {
        //constructor
        public JsonFileStadiumService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment; //chain of services
        }

        public IWebHostEnvironment WebHostEnvironment { get; }
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "stadiums.json"); }
        }
        public IEnumerable<Stadium> GetStadiums()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Stadium[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }
        public void AddRating(string StadiumId,int rating)
        {
            var stadiums = GetStadiums();
            //LINQ
            var query = stadiums.First(x => x.Id == StadiumId);
            if (query.Ratings==null)
            {
                query.Ratings = new int[] { rating };
            }
            else
            {
                var ratings = query.Ratings.ToList();
                ratings.Add(rating);
                query.Ratings = ratings.ToArray();
            }
            using (var outputStream=File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Stadium>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    stadiums   
                );
            }
        }
    }
}
