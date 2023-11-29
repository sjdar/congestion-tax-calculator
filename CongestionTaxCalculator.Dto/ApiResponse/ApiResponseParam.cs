using CongestionTaxCalculator.Dto.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CongestionTaxCalculator.Dto.ApiResponse
{
    public class ApiResponseParam
    {
        public static readonly ApiResponseParam InternalServerError = new ApiResponseParam(ServiceStatusCodes.InternalServerError);

        public bool IsSuccess => Code == ServiceStatusCodes.Success;
        public ServiceStatusCodes Code { get; }
        public string CodeTitle => Code.ToString();
        public string Message { get; }
        public IEnumerable<FieldValidationErrorParam>? Errors { get; }
        protected ApiResponseParam(ServiceStatusCodes code, IEnumerable<FieldValidationErrorParam>? errors = null)
        {
            Code = code;
            Message = code.ToString();
            Errors = errors;

        }

        public static ApiResponseParam Create(ServiceStatusCodes errorCode) => new ApiResponseParam(errorCode, null);
        public static ApiResponseParam CreateValidationError(IEnumerable<FieldValidationErrorParam> errors) => new ApiResponseParam(ServiceStatusCodes.InvalidParameters, errors);
        public static ApiResponseParam<TData> Create<TData>(ServiceStatusCodes code, TData data) where TData : class => new ApiResponseParam<TData>(code, data);
        public static ApiResponseParam CreateUnAccessibleDataError(params string[] fieldNames)
       => new ApiResponseParam(ServiceStatusCodes.UnAuthorizeAccess, fieldNames.Select(f => new FieldValidationErrorParam(f, FieldValidationErrorCodes.UnAccessibleData)));



    }
    public class ApiResponseParam<TData> : ApiResponseParam where TData : class
    {
        public ApiResponseParam(ServiceStatusCodes code, TData? data) : base(code)
        {

            Data = data;
        }
        public TData? Data { get; }

    }
}
