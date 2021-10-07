using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HealthSurveyProv.ViewModels
{
    public class SurveyQuestionViewModel : IValidatableObject
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
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!RegexUtilities.IsValidEmail(AnswerValue) && QuestionPhrase == "Email")
            {
                yield return new ValidationResult(
                    $"email of {AnswerValue} is not valid",
                    new[] { nameof(AnswerValue) });
            }
            
        }
    }

    public class AnswerAttribute : ValidationAttribute
    {
        public AnswerAttribute(string answer, string questionPhrase)
        {
            Answer = answer;
            QuestionPhrase = questionPhrase;
        }

        public string Answer { get; }
        public string QuestionPhrase { get; }


        public string GetErrorMessage() =>
            $"the value of {Answer} is not valid for {QuestionPhrase} .";

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var surveyQuestionsViewModel = (SurveyQuestionViewModel)validationContext.ObjectInstance;
            var answerviewmodel = surveyQuestionsViewModel.AnswerValue;
            var questionviewmodel = surveyQuestionsViewModel.QuestionPhrase;

            if (!RegexUtilities.IsValidEmail(answerviewmodel) && questionviewmodel == "Email")
            {
                return new ValidationResult(GetErrorMessage());
            }
           
            return ValidationResult.Success;
        }
    }

    public class RegexUtilities
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
    }

