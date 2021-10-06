using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSurveyProv.ViewModels
{
    public class SurveyQuestionViewModel
    {
        public int QustionId { get; set; }
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        public int? SortKey { get; set; }
        [StringLength(4000)]
        public string QuestionPhrase { get; set; }
        [StringLength(50)]
        public string QuestionType { get; set; }
        [DataType(DataType.Text)]

        [StringLength(50)]
        public string AnswerValue { get; set; }
    }
}
