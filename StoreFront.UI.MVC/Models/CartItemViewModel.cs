using StoreFront.DATA.EF.Models;

namespace StoreFront.UI.MVC.Models
{
    public class CartItemViewModel
    {

        public int Qty { get; set; }

        public Item Item { get; set; }

        public CartItemViewModel(int qty, Item item)
        {
            Qty = qty;
            Item = item;
        }
    }
}
