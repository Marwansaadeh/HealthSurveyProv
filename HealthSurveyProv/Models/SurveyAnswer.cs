using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HealthSurveyProv.Models
{
    [Keyless]
    public partial class SurveyAnswer
    {
        public Guid? Id { get; set; }
        [Column("SurveyID")]
        public int SurveyId { get; set; }
        [Column("SurveyQuestionID")]
        public int SurveyQuestionId { get; set; }
        [StringLength(50)]
        public string Answervalue { get; set; }

        [ForeignKey(nameof(SurveyId))]
        public virtual Survey Survey { get; set; }
        [ForeignKey(nameof(SurveyQuestionId))]
        public virtual SurveyQuestion SurveyQuestion { get; set; }
    }
}
