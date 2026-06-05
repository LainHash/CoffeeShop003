namespace CoffeeShop.Application.Common.Constants
{
    public static class PageConstants
    {
        public const string Home = "Home";
        public const string Menu = "Menu";
        public const string Contact = "Contact";
        public const string About = "About";
        public const string Service = "Service";
        public const string Testimonial = "Testimonial";
        public const string Reservation = "Reservation";

        public static string RedirectToPage(this string route)
        {
            var routeDict = new Dictionary<string, string>()
            {
                { Home.ToLower(),  "/"},
                { Menu.ToLower(), "/Menu" },
                { Contact.ToLower(), "/Contact" },
                { About.ToLower(), "/About" },
                { Service.ToLower(), "/Service" },
                { Testimonial.ToLower(), "/Testimonial" },
                { Reservation.ToLower(), "/Reservation" }
            };

            return routeDict[route.ToLower()];
        }

        public static string ViName(this string name) 
        {
            var viDict = new Dictionary<string, string>()
            {
                { Home.ToLower(),  "Trang Chủ"},
                { Menu.ToLower(), "Thực Đơn" },
                { Contact.ToLower(), "Liên Hệ" },
                { About.ToLower(), "Giới Thiệu" },
                { Service.ToLower(), "Dịch Vụ" },
                { Testimonial.ToLower(), "Đánh Giá" },
                { Reservation.ToLower(), "Đặt Bàn" }
            };

            return viDict[name];
        }
    }
}
