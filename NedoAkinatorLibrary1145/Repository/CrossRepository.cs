using Microsoft.EntityFrameworkCore;
using NedoAkinatorLibrary1145.DB;

namespace NedoAkinatorLibrary1145.Repository
{
    public class CrossRepository : BaseRepository<Cross>
    {
        public override void Create(Cross item)
        {
            var db = GetDB();
            db.Crosses.Add(new Cross
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

        public Cross Get(int idHistory, int idQuestion)
        {
            var db = GetDB();
            var s = db.Crosses.AsNoTracking().FirstOrDefault(s =>
            s.IdQuestion == idQuestion && s.IdHistory == idHistory);
            return s;
        }

        public override Cross Get(int id)
        {
            throw new Exception("Не используй меня, дурак");
        }

        public override IEnumerable<Cross> GetList()
        {
            var db = GetDB();
            return db.Crosses.
                AsNoTracking();
        }

        public override void Update(Cross item)
        {
            var db = GetDB();
            var origin = db.Crosses.AsNoTracking().FirstOrDefault(s =>
            s.IdQuestion == item.IdQuestion && s.IdHistory == item.IdHistory);
            db.Entry(origin).CurrentValues.SetValues(item);
        }

        internal int GetQuestionByCharacter(int idCharacter)
        {
            var db = GetDB();
            return db.Crosses.
                Include(s => s.IdHistoryNavigation).
                Where(s => s.IdHistoryNavigation.IdCharacter == idCharacter)
                .Count();
        }

        internal IEnumerable<Cross> GetQuestionsByHistory(int id)
        {
            var db = GetDB();
            return db.Crosses.
                Where(s => s.IdHistory == id).
                AsNoTracking();
        }

        internal int GetSameReaction(int idCharacter, int idQuestion, int reaction)
        {
            var db = GetDB();
            return db.Crosses.
                Include(s => s.IdHistoryNavigation).
                Where(s => s.Reaction == reaction &&
                s.IdQuestion == idQuestion &&
                s.IdHistoryNavigation.IdCharacter == idCharacter)
                .Count();
        }
    }
}
