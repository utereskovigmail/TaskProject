using Bogus;

namespace WebAliona.Data
{
    public class DataBaseManager
    {
        public void AddBanans(AppAlionaContext context)
        {
            var faker = new Faker<Banan>("uk")
                .RuleFor(b => b.FirstName, f => f.Name.FirstName())
                .RuleFor(b => b.LastName, f => f.Name.LastName())
                .RuleFor(b => b.Image, "https://picsum.photos/1200/800?grayscale")
                .RuleFor(b => b.Phone, f => f.Phone.PhoneNumber())
                .RuleFor(b => b.Sex, f => f.Random.Bool());

            for (int i = 0; i < 20; i++)
            {
                var b = faker.Generate(1);
                context.Add(b[0]);
                context.SaveChanges();
            }
        }

        public async Task AddNewsAsync(AppAlionaContext context, int count)
        {
            HttpClient client = new HttpClient();
            var faker = new Faker<New>("en")
                .RuleFor(b => b.title, f => f.Lorem.Sentence(3,2))
                .RuleFor(b => b.slug,(f, b) => b.title.Replace(' ', '-'))
                .RuleFor(b => b.summary, f => f.Lorem.Sentence(10, 2))
                .RuleFor(b => b.content, f => f.Lorem.Sentence(40, 5))
                .RuleFor(b => b.photo, (f,b) => $"{b.title}{f.Random.Int(1,10000)}.jpg");

            var newsList = new List<New>();  
            
            for (int i = 0; i < count; i++)
            {
                var newsItem = faker.Generate();  
                var bytes = await client.GetByteArrayAsync($"https://picsum.photos/800?grayscale");
                var path = Path.Combine("~img/", newsItem.photo);
                File.WriteAllBytes(path, bytes);
                newsList.Add(newsItem);          
            }
            
            await context.AddRangeAsync(newsList);
            await context.SaveChangesAsync();
        }
    }
}