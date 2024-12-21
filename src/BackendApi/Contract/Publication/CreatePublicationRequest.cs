namespace BackendApi.Contract.Publication
{
    public class CreatePublicationRequest
    {
        public int UsersId { get; set; }

        public string PublicationText { get; set; } = null!;

        public DateTime? PublicationDate { get; set; }

        public string? PublicationsImage { get; set; }
    }
}