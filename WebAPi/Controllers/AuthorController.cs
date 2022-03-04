using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPi.Dtos;
using WebAPi.Entities;
using WebAPi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IRepository<Author> _authorRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Return List of  authors with related books.
        /// </summary>
        /// <param name="bookContext"></param>
        public AuthorController(IRepository<Author> authorRepositoty, IMapper mapper)
        {
            _authorRepository = authorRepositoty;
            _mapper = mapper;
        }
        // GET: api/<AuthorController>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _authorRepository.GetEntities();
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(result));
        }

        /// <summary>
        /// Return author with book collection.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeBook"></param>
        /// <returns></returns>
        /// 

        // GET api/<AuthorController>/5
        [HttpGet("{id}", Name= "GetAuthor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id, bool includeBook)
        {
            if (!_authorRepository.EntityExist(id))
                return NotFound();

            var result = _authorRepository.GetEntity(id, includeBook);
            // return auhor data
            return Ok(_mapper.Map<AuthorDto>(result));
        }

        // POST api/<AuthorController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AuthorDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] AuthorCreationDto author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // map authorDto to Author entity
            var newAuthor = _mapper.Map<Author>(author);

            _authorRepository.AddEntity(newAuthor);
            _authorRepository.Save();

            // map author entity to authorDto
            var createdAuthor = _mapper.Map<AuthorDto>(newAuthor);

            return CreatedAtRoute("GetAuthor", new {id = createdAuthor.Id }, createdAuthor);
        }
    }
}
