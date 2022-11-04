

namespace DO;
/// <summary>
/// structure for details of one item in an order
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// 
    /// </summary>
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public override string ToString() => $@"
            product ID:{ProductId}
            order ID:{OrderId}
            price of item:{Price}
            amount for order:{Amount}
";


}


