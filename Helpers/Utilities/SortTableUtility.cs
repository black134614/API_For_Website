namespace API.Helpers.Utilities
{
    public class Sortable
    {
        public string SortColumn { get; set; }
        public string SortType { get; set; }
        public int SortSeq { get; set; }
    }

    public static class SortType
    {
        public static string ASC = "ASC";
        public static string DESC = "DESC";
        public static string NONE = "";
    }
}