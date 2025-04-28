namespace ResumeAnalyzer.Models
{
    public class Resume
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string ExtractedText { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
