using System.Collections.Generic;

namespace CilPlayground.Models
{
    public class UserCodeViewModel
    {
        public List<DocumentViewModel> Documents { get; set; }
        public int UserId { get; set; }
        public DocumentViewModel CurrentDocument { get; set; }
    }
}