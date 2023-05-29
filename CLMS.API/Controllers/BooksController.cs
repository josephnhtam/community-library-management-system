using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CLMS.API.Controllers {
    [ApiController, Route("api/v1/books")]
    public class BooksController : ControllerBase {

        private IMediator _mediator;

        public BooksController (IMediator mediator) {
            _mediator = mediator;
        }

    }
}
