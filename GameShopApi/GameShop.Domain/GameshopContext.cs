using GameShop.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Domain;
public class GameshopContext : DbContext
{
    public DbSet<Card> Cards => Set<Card>();
}
