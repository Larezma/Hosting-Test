namespace BackendApi.Contract.PhotoUsers
{
    public class GetPhotoUsersResponse
    {
        public int PhotoId { get; set; }

        public int UserId { get; set; }

        public string PhotoLink { get; set; } = null!;

        public DateTime? UploadPhoto { get; set; }

    }
}