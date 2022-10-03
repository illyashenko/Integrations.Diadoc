using PickPoint.Models;

namespace Integrations.Diadoc.Domain.DTOs
{
    public class SendingDocument
    {
        public int? DocumentId { get; set; }
        public int? DocumentOwnerId { get; set; }
        public string? FileName { get; set; }
        public byte[]? Content { get; set; }
        public bool? Bill { get; set; }
        public PickPointKey Key => new ( this.DocumentId ?? 0, this.DocumentOwnerId ?? 0);
    }
}