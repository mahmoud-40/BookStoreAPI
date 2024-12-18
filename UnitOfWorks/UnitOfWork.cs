using BookStoreAPI.DTOs.BookDTOs;
using BookStoreAPI.Models;
using BookStoreAPI.Repository;

namespace BookStoreAPI.UnitOfWorks
{
    public class UnitOfWork
    {
        BookStoreContext db;
        public UnitOfWork(BookStoreContext context)
        {
            db = context;
        }

        GenericRepository<Book> bookRepo;
        GenericRepository<Order> orderRepo;
        GenericRepository<OrderDetails> orderDetailsRepo;
        GenericRepository<Author> authorRepo;
        GenericRepository<Customer> customerRepo;
        GenericRepository<Catalog> catalogRepo;

        public GenericRepository<Book> BookRepo
        {
            get
            {
                if (bookRepo == null)
                    bookRepo = new GenericRepository<Book>(db);
                return bookRepo;
            }
        }

        public GenericRepository<Order> OrderRepo
        {
            get
            {
                if (orderRepo == null)
                    orderRepo = new GenericRepository<Order>(db);
                return orderRepo;
            }
        }
        public GenericRepository<OrderDetails> OrderDetailsRepo
        {
            get
            {
                if (orderDetailsRepo == null)
                    orderDetailsRepo = new GenericRepository<OrderDetails>(db);
                return orderDetailsRepo;
            }
        }

        public GenericRepository<Author> AuthorRepo
        {
            get
            {
                if (authorRepo == null)
                    authorRepo = new GenericRepository<Author>(db);
                return authorRepo;
            }
        }

        public GenericRepository<Customer> CustomerRepo
        {
            get
            {
                if (customerRepo == null)
                    customerRepo = new GenericRepository<Customer>(db);
                return customerRepo;
            }
        }

        public GenericRepository<Catalog> CatalogRepo
        {
            get
            {
                if (catalogRepo == null)
                    catalogRepo = new GenericRepository<Catalog>(db);
                return catalogRepo;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
