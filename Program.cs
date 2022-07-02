using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Models;
using Avaliacao3BimLp3.Repository;

var databaseConfig = new DatabaseConfig();
var databaseSetup = new DatabaseSetup(databaseConfig);
var productRepository = new ProductRepository(databaseConfig);

var modelName = args[0];
var modelAction = args[1];

//  --------------------------------------------------------------------------------
// | Nome: Caio Silva Ferrari; Prontuário: SP3044891                                |
//  --------------------------------------------------------------------------------
// | Observacoes:                                                                   |
//  --------------------------------------------------------------------------------
// | is e semelhante ao operador '==' e funciona da mesma maneira,                  |
// | porem e mais perfomatico, pois gera codigo IL e ASM mais compacto,             |
// | realizando menos operacoes. Mais informacoes em:                               |
// | https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/is |
//  --------------------------------------------------------------------------------


if (modelName is "Product")
    switch (modelAction)
    {
        case "New":
            {
                var id = Convert.ToInt32(args[2]);

                if (productRepository.ExistsById(id))
                {
                    Console.WriteLine($"Produto com Id {id} já existe");
                    break;
                }

                var name = args[3];
                var price = Convert.ToDouble(args[4]);
                var active = Convert.ToBoolean(args[5]);

                var product = new Product(id, name, price, active);

                productRepository.Save(product);

                Console.WriteLine($"Produto {product.Name} cadastrado com sucesso");
                break;
            }
        case "Delete":
            {
                var id = Convert.ToInt32(args[2]);

                if (!productRepository.ExistsById(id))
                {
                    Console.WriteLine($"Produto {id} não encontrado");
                    break;
                }

                productRepository.Delete(id);

                Console.WriteLine($"Produto {id} removido com sucesso");
                break;
            }
        case "Enable":
            {
                var id = Convert.ToInt32(args[2]);

                if (!productRepository.ExistsById(id))
                {
                    Console.WriteLine($"Produto {id} não encontrado");
                    break;
                }

                productRepository.Enable(id);

                Console.WriteLine($"Produto {id} habilitado com sucesso");
                break;
            }
        case "Disable":
            {
                var id = Convert.ToInt32(args[2]);

                if (!productRepository.ExistsById(id))
                {
                    Console.WriteLine($"Produto {id} não encontrado");
                    break;
                }

                productRepository.Disable(id);

                Console.WriteLine($"Produto {id} desabilitado com sucesso");
                break;
            }
        case "List":
            {
                var products = productRepository.GetAll();

                if (products is null || products.Count is 0)
                {
                    Console.WriteLine("Nenhum produto cadastrado");
                    break;
                }

                foreach (var product in products)
                    Console.WriteLine($"{product.Id}, {product.Name}, {product.Price}, {product.Active}");

                break;
            }
        case "PriceBetween":
            {
                var initialPrice = Convert.ToDouble(args[2]);
                var endPrice = Convert.ToDouble(args[3]);

                var products = productRepository.GetAllWithPriceBetween(initialPrice, endPrice);

                if (products is null || products.Count is 0)
                {
                    Console.WriteLine($"Nenhum produto encontrado dentro do intervalo de preço R${initialPrice} e R${endPrice}");
                    break;
                }

                foreach (var product in products)
                    Console.WriteLine($"{product.Id}, {product.Name}, {product.Price}, {product.Active}");

                break;
            }
        case "PriceHigherThan":
            {
                var price = Convert.ToDouble(args[2]);

                var products = productRepository.GetAllWithPriceHigherThan(price);

                if (products is null || products.Count is 0)
                {
                    Console.WriteLine($"Nenhum produto encontrado maior que R${price}");
                    break;
                }

                foreach (var product in products)
                    Console.WriteLine($"{product.Id}, {product.Name}, {product.Price}, {product.Active}");

                break;
            }
        case "PriceLowerThan":
            {
                var price = Convert.ToDouble(args[2]);

                var products = productRepository.GetAllWithPriceLowerThan(price);

                if (products is null || products.Count is 0)
                {
                    Console.WriteLine($"Nenhum produto encontrado menor que R${price}");
                    break;
                }

                foreach (var product in products)
                    Console.WriteLine($"{product.Id}, {product.Name}, {product.Price}, {product.Active}");

                break;
            }
        case "AveragePrice":
            {
                var average = productRepository.GetAveragePrice();

                if (average is 0)
                {
                    Console.WriteLine("Nenhum produto cadastrado");
                    break;
                }

                Console.WriteLine($"A media dos preços é R${Math.Round(average, 2)}");

                break;
            }
        default:
            Console.WriteLine("Ação inválida");
            break;
    }
else
    Console.WriteLine("Modelo inválido");
