//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.JumpList;
using Telerik.Windows.Data;


namespace XStream.Phone.View
{
    public class ArtistsTemplateSelector : DataTemplateSelector
    {
        private DataTemplate linkedItemTemplate;
        private DataTemplate emptyItemTemplate;
        private RadJumpList list;

        public DataTemplate LinkedItemTemplate
        {
            get
            {
                return this.linkedItemTemplate;
            }
            set
            {
                this.linkedItemTemplate = value;
            }
        }

        public DataTemplate EmptyItemTemplate
        {
            get
            {
                return this.emptyItemTemplate;
            }
            set
            {
                this.emptyItemTemplate = value;
            }
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (this.list == null)
            {
                JumpListGroupPicker picker = ElementTreeHelper.FindVisualAncestor<JumpListGroupPicker>(container);
                if (picker != null)
                {
                    this.list = picker.Owner;
                }
            }

            if (this.list == null)
            {
                return base.SelectTemplate(item, container);
            }

            return this.IsLinkedItem(item) ? this.linkedItemTemplate : this.emptyItemTemplate;
        }

        private bool IsLinkedItem(object item)
        {
            foreach (DataGroup group in this.list.Groups)
            {
                if (object.Equals(group.Key, item))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
