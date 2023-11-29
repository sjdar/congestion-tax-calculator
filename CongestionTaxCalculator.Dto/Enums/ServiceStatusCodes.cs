namespace CongestionTaxCalculator.Dto.Enums
{
    public enum ServiceStatusCodes
    {
        InvalidParameters = 400,
        Success = 200,
        ReadyToSend = 201,
        UnAuthorizeAccess = 401,
        ExpiredToken = 401,
        InternalServerError = 500,
        UserIsNotValid = 0,
        Ok = 1,
        UserNotAcssesInVersion = 2,
        Error = 3,
        NotFound = 4,


    }
}
