using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly TopLearnContext _context;
        public CommentService(TopLearnContext context)
        {
            _context = context;
        }

        public async Task AddComment(Comment comment)
        {
            comment.CreatedDate = DateTime.Now;
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Mobile == comment.Mobile);
            if (user != null)
            {
                comment.UserId = user.UserId;
            }
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetAllComments()
        {
            return await _context.Comments.Include(x => x.Comments).Include(x => x.StudentConcert).Include(x => x.Instrument)
                .Include(x => x.Product).Include(x => x.User).Where(x => x.ParentId == null).OrderByDescending(x => x.CreatedDate).ToListAsync();
        }

        public async Task<Comment> GetCommentById(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<List<Comment>> GetCommentsByRelationId(int relationId, int type)
        {
            var model = _context.Comments.Include(x => x.Comments).ThenInclude(x => x.User).Where(x => x.IsShowOnSite && x.ParentId == null).AsQueryable();
            switch (type)
            {
                case (int)ConstantValue.CommentType.StudentConcert:
                    return await model.Where(x => x.StudentConcertId == relationId).ToListAsync();
                case (int)ConstantValue.CommentType.Instrument:
                    return await model.Where(x => x.InstrumentId == relationId).ToListAsync();
                case (int)ConstantValue.CommentType.Product:
                    return await model.Where(x => x.ProductId == relationId).ToListAsync();
                default:
                    return null;
            }
        }

        public async Task ToggleShowStatus(int id)
        {
            var model = await _context.Comments.FindAsync(id);
            model.IsShowOnSite = !model.IsShowOnSite;
            await _context.SaveChangesAsync();
        }


    }
}
