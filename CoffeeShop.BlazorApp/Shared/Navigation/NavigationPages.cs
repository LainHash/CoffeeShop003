using CoffeeShop.BlazorApp.Common.Enums;
using CoffeeShop.BlazorApp.Common.Records;

namespace CoffeeShop.BlazorApp.Shared.Navigation
{
    public static class NavigationPages
    {
        public static readonly IReadOnlyDictionary<PageType, PageInfo> Data = new Dictionary<PageType, PageInfo>()
        {
            [PageType.Home] = new("/", "Trang Chủ"),
            [PageType.Contact] = new("/Contact", "Liên Hệ"),
            [PageType.About] = new("/About", "Giới Thiệu"),
            [PageType.Service] = new("/Service", "Dịch Vụ"),
            [PageType.Testimonial] = new("/Testimonial", "Đánh Giá"),
            [PageType.Menu] = new("/Menu", "Thực Đơn"),
            [PageType.Reservation] = new("/Reservation", "Đặt Bàn"),
            [PageType.Table] = new("/Table", "Sơ Đồ Bàn")
        };
    }
}