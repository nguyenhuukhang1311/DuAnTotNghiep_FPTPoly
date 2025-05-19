using MenShop_Assignment.Datas;
using MenShop_Assignment.Mapper.MapperCategory;
using MenShop_Assignment.Models.CategoryModels;
using MenShop_Assignment.Repositories.Category;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenShop_Assignment.Tests.Repositories
{
    [TestFixture]
    public class CategoryProductRepositoryTests
    {
        private ApplicationDbContext _context;
        private CategoryProductRepository _repository;
        private CategoryProductMapper _mapper;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Không cần thiết lập toàn cục cho InMemory
        }

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDb_{System.Guid.NewGuid()}")
                .Options;

            _context = new ApplicationDbContext(options);

            _context.CategoryProducts.AddRange(new List<CategoryProduct>
            {
                new CategoryProduct { CategoryId = 1, Name = "Áo sơ mi" },
                new CategoryProduct { CategoryId = 2, Name = "Quần jean" }
            });

            _context.SaveChanges();
            _repository = new CategoryProductRepository(_context, _mapper);
        }

        [Test]
        public async Task GetAllCategoriesAsync_ReturnsAllCategories()
        {
            var result = await _repository.GetAllCategoriesAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetCategoryByIdAsync_ValidId_ReturnsCategory()
        {
            var result = await _repository.GetCategoryByIdAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Áo sơ mi"));
        }

        [Test]
        public async Task GetCategoryByIdAsync_InvalidId_ReturnsNull()
        {
            var result = await _repository.GetCategoryByIdAsync(999);
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task CreateCategoryAsync_AddsCategoryAndStorage()
        {
            var model = new CategoryModelView { Name = "Giày thể thao" };

            var result = await _repository.CreateCategoryAsync(model);

            var categoryInDb = await _context.CategoryProducts.FirstOrDefaultAsync(x => x.Name == "Giày thể thao");
            var storage = await _context.Storages.FirstOrDefaultAsync(x => x.CategoryId == categoryInDb.CategoryId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Giày thể thao"));
            Assert.That(categoryInDb, Is.Not.Null);
            Assert.That(storage, Is.Not.Null);
        }

        [Test]
        public async Task UpdateCategoryAsync_ValidId_UpdatesCategory()
        {
            var updated = new CategoryModelView { CategoryId = 1, Name = "Áo sơ mi tay ngắn" };

            var result = await _repository.UpdateCategoryAsync(updated);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Áo sơ mi tay ngắn"));
        }

        [Test]
        public async Task UpdateCategoryAsync_InvalidId_ReturnsNull()
        {
            var model = new CategoryModelView { CategoryId = 999, Name = "Không tồn tại" };

            var result = await _repository.UpdateCategoryAsync(model);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task RemoveCategoryAsync_CategoryWithNoDependencies_ReturnsTrue()
        {
            var newCat = new CategoryProduct { Name = "Phụ kiện" };
            _context.CategoryProducts.Add(newCat);
            await _context.SaveChangesAsync();

            var storage = new Storage { CategoryId = newCat.CategoryId };
            _context.Storages.Add(storage);
            await _context.SaveChangesAsync();

            var result = await _repository.RemoveCategoryAsync(newCat.CategoryId);

            Assert.That(result, Is.True);
            Assert.That(await _context.CategoryProducts.FindAsync(newCat.CategoryId), Is.Null);
            Assert.That(await _context.Storages.FirstOrDefaultAsync(s => s.CategoryId == newCat.CategoryId), Is.Null);
        }

        [Test]
        public async Task RemoveCategoryAsync_CategoryWithProducts_ReturnsFalse()
        {
            _context.Products.Add(new Product { ProductName = "Sản phẩm", CategoryId = 1 });
            await _context.SaveChangesAsync();

            var result = await _repository.RemoveCategoryAsync(1);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task RemoveCategoryAsync_StorageWithDetails_ReturnsFalse()
        {
            var newCat = new CategoryProduct { Name = "Kính mát" };
            _context.CategoryProducts.Add(newCat);
            await _context.SaveChangesAsync();

            var storage = new Storage { CategoryId = newCat.CategoryId };
            _context.Storages.Add(storage);
            await _context.SaveChangesAsync();

            _context.StorageDetails.Add(new StorageDetail
            {
                StorageId = storage.StorageId,
                Quantity = 10
            });
            await _context.SaveChangesAsync();

            var result = await _repository.RemoveCategoryAsync(newCat.CategoryId);

            Assert.That(result, Is.False);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Không cần thiết lập toàn cục cho InMemory
        }
    }
}
