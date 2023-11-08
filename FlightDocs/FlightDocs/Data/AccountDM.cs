using FlightDocs.Models;
using Microsoft.EntityFrameworkCore;
using System.Security;

namespace FlightDocs.Data
{
    public class AccountDM
    {
        private readonly ApplicationDbContext _context;

        public AccountDM(ApplicationDbContext context)
        {
            _context = context;
        }

        public Account GetAccount(int Id) => _context.Account.Find(Id);

        public User GetUser(int Id) => _context.Users.Find(Id);

        public Account GetUserAccount(string name, string password)
        {
            var user  = _context.Users.FirstOrDefault(u => u.Name == name);

            if (user == null)
                return null;
            else if (user.Password != password)
                return null;

            var account = _context.Account
                .Include(a => a.User)
                .Include(a => a.Role)
                .FirstOrDefault(a => a.UserId == user.Id);

            if (!account.IsActive)
                return null;

            return account;

        }

        public string AddAccount(Account account)
        {
            _context.Account.Add(account);
            _context.SaveChanges();
            return "Add success!";
        }

        //Update to change account's group, active status and user.
        public string UpdateAccount(Account account)
        {
            _context.Account.Update(account);
            _context.SaveChanges();
            return "Update success!";
        }

        public void UpdateAccountUser(Account account, int newUserId)
        {
            //User newUser = GetUser(newUserId);

            account.UserId = newUserId;

            UpdateAccount(account);
        }

        public void UpdateAccountGroup(Account account, int? newGroupId)
        { 
            account.GroupId = newGroupId;

            UpdateAccount(account);
        }

        public void UpdateAccountStatus(Account account)
        {
            account.IsActive = !account.IsActive;

            UpdateAccount(account);
        }

        public string AddUser(User user) 
        {
            //user.Name = "Tester";
            user.Email = user.Name + "@vietjetair.com.vn";
            _context.Users.Add(user);
            _context.SaveChanges();
            return "Add success!";
        }

        public string UpdateUser(User user) 
        {            
            _context.Users.Update(user);
            _context.SaveChanges();
            return "Update success!";
        }
    }
}
