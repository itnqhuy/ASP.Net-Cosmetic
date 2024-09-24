using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EComerceMVC.Data;

public partial class CosmeticContext : DbContext
{
    public CosmeticContext()
    {
    }

    public CosmeticContext(DbContextOptions<CosmeticContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Favourite> Favourites { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Rate> Rates { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=NGUYENQUOCHUY;Database=cosmetic;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__brand__3213E83F57B6EF1B");

            entity.ToTable("brand");

            entity.Property(e => e.Id)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Datebegin)
                .HasColumnType("smalldatetime")
                .HasColumnName("datebegin");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Thumbnail)
                .HasMaxLength(255)
                .HasColumnName("thumbnail");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.IdCarditem).HasName("PK__cart_ite__1A41F5A12B0A64A5");

            entity.ToTable("cart_item");

            entity.Property(e => e.IdCarditem)
                .ValueGeneratedNever()
                .HasColumnName("id_carditem");
            entity.Property(e => e.IdCustomer)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_customer");
            entity.Property(e => e.IdProduct)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_product");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cart_item__id_cu__5441852A");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cart_item__id_pr__534D60F1");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PK__category__E548B67313927A1D");

            entity.ToTable("category");

            entity.Property(e => e.IdCategory)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_category");
            entity.Property(e => e.Datebegin)
                .HasColumnType("smalldatetime")
                .HasColumnName("datebegin");
            entity.Property(e => e.Hide).HasColumnName("hide");
            entity.Property(e => e.Meta)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("meta");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Order).HasColumnName("order");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.IdComment).HasName("PK__comment__7E14AC8531183118");

            entity.ToTable("comment");

            entity.Property(e => e.IdComment)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_comment");
            entity.Property(e => e.Content)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("content");
            entity.Property(e => e.Datebegin).HasColumnName("datebegin");
            entity.Property(e => e.Hide).HasColumnName("hide");
            entity.Property(e => e.IdCustomer)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_customer");
            entity.Property(e => e.IdPost)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_post");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.ParentComment)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("parent_comment");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__comment__id_cust__619B8048");

            entity.HasOne(d => d.IdPostNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdPost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__comment__id_post__60A75C0F");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.IdCustomer).HasName("PK__customer__8CC9BA46F4716DC1");

            entity.ToTable("customer");

            entity.Property(e => e.IdCustomer)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_customer");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.CreateAt).HasColumnName("create_at");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FullName).HasColumnName("full_name");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Phone).HasColumnName("phone");
        });

        modelBuilder.Entity<Favourite>(entity =>
        {
            entity.HasKey(e => e.IdFavourite).HasName("PK__favourit__F978963BF668A2C1");

            entity.ToTable("favourite");

            entity.Property(e => e.IdFavourite)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_favourite");
            entity.Property(e => e.DaySelect)
                .HasColumnType("smalldatetime")
                .HasColumnName("day_select");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.IdCustomer)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_customer");
            entity.Property(e => e.IdProduct)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_product");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.Favourites)
                .HasForeignKey(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__favourite__id_cu__5070F446");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Favourites)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__favourite__id_pr__4F7CD00D");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.IdImage).HasName("PK__image__C28C621CA35B3E5B");

            entity.ToTable("image");

            entity.Property(e => e.IdImage)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_image");
            entity.Property(e => e.Datebegin)
                .HasColumnType("smalldatetime")
                .HasColumnName("datebegin");
            entity.Property(e => e.Meta)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("meta");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.IdIngredient).HasName("PK__ingredie__9D79738D0EBC909A");

            entity.ToTable("ingredient");

            entity.Property(e => e.IdIngredient)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_ingredient");
            entity.Property(e => e.Datebegin)
                .HasColumnType("smalldatetime")
                .HasColumnName("datebegin");
            entity.Property(e => e.DetailText)
                .HasColumnType("text")
                .HasColumnName("detail_text");
            entity.Property(e => e.Guide)
                .HasColumnType("text")
                .HasColumnName("guide");
            entity.Property(e => e.Hide).HasColumnName("hide");
            entity.Property(e => e.Meta)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("meta");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.RiskLevel)
                .HasMaxLength(10)
                .HasColumnName("risk_level");
            entity.Property(e => e.SkinCompatibility)
                .HasMaxLength(255)
                .HasColumnName("skin_compatibility");
            entity.Property(e => e.Uses)
                .HasMaxLength(255)
                .HasColumnName("uses");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("PK__menu__68A1D9DBD1E5F2E9");

            entity.ToTable("menu");

            entity.Property(e => e.IdMenu)
                .ValueGeneratedNever()
                .HasColumnName("id_menu");
            entity.Property(e => e.Datebegin).HasColumnName("datebegin");
            entity.Property(e => e.Hide).HasColumnName("hide");
            entity.Property(e => e.Link)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("link");
            entity.Property(e => e.Meta)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("meta");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Order).HasColumnName("order");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PK__order__DD5B8F3F3331C658");

            entity.ToTable("order");

            entity.Property(e => e.IdOrder)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("id_order");
            entity.Property(e => e.Datebegin).HasColumnName("datebegin");
            entity.Property(e => e.IdStaff)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_staff");
            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");
            entity.Property(e => e.Note)
                .HasMaxLength(50)
                .HasColumnName("note");
            entity.Property(e => e.Paymethod)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("paymethod");
            entity.Property(e => e.ReceiverAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("receiver_address");
            entity.Property(e => e.ReceiverName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("receiver_name");
            entity.Property(e => e.ReceiverPhone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("receiver_phone");
            entity.Property(e => e.Shipcost).HasColumnName("shipcost");

            entity.HasOne(d => d.IdStaffNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdStaff)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__order__id_staff__571DF1D5");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.IdDetail).HasName("PK__order_de__EA833808D2B5F951");

            entity.ToTable("order_detail");

            entity.Property(e => e.IdDetail)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_detail");
            entity.Property(e => e.IdOrder)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("id_order");
            entity.Property(e => e.IdProduct)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_product");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__order_det__id_or__5CD6CB2B");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__order_det__id_pr__5DCAEF64");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.IdPost).HasName("PK__post__3840C79DCE29D9AA");

            entity.ToTable("post");

            entity.Property(e => e.IdPost)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_post");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("content");
            entity.Property(e => e.Datebegin).HasColumnName("datebegin");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Hide).HasColumnName("hide");
            entity.Property(e => e.IdProduct)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_product");
            entity.Property(e => e.Meta).HasColumnName("meta");
            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.Thumbail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("thumbail");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__post__id_product__59FA5E80");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PK__product__BA39E84FA7FBE103");

            entity.ToTable("product");

            entity.Property(e => e.IdProduct)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_product");
            entity.Property(e => e.Datebegin)
                .HasColumnType("smalldatetime")
                .HasColumnName("datebegin");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Exp)
                .HasColumnType("smalldatetime")
                .HasColumnName("exp");
            entity.Property(e => e.Hide).HasColumnName("hide");
            entity.Property(e => e.IdBrand)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_brand");
            entity.Property(e => e.IdCatrgory)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_catrgory");
            entity.Property(e => e.IdImage)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_image");
            entity.Property(e => e.IdIngredient)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_ingredient");
            entity.Property(e => e.Meta)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("meta");
            entity.Property(e => e.Name)
                .HasMaxLength(300)
                .HasColumnName("name");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TotalSold).HasColumnName("total_sold");

            entity.HasOne(d => d.IdBrandNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdBrand)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__product__id_bran__3F466844");

            entity.HasOne(d => d.IdCatrgoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdCatrgory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__product__id_catr__403A8C7D");

            entity.HasOne(d => d.IdImageNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdImage)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__product__id_imag__4222D4EF");

            entity.HasOne(d => d.IdIngredientNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdIngredient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__product__id_ingr__412EB0B6");
        });

        modelBuilder.Entity<Rate>(entity =>
        {
            entity.HasKey(e => e.IdRate).HasName("PK__rate__084F73BA973291BE");

            entity.ToTable("rate");

            entity.HasIndex(e => new { e.IdCustomer, e.IdProduct }, "UQ__rate__676A24C302C0BADD").IsUnique();

            entity.Property(e => e.IdRate)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_rate");
            entity.Property(e => e.Datebegin)
                .HasColumnType("smalldatetime")
                .HasColumnName("datebegin");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Hide).HasColumnName("hide");
            entity.Property(e => e.IdCustomer)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_customer");
            entity.Property(e => e.IdProduct)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_product");
            entity.Property(e => e.Image)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Meta)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("meta");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.Star).HasColumnName("star");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.Rates)
                .HasForeignKey(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__rate__id_custome__4BAC3F29");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Rates)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__rate__id_product__4CA06362");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.IdStaff).HasName("PK__staff__12FEDA3CDC39939A");

            entity.ToTable("staff");

            entity.Property(e => e.IdStaff)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("id_staff");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.CreateAt)
                .HasColumnType("smalldatetime")
                .HasColumnName("create_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.Password)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
