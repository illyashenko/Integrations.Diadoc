namespace Integrations.Diadoc.Data.Apt.Models;

public class CounteragentCandidate
{
    public string? OrgIg { get; set; }
    public string? BoxId { get; set; }
    public string? Title { get; set; }
    public string? Inn { get; set; }
    public string? Kpp { get; set; }
    public string? InnKpp 
    {
        get
        {
            _innkpp = Inn;
            
            if (!string.IsNullOrWhiteSpace(this.Kpp))
            {
                _innkpp = $"{Inn}/{Kpp}";
            }
            return _innkpp; 
        }
    }

    private string? _innkpp;
}
