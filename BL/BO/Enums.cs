﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// enum of product category
    /// </summary>
    public enum Category { Cakes, Donats, GiftBoxes, Desserts, Specials }
    /// <summary>
    /// enum of stages in order status
    /// </summary>
    public enum OrderStatus {Ordered,Shipped,Delivered }
    public enum UpdateOrder { Address, AmountOfItem, DeleteItem }
    public enum LogIn { Customer, Maneger };

}


