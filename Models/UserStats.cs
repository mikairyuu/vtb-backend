namespace vtb_backend.Models
{
    public class UserStats
    {
        public int score { get; set; }
        public int casesDone { get; set; }
        public int lessonsDone { get; set; }
        public bool isQualified { get; set; }
        public int money { get; set; }
    }
    
    public class ApiUserStats
    {
        public int token { get; set; }
        public int score { get; set; }
        public int casesDone { get; set; }
        public int lessonsDone { get; set; }
        public bool isQualified { get; set; }
        public int money { get; set; }
    }
}