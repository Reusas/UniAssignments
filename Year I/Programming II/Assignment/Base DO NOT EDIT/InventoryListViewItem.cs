using System.Windows.Forms;

namespace Assignment
{
    // DO NOT EDIT THIS CLASS
    public class InventoryListViewItem : ListViewItem
    {
        public Item Item { get; set; }

        public InventoryListViewItem(Item item)
        {
            Text = item.Name;
            ToolTipText = $"{item.Description} \n Cost: {item.Cost} \n Weight: " +
                $"{item.Weight} \n Cleaning Magic: {item.CleaningMagic} " +
                $"\n Protective Magic: {item.ProtectiveMagic}" +
                $"\n Type of Item: {item.TypeOfItem}";
            ImageKey = item.IconName;
            Item = item;
        }
    }
}
