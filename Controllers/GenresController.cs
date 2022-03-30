using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using movieflix_api.Controllers.ViewModels;
using movieflix_api.Data;
using movieflix_api.Models;

namespace movieflix_api.Controllers
{
    [ApiController]
    [Route("api/v1/genres")]
    public class GenresController : ControllerBase
    {
        private readonly DataContext _context;
        public GenresController(DataContext context)
        {
            _context = context;

        }

        [HttpGet()]

        public async Task<ActionResult<List<GetGenreViewModel>>>
        ListGenres()
        {
            var response = await _context.Genres.ToListAsync();
            var genres = new List<GetGenreViewModel>();

            foreach (var genre in response)
            {
                genres.Add(new GetGenreViewModel
                {
                    Id = genre.Id,
                    GenreName = genre.GenreName
                });
            }
            return Ok(genres);
        }

        [HttpPost()]
        public async Task<ActionResult<Genre>> AddGenre(PostGenreViewModel model)
        {

            var newGenre = new Genre
            {
                GenreName = model.GenreName
            };

            _context.Genres.Add(newGenre);
            await _context.SaveChangesAsync();

            return StatusCode(201, newGenre);
        }

    }
}