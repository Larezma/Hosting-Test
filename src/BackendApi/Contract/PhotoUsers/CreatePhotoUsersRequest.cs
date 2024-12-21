namespace BackendApi.Contract.PhotoUsers
{
    public class CreatePhotoUsersRequest
    {
        public int UserId { get; set; }

        public string PhotoLink { get; set; } = null!;

        public DateTime? UploadPhoto { get; set; }

    }
}