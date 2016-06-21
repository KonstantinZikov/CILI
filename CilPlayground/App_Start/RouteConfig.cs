using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CilPlayground
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Try",
                url: "Try",
                defaults: new { controller = "Try", action = "Index" }
            );

            //Instructions

            routes.MapRoute(
                name: "Instructions",
                url: "Instructions",
                defaults: new { controller = "Instructions", action = "Index" }
            );

            routes.MapRoute(
                name: "Add instruction",
                url: "Instructions/Create",
                defaults: new { controller = "Instructions", action = "Create" }
            );

            routes.MapRoute(
                name: "Delete instruction",
                url: "Instructions/Delete",
                defaults: new { controller = "Instructions", action = "Delete" }
            );

            routes.MapRoute(
                name: "Save instruction",
                url: "Instructions/Update",
                defaults: new { controller = "Instructions", action = "Update" }
            );

            //Admin

            routes.MapRoute(
                name: "Admin",
                url: "Admin",
                defaults: new { controller = "Admin", action = "Index" }
            );

            routes.MapRoute(
                name: "AdminUpdate",
                url: "Admin/Update",
                defaults: new { controller = "Admin", action = "Update" }
            );

            routes.MapRoute(
                name: "AdminDelete",
                url: "Admin/Delete",
                defaults: new { controller = "Admin", action = "Delete" }
            );

            routes.MapRoute(
                name: "AdminCreate",
                url: "Admin/Create",
                defaults: new { controller = "Admin", action = "Create" }
            );

            //User

            routes.MapRoute(
                name: "UserSignUp",
                url: "User/SignUp",
                defaults: new { controller = "User", action = "SignUp" }
            );

            routes.MapRoute(
                name: "UserSignIn",
                url: "User/SignIn",
                defaults: new { controller = "User", action = "SignIn" }
            );

            routes.MapRoute(
                name: "UserSignOut",
                url: "User/SignOut",
                defaults: new { controller = "User", action = "SignOut" }
            );

            //Examples

            routes.MapRoute(
                name: "Examples",
                url: "Examples",
                defaults: new { controller = "Examples", action = "Index" }
            );

            routes.MapRoute(
                name: "Interpreter",
                url: "Interpreter",
                defaults: new { controller = "Interpreter", action = "Index" }
            );

            routes.MapRoute(
                name: "UserCodes",
                url: "UserCodes",
                defaults: new { controller = "UserCodes", action = "Index" }
            );

            routes.MapRoute(
                name: "Execute",
                url: "Execute",
                defaults: new { controller = "Execute", action = "Execute" }
            );

            routes.MapRoute(
                name: "Continue",
                url: "Continue",
                defaults: new { controller = "Execute", action = "Continue" }
            );

            routes.MapRoute(
                name: "Stop",
                url: "Stop",
                defaults: new { controller = "Execute", action = "Stop" }
            );

            //Error

            routes.MapRoute(
                 "404-PageNotFound",
                "{*url}",
                new { controller = "Error", action = "NotFound" }
            );

        }
    }
}

