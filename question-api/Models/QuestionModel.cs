using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace question_api.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string QuestionText { get; set; }
        public string Answer { get; set; }
    }
}
