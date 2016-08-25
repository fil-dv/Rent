using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IPhotoRepository
    {
        IEnumerable<Photo> Photos { get; }
        void DeletePhoto(Photo photo);
        void SavePhotoChanges(Photo photo);
    }
}
