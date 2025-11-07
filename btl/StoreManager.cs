//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace btl
//{
//     class StoreManager
//    {
//        private List<Product> products;

//        public StoreManager()
//        {
//            products = new List<Product>();
//        }

//        // Them san pham
//        public void AddProduct(Product product)
//        {
//            if (products.Any(p => p.productId == product.productId))
//                throw new Exception("San pham co ma nay da ton tai!");
//            products.Add(product);
//            Console.WriteLine($"Da them san pham: {product.productName}");
//        }

//        // Xoa san pham theo ma
//        public void RemoveProduct(string productId)
//        {
//            var product = products.FirstOrDefault(p => p.productId == productId);
//            if (product == null)
//            {
//                Console.WriteLine("Khong tim thay san pham de xoa.");
//                return;
//            }
//            products.Remove(product);
//            Console.WriteLine($"Da xoa san pham co ma {productId}");
//        }

//        // Tim san pham theo ma
//        public Product SearchByProductId(string productId)
//        {
//            var product = products.FirstOrDefault(p => p.productId == productId);
//            if (product == null)
//                Console.WriteLine("Khong tim thay san pham co ma nay.");
//            return product;
//        }

//        // Hien thi toan bo san pham
//        public void DisplayAllProducts()
//        {
//            if (products.Count == 0)
//            {
//                Console.WriteLine("Danh sach san pham trong!");
//                return;
//            }

//            Console.WriteLine("\nDANH SACH SAN PHAM:");
//            foreach (var p in products)
//            {
//                p.DisplayInfo();
//                p.CalculateProfit();
//                Console.WriteLine($"Giam gia: {p.CalculateDiscount() * 100}%");
//                Console.WriteLine("-----------------------------------");
//            }
//        }

//        // Dem so luong theo loai
//        public void CountProductsByType()
//        {
//            int countPhone = products.OfType<Product.MobilePhone>().Count();
//            int countAccessory = products.OfType<Product.Accessory>().Count();

//            Console.WriteLine($"\nSo dien thoai: {countPhone}");
//            Console.WriteLine($"So phu kien: {countAccessory}");
//        }
//    }

//    //chuong trinh chinh
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            // hien thi tieng Viet dung trong Console
//            Console.OutputEncoding = System.Text.Encoding.UTF8;

//            try
//            {
//                StoreManager store = new StoreManager();

//                // Tao san pham
//                var phone1 = new Product.MobilePhone("IP01", "iPhone 15", 20000, 25000, 30, "Apple", 4000, 24);
//                var phone2 = new Product.MobilePhone("SS01", "Samsung S24", 18000, 22000, 20, "Samsung", 5000, 18);
//                var accessory1 = new Product.Accessory("AC01", "Tai nghe Bluetooth", 500, 800, 150, "Headphone", "Nhua");

//                // Them vao danh sach
//                store.AddProduct(phone1);
//                store.AddProduct(phone2);
//                store.AddProduct(accessory1);

//                // Hien thi danh sach
//                store.DisplayAllProducts();
//                store.CountProductsByType();

//                // Tim san pham
//                Console.WriteLine("\nTim san pham co ma 'SS01':");
//                var found = store.SearchByProductId("SS01");
//                if (found != null)
//                    found.DisplayInfo();

//                // Xoa san pham
//                Console.WriteLine("\nXoa san pham IP01...");
//                store.RemoveProduct("IP01");

//                // Hien thi lai danh sach
//                store.DisplayAllProducts();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Loi: " + ex.Message);
//            }
//        }
//    }

//}

