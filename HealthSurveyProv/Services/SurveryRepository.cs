using HealthSurveyProv.Models;
using HealthSurveyProv.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSurveyProv.Services
{
    public class SurveryRepository
    {
        private readonly HealthContext _context;
        public virtual DbSet<SurveyAnswerViewModel> surveyAnswerViewModel { get; set; }

        public SurveryRepository(HealthContext context)
        {
            _context = context;
        }

        public void AddSurveyAnswers(List<SurveyQuestionViewModel> surveyAnswers)
        {
            foreach (var item in surveyAnswers)
            {
                var Surveyanswer = _context.SurveyAnswers.FirstOrDefault(x => x.SurveyId == item.Id && x.SurveyQuestionId == item.QustionId);
                Surveyanswer.Answervalue = item.AnswerValue;
                Surveyanswer.SurveyId = item.Id;

                _context.SurveyAnswers.Update(Surveyanswer);
                _context.SaveChanges();

            }
            var Updatedsurvey = _context.Surveys.FirstOrDefault(x => x.Id == surveyAnswers[0].Id);
            Updatedsurvey.SubmitDate = DateTime.Now;

            _context.SaveChanges();
        }

        public List<SurveyAnswerViewModel> GetSurveyQuestionsAndAnswers(int Id)
        {
            var surveyAnswerViewModel = new List<SurveyAnswerViewModel>();

            var surveyQustionAndAnswers = _context.SurveyAnswers.FromSqlRaw("exec [dbo].[spGetSurveyQuestionsAndAnswers] {0}", Id);
            foreach (var item in surveyQustionAndAnswers)
            {
                surveyAnswerViewModel.Add(new SurveyAnswerViewModel()
                {
                    SurveyID = item.SurveyId,
                    AnswerValue = item.Answervalue,
                    QuestionPhrase = item.SurveyQuestion.QuestionPhrase

                });
            }
              
            return surveyAnswerViewModel;
        }
            
        


        public SurveyHealthViewModel SetSurvey()
        {
           _context.Database.ExecuteSqlRaw("exec [dbo].[NewSurvey]");

            _context.SaveChanges();
            var survey= _context.Surveys.OrderByDescending(x => x.Id).First();
            SurveyHealthViewModel surveyHealthViewModel = new SurveyHealthViewModel();
            surveyHealthViewModel.Id = survey.Id;
            surveyHealthViewModel.SurveyRnd = survey.SurveyRnd;
            surveyHealthViewModel.CreatedDate = survey.CreatedDate;
            surveyHealthViewModel.SubmitDate = survey.SubmitDate;
            return surveyHealthViewModel;

        }
        public List<SurveyQuestionViewModel> GetSurveyQuestions(int Id)
        {
            List<SurveyQuestionViewModel> surveyQuestionsViewModels = new List<SurveyQuestionViewModel>();
            var x = _context.SurveyAnswers.Include(x => x.SurveyQuestion).Where(x => x.SurveyId == Id).Select(x => x.SurveyQuestion).ToList();
            foreach (var item in x)
            {
                var surveyquestion = new SurveyQuestionViewModel();
                surveyquestion.QuestionPhrase = item.QuestionPhrase;
                surveyquestion.QuestionType = item.QuestionType;
                surveyquestion.SortKey = item.SortKey;
                surveyquestion.Id = Id;
                surveyquestion.QustionId = item.Id;
                surveyQuestionsViewModels.Add(surveyquestion);
            }
            return surveyQuestionsViewModels;
        }

    }
}

