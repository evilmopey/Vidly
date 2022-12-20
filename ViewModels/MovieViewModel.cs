using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieViewModel
    {
        public Movie Movie { get; set; }

        public List<Movie> Movies { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
