namespace DatadrivenApplicationEFCore.Models
{
    public class DbInitializer
    {
        public static void Seed(DatadrivenApplicationDbContext context)
        {
            if (!context.Categories.Any()) 
            {
                context.Categories.AddRange(Categories.Select(c => c.Value));
            }
            context.SaveChanges();

            if (!context.Cakes.Any())
            {
                context.AddRange
                (
                    new Cake
                    {
                        Name = "Strawberry Delight Cake",
                        Price = 19.99M,
                        ShortDescription = "A soft sponge cake filled with fresh strawberries.",
                        LongDescription = "Our Strawberry Delight Cake is a delicious blend of sweet and tangy strawberries layered with soft vanilla sponge and whipped cream. Topped with fresh strawberries and a strawberry glaze, this cake is perfect for summer celebrations or as a refreshing treat. Every slice is light, fruity, and beautifully balanced — a classic favorite brought to perfection.",
                        Category = Categories["Fruit Cake"], 
                        ImageUrl = "/images/cakes/strawberry-delight.jpg",
                        ImageThumbnail = "/Images/Cakes/Thumbnails/StrawberryCake.png",
                        InStock = true,
                        IsCakeOfTheWeek = true,
                        AllergyInfo = "Contains dairy, eggs, and gluten.",
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Name = "Vanilla sponge cake", Quantity = "2 layers" },
                            new Ingredient { Name = "Whipped cream", Quantity = "150 grams" },
                            new Ingredient { Name = "Fresh strawberries", Quantity = "1 cup (sliced)" },
                            new Ingredient { Name = "Strawberry glaze", Quantity = "100 grams" }
                        }
                    },

                    new Cake
                    {
                        Name = "Classic New York Cheesecake",
                        ShortDescription = "Rich and creamy cheesecake with a buttery graham cracker crust.",
                        LongDescription = "Our Classic New York Cheesecake features a dense, smooth cream cheese filling on top of a perfectly crisp graham cracker base. Baked to perfection and topped with a light glaze — a timeless dessert favorite.",
                        AllergyInfo = "Contains dairy, eggs, and gluten.",
                        Price = 24.99M,
                        ImageUrl = "/images/cakes/newyork-cheesecake.jpg",
                        ImageThumbnail = "/Images/Cakes/Thumbnails/CheeseCake.png",
                        IsCakeOfTheWeek = false,
                        InStock = true,
                        Category = Categories["Cheese Cake"],
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Name = "Cream cheese", Quantity = "500 grams" },
                            new Ingredient { Name = "Graham cracker crust", Quantity = "1 base" },
                            new Ingredient { Name = "Sugar", Quantity = "200 grams" },
                            new Ingredient { Name = "Eggs", Quantity = "3 large" },
                            new Ingredient { Name = "Vanilla extract", Quantity = "1 tsp" }
                        }
                    },
                    new Cake
                    {
                        // CakeId = 4,
                        Name = "Chocolate Fudge Fantasy",
                        ShortDescription = "A rich and decadent chocolate fudge cake.",
                        LongDescription = "Chocolate Fudge Fantasy is a moist, dense chocolate cake layered with smooth chocolate fudge frosting and topped with chocolate shavings. Perfect for chocolate lovers looking for an indulgent treat.",
                        AllergyInfo = "Contains dairy, eggs, gluten, and soy.",
                        Price = 25.99M,
                        ImageUrl = "/images/cakes/chocolate-fudge-fantasy.jpg",
                        ImageThumbnail = "/Images/Cakes/Thumbnails/ChocolateFudgeFantasy.png",
                        IsCakeOfTheWeek = false,
                        InStock = true,
                        Category = Categories["Seasonal Cakes"], // Make sure this category key exists
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Name = "Dark chocolate", Quantity = "200 grams" },
                            new Ingredient { Name = "Butter", Quantity = "150 grams" },
                            new Ingredient { Name = "Flour", Quantity = "250 grams" },
                            new Ingredient { Name = "Sugar", Quantity = "200 grams" },
                            new Ingredient { Name = "Eggs", Quantity = "3 large" }
                        }
                    },
                    new Cake
                    {
                        // CakeId = 5,
                        Name = "Lemon Drizzle Cake",
                        ShortDescription = "A zesty lemon sponge cake with a tangy drizzle icing.",
                        LongDescription = "Our Lemon Drizzle Cake is a light and fluffy lemon-flavored sponge cake, topped with a sweet and tangy lemon glaze. Perfectly refreshing and ideal for any occasion.",
                        AllergyInfo = "Contains dairy, eggs, gluten.",
                        Price = 20.50M,
                        ImageUrl = "/images/cakes/lemon-drizzle.jpg",
                        ImageThumbnail = "/Images/Cakes/Thumbnails/LemonDrizzle.png",
                        IsCakeOfTheWeek = false,
                        InStock = true,
                        Category = Categories["Fruit Cake"], // Ensure this key exists
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient { Name = "Flour", Quantity = "250 grams" },
                            new Ingredient { Name = "Sugar", Quantity = "200 grams" },
                            new Ingredient { Name = "Lemon zest", Quantity = "2 lemons" },
                            new Ingredient { Name = "Eggs", Quantity = "3 large" },
                            new Ingredient { Name = "Butter", Quantity = "150 grams" }
                        }
                    }
                );                
            }
            context.SaveChanges();

            /*if(!context.Orders.Any())
            {
                //context.AddRange 
            }
            context.SaveChanges();*/
        }

        public static Dictionary<string, Category>? categories;

        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    var genreList = new Category[]
                    {
                        new Category { Name = "Fruit Cake", DateAdded= DateTime.Today },
                        new Category { Name = "Cheese Cake", DateAdded= DateTime.Today },
                        new Category { Name = "Seasonal Cakes", DateAdded= DateTime.Today }
                    };
                    categories = new Dictionary<string, Category>();

                    foreach (var genre in genreList)
                    {
                        categories.Add(genre.Name, genre);
                    }
                }
                return categories;
            }
        }
    }
}
