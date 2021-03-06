﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyt.Core.DAL.DataModel;
using Trackyt.Core.DAL.Extensions;
using System.Data.Linq;
using System.Configuration;

namespace Trackyt.Core.DAL.Repositories.Impl
{
    public class UsersRepository : IUsersRepository
    {
        private TrackytDataContext _context;

        public UsersRepository()
            : this(new TrackytDataContext())
        {

        }

        //used in unit tests
        public UsersRepository(TrackytDataContext  context)
        {
            _context = context;
        }

        #region IUsersRepository Members

        public IQueryable<User> Users
        {
            get
            {
                return _context.Users.AsQueryable();
            }
        }

        public void Save(User user)
        {
            if (user.Id == 0)
            {
                if (Users.WithEmail(user.Email) != null)
                    throw new DuplicateKeyException(user);

                _context.Users.Add(user);
            }

            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        #endregion
    }
}
