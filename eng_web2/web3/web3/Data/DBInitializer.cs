using web3.Models;

namespace web3.Data
{
    public class DBInitializer
    {
        private web3Context _context;

        public DBInitializer(web3Context context)
        {
            _context=context;
        }

        public void Run() {

            _context.Database.EnsureCreated();
            if(_context.Category.Any())
            {
                return;
            }

            var categorias = new Category[]
            {    
                
                new Category {Name ="Programming", Description="Algorithms and programming area course"},
                new Category {Name ="Administration", Description="Algorithms and programming area course"},
                new Category {Name ="Communication", Description="Algorithms and programming area course"}

            };

            _context.Category.AddRange(categorias);
            //foreach(Category categoria in categorias)
            //{
            //    _context.Category.Add(categoria);
            //}
            _context.SaveChanges();

            var courses = new Course[]
            {
                new Course
                {
                    Name ="Web Engineering",
                    Description = "Creating new sites, ASP.NET",
                    Cost= 50, Credits=6,
                    CategoryId =categorias.Single(c=>c.Name=="Programming").id,





                },
                new Course
                {
                    Name ="Strategic Leadship and Managemnt",
                    Description = "Leadership .......",
                    Cost= 100, Credits=6,
                    Category =categorias.Single(c=>c.Name=="Administration")




                },
                new Course
                {
                    Name ="Master in Corporate Communications",
                    Description = "This Master in Corporation",
                    Cost= 80, Credits=6,
                    Category =categorias.Single(c=>c.Name=="Communication")




                },




            };
            _context.Corses.AddRange(courses);
            _context.SaveChanges();


        }
    }
}
