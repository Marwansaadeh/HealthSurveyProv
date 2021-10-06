using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSurveyProv.ViewModels
{
    public class SurveyViewModel
    {
        public List<SurveyAnswerViewModel> SurveyAnswerViewModels { get; set; }
        public SurveyHealthViewModel SurveyHealthViewModel { get; set; }
        public List<SurveyQuestionViewModel> SurveyQuestionViewModel { get; set; }
        public string SucessMessage { get; set; }
    }
}
