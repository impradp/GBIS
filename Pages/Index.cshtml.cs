using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GBIS.Models;
using GBIS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Gautam_Buddha_International_Stadium.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public JsonFileStadiumService StadiumService;
        public IEnumerable<Stadium> Stadiums { get; private set; }

        public IndexModel(ILogger<IndexModel> logger,
            JsonFileStadiumService stadiumService)
        {
            _logger = logger;
            StadiumService = stadiumService;
        }

        public void OnGet()
        {
            Stadiums = StadiumService.GetStadiums();
        }
    }
}
