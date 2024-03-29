﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Models.Contracts
{
    public interface IEntity
    {
        public long Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
