using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HealthSurveyProv.Models
{
    public partial class SurveyQuestion
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        public int? SortKey { get; set; }
        [StringLength(4000)]
        public string QuestionPhrase { get; set; }
        [StringLength(50)]
        public string QuestionType { get; set; }
    }
}
