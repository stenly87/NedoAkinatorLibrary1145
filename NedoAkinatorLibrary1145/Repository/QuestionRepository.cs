using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using NedoAkinatorLibrary1145.DB;

namespace NedoAkinatorLibrary1145.Repository
{
    public class QuestionRepository : BaseRepository<Question>
    {
        public override void Create(Question item)
        {
            var db = GetDB();
            db.Questions.Add(new Question
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

        public override Question Get(int id)
        {
            var db = GetDB();
            var s = db.Questions.AsNoTracking().FirstOrDefault(s => s.Id == id);
            return s;
        }

        public override IEnumerable<Question> GetList()
        {
            var db = GetDB();
            return db.Questions.
                AsNoTracking();
        }

        public override void Update(Question item)
        {
            var db = GetDB();
            var origin = db.Questions.Find(item.Id);
            db.Entry(origin).CurrentValues.SetValues(item);
        }

        internal Question GetRandom()
        {
            var db = GetDB();
            int count = db.Questions.Count();
            Random rnd = new Random();
            var s = db.Questions.AsNoTracking()
                .Skip(rnd.Next(0, count-1)).First();
            return s;
        }
    }
}
