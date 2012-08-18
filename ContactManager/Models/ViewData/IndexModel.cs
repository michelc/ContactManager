using System.Collections.Generic;

namespace ContactManager.Models.ViewData
{
    public class IndexModel
    {
        public Group SelectedGroup { get; set; }
        public IEnumerable<Group> Groups { get; set; }
    }
}