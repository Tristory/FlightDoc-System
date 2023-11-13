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

        public List<Account> GetAccounts()
        {
            return _context.Account
                .Include(e => e.User).ToList();
        }

        public List<Account> GetGroupMembers(int groupId)
        {
            return _context.Account
                .Include(e => e.Group)
                .Include(e => e.User)
                .Include(e => e.Role)
                .Where(e => e.GroupId == groupId).ToList();
        }

        public List<Account> GetNotGroupMembers(int groupId)
        {
            return _context.Account
                .Include(e => e.User)
                .Where(e => e.GroupId != groupId).ToList();
        }

        public Account GetAccount(int Id) => _context.Account.Find(Id);

        public Account GetAccountWithUser(int Id)
        {
            return _context.Account
                .Include(e => e.User)
                .FirstOrDefault(e => e.Id == Id);
        }

        public Account GetUserAccount(int userId)
        {
            return _context.Account.FirstOrDefault(e => e.UserId == userId);
        }

        public List<User> GetNotCurrentUser(int userId)
        {
            return _context.Users.Where(e => e.Id != userId).ToList();
        }

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

        public string UpdateAccountUser(int acId, int newUserId, string password)
        {
            //User newUser = GetUser(newUserId);

            var account = GetAccountWithUser(acId);

            if (account.User.Password == password)
                return "Wrong password!";


            account.UserId = newUserId;

            return UpdateAccount(account);
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
