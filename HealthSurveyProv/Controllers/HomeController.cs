using HealthSurveyProv.Models;
using HealthSurveyProv.Services;
using HealthSurveyProv.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSurveyProv.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISurveyRepository _surveyRepository;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ISurveyRepository surveyRepository)
        {
            _logger = logger;
            _surveyRepository = surveyRepository;
        }

        public IActionResult Index()
        {
            var model = new SurveyViewModel();
            model.SurveyHealthViewModel = _surveyRepository.SetSurvey();
            model.SurveyQuestionViewModel = _surveyRepository.GetSurveyQuestions(model.SurveyHealthViewModel.Id);
            model.SurveyAnswerViewModels = new List<SurveyAnswerViewModel>();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(SurveyViewModel model)
        {
            if (ModelState.IsValid)
            {
                _surveyRepository.AddSurveyAnswers(model.SurveyQuestionViewModel);
                model.SurveyAnswerViewModels = _surveyRepository.GetSurveyQuestionsAndAnswers(model.SurveyHealthViewModel.Id);
                model.SucessMessage = "Thanks for answering survey questions";
                model.SurveyHealthViewModel.CreatedDate = model.SurveyHealthViewModel.SubmitDate.Value;
                model.SurveyQuestionViewModel = new List<SurveyQuestionViewModel>();
                return View(model);

            }
            model.SurveyAnswerViewModels = new List<SurveyAnswerViewModel>();
            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
