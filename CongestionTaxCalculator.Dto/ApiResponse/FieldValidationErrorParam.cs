using CongestionTaxCalculator.Dto.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CongestionTaxCalculator.Dto.ApiResponse
{
    public class FieldValidationErrorParam
    {
        public FieldValidationErrorParam(string filed) => Field = filed;
        public FieldValidationErrorParam(string field, FieldValidationErrorCodes code) : this(field) => Code = code;
        public FieldValidationErrorParam(string field, FieldValidationErrorCodes code, string message) : this(field, code) => Message = message;


        public FieldValidationErrorCodes Code { get; set; }
        public string Field { get; }
        public string Message { get; set; }

    }
}
