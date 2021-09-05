﻿using System;
using System.Linq;
using MeetingDateProposer.DataLayer;
using MeetingDateProposer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingDateProposer.BusinessLayer.DatabaseServices
{
    public class UserService: IUserService
    {
        private readonly ApplicationContext _appContext;

        public UserService(ApplicationContext applicationContext)
        {
            _appContext = applicationContext;
        }

        public void AddUserToDb(User user)
        {
            _appContext.ApplicationUsers.Add(user);
            _appContext.SaveChanges();
        }

        public User RemoveUserFromDb(Guid id)
        {
            if (_appContext.ApplicationUsers.Any(u => u.Id == id))
            {
                var user = _appContext.ApplicationUsers.First(u => u.Id == id);
                _appContext.ApplicationUsers.Remove(user);
                _appContext.SaveChanges();
                return user;
            }
            else
            {
                return null;
            }
        }

        public User GetUserByIdFromDb(Guid id)
        {
            if (_appContext.ApplicationUsers.Any(u => u.Id == id))
            {
                return _appContext.ApplicationUsers
                    .Include(u => u.Calendars)
                    .First(u => u.Id == id);
            }
            else
            {
                return null;
            }
        }
    }
}
