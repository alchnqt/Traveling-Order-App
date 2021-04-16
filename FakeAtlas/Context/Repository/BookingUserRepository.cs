using FakeAtlas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeAtlas.Context.Repository
{
    public class BookingUserRepository : IRepository<BookingUser>
    {
        private BookingDBContext db;

        public BookingUserRepository(BookingDBContext context)
        {
            this.db = context;
        }

        public IEnumerable<BookingUser> GetAll()
        {
            return db.BookingUsers;
        }

        public BookingUser Get(int id)
        {
            return db.BookingUsers.Find(id);
        }

        public void Create(BookingUser book)
        {
          
            db.BookingUsers.Add(book);
        }

        public void Update(BookingUser book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            BookingUser book = db.BookingUsers.Find(id);
            if (book != null)
                db.BookingUsers.Remove(book);
        }
    }
 
}