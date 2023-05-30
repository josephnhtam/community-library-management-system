
using AutoMapper;
using CLMS.API.DtoModels.Patrons;
using CLMS.Application.Commands.Patrons;
using CLMS.Application.Queries.Patrons;
using CLMS.Domain.Aggregates.PatronAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CLMS.API.Controllers {
    [ApiController, Route("api/v1/patrons")]
    public class PatronsController : ControllerBase {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PatronsController (IMediator mediator, IMapper mapper) {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Patron>> Add (AddPatronRequest request) {
            var patron = await _mediator.Send(_mapper.Map<AddPatronCommand>(request));
            return Ok(patron);
        }

        [HttpGet]
        public async Task<ActionResult<Patron>> Get (Guid patronId) {
            var Patron = await _mediator.Send(new GetPatronByIdQuery { PatronId = patronId });
            return Ok(Patron);
        }

        [HttpPost("donate")]
        public async Task<ActionResult<BookDonation>> Donate (DonateBookRequest request) {
            var donation = await _mediator.Send(_mapper.Map<DonateBookCommand>(request));
            return Ok(donation);
        }

        [HttpPost("borrow")]
        public async Task<ActionResult<BookLoan>> Borrow (BorrowBookRequest request) {
            var loan = await _mediator.Send(_mapper.Map<BorrowBookCommand>(request));
            return Ok(loan);
        }

        [HttpPost("return")]
        public async Task<ActionResult<BookLoan>> Return (ReturnBookRequest request) {
            var loan = await _mediator.Send(_mapper.Map<ReturnBookCommand>(request));
            return Ok(loan);
        }

    }
}
