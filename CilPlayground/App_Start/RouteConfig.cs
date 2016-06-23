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

            //Home

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            //Try

            routes.MapRoute(
                name: "Try",
                url: "Try",
                defaults: new { controller = "Try", action = "Index" }
            );

            routes.MapRoute(
                name: "TryNextExample",
                url: "Try/NextExample",
                defaults: new { controller = "Try", action = "NextExample" }
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
                name: "ExamplesGetFile",
                url: "Examples/GetFile",
                defaults: new { controller = "Examples", action = "GetFile" }
            );

            //Interpreter

            routes.MapRoute(
                name: "Interpreter",
                url: "Interpreter",
                defaults: new { controller = "Interpreter", action = "Index" }
            );

            routes.MapRoute(
                name: "InterpreterDocument",
                url: "Interpreter/Document{fileName}",
                defaults: new { controller = "Interpreter", action = "Document" }
            );

            // User codes
            routes.MapRoute(
                name: "UserCodes",
                url: "UserCodes",
                defaults: new { controller = "UserCodes", action = "Index" }
            );

            routes.MapRoute(
                name: "UserCodesSave",
                url: "UserCodes/Save",
                defaults: new { controller = "UserCodes", action = "Save" }
            );

            routes.MapRoute(
                name: "UserCodesGetFile",
                url: "UserCodes/GetFile",
                defaults: new { controller = "UserCodes", action = "GetFile" }
            );

            routes.MapRoute(
                name: "UserCodesDocument",
                url: "UserCodes/Document/{fileName}",
                defaults: new { controller = "UserCodes", action = "Document" }
            );

            routes.MapRoute(
                name: "UserCodesDelete",
                url: "UserCodes/Delete",
                defaults: new { controller = "UserCodes", action = "Delete" }
            );

            routes.MapRoute(
                name: "UserCodesRename",
                url: "UserCodes/Rename",
                defaults: new { controller = "UserCodes", action = "Rename" }
            );

            //Execute 

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

