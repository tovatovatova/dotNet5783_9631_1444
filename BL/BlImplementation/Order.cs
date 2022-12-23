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

        DalApi.IDal dal = DalApi.Factory.Get();
        private delegate BO.OrderForList? sc<in T>(T obj);
        public delegate TOutput Converter<in TInput, out TOutput>(TInput input);

        //have to check if this what they mean i''l do
        /// <summary>
        /// return an order according to the given id
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns>order</returns>
        /// <exception cref="BO.BlNullPropertyException">throw when cant convert DO order to BO order</exception>
        /// <exception cref="BO.BlIdDoNotExistException">throw when order id doesnt exist</exception>
        public BO.Order GetOrderByID(int orderID)
        {
            BO.Order order;
            DO.Order ord;
            if (orderID < 0)//negative id
                throw new BO.BlInvalidInputException("order id");
            try
            {
                ord = dal.Order.GetById(orderID);//return order from do
            }
            catch (DO.DalIdDoNotExistException ex)
            {
                throw new BO.BlIdDoNotExistException("order", ex);
            }
            try
            {
                order = ConvertOrderDoToBO(ord);//try to covert the order that is return from dal
            }
            catch (BO.BlNullPropertyException e)//if couldnt convert
            {
                throw new BO.BlNullPropertyException("", e);
            }
            catch (DO.DalIdDoNotExistException ex)//if didint find this order
            {
                throw new BO.BlIdDoNotExistException("order", ex);
            }
            return order;//return BO order
        }
        /// <summary>
        /// return list of orders
        /// </summary>
        /// <returns>boOrderList></returns>
        /// <exception cref="BO.BlNullPropertyException">throw if couldnt convert DO order to BO order</exception>
        public IEnumerable<BO.OrderForList?> GetOrderList()
        {
            IEnumerable<BO.Order?> boOrder;
            IEnumerable<BO.OrderForList> boOrderList;
            IEnumerable<DO.Order?> doList = dal.Order.GetAll();//gets list of orders from dal
            try
            {
                boOrder = doList?.Select(item => ConvertOrderDoToBO(item)) ?? throw new BO.BlNullPropertyException("no orders");
            }
            catch (BO.BlNullPropertyException e)//if couldnt convert
            {
                throw new BO.BlNullPropertyException("", e);
            }
            boOrderList = from BO.Order item in boOrder//convert to order list(less details
                          select (ConvertToOrderList(item));
            return boOrderList;//return list
        }
        /// <summary>
        /// return list of tuple of status order of the given order id
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns>track</returns>
        /// <exception cref="BO.BlIdDoNotExistException"> throw if order wasnt found</exception>
        /// <exception cref="BO.BlNullPropertyException">throw if couldnt convert</exception>
        public BO.OrderTracking OrderTracking(int orderID)
        {
            if (orderID < 0)//negative id
                throw new BO.BlInvalidInputException("order id");
            DO.Order doOrder;
            try
            {
                doOrder = dal.Order.GetById(orderID);
            }
            catch (DO.DalIdDoNotExistException e)//order doesnt exist
            {
                throw new BO.BlIdDoNotExistException("order", e);
            }
            BO.Order? boOrder;
            try
            {
                 boOrder = ConvertOrderDoToBO(doOrder);//send to convert

            }
            catch (BO.BlNullPropertyException e)//couldnt convert
            {
                throw new BO.BlNullPropertyException("", e);
            }
            
            BO.OrderTracking track = new BO.OrderTracking();
            track.ID = orderID;
            track.Status = boOrder.Status;//we assume that bo order is not empty becouse we made a check up
            track.Tracking = new List<Tuple<DateTime?, string>>();//list of tuple
            Tuple<DateTime?, string> deliveredT;
            Tuple<DateTime?, string> shipedT;
            Tuple<DateTime?, string> orderedT = new Tuple<DateTime?, string>(boOrder.OrderDate, BO.OrderStatus.Ordered.ToString());//all the orders went throgh this stage-
            track.Tracking.Add(orderedT); //add to list
            if (boOrder.Status == BO.OrderStatus.Delivered)//if the order was delivered
            {
                shipedT = new Tuple<DateTime?, string>(boOrder.ShipDate, BO.OrderStatus.Shipped.ToString());//create a tuple
                track.Tracking.Add(shipedT);//add
                deliveredT = new Tuple<DateTime?, string>(boOrder.DeliveryDate, boOrder.Status.ToString());
                track.Tracking.Add(deliveredT);
            }
            else if (boOrder.Status == BO.OrderStatus.Shipped)//if the order was shipped
            {
                shipedT = new Tuple<DateTime?, string>(boOrder.ShipDate, BO.OrderStatus.Shipped.ToString());
                track.Tracking.Add(shipedT);
            }
            return track;//return list
            
        }
        /// <summary>
        /// update dat eof deleivary of order
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns>order</returns>
        /// <exception cref="BO.BlIdDoNotExistException">throw if order doesnt exist</exception>
        /// <exception cref="BO.BlIncorrectDateException">throw if coudnt update delivary date</exception>
        /// <exception cref="BO.BlNullPropertyException">throw if coudnt convert</exception>
        public BO.Order UpdateDelivery(int orderID)
        {
            if (orderID < 0)//negative id
                throw new BO.BlInvalidInputException("order id");
            DO.Order order;
            try
            {
                 order= dal.Order.GetById(orderID);//return order by id from dal
            }
            catch (DO.DalIdDoNotExistException ex)//order doesnt exist
            { 
                throw new BO.BlIdDoNotExistException("order",ex);
            }
            if (order.DeliveryDate == null)
                order.DeliveryDate = DateTime.Now;//update delivary date
            else //cant update-there a date in there already
                throw new BO.BlIncorrectDateException("cant update delivered order");
            try
            {
                return ConvertOrderDoToBO(order);//convert and return
            }
            catch (BO.BlNullPropertyException ex)//couldnt convert
            {
                throw new BO.BlNullPropertyException("cant convert to bo order",ex);
            }
        }
        /// <summary>
        /// update ship date of the given order id
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns>order</returns>
        /// <exception cref="BO.BlIdDoNotExistException">throw if order doesnt exist</exception>
        /// <exception cref="BO.BlIncorrectDateException">throw if cant update date of ship</exception>
        /// <exception cref="BO.BlNullPropertyException">throw if couldnt convert</exception>
        public BO.Order UpdateShip(int orderID)
        {
            DO.Order order;
            if (orderID < 0)//negative id
                throw new BO.BlInvalidInputException("order id");
            try
            {
                 order= dal.Order.GetById(orderID); //return order by the given id from dal
            }
            catch (DO.DalIdDoNotExistException ex)//order doesnt exist
            { 
                throw new BO.BlIdDoNotExistException("order",ex); 
            }
            if (order.DeliveryDate == null && order.ShipDate == null)
                order.DeliveryDate = DateTime.Now;//update delivary date
            else 
                throw new BO.BlIncorrectDateException("cant update shiped order");
            try
            {
                return ConvertOrderDoToBO(order);//convert and return
            }
            catch (BO.BlNullPropertyException ex)
            {
                throw new BO.BlNullPropertyException("cant convert to bo order", ex);//coudnt convert
            }
        }
        /// <summary>
        /// help function for converting order to order of list
        /// </summary>
        /// <param name="orderToCon"></param>
        /// <returns>BO.orderForList</returns>
        public BO.OrderForList? ConvertToOrderList(BO.Order orderToCon)
        {
            return new BO.OrderForList()
            {//convert
                ID = orderToCon.Id,
                CustomerName = orderToCon.CustomerName,
                Status = orderToCon.Status,
                AmountOfItems = orderToCon?.Items.Count() ?? 0,
                TotalPrice = orderToCon?.TotalPrice ?? 0
            };
        }
        /// <summary>
        /// help function for converting DO order to BO order
        /// </summary>
        /// <param name="doOrder"></param>
        /// <returns>boOrder</returns>
        /// <exception cref="BO.BlNullPropertyException">throw if couldnt convert</exception>
        public BO.Order? ConvertOrderDoToBO(DO.Order? doOrder)
        {
            BO.Order boOrder;//what about rhe exeptions below (email)%$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
            //try
            //{
                boOrder = new BO.Order()//create bo order and put tb\he details from do order
                {//if cant convert throw exception
                    Id = doOrder?.OrderId ?? throw new BO.BlNullPropertyException("order id"),
                    CustomerName = doOrder?.CustomerName ?? throw new BO.BlNullPropertyException("missing order customer name"),
                    CustomerEmail = doOrder?.CustomerEmail ?? throw new BO.BlNullPropertyException("missing order customer email"),
                    CustomerAddress = doOrder?.CustomerAddress ?? throw new BO.BlNullPropertyException("missing order customer email"),
                    OrderDate = doOrder?.OrderDate ?? throw new BO.BlNullPropertyException("missing order date"),
                    ShipDate = doOrder?.ShipDate,
                    DeliveryDate = doOrder?.DeliveryDate,
                };
                if (doOrder?.ShipDate != null)//update status
                {
                    if (doOrder?.DeliveryDate != null && doOrder?.DeliveryDate < DateTime.Now)
                        boOrder.Status = BO.OrderStatus.Delivered;
                    else boOrder.Status = BO.OrderStatus.Shipped;
                }
                else
                    boOrder.Status = BO.OrderStatus.Ordered;
                boOrder.Items = from DO.OrderItem? items in dal.OrderItem.GetAll()//runs on list of order item from dal
                                where items?.OrderId == doOrder?.OrderId//if order item has the same order id as the order
                                select new BO.OrderItem//choose those who stood in the conditions and convert them to order in BO
                                {
                                    ID = (items?.OrderItemId)??throw new BO.BlNullPropertyException("id"),
                                    Name = dal.Product.GetById(items?.ProductId??-1).Name??throw new BO.BlNullPropertyException("price"),
                                    ProductID = items?.ProductId??0,
                                    Price = items?.Price??0,
                                    Amount = items?.Amount??0,
                                    TotalPrice = items?.Price * items?.Amount?? throw new BO.BlNullPropertyException("total price")
                                };
                boOrder.TotalPrice = boOrder.Items.Sum(item => item.TotalPrice);//the total price of the order
                return boOrder;
        }
        //public BO.Order UpdateOrder<T>(int orderID,BO.UpdateOrder updateOrder,T value1,T value2)
        //{
        //    if (orderID < 0)//negative id
        //        throw new BO.BlInvalidInputException("order id");
        //    DO.Order doorder;
        //    try
        //    {
        //        doorder = dal.Order.GetById(orderID);//return order by id from dal
        //    }
        //    catch (DO.DalIdDoNotExistException ex)//order doesnt exist
        //    {
        //        throw new BO.BlIdDoNotExistException("order", ex);
        //    }
        //    switch (updateOrder)
        //    {
        //        case BO.UpdateOrder.Address:

        //            doorder.CustomerAddress = Convert.ToString(value1)??throw new BO.BlInvalidInputException("address");
        //            dal.Order.Update(doorder);
        //            break;
        //        case BO.UpdateOrder.AmountOfItem:
        //            DO.OrderItem item = dal.OrderItem.GetById(Convert.ToInt32( value1));
        //            break;
        //        case BO.UpdateOrder.DeleteItem:
        //            break;
        //        default:
        //            break;      
        //    }
        //}
    }

}
