namespace Integrations.Diadoc.Domain.Models.Enums;

public enum ExecuteCodes
{
    Unknown = 0,
    Ok = 1,
    InvalidData = -1,
    DbError = -2,
    DbNotFound = -3,
    
    //Diadoc codes
    
    SendMessageToPostError = -800,
    AcquireCounteragentRequestError = -801,
    CreationErrorMessageToPost = -802,
    ActError = -803,
    GetCounteragentError = -804
}
