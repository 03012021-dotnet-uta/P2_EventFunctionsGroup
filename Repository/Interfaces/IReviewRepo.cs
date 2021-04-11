using System;
using System.Collections.Generic;
using Domain.Models;

namespace Repository.Interfaces 
{
  
    /// <summary>
    /// This is the Interface for the Review Repository.
    /// It contains the methods that the Repo must
    /// implement, which are based on CRUD operations.
    /// Each main class of the programm will have its own
    /// interface and respective repo class.
    /// These will implement the IDisposable interface
    /// since using database context is unmanaged.
    /// Ideas implemented here learned from https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
    /// </summary>
    public interface IReviewRepository : IDisposable
    {

        /// <summary>
        /// Create Review
        /// </summary>
        void InsertReview(Review review);

        /// <summary>
        /// Read all Reviews
        /// </summary>
        ICollection<Review> GetAllReviews();

        /// <summary>
        /// Read an entity by their ReviewId
        /// </summary>
        List<Review> GetReviewAllByEventId(Guid reviewId);

        /// <summary>
        /// Update a Review
        /// </summary>
        void UpdateReview(Review review);

        /// <summary>
        /// Delete a Review
        /// </summary>
        void DeleteReview(int reviewId);

        /// <summary>
        /// Save changes
        /// </summary>
        void Save();

    }
}