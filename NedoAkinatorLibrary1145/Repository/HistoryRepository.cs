using Microsoft.EntityFrameworkCore;
using NedoAkinatorLibrary1145.DB;

namespace NedoAkinatorLibrary1145.Repository
{
    public class HistoryRepository : BaseRepository<History>
    {
        public override void Create(History item)
        {
            var db = GetDB();
            db.Histories.Add(new History
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

        public override History Get(int id)
        {
            var db = GetDB();
            var s = db.Histories.Include(s=>s.IdCharacterNavigation).AsNoTracking().FirstOrDefault(s => s.Id == id);
            return s;
        }

        public override IEnumerable<History> GetList()
        {
            var db = GetDB();
            return db.Histories.Include(s => s.IdCharacterNavigation).AsNoTracking().
                ToList();
        }

        public override void Update(History item)
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

        internal IEnumerable<History> GetHistoryByCharacter(int id)
        {
            var db = GetDB();
            return db.Histories.Include(s => s.IdCharacterNavigation).
                AsNoTracking().
                Where(s => s.IdCharacter == id).ToList();
        }

        internal int GetLastID()
        {
            var db = GetDB();
            var history = db.Histories.AsNoTracking().OrderBy(s=>s.Id).Last();
            return history.Id;
        }
    }
}
