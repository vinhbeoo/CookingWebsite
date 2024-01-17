using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;
using ProjectLibrary.Repository;
using ProjectWebAPI.App.Code;
using ProjectWebAPI.Application;
using System.ComponentModel.Design;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		private ICommentRepository repository = new CommentRepository(); // Assuming you have a CategoryRepository

        // GET: api/<CommentController> 
        [HttpGet]
        public ActionResult<IEnumerable<Comment>> GetAllComments() => repository.GetAllComments();

        // GET: api/<CommentController> get comment by recipeid
        [HttpGet("CommentByRecipe/{recipeId}")]
		public ActionResult<IEnumerable<Comment>> GetComments(int recipeId) => repository.GetComments(recipeId);

        // GET api/<CommentController>/5
        [HttpGet("{commentId}")]
		public ActionResult<Comment> GetCommentById(int commentId)
		{
			var comment = repository.GetCommentById(commentId);
			if (comment == null)
			{
				return NotFound(); // Return 404 if the category is not found
			}
			return comment;
		}


        // POST api/<CommentController>
        [HttpPost]
		public IActionResult CreateComment([FromBody] CommentDTO commentDTO)
		{
			if (commentDTO == null)
			{
				return BadRequest("Invalid comment data");
			}

			var newComment = new Comment()
			{
				CommentId = commentDTO.CommentId,
				UserId = commentDTO.UserId,
				RecipeId = commentDTO.RecipeId,
				CommentText = commentDTO.CommentText
			};
			// Call the service to add the category to the database
			repository.SaveComment(newComment);

            //Hàm ghi log UserActivity
            LogUserActivity.LogCommentActivity(newComment.UserId, newComment.CommentId, "Create", $"Created a new comment: {newComment.CommentText}");

            // Return a success message or other necessary information
            return Ok("Comment created successfully");
		}

        // PUT api/<CommentController>/5
        [HttpPut("{commentId}")]
		public IActionResult UpdateComment(int commentid, [FromBody] CommentDTO updatedCommentDTO)
		{
			if (updatedCommentDTO == null || commentid != updatedCommentDTO.CommentId)
			{
				return BadRequest("Invalid comment data");
			}

			// Check if the category exists
			var existingComment = repository.GetCommentById(commentid);
			if (existingComment == null)
			{
				return NotFound("Comment not found");
			}

			// Update category information

			existingComment.UserId = updatedCommentDTO.UserId;
			existingComment.RecipeId = updatedCommentDTO.RecipeId;
			existingComment.CommentText = updatedCommentDTO.CommentText;
			existingComment.CreateDate = updatedCommentDTO.CreateDate;
			// Call the service to save changes to the database
			repository.UpdateComment(existingComment);

            //Hàm ghi log UserActivity
            LogUserActivity.LogCommentActivity(existingComment.UserId, existingComment.CommentId, "Update", "Update a comment");

            // Return a success message or other necessary information
            return Ok("Comment updated successfully");
		}


        // DELETE api/<CommentController>/5
        [HttpDelete("{commentId}")]
		public IActionResult DeleteComment(int commentId)
		{
			var comment = repository.GetCommentById(commentId);
			if (comment == null)
			{
				return NotFound();
			}
			repository.DeleteComment(comment);

            //Hàm ghi log UserActivity
            LogUserActivity.LogCommentActivity(comment.UserId, comment.CommentId, "Delete", "Delete a comment");

            return Ok("Comment deleted successfully");
		}

        // DELETE By Recipe
        [HttpDelete("DelByRecId/{recipeId}")]
        public IActionResult DeleteCommentByRecipe(int recipeId)
        {
            var comment = repository.GetCommentByRecId(recipeId);
            if (comment == null)
            {
                return NotFound();
            }
            repository.DeleteComment(comment);

            //Hàm ghi log UserActivity
            LogUserActivity.LogCommentActivity(comment.UserId, comment.CommentId, "Delete", "Delete a comment");

            return Ok("Comment deleted successfully");
        }
    }
}
