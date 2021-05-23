using Enitites;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Reflection;
using System;
using System.Threading;

namespace Repository
{
    public class Repository: IRepository
    {
        private static readonly int DefaultLockTimeout = 500;
        private InventoryDataSet.ProductDataTable productsTable;
        private InventoryDataSet.OrderDataTable orderTable = new InventoryDataSet.OrderDataTable();
        private int lockTimeOut;
        private object syncLock = new object();
        public Repository()
        {
            LoadData();
            GetLockTimeout();
        }

        private void GetLockTimeout()
        {
            lockTimeOut = ConfigurationManager.AppSettings["DataTableLockTimeout"] == null ? DefaultLockTimeout : Convert.ToInt32(ConfigurationManager.AppSettings["DataTableLockTimeout"]);
        }

        private void LoadData()
        {
            var sourceFile =ConfigurationManager.AppSettings["DataSoureFile"];
            var jsonData = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory+ "\\"+sourceFile);
            productsTable =(InventoryDataSet.ProductDataTable)JsonConvert.DeserializeObject(jsonData, (typeof(InventoryDataSet.ProductDataTable)));
        }

        List<Product> IRepository.GetProducts()
        {
            var list = productsTable.AsEnumerable()
                           .Select(r => 
                                        new Product { Name = r.Field<string>("Name"),
                                                      ProductId = r.Field<int>("ProductId"),
                                                      Quantity = r.Field<int>("Quantity") })
                           .ToList();
            if (!list.Any())
            {
                throw new KeyNotFoundException("No data found");
            }
            return list;
        }
      
        bool IRepository.PlaceOrders(List<Order> orders)
        {
            ValidateOrders(orders);
            if (Monitor.TryEnter(syncLock, DefaultLockTimeout))
            {
                try
                {
                    foreach (var order in orders)
                    {
                        InventoryDataSet.OrderRow row = orderTable.NewOrderRow();
                        row.ProductId = order.ProductId;
                        row.Quantity = order.Quantity;
                        orderTable.Rows.Add(row);
                        InventoryDataSet.ProductRow prodRow= productsTable.FindByProductId(row.ProductId);
                        prodRow.Quantity -= row.Quantity;
                    }
                    orderTable.AcceptChanges();
                    productsTable.AcceptChanges();
                    return true;
                }
                catch(Exception ex )
                {
                    throw;
                }
                finally
                {
                    Monitor.Exit(syncLock);
                }
            }
            else
            {
                throw new ApplicationException("Unabe to get lock after wai time");
            }
        }

        private void ValidateOrders(List<Order> orders)
        {
            foreach (var prodName in from order in orders
                                     let avaiableQuantity = productsTable.FindByProductId(order.ProductId).Quantity
                                     where order.Quantity > avaiableQuantity || order.Quantity <1
                                     let prodName = productsTable.FindByProductId(order.ProductId).Name
                                     select prodName)
            {
                throw new ApplicationException($"Order Name = {prodName} has insufficient quantity/invalid quantity");
            }
        }
    }
}
