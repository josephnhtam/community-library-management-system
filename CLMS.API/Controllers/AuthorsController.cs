using CLMS.API.DtoModels;
using CLMS.Application.Commands.Authors;
using CLMS.Application.Queris.Authors;
using CLMS.Domain.Aggregates.AuthorAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CLMS.API.Controllers {
    [ApiController, Route("api/v1/authors")]
    public class AuthorsController : ControllerBase {

        private IMediator _mediator;

        public AuthorsController (IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Add (AddAuthorRequest request) {
            var author = await _mediator.Send(new AddAuthorCommand(request.Name, request.Description));
            return Ok(author.Id);
        }

        [HttpGet]
        public async Task<ActionResult<Author>> Get (Guid authorId) {
            var author = await _mediator.Send(new GetAuthorQuery(authorId));
            return Ok(author);
        }

    }
}
