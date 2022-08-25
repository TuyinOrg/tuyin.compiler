using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    class Group 
    {
        public List<GroupItem> Items { get; private set; }

        public Group() 
        {
            Items = new List<GroupItem>();
        }
    }

    class GroupItem 
    {

    }

    class BGroupBox : BUIElement
    {
        private WinFormInputHelper<Group> mGroupInputHelper;
        private WinFormInputHelper<GroupItem> mGroupItemInputHelper;

        public List<Group> Groups { get; private set; }

        public Size Size { get; set; }

        public BGroupBox(BControl ctrl)
            : base(ctrl)
        {
            mGroupInputHelper = new WinFormInputHelper<Group>(Host);
            mGroupItemInputHelper = new WinFormInputHelper<GroupItem>(Host);

            Groups = new List<Group>();
            Size = new Size(32, 32);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            for (var i = 0; i < Groups.Count; i++) 
            {
                PaintGroup(e, Groups[i]);
            }

            base.OnPaint(e);
        }

        private int PaintGroup(PaintEventArgs e, Group group) 
        {


            for (var i = 0; i < group.Items.Count; i++) 
            {
                var item = group.Items[i];
            }

            return 0;
        }

    }
}
