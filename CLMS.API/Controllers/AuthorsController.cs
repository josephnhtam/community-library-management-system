
using AutoMapper;
using CLMS.API.DtoModels.Authors;
using CLMS.Application.Commands.Authors;
using CLMS.Application.Queries.Authors;
using CLMS.Domain.Aggregates.AuthorAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CLMS.API.Controllers
{
    [ApiController, Route("api/v1/authors")]
    public class AuthorsController : ControllerBase {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthorsController (IMediator mediator, IMapper mapper) {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Author>> Add (AddAuthorRequest request) {
            var author = await _mediator.Send(_mapper.Map<AddAuthorCommand>(request));
            return Ok(author);
        }

        [HttpGet]
        public async Task<ActionResult<Author>> Get (Guid authorId) {
            var author = await _mediator.Send(new GetAuthorByIdQuery { AuthorId = authorId });
            return Ok(author);
        }

    }
}
