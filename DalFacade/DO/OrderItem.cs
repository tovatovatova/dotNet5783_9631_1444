

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
    public double price { get; set; }
    public int amount { get; set; }


}


