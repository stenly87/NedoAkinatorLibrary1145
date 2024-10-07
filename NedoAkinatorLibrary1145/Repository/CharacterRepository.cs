using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NedoAkinatorLibrary1145.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace NedoAkinatorLibrary1145.Repository
{
    public class CharacterRepository : BaseRepository<CharacterRecord>
    {
        public override void Create(CharacterRecord item)
        {
            var db = GetDB();
            db.Characters.Add(new DB.Character { Title = item.Title,
                Image = item.Image,
            });            
        }

        public override void Delete(int id)
        {
            var db = GetDB();
            var delete = db.Characters.AsNoTracking().FirstOrDefault(s => s.Id == id);
            if (delete != null)
            {
                db.Characters.Remove(delete);
            }
        }

        public override CharacterRecord Get(int id)
        {
            var db = GetDB();
            var s = db.Characters.AsNoTracking().FirstOrDefault(s => s.Id == id);
            return new CharacterRecord (s.Id, s.Title, s.Image);
        }

        public override IEnumerable<CharacterRecord> GetList()
        {
            var db = GetDB();
            return db.Characters.
                Select(s => new CharacterRecord(s.Id, s.Title, s.Image)).
                AsNoTracking();
        }

        public override void Update(CharacterRecord item)
        {
            var db = GetDB();
            var origin = db.Characters.Find(item.Id);
            db.Entry(origin).CurrentValues.SetValues(item);
        }
    }
}
