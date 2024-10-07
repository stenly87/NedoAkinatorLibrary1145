using NedoAkinatorLibrary1145.DB;
using NedoAkinatorLibrary1145.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NedoAkinatorLibrary1145.Model
{
    public class Game : IDisposable
    {
        HistoryRecord currentHistory;
        QuestionRecord currentQuestion;
        List<QuestionRecord> questionsByRank;
        List<CharacterRecord> characterByRank;
        CharacterRecord targetCharacter;

        public QuestionRecord GetNextQuestion()
        {
            return currentQuestion;
        }

        public void RememberReaction(QuestionRecord question, int reaction)
        {


            ChangeQuestion?.Invoke(this, new EventArgs());
        }

        public void Start()
        {
            var questionRep = new QuestionRepository();
            var historyRep = new HistoryRepository();
            currentHistory = new HistoryRecord();
            historyRep.Create(currentHistory);
            historyRep.Save();
            currentHistory.Id = historyRep.GetLastID();

            var rep = new CharacterRepository();
            var allCharacters = new List<CharacterRecord>(rep.GetList());
            for (int i = 0; i < allCharacters.Count; i++)
            {
                allCharacters[i].Rank = 1.0 / allCharacters.Count;
                int all = historyRep.Count();
                int allCurrentCharacter = historyRep.Count(allCharacters[i].Id);
                allCharacters[i].Rank += allCurrentCharacter / (double)all;
            }
            characterByRank = allCharacters.OrderByDescending(s => s.Rank).ToList();
            targetCharacter = characterByRank.FirstOrDefault();

            var historyByCharacter = historyRep.
                GetHistoryByCharacter(targetCharacter.Id).ToList();
            var crossRep = new CrossRepository();
            List<CrossRecord> crossRecords = new();
            if (historyByCharacter.Count != 0)
            {
                for (int i = 0; i < historyByCharacter.Count; i++)
                {
                    var questions = crossRep.
                        GetQuestionsByHistory(historyByCharacter[i].Id);
                    crossRecords.AddRange(questions);
                }
                int allQuestions = crossRecords.Count();
                var unicQuestion = crossRecords.
                    GroupBy(s => s.IdQuestion).
                    Select(s => (s.Key, Count: s.Count())).OrderByDescending(s=>s.Count);
                currentQuestion = questionRep.Get(unicQuestion.First().Key);
            }
            else
                currentQuestion = questionRep.GetRandom();

            ChangeQuestion?.Invoke(this, new EventArgs());
        }

        public void Dispose()
        {
            ChangeQuestion = null;
        }

        public event EventHandler ChangeQuestion;
    }
}
