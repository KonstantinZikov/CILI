using System;
using System.Web.Security;
using BLL.Interface.Services;
using System.Web.Mvc;

namespace CilPlayground.Providers
{
    public class CiliRoleProvider : RoleProvider
    {


        public override string[] GetRolesForUser(string username)
        {
            //anti-pattern?
            var userService = System.Web.Mvc.DependencyResolver.Current.GetService<IUserService>();
            var roleService = System.Web.Mvc.DependencyResolver.Current.GetService<IRoleService>();

            var user = userService.Get(username);
            var role = roleService.Get(user.RoleId);
            return new [] { role.Name };
            
        }
       
        public override bool IsUserInRole(string username, string roleName)
        {
            var userService = System.Web.Mvc.DependencyResolver.Current.GetService<IUserService>();
            var roleService = System.Web.Mvc.DependencyResolver.Current.GetService<IRoleService>();

            var user = userService.Get(username);
            var role = roleService.Get(roleName);
            return (user.RoleId == role.Id);
        }


        //______________________________________________________________________________//
        //<<<<// // // // NOT IMPLEMENTED LINE // DO NOT CROSS // // // // // // //>>>>>//                                                                         
        //   __ _.-"` `'-.                                           
        //  /||\'._ __{}_(                                            
        //  ||||  |'--.__\ 
        //  |  L.(   *_\*                                             
        //  \ .-' |  __ |                                             
        //  | |   )\___/                                              
        //  |  \-'`:._]                                               
        //  \__/;      '-.  

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        //______________________________________________________________________________//
        //<<<<// // // // NOT IMPLEMENTED LINE // DO NOT CROSS // // // // // // //>>>>>//
    }
}