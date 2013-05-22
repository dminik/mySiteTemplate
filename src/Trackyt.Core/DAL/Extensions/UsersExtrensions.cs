﻿using System.Linq;

using Trackyt.Core.DAL.DataModel;

namespace Trackyt.Core.DAL.Extensions
{
    public static class UsersExtensions
    {
        public static User WithEmail(this IQueryable<User> users, string email)
        {
            return users.Where(u => u.Email == email).SingleOrDefault();
        }

        public static User WithId(this IQueryable<User> users, int id)
        {
            return users.Where(u => u.Id == id).SingleOrDefault();
        }

        public static IQueryable<User> WithTemp(this IQueryable<User> users, bool flag)
        {
            return users.Where(u => u.Temp == flag);
        }

		public static Project WithName(this IQueryable<Project> projects, string name)
		{
			return projects.Where(u => u.Name == name).SingleOrDefault();
		}
    }
}
