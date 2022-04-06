using Library.Services.DTOs;
using System.Collections.Generic;

namespace Library.Web.Models
{
    public class BanViewModel
    {
        public IEnumerable<BanDTO> Banned { get; set; }

        public int MaxPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
