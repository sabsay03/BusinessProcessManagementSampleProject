using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CommentManager : ICommentService
    {
        ICommentDal _commentdal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentdal = commentDal;
        }

        public void CommentAdd(Comment comment)
        {
            _commentdal.Insert(comment);
        }

        public void CommentDelete(Comment comment)
        {
            _commentdal.Delete(comment);
        }

        public void CommentUpdate(Comment comment)
        {
            _commentdal.Update(comment);
        }

        public Comment GetById(int id)
        {
            return _commentdal.GetById(id);
        }

        public List<Comment> GetList()
        {
            return _commentdal.GetListAll();
        }
    }
}
