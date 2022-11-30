﻿using BlApi;
using BO;
using Dal;
using DalApi;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    internal class Cart : ICart
    {
        DalApi.IDal dal = new Dal.DalList();
        public BO.Cart AddToCart(BO.Cart currentCart, int ProductID)
        {
            DO.Product product = dal.Product.GetById(ProductID);//return product from dal with the given id
            BO.OrderItem? itemInCart = currentCart.Items.FirstOrDefault(item => item.ID == ProductID);// check if product exists in cart-if so, return the first object
            if (itemInCart != null)//product exists
            {
                int x = currentCart?.Items?.ToList().FindIndex(item => item.ID == ProductID) ?? -1;//find the index of the product in order to edit
                //how to do this without -1??????
                if (product.AmountInStock > itemInCart.Amount)//if the amount in stock is enough to add(at least) one item to cart
                {
                    currentCart.Items.ToList().ElementAt(x).Amount += 1;
                    currentCart.Items.ToList().ElementAt(x).Price = product.Price;
                    currentCart.Items.ToList().ElementAt(x).TotalPrice += product.Price;
                    currentCart.TotalPrice += product.Price;//add price of the one product we add
                }
            }
            else//not exist
            {
                if (product.AmountInStock > 0)//exist in stock
                {//id????????????????????????????????????????????????????????????????fvihbrfgvyuerfyvb
                    itemInCart = new BO.OrderItem()
                    {
                        ID = ProductID,
                        Name = product.Name,
                        ProductID = product.Id,
                        Price = product.Price,
                        Amount = 1,
                        TotalPrice = product.Price
                    };
                    currentCart.TotalPrice += itemInCart.Price;//add price of item to total price
                    currentCart.Items = currentCart.Items.Append(itemInCart);
                }
                else throw new Exception("not enough in stock");
            }
            return currentCart;//return cart after changes
        }
        public BO.Cart UpdateProductInCart(BO.Cart currentCart, int newAmount, int productId)
        {
            if (newAmount < 0) throw new Exception("nagative amount is invalid");
            DO.Product product = dal.Product.GetById(productId);
            BO.OrderItem? itemInCart = currentCart.Items.FirstOrDefault(item => item.ID == productId);
            int x = currentCart?.Items?.ToList().FindIndex(item => item.ID == productId) ?? -1;//find the index of the product in order to edit
            if (itemInCart != null)
            {
                int amountDifference = newAmount - itemInCart.Amount;
                if (newAmount == 0)
                {
                    currentCart.Items = currentCart.Items.ToList().Where(prod => prod.ProductID != productId);
                }
                else if (amountDifference == 0)
                    return currentCart;
                else if (amountDifference > 0)//the new amount bigger than the old one
                {
                    if (product.AmountInStock >= newAmount)//if there is enough items in stock
                    {
                        currentCart.Items.ToList().ElementAt(x).Amount = newAmount;
                        currentCart.Items.ToList().ElementAt(x).Price = product.Price;
                        currentCart.Items.ToList().ElementAt(x).TotalPrice += product.Price * amountDifference;//add the exstra price after the change
                        currentCart.TotalPrice += amountDifference * product.Price; //add to the cart total price
                    }
                }
                else if (amountDifference < 0)//if the new amount little than the old one
                {
                    if (newAmount <= product.AmountInStock)
                    {
                        currentCart.Items.ToList().ElementAt(x).Amount = newAmount;
                        currentCart.Items.ToList().ElementAt(x).Price = product.Price;
                        currentCart.Items.ToList().ElementAt(x).TotalPrice += product.Price * amountDifference;
                        currentCart.TotalPrice += amountDifference * product.Price; //sub from the cart total price
                        //%^^%%^^^ need to check in stock??????
                    }
                }

            }
            return currentCart;
            //  throw new NotImplementedException();
        }
        public void OrderCreate(BO.Cart cart)
        {
            if (cart.Items.Count() == 0) return;//what to do??????????????????????
            BO.Order boOrder = new BO.Order()
            {
                //Id=0,//just for now
                CustomerAddress = cart.CustomerAddress,
                CustomerName = cart.CustomerName,
                CustomerEmail = cart.CustomerEmail,
                OrderDate = DateTime.Now,
                Status = BO.OrderStatus.Ordered,
                ShipDate = null,
                DeliveryDate = null,
                TotalPrice = cart.TotalPrice
            };
            bool x = cart.Items.Any(item => CheckAndUpdate(item.ID, item.Amount) == false);
            // IEnumerable <OrderItem> notEnough= cart.Items.Where(item => ifInStock(item.ID, item.Amount) == false);
            if (x)
                throw new Exception("you need to update your cart!");
            boOrder.Items = cart.Items.Select(item => item);//copy the items into order
            DO.Order newOrder = new DO.Order()
            {
                CustomerName = boOrder.CustomerName,
                CustomerAddress = boOrder.CustomerAddress,
                CustomerEmail = boOrder.CustomerEmail,
                DeliveryDate = boOrder.DeliveryDate,
                ShipDate = boOrder.ShipDate,
                OrderDate = boOrder.OrderDate,
            };
            boOrder.Id = dal.Order.Add(newOrder);
            IEnumerable<DO.OrderItem> doOrderItems;
            doOrderItems = from BO.OrderItem itemInOrder in cart.Items
                           select new DO.OrderItem()
                           {
                               OrderId = boOrder.Id,
                               ProductId = itemInOrder.ProductID,
                               Price = itemInOrder.Price,
                               Amount = itemInOrder.Amount,
                           };
            //foreach (var item in doOrderItems)
            //{
            //    dal.OrderItem.Add(item);
            //}
           doOrderItems.Select(x=> dal.OrderItem.Add(x)).ToList();
         //   return boOrder;

            //DO.Order orderToAdd = new DO.Order()//the same as "order".need it for sending to dal
            //{
            //    CustomerAddress = cart.CustomerAddress,
            //    CustomerName = cart.CustomerName,
            //    CustomerEmail = cart.CustomerEmail,
            //    OrderDate = DateTime.Now
            //};
            //order.Id = dal.Order.Add(orderToAdd);//add the id to the new order in BO
            //foreach (var orderItem in cart.Items)//runs on the items in cart
            //{
            //    DO.OrderItem itemInCart = new DO.OrderItem() {ProductId = orderItem.ProductID, OrderId = orderItem.ID,
            //    Price=orderItem.Price,Amount=orderItem.Amount };//builds object of order item
            //    dal.OrderItem.Add(itemInCart);//add to order item to list of order item and put orderItem id 
            //    DO.Product product = dal.Product.GetById(orderItem.ProductID);//if not exist throw exeption
            //    BO.Product? prod = dal.Product.GetAll().FirstOrDefault(item => item.Id== product.Id);//return the first element than has this condition
            //    int x = dal.Product.GetAll().ToList().FindIndex(item => item.Value.Id == orderItem.ProductID);//return the index of the needed product
            //    if (product.AmountInStock > itemInCart.Amount)//if there are enough products in stock
            //    {

                           //        dal.Product.GetAll().ToList().ElementAt(x).AmountInStock -= itemInCart.Amount;
                           //        product.AmountInStock -= itemInCart.Amount;//reduce amount from stock
                           //    }
                           //    else //there's not enough
                           //    {
                           //        throw new Exception("not enough in stock please update your order");///648$$^&&(^^*)
                           //    }
            }
        bool CheckAndUpdate(int productId, int amount)
        {
            if (dal.Product.GetById(productId).AmountInStock >= amount);
            {
                DO.Product prod= dal.Product.GetById(productId);
                prod.AmountInStock-=amount;
                dal.Product.Update(prod);
                return true;
            }
            return false;
        }

    }
}


