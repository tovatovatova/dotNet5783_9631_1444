using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;

using DO;


namespace BlImplementation
{
    internal class Order : IOrder
    {

        DalApi.IDal dal = new Dal.DalList();

        private delegate BO.OrderForList? sc <in T>(T obj);
        public delegate TOutput Converter<in TInput, out TOutput>(TInput input);
       
        //have to check if this what they mean i''l do
        public BO.Order GetOrderByID(int orderID)
        {
            try
            {
                return ConvertO(dal.Order.GetById(orderID));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }            //BO.Order boOrder;//dont shure if its ok to do it like that 
            //    try
            //    {
            //        if (orderID <= 0) throw new Exception("Invalid ID");

            //        DO.Order doOrder = dal.Order.GetById(orderID);
            //        boOrder = new BO.Order()
            //        {
            //            Id = orderID,
            //            CustomerName = doOrder.CustomerName,
            //            CustomerEmail = doOrder.CustomerEmail,
            //            CustomerAddress = doOrder.CustomerAddress,
            //            OrderDate = doOrder.OrderDate,
            //            ShipDate = doOrder.ShipDate,
            //            DeliveryDate = doOrder.DeliveryDate,
            //        };
            //        if (doOrder.ShipDate != null)
            //        {
            //            if (doOrder.DeliveryDate != null && doOrder.DeliveryDate < DateTime.Now)
            //                boOrder.Status = OrderStatus.Delivered;
            //            else boOrder.Status = OrderStatus.Shipped;
            //        }
            //        else
            //            boOrder.Status = OrderStatus.Ordered;
            //        boOrder.Items = from DO.OrderItem? items in dal.OrderItem.GetAll()
            //                        where items.Value.OrderId == orderID
            //                        select new BO.OrderItem
            //                        {
            //                            ID = items.Value.OrderItemId,
            //                            Name = dal.Product.GetById(items.Value.ProductId).Name,
            //                            ProductID = items.Value.ProductId,
            //                            Price = items.Value.Price,
            //                            Amount = items.Value.Amount,
            //                            TotalPrice = items.Value.Price * items.Value.Amount
            //                        };
            //        boOrder.TotalPrice = boOrder.Items.Sum(item => item.TotalPrice);
            //        return boOrder;
            //    }
            //    catch (Exception e) { throw new Exception(e.Message); }
        }

        public IEnumerable<OrderForList?> GetOrderList()
        {
            try
            {
                IEnumerable<DO.Order?> doList = dal.Order.GetAll();
                IEnumerable<BO.Order> boOrder = doList.Select(item => ConvertO(item));
                return boOrder.Select(item => ConvertOrderList(item));
                //return doList.Select(item => ConvertOrderList(ConvertO(item)));
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
           // throw new NotImplementedException();
        }

        public OrderTracking OrderTracking(int orderID)
        {
          DO.Order doOrder=dal.Order.GetById(orderID);
            BO.Order? boOrder = ConvertO(doOrder)?? throw new Exception("cant convert");
            BO.OrderTracking track=new BO.OrderTracking();
            track.Tracking = new List<Tuple<DateTime?, string>>();
            Tuple<DateTime?,string> tp;
            if (boOrder.Status == OrderStatus.Delivered)
            {
                tp = new Tuple<DateTime?, string>(boOrder.DeliveryDate, boOrder.Status.ToString());
                track.Tracking.Add(tp);
                tp = new Tuple<DateTime?, string>(boOrder.ShipDate,BO.OrderStatus.Shipped.ToString());
                track.Tracking.Add(tp);
               tp=new Tuple<DateTime?, string>(boOrder.OrderDate, BO.OrderStatus.Ordered.ToString());
                track.Tracking.Add(tp);
            }
            else if (boOrder.Status == OrderStatus.Shipped)
            {
                tp = new Tuple<DateTime?, string>(boOrder.ShipDate, BO.OrderStatus.Shipped.ToString());
                track.Tracking.Add(tp);
                tp = new Tuple<DateTime?, string>(boOrder.OrderDate, BO.OrderStatus.Ordered.ToString());
                track.Tracking.Add(tp);
            }
            else
                tp = new Tuple<DateTime?, string>(boOrder.OrderDate, BO.OrderStatus.Ordered.ToString());
            track.Tracking.Add(tp);

           
            track.ID = orderID;
            track.Status = boOrder.Status;
            return track;

            //throw new NotImplementedException();
        }

        public BO.Order UpdateDelivery(int orderID)
        {
            try
            {
                DO.Order order = dal.Order.GetById(orderID);
                order.DeliveryDate = DateTime.Now;
                return ConvertO(order) ?? throw new Exception("cant convert to bo order");
            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }
        }

        public BO.Order UpdateOrder(int orderId)
        {
            throw new NotImplementedException();//bonus still stay empty
        }

        public BO.Order UpdateShip(int orderID)
        {
            try
            {
                DO.Order order = dal.Order.GetById(orderID);
                order.ShipDate = DateTime.Now;
                return ConvertO(order) ?? throw new Exception("cant convert to bo order");
            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }
        }
       public BO.OrderForList? ConvertOrderList(BO.Order orderToCon)
        {
            return new OrderForList()
            {
                ID = orderToCon.Id,
                CustomerName = orderToCon.CustomerName,
                Status = orderToCon.Status,
                AmountOfItems = orderToCon?.Items.Count() ?? 0,
                TotalPrice = orderToCon?.TotalPrice ?? 0

            };
        }
        public BO.Order? ConvertO(DO.Order? doOrder)
        {
            BO.Order boOrder;//dont shure if its ok to do it like that 

            try
            {
                //if (orderID <= 0) throw new Exception("Invalid ID");

                // DO.Order doOrder = dal.Order.GetById(orderID);
                boOrder = new BO.Order()
                {
                    Id = doOrder?.OrderId ?? throw new Exception("nulll value"),
                    CustomerName = doOrder?.CustomerName?? throw new Exception("null value"),
                    CustomerEmail = doOrder?.CustomerEmail,
                    CustomerAddress = doOrder?.CustomerAddress,
                    OrderDate = doOrder?.OrderDate,
                    ShipDate = doOrder?.ShipDate,
                    DeliveryDate = doOrder?.DeliveryDate,

                };
                if (doOrder?.ShipDate != null)
                {
                    if (doOrder?.DeliveryDate != null && doOrder?.DeliveryDate < DateTime.Now)
                        boOrder.Status = OrderStatus.Delivered;
                    else boOrder.Status = OrderStatus.Shipped;
                }
                else
                    boOrder.Status = OrderStatus.Ordered;
                boOrder.Items = from DO.OrderItem? items in dal.OrderItem.GetAll()
                                where items.Value.OrderId == doOrder?.OrderId
                                select new BO.OrderItem
                                {
                                    ID = items.Value.OrderItemId,
                                    Name = dal.Product.GetById(items.Value.ProductId).Name,
                                    ProductID = items.Value.ProductId,
                                    Price = items.Value.Price,
                                    Amount = items.Value.Amount,
                                    TotalPrice = items.Value.Price * items.Value.Amount

                                };
                boOrder.TotalPrice = boOrder.Items.Sum(item => item.TotalPrice);
                return boOrder;

            }

            catch (Exception e) { throw new Exception(e.Message); }
        }
        
    }
    
}