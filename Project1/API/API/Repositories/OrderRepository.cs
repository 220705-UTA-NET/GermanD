using System.Data;
using System.Data.SqlClient;
using API.Models;
using Dapper;

namespace API.Repositories;

public class OrderRepository : Repository, IOrderRepository
{
    public OrderRepository(IConfiguration configuration) : base(configuration) // 
    {
    }

    public async Task<Order> CreateAsync(Order order) // 
    {

        await using var connection = new SqlConnection(_connectionString); // create connectionString
        await connection.OpenAsync(); // open connection

        await using var transaction = connection.BeginTransaction(); // begin transaction will rollback if error, will commit if everything is ok
        try
        {
            var newOrderId = await CreateNewOrder(connection, transaction, order); // create new order

            await AddManyOrderItemsToOrder(connection, transaction, order, newOrderId); // add many order items to order

            await transaction.CommitAsync(); // commit transaction

            return await GetByIdAsync(newOrderId); // get order by id
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(); // rollback transaction
            throw;
        }
    }

    public async Task<Order> GetByIdAsync(Guid id) // get order with id
    {
        await using var connection = new SqlConnection(_connectionString); // create connectionString

        var result = await connection.QueryAsync<OrderItem, Item, Order, Order>( 
            @"SELECT *, O.ID as Id, O.CUSTOMER_NAME as CustomerName
FROM [Order] O
    JOIN Order_Item OI 
        ON O.ID = OI.ORDER_ID
    JOIN Item I 
        ON OI.ITEM_ID = I.ID
WHERE O.ID=@id",
            (orderItem, item, order) => // join order item and item
            {
                order.Items.Add( // add item to order
                    new OrderItem // create order item
                    {
                        Item = item, // set item
                        Quantity = orderItem.Quantity, // set quantity
                    }
                );

                return order;
            },
            splitOn: "Id", // split on order id
            param: new { id } // set id
        );

        var order = result.First(); // get first order
        order.Items = result.Select(n => n.Items).Aggregate(new List<OrderItem>(), (acc, n) => 
        {
            acc.AddRange(n); // add order item to list
            return acc; // return list
        });

        return order; 
    }

    private async Task<Guid> CreateNewOrder(IDbConnection connection, IDbTransaction transaction, Order order) 
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection)); // if connection is null
        var newOrderId = Guid.NewGuid(); // create new order id

        await connection.ExecuteAsync(@"INSERT INTO [Order](ID, CUSTOMER_NAME)
        VALUES(@Id, @CustomerName)", new
        {
            Id = newOrderId, 
            CustomerName = order.CustomerName,
        }, transaction);

        return newOrderId;
    }


    private async Task AddManyOrderItemsToOrder( // add many order items to order
        IDbConnection connection, // connection to database
        IDbTransaction transaction, // transaction to execute
        Order order, // order to add items to
        Guid newOrderId // new order id
    )
    {
        await connection.ExecuteAsync(@"INSERT INTO [Order_Item](ORDER_ID, ITEM_ID, QUANTITY)
            VALUES(@OrderId, @ItemId, @Quantity)",
            order.Items.Select(n => new { Quantity = n.Quantity, ItemId = n.Item.Id, OrderId = newOrderId }),
            transaction);
    }
}