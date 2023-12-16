using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public interface ICommentRepository
    {
        List<Comment> GetAllComments();
        List<Comment> GetComments(int recipeId);
        void SaveComment(Comment comment);
        Comment GetCommentById(int id);
        void DeleteComment(Comment comment);
        void UpdateComment(Comment comment);
    }
}
