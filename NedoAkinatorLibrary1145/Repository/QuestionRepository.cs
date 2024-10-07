using Microsoft.Extensions.DependencyInjection;
using NedoAkinatorLibrary1145.Model;
using Microsoft.EntityFrameworkCore;

namespace NedoAkinatorLibrary1145.Repository
{
    public class QuestionRepository : BaseRepository<QuestionRecord>
    {
        public override void Create(QuestionRecord item)
        {
            var db = GetDB();
            db.Questions.Add(new DB.Question
            {
                Text = item.Text
            });
        }

        public override void Delete(int id)
        {
            var db = GetDB();
            var delete = db.Questions.AsNoTracking().FirstOrDefault(s => s.Id == id);
            if (delete != null)
            {
                db.Questions.Remove(delete);
            }
        }

        public override QuestionRecord Get(int id)
        {
            var db = GetDB();
            var s = db.Questions.AsNoTracking().FirstOrDefault(s => s.Id == id);
            return new QuestionRecord(s.Id, s.Text);
        }

        public override IEnumerable<QuestionRecord> GetList()
        {
            var db = GetDB();
            return db.Questions.
                Select(s => new QuestionRecord(s.Id, s.Text)).
                AsNoTracking();
        }

        public override void Update(QuestionRecord item)
        {
            var db = GetDB();
            var origin = db.Questions.Find(item.Id);
            db.Entry(origin).CurrentValues.SetValues(item);
        }

        internal QuestionRecord GetRandom()
        {
            var db = GetDB();
            int count = db.Questions.Count();
            Random rnd = new Random();
            var s = db.Questions.AsNoTracking()
                .Skip(rnd.Next(0, count-1)).First();
            return new QuestionRecord (s.Id, s.Text );
        }
    }
}
