using CLMS.API.DtoModels;
using CLMS.Application.Commands.Books;
using CLMS.Application.Queris.Books;
using CLMS.Domain.Aggregates.BookAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CLMS.API.Controllers {
    [ApiController, Route("api/v1/books")]
    public class BooksController : ControllerBase {

        private IMediator _mediator;

        public BooksController (IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Add (AddBookRequest request) {
            var author = await _mediator.Send(
                new AddBookCommand(
                    request.Title,
                    request.Description,
                    request.PublicationDate,
                    request.Authors));

            return Ok(author.Id);
        }

        [HttpGet]
        public async Task<ActionResult<Book>> Get (Guid bookId) {
            var author = await _mediator.Send(new GetBookQuery(bookId));
            return Ok(author);
        }

    }
}
