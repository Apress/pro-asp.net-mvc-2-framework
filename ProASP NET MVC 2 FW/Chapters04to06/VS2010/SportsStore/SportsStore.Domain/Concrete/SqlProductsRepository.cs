using System;
using System.Data.Linq;
using System.Linq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{
    public class SqlProductsRepository : IProductsRepository
    {
        private Table<Product> productsTable;
        public SqlProductsRepository(string connectionString)
        {
            productsTable = (new DataContext(connectionString)).GetTable<Product>();
        }
        public IQueryable<Product> Products
        {
            get { return productsTable; }
        }

        public void SaveProduct(Product product)
        {
            // If it's a new product, just attach it to the DataContext
            if (product.ProductID == 0)
                productsTable.InsertOnSubmit(product);
            else if (productsTable.GetOriginalEntityState(product) == null)
            {
                // We're updating an existing product, but it's not attached to
                // this data context, so attach it and detect the changes
                productsTable.Attach(product);
                productsTable.Context.Refresh(RefreshMode.KeepCurrentValues, product);
            }
            productsTable.Context.SubmitChanges();
        }

        public void DeleteProduct(Product product)
        {
            productsTable.DeleteOnSubmit(product);
            productsTable.Context.SubmitChanges();
        }
    }
}