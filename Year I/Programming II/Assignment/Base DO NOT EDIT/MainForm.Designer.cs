
namespace Assignment
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.buttonEquip = new System.Windows.Forms.Button();
            this.buttonUnequip = new System.Windows.Forms.Button();
            this.buttonSell = new System.Windows.Forms.Button();
            this.buttonBuy = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelProtectiveMagic = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.labelCleaningMagic = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelWeight = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.spellListView = new System.Windows.Forms.ListView();
            this.protectiveClothingListView = new System.Windows.Forms.ListView();
            this.handItemListView = new System.Windows.Forms.ListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelGold = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.inventoryListView = new System.Windows.Forms.ListView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonRemoveItem = new System.Windows.Forms.Button();
            this.buttonCreateItem = new System.Windows.Forms.Button();
            this.shopListView = new System.Windows.Forms.ListView();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "clothing-icon");
            this.imageList.Images.SetKeyName(1, "default-icon");
            this.imageList.Images.SetKeyName(2, "hand-icon");
            this.imageList.Images.SetKeyName(3, "spell-icon");
            // 
            // buttonEquip
            // 
            this.buttonEquip.Location = new System.Drawing.Point(374, 257);
            this.buttonEquip.Name = "buttonEquip";
            this.buttonEquip.Size = new System.Drawing.Size(104, 60);
            this.buttonEquip.TabIndex = 1;
            this.buttonEquip.Text = "< Equip";
            this.buttonEquip.UseVisualStyleBackColor = true;
            this.buttonEquip.Click += new System.EventHandler(this.buttonEquip_Click);
            // 
            // buttonUnequip
            // 
            this.buttonUnequip.Location = new System.Drawing.Point(374, 323);
            this.buttonUnequip.Name = "buttonUnequip";
            this.buttonUnequip.Size = new System.Drawing.Size(104, 63);
            this.buttonUnequip.TabIndex = 4;
            this.buttonUnequip.Text = "Unequip >";
            this.buttonUnequip.UseVisualStyleBackColor = true;
            this.buttonUnequip.Click += new System.EventHandler(this.buttonUnequip_Click);
            // 
            // buttonSell
            // 
            this.buttonSell.Location = new System.Drawing.Point(829, 323);
            this.buttonSell.Name = "buttonSell";
            this.buttonSell.Size = new System.Drawing.Size(104, 63);
            this.buttonSell.TabIndex = 8;
            this.buttonSell.Text = "Sell >";
            this.buttonSell.UseVisualStyleBackColor = true;
            this.buttonSell.Click += new System.EventHandler(this.buttonSell_Click);
            // 
            // buttonBuy
            // 
            this.buttonBuy.Location = new System.Drawing.Point(829, 257);
            this.buttonBuy.Name = "buttonBuy";
            this.buttonBuy.Size = new System.Drawing.Size(104, 60);
            this.buttonBuy.TabIndex = 7;
            this.buttonBuy.Text = "< Buy";
            this.buttonBuy.UseVisualStyleBackColor = true;
            this.buttonBuy.Click += new System.EventHandler(this.buttonBuy_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1311, 33);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(54, 29);
            this.toolStripMenuItem1.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(153, 34);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(153, 34);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelProtectiveMagic);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.labelCleaningMagic);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.labelWeight);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.spellListView);
            this.groupBox1.Controls.Add(this.protectiveClothingListView);
            this.groupBox1.Controls.Add(this.handItemListView);
            this.groupBox1.Location = new System.Drawing.Point(11, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 618);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Larry Blotter (Character)";
            // 
            // labelProtectiveMagic
            // 
            this.labelProtectiveMagic.AutoSize = true;
            this.labelProtectiveMagic.Location = new System.Drawing.Point(191, 573);
            this.labelProtectiveMagic.Name = "labelProtectiveMagic";
            this.labelProtectiveMagic.Size = new System.Drawing.Size(159, 25);
            this.labelProtectiveMagic.TabIndex = 27;
            this.labelProtectiveMagic.Text = "Dummy Protective";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 573);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(143, 25);
            this.label9.TabIndex = 26;
            this.label9.Text = "Protective Magic";
            // 
            // labelCleaningMagic
            // 
            this.labelCleaningMagic.AutoSize = true;
            this.labelCleaningMagic.Location = new System.Drawing.Point(191, 535);
            this.labelCleaningMagic.Name = "labelCleaningMagic";
            this.labelCleaningMagic.Size = new System.Drawing.Size(149, 25);
            this.labelCleaningMagic.TabIndex = 25;
            this.labelCleaningMagic.Text = "Dummy Cleaning";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 535);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(142, 25);
            this.label7.TabIndex = 24;
            this.label7.Text = "Cleaning Magic :";
            // 
            // labelWeight
            // 
            this.labelWeight.AutoSize = true;
            this.labelWeight.Location = new System.Drawing.Point(191, 497);
            this.labelWeight.Name = "labelWeight";
            this.labelWeight.Size = new System.Drawing.Size(137, 25);
            this.labelWeight.TabIndex = 23;
            this.labelWeight.Text = "Dummy Weight";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 497);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(158, 25);
            this.label5.TabIndex = 22;
            this.label5.Text = "Available Weight : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 25);
            this.label3.TabIndex = 21;
            this.label3.Text = "Hand Equipment";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 25);
            this.label2.TabIndex = 20;
            this.label2.Text = "Protective Clothing";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 343);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 25);
            this.label1.TabIndex = 19;
            this.label1.Text = "Spells";
            // 
            // spellListView
            // 
            this.spellListView.HideSelection = false;
            this.spellListView.LargeImageList = this.imageList;
            this.spellListView.Location = new System.Drawing.Point(16, 382);
            this.spellListView.Name = "spellListView";
            this.spellListView.ShowItemToolTips = true;
            this.spellListView.Size = new System.Drawing.Size(330, 101);
            this.spellListView.SmallImageList = this.imageList;
            this.spellListView.TabIndex = 18;
            this.spellListView.UseCompatibleStateImageBehavior = false;
            this.spellListView.View = System.Windows.Forms.View.SmallIcon;
            // 
            // protectiveClothingListView
            // 
            this.protectiveClothingListView.HideSelection = false;
            this.protectiveClothingListView.LargeImageList = this.imageList;
            this.protectiveClothingListView.Location = new System.Drawing.Point(16, 232);
            this.protectiveClothingListView.Name = "protectiveClothingListView";
            this.protectiveClothingListView.ShowItemToolTips = true;
            this.protectiveClothingListView.Size = new System.Drawing.Size(330, 101);
            this.protectiveClothingListView.SmallImageList = this.imageList;
            this.protectiveClothingListView.TabIndex = 17;
            this.protectiveClothingListView.UseCompatibleStateImageBehavior = false;
            this.protectiveClothingListView.View = System.Windows.Forms.View.SmallIcon;
            // 
            // handItemListView
            // 
            this.handItemListView.HideSelection = false;
            this.handItemListView.LargeImageList = this.imageList;
            this.handItemListView.Location = new System.Drawing.Point(16, 77);
            this.handItemListView.Name = "handItemListView";
            this.handItemListView.ShowItemToolTips = true;
            this.handItemListView.Size = new System.Drawing.Size(330, 101);
            this.handItemListView.SmallImageList = this.imageList;
            this.handItemListView.TabIndex = 16;
            this.handItemListView.UseCompatibleStateImageBehavior = false;
            this.handItemListView.View = System.Windows.Forms.View.SmallIcon;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelGold);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.inventoryListView);
            this.groupBox2.Location = new System.Drawing.Point(491, 52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(331, 618);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Store Cupboard (Inventory)";
            // 
            // labelGold
            // 
            this.labelGold.AutoSize = true;
            this.labelGold.Location = new System.Drawing.Point(194, 523);
            this.labelGold.Name = "labelGold";
            this.labelGold.Size = new System.Drawing.Size(119, 25);
            this.labelGold.TabIndex = 15;
            this.labelGold.Text = "Dummy Gold";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 523);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 25);
            this.label4.TabIndex = 14;
            this.label4.Text = "Available Gold :";
            // 
            // inventoryListView
            // 
            this.inventoryListView.HideSelection = false;
            this.inventoryListView.LargeImageList = this.imageList;
            this.inventoryListView.Location = new System.Drawing.Point(11, 43);
            this.inventoryListView.MultiSelect = false;
            this.inventoryListView.Name = "inventoryListView";
            this.inventoryListView.ShowItemToolTips = true;
            this.inventoryListView.Size = new System.Drawing.Size(311, 432);
            this.inventoryListView.SmallImageList = this.imageList;
            this.inventoryListView.TabIndex = 13;
            this.inventoryListView.UseCompatibleStateImageBehavior = false;
            this.inventoryListView.View = System.Windows.Forms.View.SmallIcon;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonRemoveItem);
            this.groupBox3.Controls.Add(this.buttonCreateItem);
            this.groupBox3.Controls.Add(this.shopListView);
            this.groupBox3.Location = new System.Drawing.Point(944, 52);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(356, 618);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Olive Sander\'s (Shop)";
            // 
            // buttonRemoveItem
            // 
            this.buttonRemoveItem.Location = new System.Drawing.Point(231, 520);
            this.buttonRemoveItem.Name = "buttonRemoveItem";
            this.buttonRemoveItem.Size = new System.Drawing.Size(104, 40);
            this.buttonRemoveItem.TabIndex = 12;
            this.buttonRemoveItem.Text = "Remove Item";
            this.buttonRemoveItem.UseVisualStyleBackColor = true;
            this.buttonRemoveItem.Click += new System.EventHandler(this.buttonRemoveItem_Click);
            // 
            // buttonCreateItem
            // 
            this.buttonCreateItem.Location = new System.Drawing.Point(24, 520);
            this.buttonCreateItem.Name = "buttonCreateItem";
            this.buttonCreateItem.Size = new System.Drawing.Size(104, 40);
            this.buttonCreateItem.TabIndex = 11;
            this.buttonCreateItem.Text = "Create Item";
            this.buttonCreateItem.UseVisualStyleBackColor = true;
            this.buttonCreateItem.Click += new System.EventHandler(this.buttonCreateItem_Click);
            // 
            // shopListView
            // 
            this.shopListView.GridLines = true;
            this.shopListView.HideSelection = false;
            this.shopListView.LargeImageList = this.imageList;
            this.shopListView.Location = new System.Drawing.Point(23, 43);
            this.shopListView.MultiSelect = false;
            this.shopListView.Name = "shopListView";
            this.shopListView.ShowItemToolTips = true;
            this.shopListView.Size = new System.Drawing.Size(311, 432);
            this.shopListView.SmallImageList = this.imageList;
            this.shopListView.StateImageList = this.imageList;
            this.shopListView.TabIndex = 10;
            this.shopListView.UseCompatibleStateImageBehavior = false;
            this.shopListView.View = System.Windows.Forms.View.SmallIcon;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1311, 683);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonSell);
            this.Controls.Add(this.buttonBuy);
            this.Controls.Add(this.buttonUnequip);
            this.Controls.Add(this.buttonEquip);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Larry Blotter and the Janitor of Marzipan";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Button buttonEquip;
        private System.Windows.Forms.Button buttonUnequip;
        private System.Windows.Forms.Button buttonSell;
        private System.Windows.Forms.Button buttonBuy;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelProtectiveMagic;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelCleaningMagic;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelWeight;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView spellListView;
        private System.Windows.Forms.ListView protectiveClothingListView;
        private System.Windows.Forms.ListView handItemListView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelGold;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView inventoryListView;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonCreateItem;
        private System.Windows.Forms.ListView shopListView;
        private System.Windows.Forms.Button buttonRemoveItem;
    }
}

