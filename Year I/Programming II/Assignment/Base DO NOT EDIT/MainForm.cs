using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Assignment
{
    // DO NOT EDIT THIS CLASS
    public partial class MainForm : Form
    {

        // DO NOT EDIT THIS LINE
        private Tasks _theTasks = new Tasks();

        // DO NOT EDIT THIS PROPERTY
        public Tasks TheTasks
        {
            get
            {
                return _theTasks;
            }
        }

        // Do NOT EDIT THIS CONSTRUCTOR
        public MainForm()
        {
            InitializeComponent();
        }

        // DO NOT EDIT THIS METHOD
        private void MainForm_Load(object sender, EventArgs e)
        {
            // DO NOT TOUCH THE FOLLOWING 5 LINES
            _theTasks.LoadShop();
            UpdateShopView();
            UpdateInventoryView();
            UpdatePlayerView();
            DialogResult = DialogResult.OK;
        }

       
        // DO NOT EDIT THIS METHOD
        private void UpdateShopView()
        {
            UpdateListView(shopListView, _theTasks.TheShop.ShopList);
        }
        // DO NOT EDIT THIS METHOD
        private void UpdateInventoryView()
        {
            UpdateListView(inventoryListView, _theTasks.TheCharacter.InventoryList);
            labelGold.Text = _theTasks.TheCharacter.Gold.ToString();
        }

        // DO NOT EDIT THIS METHOD
        private void UpdatePlayerView()
        {
            UpdateListView(handItemListView, _theTasks.TheCharacter.HandItemList);
            UpdateListView(spellListView, _theTasks.TheCharacter.SpellList);
            UpdateListView(protectiveClothingListView, _theTasks.TheCharacter.ClothingList);
            labelWeight.Text = (_theTasks.TheCharacter.MaxWeight - _theTasks.TheCharacter.CurrentWeight).ToString();
            labelCleaningMagic.Text = _theTasks.TheCharacter.CleaningMagic.ToString();
            labelProtectiveMagic.Text = _theTasks.TheCharacter.ProtectiveMagic.ToString();
        }

        // Do NOT EDIT THIS METHOD
        private void UpdateListView(ListView listView, List<Item> itemList)
        {
            listView.Clear();
            foreach (var item in itemList)
            {
                listView.Items.Add(new InventoryListViewItem(item));
            }
        }

        // Do NOT EDIT THIS METHOD
        private void buttonEquip_Click(object sender, EventArgs e)
        {
            if (inventoryListView.SelectedItems.Count > 0)
            {
                InventoryListViewItem selectedItem = (InventoryListViewItem)inventoryListView.SelectedItems[0];
                if (_theTasks.EquipItem(selectedItem.Item))
                {
                    UpdateInventoryView();
                    UpdatePlayerView();
                }
                else
                {
                    MessageBox.Show("Could not equip the item");
                }
            }
            else
            {
                MessageBox.Show("Please select an item from the inventory to equip.");
            }
            
            
        }
        // Do NOT EDIT THIS METHOD
        private void buttonUnequip_Click(object sender, EventArgs e)
        {
            ListView currentSelection = null;
            if (handItemListView.SelectedItems.Count > 0)
            {
                currentSelection = handItemListView;
            }
            else if (protectiveClothingListView.SelectedItems.Count > 0)
            {
                currentSelection = protectiveClothingListView;
            }
            else if (spellListView.SelectedItems.Count > 0)
            {
                currentSelection = spellListView;
            }

            if (currentSelection != null)
            {
                InventoryListViewItem selectedItem = (InventoryListViewItem)currentSelection.SelectedItems[0];
                if (_theTasks.UnequipItem(selectedItem.Item))
                {
                    UpdateInventoryView();
                    UpdatePlayerView();
                }
                else
                {
                    MessageBox.Show("Could not unequip the item");
                }
            }
            else
            {
                MessageBox.Show("Please select an item from the character to equip.");
            }
        }
        // Do NOT EDIT THIS METHOD
        private void handItemListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            spellListView.SelectedIndices.Clear();
            protectiveClothingListView.SelectedIndices.Clear();
        }
        // Do NOT EDIT THIS METHOD
        private void protectiveClothingListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            handItemListView.SelectedItems.Clear();
            spellListView.SelectedIndices.Clear();
        }
        // DO NOT EDIT THIS METHOD
        private void spellListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            handItemListView.SelectedItems.Clear();
            protectiveClothingListView.SelectedIndices.Clear();

        }
        // DO NOT EDIT THIS METHOD
        private void buttonBuy_Click(object sender, EventArgs e)
        {
            if (shopListView.SelectedItems.Count > 0)
            {
                InventoryListViewItem selectedItem = (InventoryListViewItem)shopListView.SelectedItems[0];
                if (_theTasks.BuyItemFromShop(selectedItem.Item))
                {
                    UpdateShopView();
                    UpdateInventoryView();
                }
                else
                {
                    MessageBox.Show("Could not buy the item, not enough gold.");
                }
            }
            else
            {
                MessageBox.Show("Please select an item from the inventory to sell.");
            }
        }

        // DO NOT EDIT THIS METHOD
        private void buttonSell_Click(object sender, EventArgs e)
        {
            if (inventoryListView.SelectedItems.Count > 0)
            {
                InventoryListViewItem selectedItem = (InventoryListViewItem)inventoryListView.SelectedItems[0];
                if (_theTasks.SellItemToShop(selectedItem.Item))
                {
                    UpdateShopView();
                    UpdateInventoryView(); 
                }
                else
                {
                    MessageBox.Show("Could not sell the item");
                }
            }
            else
            {
                MessageBox.Show("Please select an item from the inventory to sell.");
            }
        }
        // DO NOT EDIT THIS METHOD
        private void buttonCreateItem_Click(object sender, EventArgs e)
        {
            _theTasks.CreateShopItem();
            UpdateShopView();
        }
        // DO NOT EDIT THIS METHOD
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _theTasks.LoadState();
            UpdateShopView();
            UpdateInventoryView();
            UpdatePlayerView();
        }
        // DO NOT EDIT THIS METHOD
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _theTasks.SaveState();
        }
        // DO NOT EDIT THIS METHOD
        private void buttonRemoveItem_Click(object sender, EventArgs e)
        {
            if (shopListView.SelectedItems.Count > 0)
            {
                InventoryListViewItem selectedItem = (InventoryListViewItem)shopListView.SelectedItems[0];
                if (_theTasks.RemoveShopItem(selectedItem.Item))
                {
                    UpdateShopView();
                }
                else
                {
                    MessageBox.Show("Could not remove the item from the shop.");
                }
            }
            else
            {
                MessageBox.Show("Please select an item from the shop to remove.");
            }
        }

    }
}
