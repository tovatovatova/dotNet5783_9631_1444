

namespace DO;

/// <summary>
/// Structure for products Order details 
/// </summary>
public struct Order
{
    /// <summary>
    ///  Unique identification number of each products order
    /// </summary>
    public int OrderId { get; set; }
    /// <summary>
    /// Name of the order's customer
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// Email address of the order's customer
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    /// Order creation Date
    /// </summary>
    public DateTime? OrderDate { get; set; }
    /// <summary>
    /// Delivery order departure date
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// Order arrival date
    /// </summary>
    public DateTime? DeliveryDate { get; set; }
    /// <summary>
    /// return a string for order's details
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
        order ID:{OrderId}
        customer name:{CustomerName}
        customer email:{CustomerEmail}
        date order:{OrderDate}
        ship date:{ShipDate}


    ";



}

