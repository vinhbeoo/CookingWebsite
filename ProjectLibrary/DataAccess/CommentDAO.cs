﻿using Microsoft.EntityFrameworkCore;
using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectLibrary.DataAccess
{
	public class CommentDAO
	{
		private static CommentDAO instance = null;
		private static readonly object instanceLock = new object();

		public static CommentDAO Instance
		{
			get
			{
				lock (instanceLock)
				{
					if (instance == null)
					{
						instance = new CommentDAO();
					}
					return instance;
				}
			}
		}

		// Get all comment 
		public List<Comment> GetAllComments()
		{
			var comments = new List<Comment>();
			try
			{
				using (var context = new CookingWebsiteContext())
				{
					comments = context.Comments.ToList();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error retrieving Comments list: " + ex.Message);
			}
			return comments;
		}

		// Get comment theo id recipe
		public List<Comment> GetComments(int recipeId)
		{
			var comments = new List<Comment>();
			try
			{
				using (var context = new CookingWebsiteContext())
				{
					comments = context.Comments
						.Where(c => c.RecipeId == recipeId)
						.Include(s => s.User)
						.OrderByDescending(c => c.CommentId)
						.ToList();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error retrieving Comments list: " + ex.Message);
			}
			return comments;
		}

		// get comment theo id comment
		public Comment FindCommentById(int commentid)
		{
			Comment comment = new Comment();
			try
			{
				using (var context = new CookingWebsiteContext())
				{
					comment = context.Comments.FirstOrDefault(x => x.CommentId == commentid);
				}
				if (comment == null)
				{
					throw new Exception("Comment not found");
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return comment;
		}

		// insert comment 
		public void SaveComment(Comment comment)
		{
			try
			{
				using (var context = new CookingWebsiteContext())
				{
					// Check for duplicates before adding new
					var existingComment = context.Comments.FirstOrDefault(x => x.CommentId == comment.CommentId);
					if (existingComment != null)
					{
						throw new Exception("Comment already exists");
					}

					context.Comments.Add(comment);
					context.SaveChanges();

				}
			}
			catch (DbUpdateException ex)
			{
				// Handle specific database update exception
				throw new Exception("Error saving changes to the database. See inner exception for details.", ex);
			}
			catch (Exception ex)
			{
				// Handle other exceptions
				throw new Exception("An error occurred while saving the entity changes. See the inner exception for details.", ex);
			}
		}

		//Update Comment
		public void UpdateComment(Comment comment)
		{
			try
			{
				using (var context = new CookingWebsiteContext())
				{
					var existingComment = context.Comments.FirstOrDefault(x => x.CommentId == comment.CommentId);

					if (existingComment != null)
					{
						context.Entry(existingComment).CurrentValues.SetValues(comment);
						context.SaveChanges();
						/*// Log user activity
                        context.LogUserActivity(existingComment.UserId, "UpdateComment", $"Updated comment:  {comment.CommentText}");*/
					}
					else
					{
						throw new Exception("Comment not found");
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public void DeleteComment(Comment comment)
		{
			try
			{
				using (var context = new CookingWebsiteContext())
				{
					var commentToDelete = context.Comments.FirstOrDefault(x => x.CommentId == comment.CommentId);
					if (commentToDelete == null)
					{
						throw new Exception("comment not found");
					}
					else
					{
						context.Comments.Remove(commentToDelete);
						context.SaveChanges();
						/*// Log user activity
                        context.LogUserActivity(commentToDelete.UserId, "DeleteComment", $"Deleted comment:  {comment.CommentText}");*/
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

	}
}
