using Microsoft.EntityFrameworkCore;
using TetPee.Repository.Entity;

namespace TetPee.Repository;

public class AppDbContext : DbContext //là một ..., đại diện cho db
{
    public static Guid UserId1 = Guid.NewGuid();
    public static Guid UserId2 = Guid.NewGuid();

    public static Guid SellerId1 = Guid.NewGuid();

    public static Guid CateGoryParentId1 = Guid.NewGuid();
    public static Guid CateGoryParentId2 = Guid.NewGuid();
    //tại sao có cái này

    public static Guid ProductId1 = Guid.NewGuid();
    public static Guid ProductId2 = Guid.NewGuid();
    public static Guid ProductId3 = Guid.NewGuid();
    public static Guid ProductId4 = Guid.NewGuid();

    public static Guid OrderId1 = Guid.NewGuid();
    public static Guid OrderId2 = Guid.NewGuid();
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductStorage> ProductStorages { get; set; }
    public DbSet<Storage> Storages { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    var listUsersId = new List<Guid>();
    var ListSellersId = new List<Guid>();
    var ListProductsId = new List<Guid>();
    var ListStoragesId = new List<Guid>();
    var ListCategoriesId = new List<Guid>();
    /*var ListProducts = new List<Product>();
    var ListOrders = new List<Order>();
    var ListOrderDetail = new List<OrderDetail>();*/
        // ==================== User Configuration ====================
        modelBuilder.Entity<User>(builder =>
        {
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            // LastName - required, max 100 characters
            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

            // ImageUrl - nullable, max 500 characters (URL)
            builder.Property(u => u.ImageUrl)
                .HasMaxLength(500);

            // PhoneNumber - nullable, max 20 characters
            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(20);

            // HashedPassword - required, max 500 characters
            builder.Property(u => u.HashedPassword)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("User");

            // Relationship: User has one Seller (one-to-one)
            builder.HasOne(u => u.Seller)
                .WithOne(s => s.User)
                .HasForeignKey<Seller>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // DeleteBehavior.Cascade: Khi một User bị xóa, thì Seller liên quan cũng sẽ bị xóa theo.
            // DeleteBehavior.Restrict: Ngăn chặn việc xóa một User nếu có Seller liên quan tồn tại.
            //(Tham chiếu tới PK tồn tại)
            // 1 Project còn Task thì không xoá được
            // DeleteBehavior.NoAction: Không thực hiện hành động gì đặc biệt khi User bị xóa. ( Gàn giống Restrict, xử lí ở DB)
            // DeleteBehavior.SetNull: Khi một User bị xóa, thì trường UserId trong bảng Seller sẽ được đặt thành NULL.
            //(Áp dụng khi trường FK cho phép NULL)

            List<User> users = new List<User>()
            {
                new()
                {
                    Id = UserId1,
                    Email = "tan182205@gmail.com",
                    FirstName = "Tan",
                    LastName = "Tran",
                    HashedPassword = "hashed_password_1",
                },
                new()
                {
                    Id = UserId2,
                    Email = "tan182206@gmail.com",
                    FirstName = "Tan",
                    LastName = "Tran",
                    HashedPassword = "hashed_password_1",
                }
            };
            for (int i = 0; i <= 50; i++)
            {
                var newUsers = new User()
                {
                    Id = Guid.NewGuid(),
                    Email = i + "tan182205@gmail.com",
                    FirstName = "Tan" + i,
                    LastName = "Tran" + i,
                    HashedPassword = "hashed_password_1"

                };
                listUsersId.Add(newUsers.Id);
                users.Add(newUsers);
            }

            builder.HasData(users);
        });

        modelBuilder.Entity<Seller>(builder =>
        {
            builder.Property(s => s.TaxCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.CompanyName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.CompanyAddress)
                .IsRequired()
                .HasMaxLength(500);
            
            var seller = new List<Seller>()
            {
                new()
                {
                    Id = SellerId1,
                    TaxCode = "TAXCODE123",
                    CompanyName = "ABC Company",
                    CompanyAddress = "123 Main St, Cityville",
                    UserId = UserId1,
                }
            };
            for (int i = 0; i < listUsersId.Count - 1; i++)
            {
                var newSeller = new Seller()
                {
                    Id = Guid.NewGuid(),
                    TaxCode = "TAXCODE" + i,
                    CompanyName = "ABC " + i + " Company",
                    CompanyAddress = i + " Main St, Cityville",
                    UserId = listUsersId[i],
                };
                ListSellersId.Add(newSeller.Id);
                seller.Add(newSeller);
            }

            builder.HasData(seller);
        });


        modelBuilder.Entity<Category>(builder =>
        {
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasOne(p => p.Parent)
                .WithMany(p =>  p.Children)
                .HasForeignKey(p => p.ParentId)
                .OnDelete(DeleteBehavior.Cascade);
            
            var categories = new List<Category>()
            {
                new()
                {
                    Id = CateGoryParentId1,
                    Name = "Áo",
                },
                new()
                {
                    Id = CateGoryParentId2,
                    Name = "Quần",
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Áo thể thao",
                    ParentId = CateGoryParentId1
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Áo ba lỗ",
                    ParentId = CateGoryParentId1
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Quần Jean",
                    ParentId = CateGoryParentId2
                },
            };
            for (int i = 0; i <= 50; i++)
            {
                if (i % 2 == 0)
                {
                    var category = new Category()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Áo ba lỗ mã" + i,
                        ParentId = CateGoryParentId1
                    };
                    ListCategoriesId.Add(category.Id);
                    categories.Add(category);
                }
                else
                {
                    var category = new Category()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Quần Jean mã " + i,
                        ParentId = CateGoryParentId2
                    };
                    ListCategoriesId.Add(category.Id);
                    categories.Add(category);
                }

            }
            builder.HasData(categories);
        });

        modelBuilder.Entity<Product>(builder =>
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(500);
            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(p => p.UrlImage)
                .IsRequired()
                .HasMaxLength(500);
            builder.HasOne(p => p.Seller)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.Cascade);

            var products = new List<Product>()
            {

                new Product()
                {
                    Id = ProductId1,
                    Name = "Áo Thun Nam",
                    Description =
                        "Áo thun nam chất liệu cotton cao cấp, thoáng mát, phù hợp cho mọi hoạt động hàng ngày.",
                    UrlImage = "https://example.com/images/ao_thun_nam.jpg",
                    Price = 199000m,
                    SellerId = SellerId1
                },
                new Product()
                {
                    Id = ProductId2,
                    Name = "Quần Jeans Nữ",
                    Description = "Quần jeans nữ dáng ôm, tôn dáng, chất liệu denim co giãn, phù hợp cho mọi dịp.",
                    UrlImage = "https://example.com/images/quan_jeans_nu.jpg",
                    Price = 399000m,
                    SellerId = SellerId1
                },
                new Product()
                {
                    Id = ProductId3,
                    Name = "Áo Sơ Mi Nam",
                    Description = "Áo sơ mi nam công sở, thiết kế hiện đại, chất liệu vải cao cấp, thoáng mát.",
                    UrlImage = "https://example.com/images/ao_so_mi_nam.jpg",
                    Price = 299000m,
                    SellerId = SellerId1
                },
                new Product()
                {
                    Id = ProductId4,
                    Name = "Chân Váy Nữ",
                    Description = "Chân váy nữ xòe, thiết kế trẻ trung, chất liệu vải mềm mại, phù hợp cho mọi dịp.",
                    UrlImage = "https://example.com/images/chan_vay_nu.jpg",
                    Price = 249000m,
                    SellerId = SellerId1
                }

            };

            for (int i = 0; i <= 50; i++)
            {
                var newProduct = new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Áo Thun Nam"+ i,
                    Description =
                        "Áo thun nam chất liệu cotton cao cấp, thoáng mát, phù hợp cho mọi hoạt động hàng ngày."+ i,
                    UrlImage = "https://example.com/images" + i + "/ao_thun_nam.jpg",
                    Price = i * 99000m,
                    SellerId = ListSellersId[0],
                };
                ListProductsId.Add(newProduct.Id);
                products.Add(newProduct);
            }

            builder.HasData(products);
        });


        modelBuilder.Entity<Order>(builder =>
        {
            builder.Property(o =>  o.Address)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(o => o.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            
            builder.Property(o => o.Status)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            var order = new List<Order>()
            {
                new Order()
                {
                    Id = OrderId2,
                    UserId = UserId1,
                    Address = "Bien Hoa, Dong Nai",
                    TotalAmount = 100000m,
                    Status = "Completed",
                },
                new Order()
                {
                Id = OrderId1,
                UserId = UserId1,
                Address = "Bien Hoa, Dong Nai",
                TotalAmount = 100000m,
                Status = "Completed",
                }
            };
                for (int i = 0; i < listUsersId.Count - 1 ; i++)
                {
                    var newOrders = new Order()
                    {
                        Id = Guid.NewGuid(),
                        UserId = listUsersId[i],
                        Address = "Bien Hoa, Dong Nai" + i,
                        TotalAmount = i * 230000m,
                        Status = "Completed",
                    };
                    order.Add(newOrders);
                }
                builder.HasData(order);
        });

        modelBuilder.Entity<OrderDetail>(builder =>
        {
            builder.Property(o => o.Quantity)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(o => o.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            
            builder.HasOne(o => o.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(o => o.OrderID)
                .OnDelete(DeleteBehavior.Cascade);     
            builder.HasOne(o => o.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(o => o.ProductID)
                .OnDelete(DeleteBehavior.Cascade);     
            var orderDetails = new List<OrderDetail>()
            {
                new OrderDetail()
                {
                    Id = Guid.NewGuid(),
                    OrderID = OrderId1,
                    ProductID = ProductId1,
                    Quantity = 1,
                    UnitPrice = 399000m,
                },
                new OrderDetail()
                {
                    Id = Guid.NewGuid(),
                    OrderID = OrderId2,
                    ProductID = ProductId1,
                    Quantity = 12,
                    UnitPrice = 399000m,
                },
                new OrderDetail()
                {
                    Id = Guid.NewGuid(),
                    OrderID = OrderId2,
                    ProductID = ProductId2,
                    Quantity = 3,
                    UnitPrice = 399000m,
                },
                new OrderDetail()
                {
                    Id = Guid.NewGuid(),
                    OrderID = OrderId1,
                    ProductID = ProductId2,
                    Quantity = 5,
                    UnitPrice = 399000m,
                },

            };
            for (int i = 0; i < ListProductsId.Count - 1; i++)
            {
                var newOrderDetail = new OrderDetail()
                {
                    Id = Guid.NewGuid(),
                    OrderID = OrderId1,
                    ProductID = ListProductsId[i],
                    Quantity = i,
                    UnitPrice = i * 99000m,
                };
                orderDetails.Add(newOrderDetail);
            }
            builder.HasData(orderDetails);

        });
        modelBuilder.Entity<Storage>(builder =>
        {
            builder.Property(s => s.Id)
                .IsRequired()
                .HasMaxLength(100);
            List<Storage> storage = new List<Storage>();
            for (int i = 0; i < 50; i++)
            {
                var typeSto = "Import";
                if (i % 5 == 0)
                {
                    typeSto = "Export";
                }
                var newStorage = new Storage()
                {
                    Id = Guid.NewGuid(),
                    Price = i * 98000m,
                    Type = typeSto
                };
                ListStoragesId.Add(newStorage.Id);
                storage.Add(newStorage);
            }
            builder.HasData(storage);
        });
        modelBuilder.Entity<ProductStorage>(builder =>
            {
                builder.Property(p => p.Id)
                    .IsRequired()
                    .HasMaxLength(100);
                List<ProductStorage> productStorages = new List<ProductStorage>();
                for (int i = 0; i < 50; i++)
                {
                    var newProStorage = new ProductStorage()
                    {
                        Id = Guid.NewGuid(),
                        ProductId = ListProductsId[i],
                        StorageId = ListStoragesId[i],
                    };
                    productStorages.Add(newProStorage);
                }
                builder.HasData(productStorages);
            });
        modelBuilder.Entity<ProductCategory>(builder =>
        {
            builder.Property(p => p.Id)
                .IsRequired()
                .HasMaxLength(100);
            List<ProductCategory> productCategory = new List<ProductCategory>();
            for (int i = 0; i < 50; i++)
            {
                var newProCategory = new ProductCategory()
                {
                    Id = Guid.NewGuid(),
                    CategoryId = ListCategoriesId[i],
                    ProductID = ListProductsId[i],
                };
                productCategory.Add(newProCategory);
            }
            builder.HasData(productCategory);
        });
        modelBuilder.Entity<Inventory>(builder =>
        {
            List<Inventory> inventories = new List<Inventory>();
            for (int i = 0; i < ListProductsId.Count - 1; i++)
            {
                var newInvent = new Inventory()
                {
                    Id = Guid.NewGuid(),
                    ProductId = ListProductsId[i],
                    TotalInStock = i * 54,
                    TotalSell = 1 + i * 23,
                };
                inventories.Add(newInvent);
            }
            builder.HasData(inventories);
        });
        modelBuilder.Entity<Cart>(builder =>
        {
            builder.Property(p => p.Id)
                .IsRequired()
                .HasMaxLength(100);
            List<Cart> carts = new List<Cart>();
            for (int i = 0; i < listUsersId.Count - 1; i++)
            {
                var newCart = new Cart()
                {
                    Id = Guid.NewGuid(),
                    UserId = listUsersId[i],
                };
                carts.Add(newCart);
            }
            builder.HasData(carts);
        });
    }

}
//xong category với có 1000 user trong db"

