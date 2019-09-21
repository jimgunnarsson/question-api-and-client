using Newtonsoft.Json;
using question_api_client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace question_api_client
{
    class Program
    {
        static readonly HttpClient Client = new HttpClient();


        static void Main(string[] args)
        {
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Console.WriteLine("Question API Client");
            //RunRetrieveAll();
            //RunRetrieveById(1);
            //RunRetrieveByCategory("Spel");
            AwaitInput();
            
        }
        static void AwaitInput()
        {
            Console.WriteLine("====================================================");
            Console.WriteLine("Commands: \n"
                   + "1. Retrieve all questions \n"
                   + "2. Retrieve question by ID \n"
                   + "3. Retrieve question by Category \n"
                   + "4. Submit new question \n"
                   + "\n");

            switch (Console.ReadLine())
            {
                case "1":
                    RunRetrieveAll();
                    AwaitInput();
                    break;
                case "2":
                    RunRetrieveById(1);
                    AwaitInput();
                    break;
                case "3":
                    RunRetrieveByCategory("Spel");
                    AwaitInput();
                    break;
                case "4":
                    CreateNewQuestion();
                    break;
                default:
                    AwaitInput();
                    break;
            }
        }
        static void CreateNewQuestion()
        {
            Question QuestionHolder = new Question();
            QuestionHolder.Id = 0;
            Console.WriteLine("Choose a category for the questions, (\"Spel\", \"Geografi\", \"Natur\", etc)");
            QuestionHolder.Category = Console.ReadLine();
            Console.WriteLine("What is the question?");
            QuestionHolder.QuestionText = Console.ReadLine();
            Console.WriteLine("What is the answer?");
            QuestionHolder.Answer = Console.ReadLine();
            Console.WriteLine("Confirm the contents of the question. Y = To submit, N = Cancel and start over");
            Console.WriteLine("Category: " + QuestionHolder.Category
                  + "\n"
                  + "Question: " + QuestionHolder.QuestionText
                  + "\n"
                  + "Answer: " + QuestionHolder.Answer);
            switch (Console.ReadLine()) {
                case "Y":
                case "y":
                    RunSendQuestion(QuestionHolder);
                    AwaitInput();
                    break;
                case "N":
                case "n":
                    AwaitInput();
                    break;
                default:
                    AwaitInput();
                    break;
            }
        }
        static async Task RunRetrieveAll()
        {
            List<Question> r = await RetrieveAllQuestions();
            foreach (Question e in r)
            {
                Console.WriteLine("ID: " + e.Id
                                  + "\n"
                                  + "Category: " + e.Category
                                  + "\n"
                                  + "Question: " + e.QuestionText
                                  + "\n"
                                  + "Answer: " + e.Answer
                                  + "\n");
            }
            Console.WriteLine("Retrieved all questions. \n ===================================");
        }
        static async Task RunRetrieveById(int id)
        {
            Question r = await RetrieveOneQuestion(id);
            Console.WriteLine("ID: " + r.Id
            + "\n"
            + "Category: " + r.Category
            + "\n"
            + "Question: " + r.QuestionText
            + "\n"
            + "Answer: " + r.Answer
            + "\n");
            Console.WriteLine("Retrieved question by ID. \n ===================================");
        }
        static async Task RunRetrieveByCategory(string category)
        {
            List<Question> r = await RetrieveAllQuestionsInCategory("Spel");
            foreach (Question e in r)
            {
                Console.WriteLine("ID: " + e.Id
                                  + "\n"
                                  + "Category: " + e.Category
                                  + "\n"
                                  + "Question: " + e.QuestionText
                                  + "\n"
                                  + "Answer: " + e.Answer
                                  + "\n");
            }
            Console.WriteLine("Retrieved question by Category. \n ===================================");
        }
        static async Task RunSendQuestion(Question submission)
        {
            Question r = await SendQuestion(submission);
            Console.WriteLine("ID: " + r.Id
                            + "\n"
                            + "Category: " + r.Category
                            + "\n"
                            + "Question: " + r.QuestionText
                            + "\n"
                            + "Answer: " + r.Answer
                            + "\n");
            Console.WriteLine("Question submitted. \n ===================================");
        }
        static async Task<List<Question>> RetrieveAllQuestions()
        {
           
            HttpResponseMessage Response = await Client.GetAsync("https://localhost:44347/Question/GetAll/");
            string Data = await Response.Content.ReadAsStringAsync();
            List<Question> Result = JsonConvert.DeserializeObject<List<Question>>(Data);
           
            return Result;
        }
        static async Task<Question> RetrieveOneQuestion(int id)
        {
            HttpResponseMessage Response = await Client.GetAsync("https://localhost:44347/Question/GetById/"+id);
            string Data = await Response.Content.ReadAsStringAsync();
            Question Result = JsonConvert.DeserializeObject<Question>(Data);
            return Result;
        }
        static async Task<List<Question>> RetrieveAllQuestionsInCategory(string Category)
        {
            HttpResponseMessage Response = await Client.GetAsync("https://localhost:44347/Question/GetByCategoryName/"+Category);
            string Data = await Response.Content.ReadAsStringAsync();
            List<Question> Result = JsonConvert.DeserializeObject<List<Question>>(Data);
            return Result;
        }
        static async Task<Question> SendQuestion(Question submission)
        {
            var Data = JsonConvert.SerializeObject(submission);
            var JsonString = new StringContent(Data.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage Response = await Client.PostAsync("https://localhost:44347/Question/CreateQuestion/", JsonString);
            string ResponseData = await Response.Content.ReadAsStringAsync();
            Question Result = JsonConvert.DeserializeObject<Question>(ResponseData);
            return Result;
        }
    }
}
