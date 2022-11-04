

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
    public double Price { get; set; }
    public int Amount { get; set; }
    /// <summary>
    /// return a string for order item's details
    /// </summary>
    /// <returns></returns>
 public override string ToString() => $@"
            product ID:{ProductId}
            order ID:{OrderId}
            price of item:{Price}
            amount for order:{Amount}
";


}


