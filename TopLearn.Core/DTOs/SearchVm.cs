using System;
using System.Collections.Generic;
using System.Text;

namespace TopLearn.Core.DTOs
{
    public class SearchVm
    {
        public Tuple<List<SearchItemVM>, int> Search { get; set; }
    }

    public class SearchItemVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string Link { get; set; }
        public string q { get; set; }
        public bool IsNoteType { get; set; }
    }
}
