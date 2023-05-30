namespace CLMS.API.DtoModels.Books
{
    public class AddBookRequest
    {
        public string Title { get; init; } = default!;
        public string Description { get; init; } = default!;
        public DateTimeOffset PublicationDate { get; init; } = default!;
        public List<Guid> Authors { get; init; } = default!;
    }
}
