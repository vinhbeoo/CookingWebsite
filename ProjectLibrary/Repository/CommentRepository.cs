using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public class CommentRepository : ICommentRepository
    {
        public List<Comment> GetComments(int recipeId) => CommentDAO.Instance.GetComments(recipeId);
        public void SaveComment(Comment comment, int userId) => CommentDAO.Instance.SaveComment(comment,userId);
        public Comment GetCommentById(int id) => CommentDAO.Instance.FindCommentById(id);
        public void DeleteComment(Comment comment, int userId) => CommentDAO.Instance.DeleteComment(comment, userId);
        public void UpdateComment(Comment comment, int userId   ) => CommentDAO.Instance.UpdateComment(comment, userId);
    }
}
