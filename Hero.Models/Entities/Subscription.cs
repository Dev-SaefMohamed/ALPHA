using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero.Models.Entities
{
    public class Subscription
    {

        [Key]
        public int Id { get; set; }

        // الشركة
        [Required]
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public CorporateCompany Company { get; set; }

        // خطة الاشتراك
        [Required]
        public string PlanName { get; set; }  // Free - Basic - Pro - Enterprise

        [Required]
        public int MonthlyApiLimit { get; set; }  // 100 - 1000 - 10000

        [Required]
        public decimal PricePerMonth { get; set; }

        // API Usage
        public int ApiCallsUsedThisMonth { get; set; } = 0;

        // مواعيد
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        // حالة الاشتراك
        [NotMapped]
        public bool IsActive => DateTime.Now >= StartDate && DateTime.Now <= EndDate;

        // API Key
        public string ApiKey { get; set; }

    }
}
