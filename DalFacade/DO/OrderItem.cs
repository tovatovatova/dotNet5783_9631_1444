

namespace DO;

public struct OrderItem
{
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


