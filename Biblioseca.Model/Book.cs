using Biblioseca.Model.Exceptions;

namespace Biblioseca.Model
{
    public class Book : Entity
    {
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string ISBN { get; set; }
        public virtual double Price { get; set; }
        public virtual Author Author { get; set; }
        public virtual Category Category { get; set; }
        public virtual int Stock { get; set; }

        public virtual void DecreaseStock()
        {
            Stock -= 1;
        }

        public virtual void IncreaseStock()
        {
            Stock += 1;
        }

        public static Book Create(
            string title,
            string description,
            string ISBN,
            double price,
            Category category,
            Author author,
            int stock)
        {
            Ensure.NotNull(title, "Titulo no puede ser nulo. ");
            Ensure.NotNull(description, "Descrición no puede ser nula. ");
            Ensure.NotNull(ISBN, "ISBN no puede ser nulo. ");
            Ensure.IsTrue(price > 0, "Precio debe ser mayor que 0. ");
            Ensure.NotNull(category, "Categoría no puede ser nula. ");
            Ensure.NotNull(author, "Author no puede ser nulo. ");
            Ensure.IsTrue(stock > 0, "Stock debe ser mayor que 0. ");

            Book book = new Book
            {
                Title = title,
                Description = description,
                ISBN = ISBN,
                Price = price,
                Category = category,
                Author = author,
                Stock = stock
            };

            return book;
        }
    }
}