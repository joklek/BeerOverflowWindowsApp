using BeerOverflowWindowsApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi
{
    class UserComparer : IEqualityComparer<User>
    {
        public bool Equals(User user1, User user2)
        {
            if (user1 == null) throw new ArgumentNullException(nameof(user1));
            if (user2 == null) throw new ArgumentNullException(nameof(user2));
            return user1.Username == user2.Username;
        }

        public int GetHashCode(User obj)
        {
            throw new NotImplementedException();
        }
    }
}
