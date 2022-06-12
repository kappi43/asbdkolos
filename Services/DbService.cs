using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class DbService : IDbService
    {
        MainDbContext _dbContext;

        public DbService(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool DeleteMusician(int id)
        {
            var toDelete = new Musician { IdMusician = id };
            foreach(var x in _dbContext.Musician_Tracks.Include(x => x.Musician).Include(x => x.Track).Where(x => x.IdMusician == id).ToList())
            {
                if(x.Track.Album != null)
                {
                    return false;
                }
      
            }
            _dbContext.Add(toDelete);
            _dbContext.Remove(toDelete);
            _dbContext.SaveChanges();
            return true;
        }

        public Musician GetMusician(int id)
        {
            return _dbContext.Musicians.Include(e => e.Musician_Track).Where(e => e.IdMusician == id).FirstOrDefault();
        }
    }
}
