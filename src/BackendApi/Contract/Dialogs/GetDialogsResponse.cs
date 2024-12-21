namespace BackendApi.Contract.Dialogs
{
    public class GetDialogsResponse
    {
        public int DialogsId { get; set; }

        public string TextDialogs { get; set; } = null!;

        public DateTime? TimeCreate { get; set; }

        public DateTime? EndTime { get; set; }
    }
}