using EFLinq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFLinq.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult ListOfProductOrderAgainstCustomer()
        {
            ProductsEntities iEntity = new ProductsEntities();
            List<Customer> customers = iEntity.Customers.ToList();
            List<PurchaseOrder> purchaseOrder = iEntity.PurchaseOrders.ToList();
            List<Product> product = iEntity.Products.ToList();
            List<PurchaseOrderDetail> purchaseOrderDetail = iEntity.PurchaseOrderDetails.ToList();
            var IE = from p in product
                     join pod in purchaseOrderDetail on p.ProductID equals pod.ProductID
                     join po in purchaseOrder on pod.POID equals po.POID
                     join c in customers on po.CustomerID equals c.CustomerID
                     orderby c.CustomerID
                     select new CustomerProducts
                     {
                         CustomerName = c,
                         POID = po,
                         ProductName = p,
                         Price = pod
                     };

            return View(IE);
        }

        public ActionResult MonthWiseCustomerReportWithProducts()
        {
            ProductsEntities iEntity = new ProductsEntities();
            List<Customer> customers = iEntity.Customers.ToList();
            List<PurchaseOrder> purchaseOrder = iEntity.PurchaseOrders.ToList();
            var IE = from po in purchaseOrder
                     join c in customers on po.CustomerID equals c.CustomerID
                     orderby po.DateOfPurchase.Month
                     select new MonthWiseCustomerReport
                     {
                         CustomerName = c,
                         DateOfPurchase = po.DateOfPurchase,
                         Amount = po
                     };
            return View(IE);
        }
        public ActionResult ProductSalesReportMonthWise()
        {
              ProductsEntities iEntity = new ProductsEntities();
            List<Product> products = iEntity.Products.ToList();
            List<PurchaseOrder> purchaseOrder = iEntity.PurchaseOrders.ToList();
            List<PurchaseOrderDetail> purchaseOrderDetail = iEntity.PurchaseOrderDetails.ToList();
            var IE = from p in products
                     join po in purchaseOrder on p.ProductID equals po.ProductID
                     join pod in purchaseOrderDetail on po.POID equals pod.POID
                     orderby po.DateOfPurchase.Month
                     select new MonthWiseProductSales
                     {
                         DateOfPurchase = po.DateOfPurchase,
                         ProductName = p,
                         Quantity = pod
                     };
            return View(IE);
        }
        public ActionResult MaximumOrderPriceMonthWise()
        {
            ProductsEntities iEntity = new ProductsEntities();
            List<PurchaseOrder> purchaseOrder = iEntity.PurchaseOrders.ToList();
            List<PurchaseOrderDetail> purchaseOrderDetail = iEntity.PurchaseOrderDetails.ToList();
            var IE1 = from po in purchaseOrder
                      join pod in purchaseOrderDetail on po.POID equals pod.POID
                      group new { po, pod } by new { po.DateOfPurchase.Month } into G
                      let firstproductgroup = G.FirstOrDefault()
                      let DOP = firstproductgroup.po
                      let POID = firstproductgroup.pod
                      let maxprice = G.Max(m => m.pod.Price)
                      select new MaxPriceMonthWise
                      {
                          DateOfPurchase = DOP.DateOfPurchase,
                          POID = POID,
                          Price = maxprice
                      };
            return View(IE1);
        }
    }
}