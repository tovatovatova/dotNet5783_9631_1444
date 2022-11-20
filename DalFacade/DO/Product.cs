

namespace DO;
/// <summary>
/// Structure for detail of product in the store
/// </summary>
public struct Product
{
    /// <summary>
    ///Unique identification number for product in store
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Name of product
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Price of the product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// Boolean variable with value true if the product is avaliable to order and false else
    /// </summary>
    public int AmountInStock  { get; set; }
    /// <summary>
    /// Product Category from the categoies in the enum
    /// </summary>
    public Category ProductCategoty { get; set; }

    /// <summary>
    /// return a string for product's details
    /// </summary>
    /// <returns></returns>
        public override string ToString() => this.ToStringProperty();

   
}

