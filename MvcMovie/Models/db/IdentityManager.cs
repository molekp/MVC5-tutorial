using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Threading.Tasks;
using System;

namespace MvcMovie.Models.db
{
    public class IdentityManager :IDisposable
    {
        public IdentityManager(){
            this.RoleManager =  new RoleManager<IdentityRole>(
            new RoleStore<IdentityRole>(_db));
            this.UserManager = new UserManager<IdentityUser>(
            new UserStore<IdentityUser>(_db));
        }
        // Swap ApplicationRole for IdentityRole:
        public RoleManager<IdentityRole> RoleManager { get; private set; }

        public UserManager<IdentityUser> UserManager  {get; private set;}
            
        ApplicationDbContext _db = new ApplicationDbContext();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~IdentityManager() 
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing )
            {
                if (UserManager != null)
                {
                    this.UserManager.Dispose();
                    this.UserManager = null;
                }
                if (RoleManager != null)
                {
                    this.RoleManager.Dispose();
                    this.RoleManager = null;
                }
            }
        }
        
        public bool RoleExists(string name)
        {
            return RoleManager.RoleExists(name);
        }


        public bool CreateRole(string name)
        {
            // Swap ApplicationRole for IdentityRole:
            var idResult = RoleManager.Create(new IdentityRole(name));
            return idResult.Succeeded;
        }


        public bool CreateUser(IdentityUser user, string password)
        {
            var idResult = UserManager.Create(user, password);
            return idResult.Succeeded;
        }

        public async Task<IdentityResult> CreateUserAsync(IdentityUser user, string password)
        {
            return await UserManager.CreateAsync(user, password);
        }

        public bool AddUserToRole(string userId, string roleName)
        {
            var idResult = UserManager.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }

        public async Task<IdentityResult> AddUserToRoleAsync(string userId, string roleName)
        {
            return await UserManager.AddToRoleAsync(userId, roleName);
        }

        public async Task<IdentityResult> CreateUserWithStandardRoleAsync(IdentityUser user, string password)
        {
            var result = await CreateUserAsync(user, password);
            if (result.Succeeded)
            {
                var assigned =await AddUserToRoleAsync(user.Id, "User");
            }
            return result;
        }

        public void ClearUserRoles(string userId)
        {
            var user = UserManager.FindById(userId);
            var userRoles = new List<IdentityUserRole>();
            var roleNames=  user.Roles.Select(x => this.RoleManager.FindById(x.RoleId).Name).ToArray();
            Roles.RemoveUserFromRoles(user.Id, roleNames);
        }


        public void RemoveFromRole(string userId, string roleName)
        {
            UserManager.RemoveFromRole(userId, roleName);
        }


        public void DeleteRole(string roleId)
        {
            var roleUsers = _db.Users.Where(u => u.Roles.Any(r => r.RoleId == roleId));
            var role = _db.Roles.Find(roleId);

            foreach (var user in roleUsers)
            {
                this.RemoveFromRole(user.Id, role.Name);
            }
            _db.Roles.Remove(role);
            _db.SaveChanges();
        }
    }
}