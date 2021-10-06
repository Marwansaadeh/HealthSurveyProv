using HealthSurveyProv.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSurveyProv.Services
{
    public interface ISurveyRepository
    {
        public SurveyHealthViewModel SetSurvey();
        public List<SurveyAnswerViewModel> GetSurveyQuestionsAndAnswers(int Id);

        public void AddSurveyAnswers(List<SurveyQuestionViewModel> surveyAnswers);
        public List<SurveyQuestionViewModel> GetSurveyQuestions(int Id);
    }
}
