using BlApi;
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
                    currentCart.Items = currentCart.Items.Where(item => item.ProductID != productId);
                    currentCart.TotalPrice += product.Price * amountDifference;
                }
                else if (amountDifference == 0)
                    return currentCart;
                else //the new amount bigger than the old one or little than it
                {
                    if (product.AmountInStock >= newAmount)//if there is enough items in stock
                    {
                        currentCart.Items.ToList().ElementAt(x).Amount = newAmount;
                        currentCart.Items.ToList().ElementAt(x).Price = product.Price;
                        currentCart.Items.ToList().ElementAt(x).TotalPrice += product.Price * amountDifference;//add the exstra price after the change
                        currentCart.TotalPrice += amountDifference * product.Price; //add to the cart total price
                    }
                    else throw new Exception("not enough in stock");
                }
            }
            else
            {
                if (newAmount != 0)
                {
                    if (product.AmountInStock >= newAmount)
                    {
                        BO.OrderItem itemCart = new BO.OrderItem()
                        {
                            ID = productId,
                            Name = product.Name,
                            ProductID = product.Id,
                            Price = product.Price,
                            Amount = newAmount,
                            TotalPrice = product.Price * newAmount
                        };
                        currentCart.Items = currentCart.Items.Append(itemCart);
                        currentCart.TotalPrice=product.Price* newAmount;    
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
            boOrder.Items = cart.Items.Where(item => dal.Product.GetById(item.ProductID).AmountInStock >= item.Amount);
           
            if (boOrder.Items.Count() != cart.Items.Count())
                throw new Exception("you need to update your cart!");

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
            doOrderItems.Select(x => dal.OrderItem.Add(x)).ToList();
            boOrder.Items.ToList().ForEach(item => UpdateAmount(item.ProductID, item.Amount));
        }
        bool Check(int productId, int amount)
        {
            if (dal.Product.GetById(productId).AmountInStock >= amount)

                return true;

            return false;
        }
        void UpdateAmount(int productId, int amount)
        {
            DO.Product prod = dal.Product.GetById(productId);
            prod.AmountInStock -= amount;
            dal.Product.Update(prod);
        }
    }
}


