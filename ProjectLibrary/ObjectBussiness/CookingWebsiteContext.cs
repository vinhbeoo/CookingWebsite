using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectLibrary.ObjectBussiness;

public partial class CookingWebsiteContext : DbContext
{
    public CookingWebsiteContext()
    {
    }

    public CookingWebsiteContext(DbContextOptions<CookingWebsiteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Contest> Contests { get; set; }

    public virtual DbSet<IngredientsDetail> IngredientsDetails { get; set; }

    public virtual DbSet<IngredientsGroup> IngredientsGroups { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipesStep> RecipesSteps { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserActivity> UserActivities { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    public virtual DbSet<UserRegHistory> UserRegHistories { get; set; }

    public virtual DbSet<WinnerInfo> WinnerInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=VINHBEOO\\SQLEXPRESS; database=CookingWebsite;uid=;pwd=;TrustServerCertificate=true;Trusted_Connection=SSPI;Encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("Comment");

            entity.Property(e => e.CommentText).HasMaxLength(500);

            entity.HasOne(d => d.Recipe).WithMany(p => p.Comments)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK_Comment_Recipes");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Comment_Users");
        });

        modelBuilder.Entity<Contest>(entity =>
        {
            entity.HasKey(e => e.ContestId).HasName("PK__Contests__87DE0B1A63E36EED");

            entity.HasIndex(e => new { e.StartTime, e.EndTime }, "UC_ContestDates").IsUnique();

            entity.Property(e => e.ContestName).HasMaxLength(100);
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.HasOne(d => d.OwnerUser).WithMany(p => p.Contests)
                .HasForeignKey(d => d.OwnerUserId)
                .HasConstraintName("FK__Contests__OwnerU__6383C8BA");
        });

        modelBuilder.Entity<IngredientsDetail>(entity =>
        {
            entity.HasKey(e => e.Stt);

            entity.ToTable("Ingredients_Detail");

            entity.Property(e => e.Stt)
                .ValueGeneratedNever()
                .HasColumnName("stt");
            entity.Property(e => e.IngredientId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Ingredient).WithMany(p => p.IngredientsDetails)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ingredients_Detail_Ingredients_Group");
        });

        modelBuilder.Entity<IngredientsGroup>(entity =>
        {
            entity.HasKey(e => e.IngredientId).HasName("PK__Ingredie__BEAEB25A7C2114F2");

            entity.ToTable("Ingredients_Group");

            entity.Property(e => e.NameIngredients).HasMaxLength(255);

            entity.HasOne(d => d.Recipe).WithMany(p => p.IngredientsGroups)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__Ingredien__Recip__73BA3083");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("Notification");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Notification_Users");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__Ratings__FCCDF87CA929BC9E");

            entity.HasIndex(e => new { e.UserId, e.RecipeId }, "UC_User_Contest").IsUnique();

            entity.HasOne(d => d.Recipe).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK_Ratings_Recipes");

            entity.HasOne(d => d.User).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Ratings__UserId__6754599E");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__Recipes__45D89E1C9D62B44D");

            entity.Property(e => e.Calories).HasMaxLength(50);
            entity.Property(e => e.Carbs).HasMaxLength(50);
            entity.Property(e => e.CookTime).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Fat).HasMaxLength(50);
            entity.Property(e => e.ImageTitle).HasMaxLength(500);
            entity.Property(e => e.PrepTime).HasMaxLength(50);
            entity.Property(e => e.Protein).HasMaxLength(50);
            entity.Property(e => e.RecipeTitle).HasMaxLength(500);
            entity.Property(e => e.Servings).HasMaxLength(50);
            entity.Property(e => e.TotalTime).HasMaxLength(50);
            entity.Property(e => e.VideoUrl)
                .HasMaxLength(255)
                .HasColumnName("VideoURL");

            entity.HasOne(d => d.Contest).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.ContestId)
                .HasConstraintName("FK_Recipes_Contests");

            entity.HasOne(d => d.CreatorNavigation).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.Creator)
                .HasConstraintName("FK__Recipes__CreateU__6EF57B66");

            entity.HasOne(d => d.Tag).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Recipes__IdTags__6FE99F9F");

            entity.HasOne(d => d.Type).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_Recipes_Type");
        });

        modelBuilder.Entity<RecipesStep>(entity =>
        {
            entity.HasKey(e => e.Step);

            entity.ToTable("Recipes_Step");

            entity.Property(e => e.Step).ValueGeneratedNever();
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("ImageURL");
            entity.Property(e => e.VideoUrl)
                .HasMaxLength(255)
                .HasColumnName("VideoURL");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipesSteps)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK_Recipes_Step_Recipes");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A38D0BE6E");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B61604B25B500").IsUnique();

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.IdTags).HasName("PK__Tags__9FCD3F7E1BFA9C7C");

            entity.HasIndex(e => e.NameTags, "UQ__Tags__2EAE8F47CB170E55").IsUnique();

            entity.Property(e => e.NameTags).HasMaxLength(50);
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.ToTable("Type");

            entity.Property(e => e.TypeName).HasMaxLength(500);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C95FAAD11");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534638514AD").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__Users__C9F284564CC9CD89").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleId__4E88ABD4");
        });

        modelBuilder.Entity<UserActivity>(entity =>
        {
            entity.HasKey(e => e.ActivityId).HasName("PK__UserActi__45F4A791EBC83F5E");

            entity.ToTable("UserActivity");

            entity.HasOne(d => d.User).WithMany(p => p.UserActivities)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserActiv__UserI__59FA5E80");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserDeta__1788CC4C5A774286");

            entity.ToTable("UserDetail");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Avatar).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Phone).HasMaxLength(20);

            entity.HasOne(d => d.User).WithOne(p => p.UserDetail)
                .HasForeignKey<UserDetail>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserDetai__UserI__5165187F");
        });

        modelBuilder.Entity<UserRegHistory>(entity =>
        {
            entity.HasKey(e => e.RegistrationId).HasName("PK__UserRegH__6EF58810FC7204B9");

            entity.ToTable("UserRegHistory");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.SubscriptionType).HasMaxLength(20);

            entity.HasOne(d => d.User).WithMany(p => p.UserRegHistories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserRegHi__UserI__571DF1D5");
        });

        modelBuilder.Entity<WinnerInfo>(entity =>
        {
            entity.HasKey(e => e.WinnerId).HasName("PK__WinnerIn__8A3D1DA89A7B71C7");

            entity.ToTable("WinnerInfo");

            entity.Property(e => e.WinningDate).HasColumnType("datetime");

            entity.HasOne(d => d.Contest).WithMany(p => p.WinnerInfos)
                .HasForeignKey(d => d.ContestId)
                .HasConstraintName("FK__WinnerInf__Conte__6B24EA82");

            entity.HasOne(d => d.WinnerUser).WithMany(p => p.WinnerInfos)
                .HasForeignKey(d => d.WinnerUserId)
                .HasConstraintName("FK__WinnerInf__Winne__6C190EBB");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
