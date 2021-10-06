using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSurveyProv.ViewModels
{
    public class SurveyAnswerViewModel
    {
        public int SurveyID { get; set; }
        public Int64 RowId { get; set; }
        public string AnswerValue { get; set; }
        public string QuestionPhrase { get; set; }
    }
}
