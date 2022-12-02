using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlApi;


using DO;


namespace BlImplementation
{
    internal class Order : IOrder
    {

        DalApi.IDal dal = new Dal.DalList();

        private delegate BO.OrderForList? sc<in T>(T obj);
        public delegate TOutput Converter<in TInput, out TOutput>(TInput input);

        //have to check if this what they mean i''l do
        public BO.Order GetOrderByID(int orderID)
        {
            BO.Order order;
            try
            {
                order = ConvertOrderDoToBO(dal.Order.GetById(orderID));
            }
            catch (BO.BlNullPropertyException e)
            {
                throw new BO.BlNullPropertyException("", e);
            }
            catch (DO.DalIdDoNotExistException ex)
            {
                throw new BO.BlIdDoNotExistException("order", ex);
            }
            return order;
        }
        public IEnumerable<BO.OrderForList?> GetOrderList()
        {
            IEnumerable<BO.Order?> boOrder;
            IEnumerable<BO.OrderForList> boOrderList;
            IEnumerable<DO.Order?> doList = dal.Order.GetAll();
            try
            {
                boOrder = doList?.Select(item => ConvertOrderDoToBO(item)) ?? throw new BO.BlNullPropertyException("no orders");
            }
            catch (BO.BlNullPropertyException e)
            {
                throw new BO.BlNullPropertyException("", e);
            }
            boOrderList = from BO.Order item in boOrder
                          select (ConvertToOrderList(item));
            return boOrderList;
        }
        public BO.OrderTracking OrderTracking(int orderID)
        {
            DO.Order doOrder;
            try
            {
                doOrder = dal.Order.GetById(orderID);
            }
            catch (BO.BlNullPropertyException e)
            {
                throw new BO.BlNullPropertyException("order", e);
            }
            BO.Order? boOrder = ConvertOrderDoToBO(doOrder);
            BO.OrderTracking track = new BO.OrderTracking();
            track.ID = orderID;
            track.Status = boOrder.Status;//we assume that bo order is not empty couse we made a check up
            track.Tracking = new List<Tuple<DateTime?, string>>();
            Tuple<DateTime?, string> deliveredT;
            Tuple<DateTime?, string> shipedT;
            Tuple<DateTime?, string> orderedT = new Tuple<DateTime?, string>(boOrder.OrderDate, BO.OrderStatus.Ordered.ToString());
            track.Tracking.Add(orderedT);
            if (boOrder.Status == BO.OrderStatus.Delivered)
            {
                shipedT = new Tuple<DateTime?, string>(boOrder.ShipDate, BO.OrderStatus.Shipped.ToString());
                track.Tracking.Add(shipedT);
                deliveredT = new Tuple<DateTime?, string>(boOrder.DeliveryDate, boOrder.Status.ToString());
                track.Tracking.Add(deliveredT);
            }
            else if (boOrder.Status == BO.OrderStatus.Shipped)
            {
                shipedT = new Tuple<DateTime?, string>(boOrder.ShipDate, BO.OrderStatus.Shipped.ToString());
                track.Tracking.Add(shipedT);
                //throw new NotImplementedException();
            }
            return track;
            
        }
        public BO.Order UpdateDelivery(int orderID)
        {
            try
            {
                DO.Order order = dal.Order.GetById(orderID);
                if (order.DeliveryDate == null)
                    order.DeliveryDate = DateTime.Now;
                else throw new Exception("cant update delivered order");
                return ConvertOrderDoToBO(order) ?? throw new Exception("cant convert to bo order");
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
                if (order.DeliveryDate == null && order.ShipDate == null)
                    order.DeliveryDate = DateTime.Now;
                else throw new Exception("cant update shiped order");
                return ConvertOrderDoToBO(order) ?? throw new Exception("cant convert to bo order");
            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }
        }
        public BO.OrderForList? ConvertToOrderList(BO.Order orderToCon)
        {
            return new BO.OrderForList()
            {
                ID = orderToCon.Id,
                CustomerName = orderToCon.CustomerName,
                Status = orderToCon.Status,
                AmountOfItems = orderToCon?.Items.Count() ?? 0,
                TotalPrice = orderToCon?.TotalPrice ?? 0
            };
        }
        public BO.Order? ConvertOrderDoToBO(DO.Order? doOrder)
        {
            BO.Order boOrder;//what about rhe exeptions below (email)%$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
            try
            {
                boOrder = new BO.Order()
                {
                    Id = doOrder?.OrderId ?? throw new BO.BlNullPropertyException("order id"),
                    CustomerName = doOrder?.CustomerName ?? throw new BO.BlNullPropertyException("missing order customer name"),
                    CustomerEmail = doOrder?.CustomerEmail ?? throw new BO.BlNullPropertyException("missing order customer email"),
                    CustomerAddress = doOrder?.CustomerAddress ?? throw new BO.BlNullPropertyException("missing order customer email"),
                    OrderDate = doOrder?.OrderDate ?? throw new BO.BlNullPropertyException("missing order date"),
                    ShipDate = doOrder?.ShipDate,
                    DeliveryDate = doOrder?.DeliveryDate,
                };
                if (doOrder?.ShipDate != null)
                {
                    if (doOrder?.DeliveryDate != null && doOrder?.DeliveryDate < DateTime.Now)
                        boOrder.Status = BO.OrderStatus.Delivered;
                    else boOrder.Status = BO.OrderStatus.Shipped;
                }
                else
                    boOrder.Status = BO.OrderStatus.Ordered;
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

            catch (Exception e) { throw new BO.BlNullPropertyException("order", e); }
        }

    }

}
