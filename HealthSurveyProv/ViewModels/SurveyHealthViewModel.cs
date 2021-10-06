using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSurveyProv.ViewModels
{
    public class SurveyHealthViewModel
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
