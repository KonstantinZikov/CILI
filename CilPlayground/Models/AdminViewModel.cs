using System.Collections.Generic;

namespace CilPlayground.Models
{
    public class AdminViewModel
    {
        public List<UserViewModel> Users { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}