using API_TEST.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace API_TEST.Repositories
{
    public interface IRepositoryWrapper
    {
        IRepositoryBase<Genre> Genres { get; }
        IRepositoryBase<Format> Formats { get; }
        IRepositoryBase<Recording> Recordings { get; }
        IRepositoryBase<PlaybackSchedule> PlaybackSchedules { get; }

        void Save();
        IDbContextTransaction Transaction();
    }
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly MyDBContext dbContext;
        private IRepositoryBase<Genre> genre;
        private IRepositoryBase<Format> format;
        private IRepositoryBase<Recording> recording;
        private IRepositoryBase<PlaybackSchedule> playbackSchedule;

        public RepositoryWrapper(MyDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IRepositoryBase<Genre> Genres
        {
            get
            {
                if (genre == null)
                {
                    genre = new RepositoryBase<Genre>(dbContext);
                }
                return genre;
            }
        }

        public IRepositoryBase<Format> Formats
        {
            get
            {
                if (format == null)
                {
                    format = new RepositoryBase<Format>(dbContext);
                }
                return format;
            }
        }

        public IRepositoryBase<Recording> Recordings
        {
            get
            {
                if (recording == null)
                {
                    recording = new RepositoryBase<Recording>(dbContext);
                }
                return recording;
            }
        }

        public IRepositoryBase<PlaybackSchedule> PlaybackSchedules
        {
            get
            {
                if (playbackSchedule == null)
                {
                    playbackSchedule = new RepositoryBase<PlaybackSchedule>(dbContext);
                }
                return playbackSchedule;
            }
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public IDbContextTransaction Transaction()
        {
            return dbContext.Database.BeginTransaction();
        }
    }
}
