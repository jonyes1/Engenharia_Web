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


        }
    }
}
