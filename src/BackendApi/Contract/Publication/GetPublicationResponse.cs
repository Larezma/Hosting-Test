namespace BackendApi.Contract.Publication
{
    public class GetPublicationResponse
    {
        public int PublicationsId { get; set; }

        public int UsersId { get; set; }

        public string PublicationText { get; set; } = null!;

        public DateTime? PublicationDate { get; set; }

        public string? PublicationsImage { get; set; }
    }
}