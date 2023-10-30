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

        public string AddAccount(Account account) 
        {
            _context.Account.Add(account);
            _context.SaveChanges();
            return "Add success!";
        }

        //Update to change account's group and user.
        public string UpdateAccount(Account account)
        {
            _context.Account.Update(account);
            _context.SaveChanges();
            return "Update success!";
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
