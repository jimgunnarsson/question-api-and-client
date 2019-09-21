using System;
using System.Collections.Generic;
using System.Text;

namespace question_api_client.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string QuestionText { get; set; }
        public string Answer { get; set; }
    }
}
