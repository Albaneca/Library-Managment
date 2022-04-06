using Library.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.DTOs.PublishHouseDTOs
{
    public class DisplayPublishHouseDTO : IErrorMessage
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public int BookCount { get; set; }
        public string ErrorMessage { get; set; }
    }
}
