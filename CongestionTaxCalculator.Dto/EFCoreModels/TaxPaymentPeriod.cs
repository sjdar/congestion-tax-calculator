using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CongestionTaxCalculator.Dto.EFCoreModels
{
    public class TaxPaymentPeriod
    {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime StartTime { get; set; } 
        public DateTime EndTime { get; set; } 
        public string Currency { get; set; } = "SEK";    
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
