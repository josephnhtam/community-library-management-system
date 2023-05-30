
using AutoMapper;
using CLMS.API.DtoModels.Books;
using CLMS.Application.Commands.Books;
using CLMS.Application.Queries.Books;
using CLMS.Domain.Aggregates.BookAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CLMS.API.Controllers
{
    [ApiController, Route("api/v1/books")]
    public class BooksController : ControllerBase {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BooksController (IMediator mediator, IMapper mapper) {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Add (AddBookRequest request) {
            var book = await _mediator.Send(_mapper.Map<AddBookCommand>(request));
            return Ok(book);
        }

        [HttpGet]
        public async Task<ActionResult<Book>> Get (Guid bookId) {
            var book = await _mediator.Send(new GetBookByIdQuery { BookId = bookId });
            return Ok(book);
        }

        [HttpGet]
        public async Task<ActionResult<Book>> GetByCopyId (Guid bookCopyId) {
            var book = await _mediator.Send(new GetBookByCopyIdQuery { BookCopyId = bookCopyId });
            return Ok(book);
        }

    }
}
