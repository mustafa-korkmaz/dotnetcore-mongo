﻿using Presentation.Middlewares.Validations;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels
{
    public class ViewModelBase
    {
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class ListViewModelRequest
    {
        public int Offset { get; set; }

        [Range(1, 1000)]
        public int Limit { get; set; }
    }

    public class ListViewModelResponse<TViewModel> where TViewModel : class
    {
        /// <summary>
        /// Paged list items
        /// </summary>
        public IReadOnlyCollection<TViewModel> Items { get; set; }

        /// <summary>
        /// Total count of items stored in repository
        /// </summary>
        public long TotalCount { get; set; }
    }

    public class ObjectIdViewModel
    {
        [ObjectIdValidation]
        [Display(Name = "ID")]
        public string? id { get; set; }
    }
}
