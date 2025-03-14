﻿using StaffingSolution.Models;

namespace StaffingSolution.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        User GetByEmail(string email);
    }
}