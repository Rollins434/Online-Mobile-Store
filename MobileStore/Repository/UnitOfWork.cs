using MobileStore.DatabaseFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileStore.Repository
{
    public class UnitOfWork :IDisposable
    {
        private dbMobileStoreEntities DBEntity = new dbMobileStoreEntities();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IRepository<Tbl_EntityType> GetRepositoryInstance<Tbl_EntityType>() where Tbl_EntityType: class
        {
            return new GenericRepository<Tbl_EntityType>(DBEntity);
        }
        public void SaveChanges()
        {
            DBEntity.SaveChanges();
        }
        private bool disposed = false;
        protected void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DBEntity.Dispose();
                }
            }
            this.disposed = true;

        }

    }
}