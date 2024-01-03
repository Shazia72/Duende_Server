﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime PublishedDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
