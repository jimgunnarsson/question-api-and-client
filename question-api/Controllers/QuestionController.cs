using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using question_api.Models;

namespace question_api.Controllers
{
    [Route("[controller]")]
    public class QuestionController : Controller
    {
      
        public List<Question> QuestionDb { get; set; }
        public QuestionController()
        {
            QuestionDb = new List<Question>();
            QuestionDb.Add(new Question
            {
                Id = 1,
                Category = "Geografi",
                QuestionText = "Vart ligger Skottland?",
                Answer = "Storbritannien"
            });

            QuestionDb.Add(new Question
            {
                Id = 2,
                Category = "Natur",
                QuestionText = "Vart befinner sig isbjörnen?",
                Answer = "Polcirkeln"
            });

            QuestionDb.Add(new Question
            {
                Id = 3,
                Category = "Spel",
                QuestionText = "Vad har Super Mario för yrke?",
                Answer = "Rörmokare"
            });

            QuestionDb.Add(new Question
            {
                Id = 4,
                Category = "Spel",
                QuestionText = "Vad heter hjälten i Legend of Zelda?",
                Answer = "Link"
            });
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(QuestionDb);
        }
        [HttpGet("GetById/{id:int}")]
        public IActionResult GetById(int id)
        {
            var result = QuestionDb.Find(x => x.Id == id);
            return Ok(result);
        }
        [HttpGet("GetByCategoryName/{name}")]
        public IActionResult GetByName(string name)
        {
            var result = QuestionDb.FindAll(x => x.Category == name);
            return Ok(result);
        }
        [HttpPost("CreateQuestion")]
        public ActionResult<Question> CreateQuestion([FromBody]Question submission)
        {
            submission.Id = QuestionDb.Count + 1;
            QuestionDb.Add(submission);
            return Ok(submission);
        }
    }
}