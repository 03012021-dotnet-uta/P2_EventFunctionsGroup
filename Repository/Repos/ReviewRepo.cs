using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Repository.Contexts;
using Repository.Interfaces;
using Domain.Models;

namespace Repository.Repos
{
    /// <summary>
    /// This is the Repo class for Reviews
    /// It implements the CRUD functions from its
    /// respective interface.
    /// It also implements Disposal pattern since it
    /// contains unmanaged resources.
    /// Ideas implemented here learned from https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application</summary>
    public class ReviewRepo : IReviewRepository
    {
    
        /// <summary>
        /// Context variable that will be injected and used by business logic
        /// </summary>
        private readonly EventFunctionsContext context;


        /// <summary>
        /// Empty constructor to instantiate the context
        /// and then assign to context variable
        /// </summary>
        public ReviewRepo() 
        {
            context = new EventFunctionsContext();
        }

        /// <summary>
        /// Pass in context using Dependency Injection
        /// and assign to context variable
        /// </summary>
        public ReviewRepo(EventFunctionsContext eventFunctionsContext) 
        {
            context = eventFunctionsContext;
        }

        /// <summary>
        /// Insert a new item to context
        /// </summary>
        public void InsertReview(Review review) 
        {
            context.Reviews.Add(review);
            Save();
        }

        /// <summary>
        /// Get the Reviews from database and present back to context
        /// </summary>
        public ICollection<Review> GetAllReviews() 
        {
            return context.Reviews.ToList();
        }

        /// <summary>
        /// Get an entity by its ReviewId
        /// </summary>
        public List<Review> GetReviewAllByEventId(Guid id)
        {
            return context.Reviews.Include(x => x.User).Include(x => x.Event).Where(x => Guid.Equals(id, x.Event.Id)).ToList();
        }        

        /// <summary>
        /// Update an item in context and database
        /// </summary>
        public void UpdateReview(Review review) 
        {
            context.Entry(review).State = EntityState.Modified;
        }

        /// <summary>
        /// Delete an item from context and database
        /// </summary>
        public void DeleteReview(Guid reviewId)
        {
            Review review = context.Reviews.Find(reviewId);
            context.Entry(review).State = EntityState.Deleted;
            context.Reviews.Remove(review);
        }

        /// <summary>
        /// Save changes back to database
        /// </summary>
        public void Save() 
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Implementing the disposal pattern since
        /// repo/context is unmanaged resource
        /// Interface requires Dispose() and Dipose(bool)
        /// Dispose(bool) to determine if call comes
        /// from a Dispose method or from a finalizer
        /// ref:
        /// https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
        /// Dispose() to inform GC that finalizer does not have to run
        /// </summary>
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                context.Dispose();
            }

            this.disposed = true;
        }

        /// <summary>
        /// Standard implementation to free the actual memory
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            // prevent Garbage Collector from running finalizer
            GC.SuppressFinalize(this);
        }


    }
}
