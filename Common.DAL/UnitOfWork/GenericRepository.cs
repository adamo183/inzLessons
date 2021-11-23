using inzLessons.Common.Context;
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
        private GenericRepository<Allowedreservation> _allowedReservation;
        private GenericRepository<Lessonsgroup> _lessonsGroup;
        private GenericRepository<Lessoncondition> _lessonsCondition;
        private GenericRepository<Useringroup> _userInGroup;
        private GenericRepository<Reservation> _reservation;
        private GenericRepository<Reservationmessage> _reservationMessage;
        private GenericRepository<Messagefile> _messageFile;
        private GenericRepository<Userinreservation> _userInReservation;

        public GenericRepository<Reservation> ReservationRespository => _reservation ?? new GenericRepository<Reservation>(context);
        public GenericRepository<Useringroup> UserInGroupRepository => _userInGroup ?? new GenericRepository<Useringroup>(context);
        public GenericRepository<Lessoncondition> LessonsConditionRepository => _lessonsCondition ?? new GenericRepository<Lessoncondition>(context);
        public GenericRepository<Lessonsgroup> LessonsGroupRepository => _lessonsGroup ?? new GenericRepository<Lessonsgroup>(context);
        public GenericRepository<Allowedreservation> AllowedReservationRepository => _allowedReservation ?? new GenericRepository<Allowedreservation>(context);
        public GenericRepository<Membership> MembershipRepository => _membershipRepository ?? new GenericRepository<Membership>(context);
        public GenericRepository<Role> RoleRepository => _roleRepository ?? new  GenericRepository<Role>(context);
        public GenericRepository<Users> UsersRepository => _usersRepository ?? new GenericRepository<Users>(context);
        public GenericRepository<Reservationmessage> MessageRepository => _reservationMessage ?? new GenericRepository<Reservationmessage>(context);
        public GenericRepository<Messagefile> MessageFileRepository => _messageFile ?? new GenericRepository<Messagefile>(context);
        public GenericRepository<Userinreservation> UserInReservationRepository => _userInReservation ?? new GenericRepository<Userinreservation>(context);
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
