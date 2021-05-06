using FakeAtlas.Context.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeAtlas.Context.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private BookingDBContext db = new BookingDBContext();

        private BookingDBRepository<BookingUser> userRepository;

        public BookingDBRepository<BookingUser> BookingUsers
        {
            get
            {
                if (userRepository == null)
                    userRepository = new BookingDBRepository<BookingUser>(db);
                return userRepository;
            }
        }

        private BookingDBRepository<UniqueAddress> addressRepository;

        public BookingDBRepository<UniqueAddress> UniqueAddress
        {
            get
            {
                if (addressRepository == null)
                    addressRepository = new BookingDBRepository<UniqueAddress>(db);
                return addressRepository;
            }
        }


        private BookingDBRepository<UniqueAddress> ordersRepository;

        public BookingDBRepository<UniqueAddress> OrdersRepository;

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

