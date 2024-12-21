namespace BackendApi.Contract.Comments
{
    public class GetCommentsResponse
    {
        public int CommentsId { get; set; }

        public int UserId { get; set; }

        public int ItemId { get; set; }

        public int ItemType { get; set; }

        public string CommentsText { get; set; } = null!;

        public DateTime? CommentsDate { get; set; }
    }
}