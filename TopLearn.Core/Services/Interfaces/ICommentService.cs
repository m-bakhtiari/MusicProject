using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services.Interfaces
{
    public interface ICommentService
    {
        Task AddComment(Comment comment);

        Task ToggleShowStatus(int id);

        Task<List<Comment>> GetCommentsByRelationId(int relationId, int type);

        Task<List<Comment>> GetAllComments();
        Task<Comment> GetCommentById(int id);
    }
}
