using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CLMS.API.Controllers {
    [ApiController, Route("api/v1/patrons")]
    public class PatronsController : ControllerBase {

        private IMediator _mediator;

        public PatronsController (IMediator mediator) {
            _mediator = mediator;
        }

    }
}
