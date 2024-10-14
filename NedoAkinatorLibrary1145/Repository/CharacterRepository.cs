using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using NedoAkinatorLibrary1145.DB;

namespace NedoAkinatorLibrary1145.Repository
{
    public class CharacterRepository : BaseRepository<Character>
    {
        public override void Create(Character item)
        {
            var db = GetDB();
            db.Characters.Add(new Character { Title = item.Title,
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

        public override Character Get(int id)
        {
            var db = GetDB();
            var s = db.Characters.AsNoTracking().FirstOrDefault(s => s.Id == id);
            return s;
        }

        public override IEnumerable<Character> GetList()
        {
            var db = GetDB();
            return db.Characters.
                AsNoTracking();
        }

        public override void Update(Character item)
        {
            var db = GetDB();
            var origin = db.Characters.Find(item.Id);
            db.Entry(origin).CurrentValues.SetValues(item);
        }
    }
}
