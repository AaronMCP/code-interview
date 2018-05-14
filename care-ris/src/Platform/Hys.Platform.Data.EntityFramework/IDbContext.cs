#region

using System.Data.Entity;
using Hys.Platform.Domain;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity.Infrastructure;
using System;
using System.Data.Common;

#endregion

namespace Hys.Platform.Data.EntityFramework
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : Entity;
        void SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        //
        // Summary:
        //     Executes the given DDL/DML command against the database.  As with any API
        //     that accepts SQL it is important to parameterize any user input to protect
        //     against a SQL injection attack. You can include parameter place holders in
        //     the SQL query string and then supply parameter values as additional arguments.
        //     Any parameter values you supply will automatically be converted to a DbParameter.
        //      context.Database.ExecuteSqlCommand("UPDATE dbo.Posts SET Rating = 5 WHERE
        //     Author = @p0", userSuppliedAuthor); Alternatively, you can also construct
        //     a DbParameter and supply it to SqlQuery. This allows you to use named parameters
        //     in the SQL query string.  context.Database.ExecuteSqlCommand("UPDATE dbo.Posts
        //     SET Rating = 5 WHERE Author = @author", new SqlParameter("@author", userSuppliedAuthor));
        //
        // Parameters:
        //   sql:
        //     The command string.
        //
        //   parameters:
        //     The parameters to apply to the command string.
        //
        // Returns:
        //     The result returned by the database after executing the command.
        //
        // Remarks:
        //     If there isn't an existing local or ambient transaction a new transaction
        //     will be used to execute the command.
        int ExecuteSqlCommand(string sql, params object[] parameters);
        //
        // Summary:
        //     Executes the given DDL/DML command against the database.  As with any API
        //     that accepts SQL it is important to parameterize any user input to protect
        //     against a SQL injection attack. You can include parameter place holders in
        //     the SQL query string and then supply parameter values as additional arguments.
        //     Any parameter values you supply will automatically be converted to a DbParameter.
        //      context.Database.ExecuteSqlCommand("UPDATE dbo.Posts SET Rating = 5 WHERE
        //     Author = @p0", userSuppliedAuthor); Alternatively, you can also construct
        //     a DbParameter and supply it to SqlQuery. This allows you to use named parameters
        //     in the SQL query string.  context.Database.ExecuteSqlCommand("UPDATE dbo.Posts
        //     SET Rating = 5 WHERE Author = @author", new SqlParameter("@author", userSuppliedAuthor));
        //
        // Parameters:
        //   transactionalBehavior:
        //     Controls the creation of a transaction for this command.
        //
        //   sql:
        //     The command string.
        //
        //   parameters:
        //     The parameters to apply to the command string.
        //
        // Returns:
        //     The result returned by the database after executing the command.
        int ExecuteSqlCommand(TransactionalBehavior transactionalBehavior, string sql, params object[] parameters);
        //
        // Summary:
        //     Asynchronously executes the given DDL/DML command against the database. 
        //     As with any API that accepts SQL it is important to parameterize any user
        //     input to protect against a SQL injection attack. You can include parameter
        //     place holders in the SQL query string and then supply parameter values as
        //     additional arguments. Any parameter values you supply will automatically
        //     be converted to a DbParameter.  context.Database.ExecuteSqlCommandAsync("UPDATE
        //     dbo.Posts SET Rating = 5 WHERE Author = @p0", userSuppliedAuthor); Alternatively,
        //     you can also construct a DbParameter and supply it to SqlQuery. This allows
        //     you to use named parameters in the SQL query string.  context.Database.ExecuteSqlCommandAsync("UPDATE
        //     dbo.Posts SET Rating = 5 WHERE Author = @author", new SqlParameter("@author",
        //     userSuppliedAuthor));
        //
        // Parameters:
        //   sql:
        //     The command string.
        //
        //   parameters:
        //     The parameters to apply to the command string.
        //
        // Returns:
        //     A task that represents the asynchronous operation.  The task result contains
        //     the result returned by the database after executing the command.
        //
        // Remarks:
        //     Multiple active operations on the same context instance are not supported.
        //     Use 'await' to ensure that any asynchronous operations have completed before
        //     calling another method on this context.  If there isn't an existing local
        //     transaction a new transaction will be used to execute the command.
        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);
        //
        // Summary:
        //     Asynchronously executes the given DDL/DML command against the database. 
        //     As with any API that accepts SQL it is important to parameterize any user
        //     input to protect against a SQL injection attack. You can include parameter
        //     place holders in the SQL query string and then supply parameter values as
        //     additional arguments. Any parameter values you supply will automatically
        //     be converted to a DbParameter.  context.Database.ExecuteSqlCommandAsync("UPDATE
        //     dbo.Posts SET Rating = 5 WHERE Author = @p0", userSuppliedAuthor); Alternatively,
        //     you can also construct a DbParameter and supply it to SqlQuery. This allows
        //     you to use named parameters in the SQL query string.  context.Database.ExecuteSqlCommandAsync("UPDATE
        //     dbo.Posts SET Rating = 5 WHERE Author = @author", new SqlParameter("@author",
        //     userSuppliedAuthor));
        //
        // Parameters:
        //   sql:
        //     The command string.
        //
        //   cancellationToken:
        //     A System.Threading.CancellationToken to observe while waiting for the task
        //     to complete.
        //
        //   parameters:
        //     The parameters to apply to the command string.
        //
        // Returns:
        //     A task that represents the asynchronous operation.  The task result contains
        //     the result returned by the database after executing the command.
        //
        // Remarks:
        //     Multiple active operations on the same context instance are not supported.
        //     Use 'await' to ensure that any asynchronous operations have completed before
        //     calling another method on this context.  If there isn't an existing local
        //     transaction a new transaction will be used to execute the command.
        Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken, params object[] parameters);
        //
        // Summary:
        //     Asynchronously executes the given DDL/DML command against the database. 
        //     As with any API that accepts SQL it is important to parameterize any user
        //     input to protect against a SQL injection attack. You can include parameter
        //     place holders in the SQL query string and then supply parameter values as
        //     additional arguments. Any parameter values you supply will automatically
        //     be converted to a DbParameter.  context.Database.ExecuteSqlCommandAsync("UPDATE
        //     dbo.Posts SET Rating = 5 WHERE Author = @p0", userSuppliedAuthor); Alternatively,
        //     you can also construct a DbParameter and supply it to SqlQuery. This allows
        //     you to use named parameters in the SQL query string.  context.Database.ExecuteSqlCommandAsync("UPDATE
        //     dbo.Posts SET Rating = 5 WHERE Author = @author", new SqlParameter("@author",
        //     userSuppliedAuthor));
        //
        // Parameters:
        //   transactionalBehavior:
        //     Controls the creation of a transaction for this command.
        //
        //   sql:
        //     The command string.
        //
        //   parameters:
        //     The parameters to apply to the command string.
        //
        // Returns:
        //     A task that represents the asynchronous operation.  The task result contains
        //     the result returned by the database after executing the command.
        //
        // Remarks:
        //     Multiple active operations on the same context instance are not supported.
        //     Use 'await' to ensure that any asynchronous operations have completed before
        //     calling another method on this context.
        Task<int> ExecuteSqlCommandAsync(TransactionalBehavior transactionalBehavior, string sql, params object[] parameters);
        //
        // Summary:
        //     Asynchronously executes the given DDL/DML command against the database. 
        //     As with any API that accepts SQL it is important to parameterize any user
        //     input to protect against a SQL injection attack. You can include parameter
        //     place holders in the SQL query string and then supply parameter values as
        //     additional arguments. Any parameter values you supply will automatically
        //     be converted to a DbParameter.  context.Database.ExecuteSqlCommandAsync("UPDATE
        //     dbo.Posts SET Rating = 5 WHERE Author = @p0", userSuppliedAuthor); Alternatively,
        //     you can also construct a DbParameter and supply it to SqlQuery. This allows
        //     you to use named parameters in the SQL query string.  context.Database.ExecuteSqlCommandAsync("UPDATE
        //     dbo.Posts SET Rating = 5 WHERE Author = @author", new SqlParameter("@author",
        //     userSuppliedAuthor));
        //
        // Parameters:
        //   transactionalBehavior:
        //     Controls the creation of a transaction for this command.
        //
        //   sql:
        //     The command string.
        //
        //   cancellationToken:
        //     A System.Threading.CancellationToken to observe while waiting for the task
        //     to complete.
        //
        //   parameters:
        //     The parameters to apply to the command string.
        //
        // Returns:
        //     A task that represents the asynchronous operation.  The task result contains
        //     the result returned by the database after executing the command.
        //
        // Remarks:
        //     Multiple active operations on the same context instance are not supported.
        //     Use 'await' to ensure that any asynchronous operations have completed before
        //     calling another method on this context.
        Task<int> ExecuteSqlCommandAsync(TransactionalBehavior transactionalBehavior, string sql, CancellationToken cancellationToken, params object[] parameters);

        //
        // Summary:
        //     Creates a raw SQL query that will return elements of the given generic type.
        //      The type can be any type that has properties that match the names of the
        //     columns returned from the query, or can be a simple primitive type. The type
        //     does not have to be an entity type. The results of this query are never tracked
        //     by the context even if the type of object returned is an entity type. Use
        //     the System.Data.Entity.DbSet<TEntity>.SqlQuery(System.String,System.Object[])
        //     method to return entities that are tracked by the context.  As with any API
        //     that accepts SQL it is important to parameterize any user input to protect
        //     against a SQL injection attack. You can include parameter place holders in
        //     the SQL query string and then supply parameter values as additional arguments.
        //     Any parameter values you supply will automatically be converted to a DbParameter.
        //      context.Database.SqlQuery<Post>("SELECT * FROM dbo.Posts WHERE Author =
        //     @p0", userSuppliedAuthor); Alternatively, you can also construct a DbParameter
        //     and supply it to SqlQuery. This allows you to use named parameters in the
        //     SQL query string.  context.Database.SqlQuery<Post>("SELECT * FROM dbo.Posts
        //     WHERE Author = @author", new SqlParameter("@author", userSuppliedAuthor));
        //
        // Parameters:
        //   sql:
        //     The SQL query string.
        //
        //   parameters:
        //     The parameters to apply to the SQL query string. If output parameters are
        //     used, their values will not be available until the results have been read
        //     completely. This is due to the underlying behavior of DbDataReader, see http://go.microsoft.com/fwlink/?LinkID=398589
        //     for more details.
        //
        // Type parameters:
        //   TElement:
        //     The type of object returned by the query.
        //
        // Returns:
        //     A System.Data.Entity.Infrastructure.DbRawSqlQuery<TElement> object that will
        //     execute the query when it is enumerated.
        DbRawSqlQuery<TElement> SqlQuery<TElement>(string sql, params object[] parameters);
        //
        // Summary:
        //     Creates a raw SQL query that will return elements of the given type.  The
        //     type can be any type that has properties that match the names of the columns
        //     returned from the query, or can be a simple primitive type. The type does
        //     not have to be an entity type. The results of this query are never tracked
        //     by the context even if the type of object returned is an entity type. Use
        //     the System.Data.Entity.DbSet.SqlQuery(System.String,System.Object[]) method
        //     to return entities that are tracked by the context.  As with any API that
        //     accepts SQL it is important to parameterize any user input to protect against
        //     a SQL injection attack. You can include parameter place holders in the SQL
        //     query string and then supply parameter values as additional arguments. Any
        //     parameter values you supply will automatically be converted to a DbParameter.
        //      context.Database.SqlQuery(typeof(Post), "SELECT * FROM dbo.Posts WHERE Author
        //     = @p0", userSuppliedAuthor); Alternatively, you can also construct a DbParameter
        //     and supply it to SqlQuery. This allows you to use named parameters in the
        //     SQL query string.  context.Database.SqlQuery(typeof(Post), "SELECT * FROM
        //     dbo.Posts WHERE Author = @author", new SqlParameter("@author", userSuppliedAuthor));
        //
        // Parameters:
        //   elementType:
        //     The type of object returned by the query.
        //
        //   sql:
        //     The SQL query string.
        //
        //   parameters:
        //     The parameters to apply to the SQL query string. If output parameters are
        //     used, their values will not be available until the results have been read
        //     completely. This is due to the underlying behavior of DbDataReader, see http://go.microsoft.com/fwlink/?LinkID=398589
        //     for more details.
        //
        // Returns:
        //     A System.Data.Entity.Infrastructure.DbRawSqlQuery object that will execute
        //     the query when it is enumerated.
        DbRawSqlQuery SqlQuery(Type elementType, string sql, params object[] parameters);
        //
        // Summary:
        //     Enables the user to pass in a database transaction created outside of the
        //     System.Data.Entity.Database object if you want the Entity Framework to execute
        //     commands within that external transaction.  Alternatively, pass in null to
        //     clear the framework's knowledge of that transaction.
        //
        // Parameters:
        //   transaction:
        //     the external transaction
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     Thrown if the transaction is already completed
        //
        //   System.InvalidOperationException:
        //     Thrown if the connection associated with the System.Data.Entity.Database
        //     object is already enlisted in a System.Transactions.TransactionScope transaction
        //
        //   System.InvalidOperationException:
        //     Thrown if the connection associated with the System.Data.Entity.Database
        //     object is already participating in a transaction
        //
        //   System.InvalidOperationException:
        //     Thrown if the connection associated with the transaction does not match the
        //     Entity Framework's connection
        void UseTransaction(DbTransaction transaction);
    }
}