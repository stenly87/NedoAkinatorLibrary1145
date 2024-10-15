using NedoAkinatorLibrary1145.DB;
using NedoAkinatorLibrary1145.Repository;

namespace NedoAkinatorView
{
    public class Game : IDisposable
    {
        History currentHistory;
        Question currentQuestion;
        IEnumerable<Question> allQuestions;
        List<Cross> reactionHistory = new ();
        IEnumerable<Character> characterByRank;
        public Character targetCharacter;

        public Question GetNextQuestion()
        {
            if (currentQuestion == null)
            {
                ReRankCharacters();
                ReRankQuestions();
            }
            return currentQuestion;
        }

        private void ReRankQuestions()
        {
            var rep = new CrossRepository();
            var notAsked = allQuestions.Where(s =>
                reactionHistory.FirstOrDefault(r => r.IdQuestion == s.Id) == null);
            
            foreach (var question in notAsked)
            {
                List<float> Bj = new List<float>();
                foreach (var cross in reactionHistory)
                {
                    int count = rep.GetSameReaction(targetCharacter.Id,
                        cross.IdQuestion,
                        cross.Reaction.Value);

                    int countTotal = rep.GetQuestionByCharacter(targetCharacter.Id);
                    
                    if (countTotal > 0)
                        Bj.Add(count / (float)countTotal);
                }
                if (Bj.Count > 0)
                    question.Rank = Or(Bj.ToArray());
            }
            notAsked = notAsked.OrderByDescending(s=> s.Rank);
            currentQuestion = notAsked.FirstOrDefault();
        }

        private void ReRankCharacters()
        {
            var rep = new CrossRepository();
            foreach (var character in characterByRank)
            {
                List<float> Bj = new List<float>();
                foreach (var cross in reactionHistory)
                {
                    int count = rep.GetSameReaction(character.Id,
                        cross.IdQuestion,
                        cross.Reaction.Value);

                    int countTotal = rep.GetQuestionByCharacter(character.Id);

                    if (countTotal > 0) 
                        Bj.Add(count / (float)countTotal);
                }
                if (Bj.Count > 0)
                    character.Rank = Or(Bj.ToArray());
            }
            characterByRank = characterByRank.OrderByDescending(s=> s.Rank);
            targetCharacter = characterByRank.FirstOrDefault();
        }

        public static float Or(params float[] eventsPossibility)
        {
            var summ = eventsPossibility.Aggregate((p, x) => p += x);

            return summ / eventsPossibility.Length;
        }

        public void RememberReaction(Question question, int reaction)
        {
            var rep = new CrossRepository();
            var react = new Cross
            {
                IdHistory = currentHistory.Id,
                IdQuestion = question.Id,
                Reaction = reaction
            };
            rep.Create(react);
            rep.Save();
            reactionHistory.Add(react);


            currentQuestion = null;
            ChangeQuestion?.Invoke(this, new EventArgs());
        }

        public void Start()
        {
            var questionRep = new QuestionRepository();
            var historyRep = new HistoryRepository();
            allQuestions = questionRep.GetList();
            foreach (var item in allQuestions)
                item.Rank = 1.0 / allQuestions.Count();
            allQuestions = allQuestions.OrderBy(s => s.Rank);

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
                currentQuestion = allQuestions.First();

            ChangeQuestion?.Invoke(this, new EventArgs());
        }

        public void Dispose()
        {
            ChangeQuestion = null;
        }

        public void RememberResult(Character targetCharacter)
        {
            currentHistory.IdCharacter = targetCharacter.Id;

            var hisRep = new HistoryRepository();
            hisRep.Update(currentHistory);
            hisRep.Save();
        }

        public void Save(Character character)
        {
            var rep = new CharacterRepository();
            currentHistory.IdCharacter = rep.FindIdByName(character.Title);
            if (currentHistory.IdCharacter == null)
            {
                rep.Create(character);
                rep.Save();
            }
            currentHistory.IdCharacter = rep.FindIdByName(character.Title);

            var hisRep = new HistoryRepository();
            hisRep.Update(currentHistory);
            hisRep.Save();
        }

        public event EventHandler ChangeQuestion;
    }
}