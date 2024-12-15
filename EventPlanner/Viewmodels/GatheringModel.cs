using EventPlanner.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace EventPlanner.Viewmodels
{
    public class GatheringModel
    {
        public Gathering Gathering { get; set; }
        public int SelectedCategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; }

    }
}
