using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;







namespace LaTiendita
{
    class Program
    {
       static List<string> names = new List<string>();
       static List<double> prices = new List<double>();
       static List<int> stock = new List<int>();

       static string archive = "inventario.txt";
       static void Main(string[] args)
        {
            Loadarchive();
            Showmenu();
        }

        static void Showmenu()  // funcion para vizualizar menu 
        {
            int option = 0;
            while (option != 6)
            {
                Console.Clear();
                Console.WriteLine("La Tiendita ");
                Console.WriteLine("\n");
                Console.WriteLine("1- Añadir Producto ");
                Console.WriteLine("2- Eliminar Producto ");
                Console.WriteLine("3- Actualizar Stock ");
                Console.WriteLine("4- Lista de productos ");
                Console.WriteLine("5- Generar Factura ");
                Console.WriteLine("6- Salir\n");
                
                
                if(int.TryParse(Console.ReadLine(), out option))
                {
                    switch (option)
                    {
                        case 1: Addproduct(); break;
                        case 2: Deleteproduct(); break;
                        case 3: Updatestock(); break;
                        case 4: Inventory(); break;
                        case 5: Receipt(); break;
                        case 6: Savearchive(); break;
                        
                        
                    }
                }
                
            }

        } 

        static void Addproduct() // funcion para añadir productos
        {
            Console.Clear();
            Console.Write("Nombre del producto.\n");
            string name = Console.ReadLine().Trim().ToUpper();

            if (names.Contains(name))
            {
                Console.WriteLine("Este Producto Ya Existe");
            }
            else
            {
                Console.Write("Precio $RD:\n");
                double.TryParse(Console.ReadLine(), out double price);
                Console.Write("Stock Inicial:\n");
                int.TryParse(Console.ReadLine(), out int item);

                names.Add(name);
                prices.Add(price);
                stock.Add(item);

                Savearchive();
                Console.WriteLine("Producto Añadido.");
            }
            
            Console.ReadLine();
        }

       static void Deleteproduct() //funcion para eliminar productos
        {
            Console.Clear();
            Console.Write("Nombre del producto que desea eliminar.\n");
            string search = Console.ReadLine().ToUpper();
            int index = names.IndexOf(search);

            if(index != -1)
            {
                names.RemoveAt(index);
                prices.RemoveAt(index);
                stock.RemoveAt(index);

                Savearchive();
                Console.WriteLine ("Eliminado correctamente.");
            }
            else
            {
                Console.WriteLine("No encontrado.");
            }
            Console.ReadLine();
        }

        static void Updatestock()  // funcion para actualizar productos 
        {
            Console.Clear();
            Console.Write("que producto desea actualizar? ");
            string search = Console.ReadLine().ToUpper();
            int index = names.IndexOf(search);

            if(index != -1)
            {
                Console.Write($"Stock actuales: {stock[index]}.\n ingrese la cantidad a añadir o remover: ");
                int.TryParse(Console.ReadLine(), out int adjust);
                stock[index] += adjust;
                Savearchive();
                Console.WriteLine("Stock Actualizado. ");
        
            }
            else
            {
                Console.WriteLine("Producto no encontrado");
            }
            Console.ReadLine();
        }

        static void Inventory() // funcion para vizualizar productos del inventario
        {
            Console.Clear();
            Console.WriteLine("Lista de productos");
            Console.WriteLine("{0,-20} {1,-10} {2,-10}","Producto","Precio","Stock");

            for(int i= 0; i< names.Count; i++)
            {
                Console.WriteLine("{0,-20} ${1,-9:F2} {2, -10}", names[i], prices[i], stock[i]);
            }

            Console.WriteLine("Presiona cualquier tecla para volver. ");
            Console.ReadLine();
                
            
        }

         static void Receipt() // funcion para generar una factura
        {
            Console.Clear();
            double total = 0;
            string list = "";
            
            while (true)
            {
                Console.Write("Producto a Facturar (o'FIN'):\n");
                string prod = Console.ReadLine().ToUpper();
                if (prod =="FIN") break; 

                int index = names.IndexOf(prod);
                if (index != -1 && stock[index] > 0)
                {
                    Console.Write("Cantidad: ");
                    int.TryParse(Console.ReadLine(), out int amount);

                    if (amount<= stock[index])
                    {
                        stock[index] -= amount;
                        double subtotal = amount * prices[index];
                        total += subtotal;
                        list += $"{amount} x {names[index]} - ${subtotal:F2} \n";
                        Console.WriteLine("Añadido. ");
                    }
                    else Console.WriteLine("Stock insuficiente.");
                }
                else Console.WriteLine("Product no disponible");
                
            }
            if (total > 0)
            {
                Console.WriteLine("\n Factura Final ");
                Console.WriteLine(list);
                Console.WriteLine($"TOTAL: ${total:F2}");
                Savearchive();
            }
            Console.ReadLine();
        }

        static void Savearchive()   // funcion para guardar datos en un archivo .txt
        {
            List<string> lines = new List<string>();
            for(int i = 0; i < names.Count; i++)
            {
                lines.Add($"{names[i]},{prices[i]},{stock[i]}");
            }
            File.WriteAllLines(archive, lines);
            Console.WriteLine("Datos Guardados");
        }
        
        static void Loadarchive()  // funcion para cargar datos anteriores desde el archivo .txt
        {
            if (File.Exists(archive))
            {
                string[]lines = File.ReadAllLines(archive);
                foreach (string linea in lines)
                {
                    string[] parts = linea.Split(',');
                    if (parts.Length== 3)
                    {
                        names.Add(parts[0]);
                        prices.Add(double.Parse(parts[1]));
                        stock.Add(int.Parse(parts[2]));
                    }
                }
            }
        }

        
        


    }
        
    }

