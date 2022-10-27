namespace Integrations.Diadoc.Infrastructure.DTOs
{
    public class SignedContentModel 
    {
        public byte[]? Content { get; set; }
        public bool SignByAttorney { get; set; } = false;
        public bool SignWithTestSignature { get; set; } = false;
    }
}