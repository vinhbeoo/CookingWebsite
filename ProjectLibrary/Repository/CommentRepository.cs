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
        public List<Comment> GetAllComments() => CommentDAO.Instance.GetAllComments();
        public List<Comment> GetComments(int recipeId) => CommentDAO.Instance.GetComments(recipeId);
        public void SaveComment(Comment comment) => CommentDAO.Instance.SaveComment(comment);
        public Comment GetCommentById(int id) => CommentDAO.Instance.FindCommentById(id);
        public void DeleteComment(Comment comment) => CommentDAO.Instance.DeleteComment(comment);
        public void UpdateComment(Comment comment) => CommentDAO.Instance.UpdateComment(comment);
    }
}
