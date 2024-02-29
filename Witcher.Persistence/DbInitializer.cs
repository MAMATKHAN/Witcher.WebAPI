namespace Witcher.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(WitcherDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
