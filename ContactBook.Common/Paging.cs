using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactBook.Common
{
    public class Paging<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalNumberOfPages { get; private set; }
        public int UsersPerPage { get; private set; }
        public int TotalUsersToBeDisplayed { get; private set; }
        public bool HasPrevious => (CurrentPage > 1);
        public bool HasNext => (CurrentPage < TotalNumberOfPages);

        public Paging(List<T> processedUsers, int totalUsersToBeDisplayed, int currentPage, int usersPerPage)
        {
            TotalUsersToBeDisplayed = totalUsersToBeDisplayed;
            CurrentPage = currentPage;
            UsersPerPage = usersPerPage;
            TotalNumberOfPages = (int)Math.Ceiling(Count / (double)usersPerPage);
            AddRange(processedUsers);
        }
        public static Paging<T> CreatePages(List<T> users, int currentPage, int usersPerPage)
        {
            int totalUsersToBeDisplayed = users.Count();
            var processedUsers = users.Skip(usersPerPage * (currentPage - 1))
                .Take(usersPerPage)
                .ToList();
            return new Paging<T>(processedUsers, totalUsersToBeDisplayed, currentPage, usersPerPage);
        }
    }

}
