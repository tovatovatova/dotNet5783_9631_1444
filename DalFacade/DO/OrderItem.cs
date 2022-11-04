

namespace DO;
/// <summary>
/// structure for details of one item in an order
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// Product's identification number
    /// </summary>
    public int ProductId { get; set; }
    /// <summary>
    /// Order identification number
    /// </summary>
    public int OrderId { get; set; }
    /// <summary>
    ///Price of the Item in Order
    /// </summary>
    public double price { get; set; }
    /// <summary>
    /// The quantity of this item in the order
    /// </summary>
    public int amount { get; set; }


}


