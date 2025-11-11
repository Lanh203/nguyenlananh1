
using System;
using System.Collections.Generic;
using System.Linq;

//bai 1 
public class Product
{


    private string ProductId;//Dùng private + property kiểm tra dữ liệu
    private string ProductName { get; set; }
    private decimal ImportPrice;
    private decimal SalePrice;
    private int StockQuantity;

    public string productId
    {
        get => ProductId;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Ma san pham khong duoc rong..");
            ProductId = value;
        }
    }
    public string productName
    {
        get => ProductName;
        set => ProductName = value;
    }
    public decimal importPrice
    {
        get => ImportPrice;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(importPrice), "Gia nhap vao phai lon hon 0..");
            ImportPrice = value;
        }
    }
    public decimal salePrice
    {
        get => SalePrice;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(salePrice), "Gia ban ra phai lon hon 0..");
            SalePrice = value;
        }
    }
    public int stockQuantity
    {
        get => StockQuantity;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Hang ton kho phai lon hon hoac bang 0..");
            StockQuantity = value;
        }
    }

    public Product()
    {
        productId = "DF01";
        productName = "San pham";
        importPrice = 1;
        salePrice = 1;
        stockQuantity = 0;
    }

    public Product(string proId, string proName, decimal impoPrice, decimal salePri, int stockQuan)
    {
        productId = proId;
        productName = proName;
        importPrice = impoPrice;
        salePrice = salePri;
        stockQuantity = stockQuan;
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Ma SP: {productId} | Ten SP: {productName} | Gia nhap: {importPrice} | Gia ban: {salePrice} | Ton kho: {stockQuantity}");
    }

    public void CalculateProfit()
    {
        decimal profit = salePrice - importPrice;
        Console.WriteLine("Loi nhuan la: " + profit);
    }

    public virtual decimal CalculateDiscount()
    {
        return 0;
    }

    //bai2
    public class MobilePhone : Product
    {
        public string Manufacturer { get; set; }
        public int BatteryCapacity { get; set; }
        public int WarrantyPeriod { get; set; }

        public MobilePhone() { }

        public MobilePhone(string id, string name, decimal imPrice, decimal saPrice, int stock, string manufacturer, int batteryCapacity, int warrantyPeriod)
            : base(id, name, imPrice, saPrice, stock)
        {
            Manufacturer = manufacturer;
            BatteryCapacity = batteryCapacity;
            WarrantyPeriod = warrantyPeriod;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Hang san xuat: {Manufacturer}");
            Console.WriteLine($"Dung luong pin: {BatteryCapacity} mAh");
            Console.WriteLine($"Bao hanh: {WarrantyPeriod} thang");
        }

        public override decimal CalculateDiscount()
        {
            return stockQuantity > 50 ? 0.1m : 0;
        }
    }

    public class Accessory : Product
    {
        public string AccessoryType { get; set; }
        public string Material { get; set; }

        public Accessory() { }

        public Accessory(string id, string name, decimal imPrice, decimal saPrice, int stock, string accessoryType, string material)
            : base(id, name, imPrice, saPrice, stock)
        {
            AccessoryType = accessoryType;
            Material = material;
        }

        public override void DisplayInfo()//da hinh
        {
            base.DisplayInfo();
            Console.WriteLine($"\nLoai phu kien: {AccessoryType}");
            Console.WriteLine($"Chat lieu: {Material}");
        }

        public override decimal CalculateDiscount()//da hinh 
        {
            return stockQuantity > 100 ? 0.05m : 0;
        }
    }
}

//cau3
class StoreManager
{
    private List<Product> products;

    public StoreManager()
    {
        products = new List<Product>();
    }

    // Them san pham
    public void AddProduct(Product product)
    {
        if (products.Any(p => p.productId == product.productId))
            throw new Exception("San pham co ma nay da ton tai!");
        products.Add(product);
        Console.WriteLine($"Da them san pham: {product.productName}");
    }

    // Xoa san pham theo ma
    public void RemoveProduct(string productId)
    {
        var product = products.FirstOrDefault(p => p.productId == productId);
        if (product == null)
        {
            Console.WriteLine("Khong tim thay san pham de xoa.");
            return;
        }
        products.Remove(product);
        Console.WriteLine($"Da xoa san pham co ma {productId}");
    }

