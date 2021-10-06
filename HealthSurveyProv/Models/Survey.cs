using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HealthSurveyProv.Models
{
    [Table("Survey")]
    public partial class Survey
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SubmitDate { get; set; }
        [Column("SurveyRND")]
        public int? SurveyRnd { get; set; }
    }
}
