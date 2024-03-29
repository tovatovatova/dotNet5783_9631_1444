﻿

namespace DO;
/// <summary>
/// structure for details of one item in an order
/// </summary>
public struct OrderItem
{

    /// <summary>
    /// identificatior number for item in order,running number
    /// </summary>
    public int OrderItemId { get; set; }
    /// <summary>
    /// Product's identification number
    /// </summary>
    public int ProductId { get; set; }
    /// <summary>
    /// Order identification number
    /// </summary>
    public int OrderId { get; set; }
    /// <summary>
    /// Product price
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// Quantity of this item in order
    /// </summary>
    public int Amount { get; set; }
    public string? ImagesSource { get; set; }


    /// <summary>
    /// return a string for order item's details
    /// </summary>
    /// <returns></returns>
    public override string ToString() => this.ToStringProperty();



}


