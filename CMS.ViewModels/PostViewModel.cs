using CMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime PublishedDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ApplicationUserId { get; set; }
        public PostViewModel()
        {

        }
        public PostViewModel(Post model)
        {
            Id = model.Id;
            Title = model.Title;
            Description = model.Description;
            CreatedDate = model.CreatedDate;
            PublishedDate = model.PublishedDate;
            CategoryId = model.CategoryId;
            Category = model.Category;
            ApplicationUserId = model.ApplicationUserId;
        }
        public Post ConvertViewModel(PostViewModel model)
        {
            return new Post
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                CreatedDate = model.CreatedDate,
                PublishedDate = model.PublishedDate,
                CategoryId = model.CategoryId,
                Category = model.Category,
                ApplicationUserId = model.ApplicationUserId
            };

        }
    }
}
