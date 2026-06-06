using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Ingredients.DTOs;
using CoffeeShop.Application.Features.Misc.Images.DTOs;
using CoffeeShop.Application.Interfaces.Repositories.Catalog;
using CoffeeShop.Domain.Common.Constants;
using CoffeeShop.Domain.Entities.Catalog;
using CoffeeShop.Domain.Entities.Inventory;
using CoffeeShop.Domain.Entities.Misc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CoffeeShop.Infrastructure.Persistence.Repositories.Catalog
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly CoffeeShopDbContext _context;
        public IngredientRepository(CoffeeShopDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<IngredientDTO>>> GetIngredientsAsync(CancellationToken cancellationToken = default)
        {
            var query = _context.Ingredients
                .Include(i => i.Brand)
                .Include(i => i.Category)
                .Include(i => i.IngredientSku)
                .Join(_context.Images,
                    ir => ir.Id,
                    im => im.ReferenceId,
                    (ir, im) => new
                    {
                        Ingredient = ir,
                        Image = im
                    })
                .Where(x => x.Image.Type == ReferenceType.Ingredient);
            var ingredients = await query
                .Select(i => new IngredientDTO
                {
                    PublicId = i.Ingredient.PublicId,
                    Name = i.Ingredient.Name,
                    Description = i.Ingredient.Description,
                    BrandName = i.Ingredient.Brand.Name,
                    CategoryName = i.Ingredient.Category.Name,
                    Images = new List<ImageDTO>
                    {
                        new ImageDTO
                        {
                            ImageUrl = i.Image.ImageUrl,
                            IsPrimary = i.Image.IsPrimary,
                            CreatedAt = i.Image.CreatedAt
                        }
                    },
                    UnitPrice = i.Ingredient.IngredientSku.UnitPrice,
                    Unit = i.Ingredient.IngredientSku.Unit,
                    Stock = i.Ingredient.IngredientSku.Stock,
                    Status = i.Ingredient.IngredientSku.Status,
                    CreatedAt = i.Ingredient.CreatedAt,
                    UpdatedAt = i.Ingredient.UpdatedAt,
                    IsDeleted = i.Ingredient.IsDeleted
                })
                .ToListAsync(cancellationToken);
            return Result<List<IngredientDTO>>
                .SuccessResponse(ingredients, "Ingredients retrieved successfully.", HttpStatusCode.OK);
        }

        public async Task<Result<IngredientDTO>> GetIngredientByPublicIdAsync(Guid id, 
                                                                                CancellationToken cancellationToken = default)
        {
            var query = _context.Ingredients
                .Include(i => i.Brand)
                .Include(i => i.Category)
                .Include(i => i.IngredientSku)
                .Join(_context.Images,
                    ir => ir.Id,
                    im => im.ReferenceId,
                    (ir, im) => new
                    {
                        Ingredient = ir,
                        Image = im
                    })
                .Where(x => x.Ingredient.PublicId == id && x.Image.Type == ReferenceType.Ingredient);
            var ingredient = await query
                .Select(i => new IngredientDTO
                {
                    PublicId = i.Ingredient.PublicId,
                    Name = i.Ingredient.Name,
                    Description = i.Ingredient.Description,
                    BrandName = i.Ingredient.Brand.Name,
                    CategoryName = i.Ingredient.Category.Name,
                    Images = new List<ImageDTO>
                    {
                        new ImageDTO
                        {
                            ImageUrl = i.Image.ImageUrl,
                            IsPrimary = i.Image.IsPrimary,
                            CreatedAt = i.Image.CreatedAt
                        }
                    },
                    UnitPrice = i.Ingredient.IngredientSku!.UnitPrice,
                    Unit = i.Ingredient.IngredientSku!.Unit,
                    Stock = i.Ingredient.IngredientSku!.Stock,
                    Status = i.Ingredient.IngredientSku!.Status,
                    CreatedAt = i.Ingredient.CreatedAt,
                    UpdatedAt = i.Ingredient.UpdatedAt,
                    IsDeleted = i.Ingredient.IsDeleted
                })
                .FirstOrDefaultAsync(cancellationToken);
            if (ingredient == null)
            {
                return Result<IngredientDTO>
                    .ErrorResponse("Ingredient not found.", HttpStatusCode.NotFound);
            }
            return Result<IngredientDTO>
                .SuccessResponse(ingredient, "Ingredient retrieved successfully.", HttpStatusCode.OK);
        }

        public async Task<Result<IngredientDTO>> CreateIngredientAsync(CreateIngredientDTO createIngredientDTO, 
                                                                        CancellationToken cancellationToken = default)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var ingredient = new Ingredient
                {
                    PublicId = Guid.NewGuid(),
                    Name = createIngredientDTO.Name,
                    Description = createIngredientDTO.Description,
                    BrandId = await GetBrandId(createIngredientDTO.BrandName),
                    CategoryId = await GetCategoryId(createIngredientDTO.CategoryName)
                };
                _context.Ingredients.Add(ingredient);
                await _context.SaveChangesAsync(cancellationToken);

                var ingredientSku = new IngredientSku
                {
                    IngredientId = ingredient.Id,
                    UnitPrice = createIngredientDTO.UnitPrice,
                    Unit = createIngredientDTO.Unit,
                    Stock = createIngredientDTO.Stock,
                    Status = StockStatus.Active
                };
                _context.IngredientSkus.Add(ingredientSku);
                await _context.SaveChangesAsync(cancellationToken);

                var images = createIngredientDTO.Images.Select(img => new Image
                {
                    ReferenceId = ingredient.Id,
                    Type = ReferenceType.Ingredient,
                    ImageUrl = img.ImageUrl,
                    IsPrimary = img.IsPrimary
                }).ToList();
                _context.Images.AddRange(images);
                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                var ingredientDTO = new IngredientDTO
                {
                    PublicId = ingredient.PublicId,
                    Name = ingredient.Name,
                    Description = ingredient.Description,
                    BrandName = createIngredientDTO.BrandName,
                    CategoryName = createIngredientDTO.CategoryName,
                    Images = createIngredientDTO.Images,
                    UnitPrice = ingredientSku.UnitPrice,
                    Unit = ingredientSku.Unit,
                    Stock = ingredientSku.Stock,
                    Status = ingredientSku.Status,
                    CreatedAt = ingredient.CreatedAt,
                    UpdatedAt = ingredient.UpdatedAt,
                    IsDeleted = ingredient.IsDeleted
                };
                return Result<IngredientDTO>
                    .SuccessResponse(ingredientDTO, "Ingredient created successfully.", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result<IngredientDTO>
                    .ErrorResponse($"An error occurred while creating the ingredient: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result<IngredientDTO>> UpdateIngredientAsync(Guid id, UpdateIngredientDTO updateIngredientDTO, 
                                                                        CancellationToken cancellationToken = default)
        {
            var query = _context.Ingredients
                .Include(i => i.Brand)
                .Include(i => i.Category)
                .Include(i => i.IngredientSku)
                .Join(_context.Images,
                    ir => ir.Id,
                    im => im.ReferenceId,
                    (ir, im) => new
                    {
                        Ingredient = ir,
                        Image = im
                    })
                .Where(x => x.Ingredient.PublicId == id && x.Image.Type == ReferenceType.Ingredient);

            var ingredient = await query.FirstOrDefaultAsync();
            if (ingredient == null)
            {
                return Result<IngredientDTO>
                    .ErrorResponse("Ingredient not found.", HttpStatusCode.NotFound);
            }

            ingredient.Ingredient.Name = updateIngredientDTO.Name;
            ingredient.Ingredient.Description = updateIngredientDTO.Description;

            ingredient.Ingredient.BrandId = await GetBrandId(updateIngredientDTO.BrandName);
            ingredient.Ingredient.CategoryId = await GetCategoryId(updateIngredientDTO.CategoryName);

            ingredient.Ingredient.UpdatedAt = DateTime.Now;

            ingredient.Ingredient.IngredientSku.UnitPrice = updateIngredientDTO.UnitPrice;
            ingredient.Ingredient.IngredientSku.Unit = updateIngredientDTO.Unit;
            ingredient.Ingredient.IngredientSku.Stock = updateIngredientDTO.Stock;

            var ingredientDTO = new IngredientDTO
            {
                PublicId = ingredient.Ingredient.PublicId,
                Name = ingredient.Ingredient.Name,
                Description = ingredient.Ingredient.Description,
                BrandName = updateIngredientDTO.BrandName,
                CategoryName = updateIngredientDTO.CategoryName,
                Images = ingredient.Image != null ? new List<ImageDTO>
                {
                    new ImageDTO
                    {
                        ImageUrl = ingredient.Image.ImageUrl,
                        IsPrimary = ingredient.Image.IsPrimary,
                        CreatedAt = ingredient.Image.CreatedAt
                    }
                } : new List<ImageDTO>(),
                UnitPrice = ingredient.Ingredient.IngredientSku.UnitPrice,
                Unit = ingredient.Ingredient.IngredientSku.Unit,
                Stock = ingredient.Ingredient.IngredientSku.Stock,
                Status = ingredient.Ingredient.IngredientSku.Status,
                CreatedAt = ingredient.Ingredient.CreatedAt,
                UpdatedAt = ingredient.Ingredient.UpdatedAt,
                IsDeleted = ingredient.Ingredient.IsDeleted
            };
            await _context.SaveChangesAsync(cancellationToken);
            return Result<IngredientDTO>
                .SuccessResponse(ingredientDTO, "Ingredient updated successfully.", HttpStatusCode.OK);
        }

        public async Task<Result> DeleteIngredientAsync(Guid id, 
                                                        CancellationToken cancellationToken = default)
        {
            var ingredient = await _context.Ingredients
                .FirstOrDefaultAsync(i => i.PublicId == id, cancellationToken);
            if (ingredient == null)
            {
                return Result
                    .ErrorResponse("Ingredient not found.", HttpStatusCode.NotFound);
            }
            if(ingredient.IsDeleted)
            {
                return Result
                    .ErrorResponse("Ingredient is already deleted.", HttpStatusCode.Conflict);
            }
            ingredient.IsDeleted = true;
            ingredient.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return Result
                .SuccessResponse("Ingredient deleted successfully.", HttpStatusCode.OK);
        }

        public async Task<Result> RestoreIngredientAsync(Guid id, 
                                                        CancellationToken cancellationToken = default)
        {
            var ingredient = await _context.Ingredients
                .FirstOrDefaultAsync(i => i.PublicId == id, cancellationToken);
            if (ingredient == null)
            {
                return Result
                    .ErrorResponse("Ingredient not found.", HttpStatusCode.NotFound);
            }
            if (!ingredient.IsDeleted)
            {
                return Result
                    .ErrorResponse("Ingredient is not deleted.", HttpStatusCode.Conflict);
            }
            ingredient.IsDeleted = false;
            ingredient.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return Result
                .SuccessResponse("Ingredient restored successfully.", HttpStatusCode.OK);
        }

        private async Task<int> GetCategoryId(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Category name cannot be null or empty.");
            }
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == name);
            if (category == null)
            {
                var newCategory = new Category { Name = name };
                _context.Categories.Add(newCategory);
                await _context.SaveChangesAsync();
                return newCategory.Id;
            }
            return category.Id;
        }

        private async Task<int> GetBrandId(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Brand name cannot be null or empty.");
            }
            var brand = await _context.Brands
                .FirstOrDefaultAsync(b => b.Name == name);
            if (brand == null)
            {
                var newBrand = new Brand { Name = name };
                _context.Brands.Add(newBrand);
                await _context.SaveChangesAsync();
                return newBrand.Id;
            }
            return brand.Id;
        }
    }
}
