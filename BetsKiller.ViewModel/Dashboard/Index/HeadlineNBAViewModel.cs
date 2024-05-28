using System.ComponentModel.DataAnnotations;

namespace BetsKiller.ViewModel.Dashboard.Index
{
    public class HeadlineNBAViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }

        public string Published { get; set; }
    }
}
