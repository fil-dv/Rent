﻿using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete.Repositories
{
    public class EFPhotoRepository : IPhotoRepository
    {
        EFDbContext _context = new EFDbContext();

        public IEnumerable<Photo> Photos
        {
            get
            {
                return _context.Photos;
            }
        }
    }
}