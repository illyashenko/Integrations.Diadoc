namespace Integrations.Diadoc.Infrastructure.DTOs
{
    public class NonformalizedAttachmentModel
    {
        public string? FileName { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string? DocumentNumber { get; set; }
        
        public int NeedReceipt { get; set; } = 0;
        
        public SignedContentModel? SignedContentModel { get; set; } 
    }
}