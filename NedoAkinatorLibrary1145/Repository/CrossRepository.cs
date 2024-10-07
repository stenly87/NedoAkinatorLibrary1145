using NedoAkinatorLibrary1145.Model;
using Microsoft.EntityFrameworkCore;
using NedoAkinatorLibrary1145.DB;

namespace NedoAkinatorLibrary1145.Repository
{
    public class CrossRepository : BaseRepository<CrossRecord>
    {
        public override void Create(CrossRecord item)
        {
            var db = GetDB();
            db.Crosses.Add(new DB.Cross
            {
                 IdHistory = item.IdHistory,
                 IdQuestion = item.IdQuestion, 
                 Reaction = item.Reaction
            });
        }

        public void Delete(int idHistory, int idQuestion)
        {
            var db = GetDB();
            var delete = db.Crosses.AsNoTracking().FirstOrDefault(s => 
            s.IdHistory == idHistory && s.IdQuestion == idQuestion);
            if (delete != null)
            {
                db.Crosses.Remove(delete);
            }
        }

        public override void Delete(int id)
        {
            throw new Exception("Не используй меня, дурак");
        }

        public CrossRecord Get(int idHistory, int idQuestion)
        {
            var db = GetDB();
            var s = db.Crosses.AsNoTracking().FirstOrDefault(s =>
            s.IdQuestion == idQuestion && s.IdHistory == idHistory);
            return new CrossRecord(s.IdHistory, s.IdQuestion, s.Reaction);
        }

        public override CrossRecord Get(int id)
        {
            throw new Exception("Не используй меня, дурак");
        }

        public override IEnumerable<CrossRecord> GetList()
        {
            var db = GetDB();
            return db.Crosses.
                Select(s => new CrossRecord(s.IdHistory, s.IdQuestion, s.Reaction)).
                AsNoTracking();
        }

        public override void Update(CrossRecord item)
        {
            var db = GetDB();
            var origin = db.Crosses.AsNoTracking().FirstOrDefault(s =>
            s.IdQuestion == item.IdQuestion && s.IdHistory == item.IdHistory);
            db.Entry(origin).CurrentValues.SetValues(item);
        }

        internal IEnumerable<CrossRecord> GetQuestionsByHistory(int id)
        {
            var db = GetDB();
            return db.Crosses.
                Select(s => new CrossRecord(s.IdHistory, s.IdQuestion, s.Reaction)).
                Where(s => s.IdHistory == id).
                AsNoTracking();
        }
    }
}
