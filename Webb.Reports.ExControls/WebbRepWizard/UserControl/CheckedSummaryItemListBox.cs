using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Webb.Reports.ExControls.WebbRepWizard.Data;

namespace Webb.Reports.ReportWizard.WizardInfo
{
    public delegate void SelectObjectChangedEventHandler(object serder, EventArgs e);     

    public partial class CheckedSummaryItemListBox : UserControl
    {
        private CustomSummaryCheckedItem _SelectedSummaryItemBox=null;

        public event SelectObjectChangedEventHandler SelectObjectChanged;

        private List<CustomSummaryCheckedItem> AllControls = new List<CustomSummaryCheckedItem>();


        public CheckedSummaryItemListBox()
        {
            InitializeComponent();
        }

        public void Add(SummaryItemDescription summaryItem)
        {
            CustomSummaryCheckedItem customItem=new CustomSummaryCheckedItem(summaryItem, this);

            this.palList.Controls.Add(customItem);

            customItem.BringToFront();

            customItem.Dock = DockStyle.Top;

            AllControls.Add(customItem);           
        }

        public int ItemCount
        {
            get
            {
                return AllControls.Count;
            }
        }

        public void SetItemChecked(int index,bool bChecked)
        {
            if (index < 0 || index > AllControls.Count) return;

            CustomSummaryCheckedItem itemBox = AllControls[index];

            itemBox.ItemChecked = bChecked;
        }

        public CustomSummaryCheckedItem SelectedSummaryItemBox
        {
            get
            {
                return this._SelectedSummaryItemBox;
            }
            set
            {
                this._SelectedSummaryItemBox = value;

                if (this._SelectedSummaryItemBox == null)
                {
                    foreach (CustomSummaryCheckedItem item in AllControls)
                    {
                        item.SetSelectionColor(false);
                    }
                }

                if(SelectObjectChanged!=null)SelectObjectChanged(this, new EventArgs());
            }
        }
        public void InitList(List<SummaryItemDescription> allItems)
        {
            this.palList.Controls.Clear();

            AllControls.Clear();

            if (allItems == null) return;
          
            foreach (SummaryItemDescription item in allItems)
            {
                item.IsChecked = false;

                this.Add(item);
               
            }

        }
        public void InitList(List<SummaryItemDescription> allItems,int nStartIndex,int nCount)
        {
            this.palList.Controls.Clear();

            AllControls.Clear();

            if (allItems == null) return;
          
            for (int i = nStartIndex; i < nStartIndex + nCount; i++)
            {
                if (i < 0 || i >= allItems.Count) continue;

                SummaryItemDescription item = allItems[i];

                item.IsChecked = false;

                this.Add(item);
            }

        }
        public void SetView(GroupWizardOption groupOption)
        {
            if (groupOption == null) return;

            this.palList.AutoScrollPosition = new Point(this.palList.AutoScrollPosition.X, this.palList.AutoScrollPosition.Y);
           
            foreach (CustomSummaryCheckedItem customSummaryItem in AllControls)
            {
                SummaryItemDescription groupSummaryItem = groupOption.ListSummaries.Find(delegate(SummaryItemDescription item) { return item.Description == customSummaryItem.SummaryItem.Description; });
 
                customSummaryItem.SetView(groupSummaryItem); 
            }          

        }

       
        public List<SummaryItemDescription> GetSelectedListSummaries()
        {
            List<SummaryItemDescription> listSummaries = new List<SummaryItemDescription>();            

            foreach (CustomSummaryCheckedItem customSummaryItem in AllControls)
            {
                if (!customSummaryItem.ItemChecked) continue;

                listSummaries.Add(customSummaryItem.SummaryItem.Copy());
            }

            //listSummaries.Sort(delegate(SummaryItemDescription item1, SummaryItemDescription item2) { return item1.OrderIndex!=item2.OrderIndex?item1.OrderIndex.CompareTo(item2.OrderIndex):-1; });
           
            CompareSummaryOrder compareSummaryOrder = new CompareSummaryOrder();

            //listSummaries.Sort(compareSummaryOrder);

            compareSummaryOrder.SortSummarysByOrder(listSummaries);

            return listSummaries;

        } 
    }
}