    // Tim san pham theo ma
    public Product SearchByProductId(string productId)
    {
        var product = products.FirstOrDefault(p => p.productId == productId);
        if (product == null)
            Console.WriteLine("Khong tim thay san pham co ma nay.");
        return product;
    }
    

    // Hien thi toan bo san pham
    public void DisplayAllProducts()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("Danh sach san pham trong!");
            return;
        }

        Console.WriteLine("\nDANH SACH SAN PHAM:");
        foreach (var p in products)
        {
            p.DisplayInfo();
            p.CalculateProfit();
            Console.WriteLine($"Giam gia: {p.CalculateDiscount() * 100}%");
            Console.WriteLine("-----------------------------------");
        }
    }

    // Dem so luong theo loai
    public void CountProductsByType()
    {
        int countPhone = products.OfType<Product.MobilePhone>().Count();
        int countAccessory = products.OfType<Product.Accessory>().Count();

        Console.WriteLine($"\nSo dien thoai: {countPhone}");
        Console.WriteLine($"So phu kien: {countAccessory}");
    }
    //bai4
    //tim sp theo hang 
    public List<Product.MobilePhone> SearchPhoneByManufacturer(string manufacture)
    {
        var product = products.OfType<Product.MobilePhone>().Where(p => p.Manufacturer == manufacture).ToList();
        if (product == null)
            Console.WriteLine("Khong tim thay san pham nay.");
        return product;
    }
    //tim sp theo gia 
    public List<Product> SearchByPriceRange(decimal minPrice, decimal maxPrice)
    {
        var result = products.Where(p => p.salePrice >= minPrice && p.salePrice <= maxPrice).ToList();
        if (result.Count == 0)
            Console.WriteLine($"ko co sp trong khoang {minPrice} - {maxPrice}");
        else
            result.ForEach(p => { p.DisplayInfo(); Console.WriteLine("======"); });
        return result;
    }
    // tinh gtri hang ton kho 
   public decimal CalculateTotalInventoryValue()
    {
        decimal totalValue = products.Sum(p => p.importPrice * p.stockQuantity);
        Console.WriteLine($"Tổng giá trị hàng tồn kho: {totalValue}");
        return totalValue;
    }

    //tim top sp cao nhat 


    //sd thuat toan sap xep bubble sort

    //bai5
    private List<Invoice> invoices = new List<Invoice>();
    public void CreateInvoice (Invoice invoice)
    {
        foreach (var item in invoice.ProductList)
        {
            var product = item.Key;
            int quantity = item.Value;

            if (product.stockQuantity < quantity)
                throw new Exception($"Không đủ hàng trong kho cho sản phẩm: {product.productName}");
            product.stockQuantity -= quantity;
        }

    }








}

//chuong trinh chinh
class Program
{
    static void Main(string[] args)
    {
        // hien thi tieng Viet dung trong Console
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        try
        {
            StoreManager store = new StoreManager();

            // Tao san pham
            var phone1 = new Product.MobilePhone("IP01", "iPhone 15", 20000, 25000, 30, "Apple", 4000, 24);
            var phone2 = new Product.MobilePhone("SS01", "Samsung S24", 18000, 22000, 20, "Samsung", 5000, 18);
            var accessory1 = new Product.Accessory("AC01", "Tai nghe Bluetooth", 500, 800, 150, "Headphone", "Nhua");

            // Them vao danh sach
            store.AddProduct(phone1);
            store.AddProduct(phone2);
            store.AddProduct(accessory1);

            // Hien thi danh sach
            store.DisplayAllProducts();
            store.CountProductsByType();

            // Tim san pham
            Console.WriteLine("\nTim san pham co ma 'SS01':");
            var found = store.SearchByProductId("SS01");
            if (found != null)
                found.DisplayInfo();

            // Xoa san pham
            Console.WriteLine("\nXoa san pham IP01...");
            store.RemoveProduct("IP01");

            // Hien thi lai danh sach
            store.DisplayAllProducts();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Loi: " + ex.Message);
        }
    }
}
