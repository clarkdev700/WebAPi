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
    public class BookController : ControllerBase
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IMapper _mapper;

        public BookController(IRepository<Book> bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        // GET: api/<BookController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            //return _bookRepository.GetEntities();
            return Ok(_mapper.Map<IEnumerable<BookDto>>(_bookRepository.GetEntities()));
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var searchBook = _bookRepository.GetEntity(id, false);
            if (searchBook == null)
                return NotFound();
            var bookResult = _mapper.Map<BookDto>(searchBook); 

            return Ok(bookResult);
        }
    }
}
