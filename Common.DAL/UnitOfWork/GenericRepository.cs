﻿using inzLessons.Common.Context;
using inzLessons.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DAL.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private InzlessonsdbContext context = new InzlessonsdbContext();

        private GenericRepository<Membership> _membershipRepository;
        private GenericRepository<Role> _roleRepository;
        private GenericRepository<Users> _usersRepository;

        public GenericRepository<Membership> MembershipRepository => _membershipRepository ?? new GenericRepository<Membership>(context);
        public GenericRepository<Role> RoleRepository => _roleRepository ?? new  GenericRepository<Role>(context);
        public GenericRepository<Users> UsersRepository => _usersRepository ?? new GenericRepository<Users>(context);
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
