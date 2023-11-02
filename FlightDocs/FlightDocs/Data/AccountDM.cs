using FlightDocs.Models;
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
