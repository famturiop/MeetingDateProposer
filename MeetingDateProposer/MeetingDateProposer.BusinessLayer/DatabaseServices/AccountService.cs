using System;
using System.Linq;
using MeetingDateProposer.DataLayer;
using MeetingDateProposer.Domain.Models;
using MeetingDateProposer.Domain.Models.AccountViewModels;
using Microsoft.EntityFrameworkCore;

namespace MeetingDateProposer.BusinessLayer.DatabaseServices
{
    public class AccountService: IAccountService
    {
        private readonly ApplicationContext _appContext;

        public AccountService(ApplicationContext applicationContext)
        {
            _appContext = applicationContext;
        }

        public void AddUserToDb(ApplicationUser user)
        {
            _appContext.ApplicationUsers.Add(user);
            _appContext.SaveChanges();
        }

        public AccountUser RemoveUserFromDb(Guid id)
        {
            if (_appContext.Users.Any(u => u.Id == id))
            {
                var user = _appContext.Users.First(u => u.Id == id);
                _appContext.Users.Remove(user);
                _appContext.SaveChanges();
                return user;
            }
            else
            {
                return null;
            }
        }

        public AccountUser GetUserByIdFromDb(Guid id)
        {
            if (_appContext.Users.Any(u => u.Id == id))
            {
                return _appContext.Users
                    .First(u => u.Id == id);
            }
            else
            {
                return null;
            }
        }
    }
}