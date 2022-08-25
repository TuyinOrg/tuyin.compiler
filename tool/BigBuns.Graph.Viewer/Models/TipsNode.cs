namespace BigBuns.Graph.Viewer
{
    internal class TipsNode : TreeNode
    {
        public new TipsNode Parent 
        {
            get { return base.Parent as TipsNode; }
            set 
            {
                if (Parent != value) 
                {
                    if (Parent != null)
                        Parent.Nodes.Remove(this);

                    Parent.Nodes.Add(this);
                }
            }
        }

        public string Value 
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public List<TipsNode> ChildNodes 
        {
            get { return base.Nodes.Cast<TipsNode>().ToList(); }
            set 
            {
                base.Nodes.Clear();
                base.Nodes.AddRange(value.ToArray());
            }
        }

        public int CompareTo(TipsNode that)
        {
            return string.Compare(this.Value, that.Value, StringComparison.Ordinal);
        }
    }
}
