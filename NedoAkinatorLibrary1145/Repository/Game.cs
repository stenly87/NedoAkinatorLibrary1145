using NedoAkinatorLibrary1145.DB;
using NedoAkinatorLibrary1145.Repository;

namespace NedoAkinatorView
{
    public class Game : IDisposable
    {
        History currentHistory;
        Question currentQuestion;
        List<Question> questionsByRank;
        List<Character> characterByRank;
        Character targetCharacter;

        public Question GetNextQuestion()
        {
            return currentQuestion;
        }

        public void RememberReaction(Question question, int reaction)
        {


            ChangeQuestion?.Invoke(this, new EventArgs());
        }

        public void Start()
        {
            var questionRep = new QuestionRepository();
            var historyRep = new HistoryRepository();
            currentHistory = new History();
            historyRep.Create(currentHistory);
            historyRep.Save();
            currentHistory.Id = historyRep.GetLastID();

            var rep = new CharacterRepository();
            var allCharacters = new List<Character>(rep.GetList());
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
            List<Cross> crosss = new();
            if (historyByCharacter.Count != 0)
            {
                for (int i = 0; i < historyByCharacter.Count; i++)
                {
                    var questions = crossRep.
                        GetQuestionsByHistory(historyByCharacter[i].Id);
                    crosss.AddRange(questions);
                }
                int allQuestions = crosss.Count();
                var unicQuestion = crosss.
                    GroupBy(s => s.IdQuestion).
                    Select(s => (s.Key, Count: s.Count())).OrderByDescending(s => s.Count);
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