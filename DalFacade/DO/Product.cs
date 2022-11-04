

namespace DO;
/// <summary>
/// structure for product
/// </summary>
public struct Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public bool InStock { get; set; }
    /// <summary>
    /// return a string for product's details
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
        product ID:{Id}
        product name:{Name}
        price of product:{Price}
        whether in stock:{InStock}
    ";
}

