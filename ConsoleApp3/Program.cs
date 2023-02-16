using System;
using System.Collections.Generic;

class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
}

class ShoppingCartItem
{
    public Product Item { get; set; }
    public int Quantity { get; set; }
}

class ShoppingCart
{
    private List<ShoppingCartItem> items = new List<ShoppingCartItem>();

    public void AddItem(Product item, int quantity)
    {
        // Check if the item already exists in the cart
        ShoppingCartItem existingItem = items.Find(x => x.Item.Name == item.Name);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            // Add the new item to the cart
            items.Add(new ShoppingCartItem { Item = item, Quantity = quantity });
        }
    }

    public void RemoveItem(Product item)
    {
        ShoppingCartItem existingItem = items.Find(x => x.Item.Name == item.Name);
        if (existingItem != null)
        {
            if (existingItem.Quantity > 1)
            {
                existingItem.Quantity -= 1;
            }
            else
            {
                items.Remove(existingItem);
            }
        }
    }

    public double CalculateTotal()
    {
        double total = 0;
        foreach (ShoppingCartItem item in items)
        {
            total += item.Item.Price * item.Quantity;
        }
        return total;
    }

    public void DisplayCart()
    {
        Console.WriteLine("Shopping Cart:");
        foreach (ShoppingCartItem item in items)
        {
            Console.WriteLine("- {0} ({1} x ${2})", item.Item.Name, item.Quantity, item.Item.Price);
        }
        Console.WriteLine("Total: ${0}", CalculateTotal());
    }
}

class Program
{
    static void Main(string[] args)
    {
        Dictionary<string, Product> library = new Dictionary<string, Product>();

        while (true)
        {
            Console.WriteLine("Add a new item to the library? (yes/no)");
            string addNewItem = Console.ReadLine();

            if (addNewItem.ToLower() == "yes")
            {
                Console.WriteLine("Enter the name of the item:");
                string name = Console.ReadLine();

                Console.WriteLine("Enter the price of the item:");
                double price = Convert.ToDouble(Console.ReadLine());

                Product newItem = new Product { Name = name, Price = price };

                if (!library.ContainsKey(name))
                {
                    library.Add(name, newItem);
                    Console.WriteLine("{0} added to the library.", name);
                }
                else
                {
                    Console.WriteLine("{0} already exists in the library.", name);
                }

                Console.WriteLine("Add {0} to the cart? (yes/no)", name);
                string addToCart = Console.ReadLine();

                if (addToCart.ToLower() == "yes")
                {
                    Console.WriteLine("Enter the quantity:");
                    int quantity = Convert.ToInt32(Console.ReadLine());

                    ShoppingCart cart = new ShoppingCart();
                    cart.AddItem(newItem, quantity);
                    Console.WriteLine("{0} x {1} added to the cart.", quantity, name);
                }
            }
            else
            {
                break;
            }
        }

        Console.WriteLine();
        Console.WriteLine("Current library:");
        foreach (KeyValuePair<string, Product> item in library)
        {
            Console.WriteLine("- {0} (${1})", item.Key, item.Value.Price);
        }

        Console.WriteLine();
        Console.WriteLine("Current cart:");
        ShoppingCart currentCart = new ShoppingCart();
        while (true)
        {
            Console.WriteLine("Add an item to the cart? (yes/no)");
            string addItem = Console.ReadLine();

            if (addItem.ToLower() == "yes")
            {
                Console.WriteLine("Enter the name of the item:");
                string name = Console.ReadLine();

                if (library.ContainsKey(name))
                {
                    Console.WriteLine("Enter the quantity:");
                    int quantity = Convert.ToInt32(Console.ReadLine());

                    currentCart.AddItem(library[name], quantity);
                    Console.WriteLine("{0} x {1} added to the cart.", quantity, name);
                }
                else
                {
                    Console.WriteLine("{0} does not exist in the library.", name);
                }
            }
            else
            {
                break;
            }
        }

        Console.WriteLine();
        currentCart.DisplayCart();
    }
}
          
