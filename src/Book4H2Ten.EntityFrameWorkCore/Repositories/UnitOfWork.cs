using Book4H2Ten.Core.Errors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.EntityFrameWorkCore.Repositories
{
    public interface IUnitOfWork<out TContext> where TContext : DbContext
    {
        TContext Context { get; }
        //Start the database Transaction
        Task CreateTransaction();
        //Commit the database Transaction
        Task CommitAsync();
        //Rollback the database Transaction
        Task RollbackAsync();
        //DbContext Class SaveChanges method
        Task SaveChangesAsync();
    }

    //Generic UnitOfWork Class. 
    //While Creating an Instance of the UnitOfWork object, we need to specify the actual type for the TContext Generic Type
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        public TContext Context { get; }

        //Using the Constructor we are initializing the Context Property which is declared in the IUnitOfWork Interface
        //This is nothing but we are storing the DBContext object in Context Property
        public UnitOfWork(TContext context)
        {
            Context = context;
        }

        public async Task CreateTransaction()
        {
            //Commits the underlying store transaction
            await Context.Database.BeginTransactionAsync();
        }

        //If all the Transactions are completed successfully then we need to call this Commit() 
        //method to Save the changes permanently in the database
        public async Task CommitAsync()
        {
            //Commits the underlying store transaction
            await Context.Database.CommitTransactionAsync();
        }

        //If at least one of the Transaction is Failed then we need to call this Rollback() 
        //method to Rollback the database changes to its previous state
        public async Task RollbackAsync()
        {
            //Rolls back the underlying store transaction
            await Context.Database.RollbackTransactionAsync();
            //The Dispose Method will clean up this transaction object and ensures Entity Framework
            //is no longer using that transaction.
            await Context.DisposeAsync();
        }

        //The SaveChangesAsync() Method Implement DbContext Class SaveChanges method 
        //So whenever we do a transaction we need to call this SaveChangesAsync() method 
        //so that it will make the changes in the database permanently
        public async Task SaveChangesAsync()
        {
            try
            {
                //Calling DbContext Class SaveChanges method 
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {

                throw new InternalServerErrorException(dbEx.Message);
            }
        }
    }
}
