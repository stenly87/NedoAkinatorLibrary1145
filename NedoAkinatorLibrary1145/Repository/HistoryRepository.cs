using NedoAkinatorLibrary1145.Model;
using Microsoft.EntityFrameworkCore;

namespace NedoAkinatorLibrary1145.Repository
{
    public class HistoryRepository : BaseRepository<HistoryRecord>
    {
        public override void Create(HistoryRecord item)
        {
            var db = GetDB();
            db.Histories.Add(new DB.History
            {
                 IdCharacter = item.IdCharacter
            });
        }

        public override void Delete(int id)
        {
            var db = GetDB();
            var delete = db.Histories.AsNoTracking().FirstOrDefault(s => s.Id == id);
            if (delete != null)
            {
                db.Histories.Remove(delete);
            }
        }

        public override HistoryRecord Get(int id)
        {
            var db = GetDB();
            var s = db.Histories.Include(s=>s.IdCharacterNavigation).AsNoTracking().FirstOrDefault(s => s.Id == id);
            return new HistoryRecord(s.Id, s.IdCharacter, new CharacterRecord (
                 s.IdCharacterNavigation.Id,  s.IdCharacterNavigation.Title,
                 s.IdCharacterNavigation.Image));
        }

        public override IEnumerable<HistoryRecord> GetList()
        {
            var db = GetDB();
            return db.Histories.Include(s => s.IdCharacterNavigation).AsNoTracking().
                Select(s => new HistoryRecord(s.Id, s.IdCharacter, new CharacterRecord(
                s.IdCharacterNavigation.Id, s.IdCharacterNavigation.Title,
                s.IdCharacterNavigation.Image)));
        }

        public override void Update(HistoryRecord item)
        {
            var db = GetDB();
            var origin = db.Histories.Find(item.Id);
            db.Entry(origin).CurrentValues.SetValues(item);
        }

        internal int Count(int id)
        {
            var db = GetDB();
            return db.Histories.AsNoTracking()
                .Where(s=>s.Id == id).Count();
        }

        internal int Count()
        {
            var db = GetDB();
            return db.Histories.Count();
        }

        internal IEnumerable<HistoryRecord> GetHistoryByCharacter(int id)
        {
            var db = GetDB();
            return db.Histories.Include(s => s.IdCharacterNavigation).
                AsNoTracking().
                Where(s => s.IdCharacter == id).
                Select(s => new HistoryRecord(s.Id, s.IdCharacter, new CharacterRecord(
                s.IdCharacterNavigation.Id, s.IdCharacterNavigation.Title,
                s.IdCharacterNavigation.Image)))
                ;
        }

        internal int GetLastID()
        {
            var db = GetDB();
            var history = db.Histories.AsNoTracking().Last();
            return history.Id;
        }
    }
}
