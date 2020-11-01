using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Web.Areas.EventManager.Data
{
    public class AreaPriceViewModel
    {
        public int EventAreaId { get; set; }

        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0", "99999999")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Display(Name = "Price", ResourceType = typeof(Resources.TicketManagementResource))]
        public decimal Price { get; set; }
    }
}