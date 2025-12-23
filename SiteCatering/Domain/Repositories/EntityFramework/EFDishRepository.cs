using Microsoft.EntityFrameworkCore;
using SiteCatering.Domain;
using SiteCatering.Domain.Entities;
using SiteCatering.Domain.Enums;
using SiteCatering.Domain.Repositories.Abstract;

public class EFDishRepository : IDishRepository
{
    private readonly AppDbContext _context;

    public EFDishRepository(AppDbContext context)
    { _context = context; }

    public async Task<IEnumerable<Dish>> GetDishesAsync()
    {
        return await _context.Dishes.ToListAsync();
    }

    public async Task<Dish?> GetDishByIdAsync(int Id)
    {
        return await _context.Dishes.FirstOrDefaultAsync(x => x.Id == Id);
    }

    public async Task SaveDishAsync(Dish dish)
    {
        _context.Entry(dish).State = dish.Id == default ? EntityState.Added : EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteDishAsync(int id)
    {
        _context.Entry(new Dish() { Id = id }).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Dish>> GetDishesByCategoryAsync(MenuCategoryEnum category)
    {
        return await _context.Dishes
            .Where(d => d.MenuCategoryEnum == category)
            .ToListAsync();
    }

    public  async Task<IEnumerable<Dish>> GetRecommendedDishesAsync()
    {
        return await _context.Dishes
        .Where(d=>d.IsRecommended)
            .ToListAsync();
    }
}
