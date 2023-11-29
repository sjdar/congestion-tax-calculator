using System;
using System.Collections.Generic;
using System.Text;

namespace CongestionTaxCalculator.Dto.Enums
{
    public enum FieldValidationErrorCodes
    {
        RequiredField = 1,
        InvalidParameters = 4,
        InvalidUser = 2,
        InvalidPass = 3,
        DuplicateRow = 3,
        UnAccessibleData = 8,

    }
}
