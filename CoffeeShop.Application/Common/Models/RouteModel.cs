using CoffeeShop.Application.Common.Constants;

namespace CoffeeShop.Application.Common.Models
{
    public class RouteModel
    {
        public string Name { get; set; } = null!;
        public string ViName { get; set; } = null!;
        public string Endpoint { get; set; } = null!;

        public RouteModel(string name)
        {
            Name = name;
            ViName = PageConstants.ViName(name);
            Endpoint = PageConstants.RedirectToPage(name);
        }
    }
}
