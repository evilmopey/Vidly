using Microsoft.AspNetCore.Mvc.Rendering;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomerFormViewModel
    {
        public IEnumerable<SelectListItem> MembershipTypes { get; set; }
        public Customer Customer { get; set; }
    }
}
