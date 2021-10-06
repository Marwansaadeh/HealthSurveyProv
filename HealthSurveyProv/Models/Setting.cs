using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HealthSurveyProv.Models
{
    [Keyless]
    public partial class Setting
    {
        [StringLength(4000)]
        public string SurveyfooterText { get; set; }
        [StringLength(4000)]
        public string SurveyHeaderText { get; set; }
    }
}
