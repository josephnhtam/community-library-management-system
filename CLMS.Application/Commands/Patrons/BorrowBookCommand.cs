﻿using Application.Contracts;
using CLMS.Domain.Aggregates.PatronAggregate;

namespace CLMS.Application.Commands.Patrons {
    public class BorrowBookCommand : ICommand<BookLoan> {
        public Guid PatronId { get; init; } = default!;
        public Guid BookCopyId { get; init; } = default!;
        public DateTimeOffset Date { get; init; } = default!;
        public DateTimeOffset DueDate { get; init; } = default!;
    }
}
