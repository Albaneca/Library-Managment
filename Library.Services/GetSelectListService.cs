using Library.Data;
using Library.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services
{
    public class GetSelectListService
    {
        private readonly LibraryDbContext _db;
        public GetSelectListService(LibraryDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Author> GetAuthors()
        {
            return this._db.Authors; 
        }

        public IEnumerable<PublishHouse> GetHouses()
        {
            return this._db.PublishHouses;
        }
    }
}
