using GraphX.Common.Enums;
using GraphX.Controls;
using GraphX.Controls.Models;
using GraphX.Logic.Algorithms.EdgeRouting;
using GraphX.Logic.Algorithms.LayoutAlgorithms;
using GraphX.Logic.Algorithms.OverlapRemoval;
using GraphX.Logic.Models;
using libgraph;
using QuickGraph;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Forms.Integration;

namespace BigBuns.Graph.Viewer
{
    public partial class Form1 : Form
    {
        public string DataFileName 
        {
            get;
            internal set;
        }

        public string ImageFileName 
        { 
            get; 
            internal set;
        }

        private ZoomControl Zoom => (tabControl1.SelectedTab.Controls[0] as ElementHost)?.Child as ZoomControl;

        private GraphAreaExample GraphArea => Zoom?.Content as GraphAreaExample;


        public Form1()
        {
            //ShowInTaskbar = false;
            InitializeComponent();
            Load += Form1_Load;
        }

        void Form1_Load(object sender, EventArgs e)
        {
            foreach (var item in Enum.GetValues(typeof(LayoutAlgorithmTypeEnum)).Cast<LayoutAlgorithmTypeEnum>())
                comboBox1.Items.Add(item);

            foreach (var item in Enum.GetValues(typeof(OverlapRemovalAlgorithmTypeEnum)).Cast<OverlapRemovalAlgorithmTypeEnum>())
                comboBox2.Items.Add(item);

            foreach (var item in Enum.GetValues(typeof(EdgeRoutingAlgorithmTypeEnum)).Cast<EdgeRoutingAlgorithmTypeEnum>())
                comboBox3.Items.Add(item);

            comboBox1.SelectedItem = LayoutAlgorithmTypeEnum.EfficientSugiyama;
            comboBox2.SelectedItem = OverlapRemovalAlgorithmTypeEnum.FSA;
            comboBox3.SelectedItem = EdgeRoutingAlgorithmTypeEnum.SimpleER;

            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);

            CreateTabs();

            if (ImageFileName != null)
            {
                var ticks = new System.Windows.Forms.Timer();
                ticks.Interval = 1000;
                ticks.Tick += Ticks_Tick2;
                ticks.Start();
            }
        }

        private void Ticks_Tick2(object sender, EventArgs e)
        {
            Zoom.ZoomToFill();
            var ticks = new System.Windows.Forms.Timer();
            ticks.Interval = 1000;
            ticks.Tick += Ticks_Tick;
            ticks.Start();
        }

        private void Ticks_Tick(object sender, EventArgs e)
        {
            var ctrl = tabControl1.TabPages[0].Controls[0];
            Bitmap b = new Bitmap(ctrl.Width, ctrl.Height);
            ctrl.DrawToBitmap(b, new Rectangle(0, 0, b.Width, b.Height));
            b.Save(ImageFileName);

            System.Windows.Forms.Application.Exit();
        }

        private void CreateTabs() 
        {
            tabControl1.TabPages.Clear();
            tabControl1.TabPages.AddRange(GenerateGraph(DataFileName).ToArray());
        }

        private UIElement GenerateWpfVisuals(GraphExample ge)
        {
            var _zoomctrl = new ZoomControl();
            ZoomControl.SetViewFinderVisibility(_zoomctrl, ImageFileName == null ? Visibility.Visible : Visibility.Hidden);
            var logic = new GXLogicCore<DataVertex, DataEdge, BidirectionalGraph<DataVertex, DataEdge>>();
            logic.Graph = ge;
            logic.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.LinLog;
            logic.DefaultLayoutAlgorithmParams = logic.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.LinLog);
            //((LinLogLayoutParameters)logic.DefaultLayoutAlgorithmParams). = 100;
            logic.DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA;
            logic.DefaultOverlapRemovalAlgorithmParams = logic.AlgorithmFactory.CreateOverlapRemovalParameters(OverlapRemovalAlgorithmTypeEnum.FSA);
            logic.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.None;
            logic.AsyncAlgorithmCompute = true;

            ((OverlapRemovalParameters)logic.DefaultOverlapRemovalAlgorithmParams).HorizontalGap = 50;
            ((OverlapRemovalParameters)logic.DefaultOverlapRemovalAlgorithmParams).VerticalGap = 50;

            var _gArea = new GraphAreaExample
            {
                LogicCore = logic,
                EdgeLabelFactory = new DefaultEdgelabelFactory()
            };
            _zoomctrl.Content = _gArea;
            _gArea.RelayoutFinished += gArea_RelayoutFinished;
            _gArea.ShowAllEdgesLabels(true);
            _gArea.ShowAllEdgesArrows();
            _gArea.SetEdgesDrag(true);
            _gArea.SetVerticesDrag(true, true);
            _gArea.GenerateGraph(true);
            _gArea.EdgeSelected += _gArea_EdgeSelected;
            _gArea.VertexSelected += _gArea_VertexSelected;

            var myResourceDictionary = new ResourceDictionary { Source = new Uri("Templates\\template.xaml", UriKind.Relative) };
            _zoomctrl.Resources.MergedDictionaries.Add(myResourceDictionary);
            return _zoomctrl;
        }

        private IEnumerable<TabPage> GenerateGraph(string fileName)
        {
            var br = new BinaryReader(File.OpenRead(fileName), Encoding.UTF8);
            var count = br.ReadInt32();
            for (var i = 0; i < count; i++) 
            {
                var graph = DebugGraph.Load(br.BaseStream);
                var eg = GraphConverter.Convert(graph);

                var page = new TabPage(graph.GraphName);
                var host = new ElementHost();
                host.Dock = DockStyle.Fill;
                host.Child = GenerateWpfVisuals(eg);
                page.Controls.Add(host);
                yield return page;
            }
        }

        private void Relayout()
        {
            var _gArea = GraphArea;

            comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged -= comboBox2_SelectedIndexChanged;
            comboBox3.SelectedIndexChanged -= comboBox3_SelectedIndexChanged;

            var late = (LayoutAlgorithmTypeEnum)comboBox1.SelectedItem;
            _gArea.LogicCore.DefaultLayoutAlgorithm = late;
            if (late == LayoutAlgorithmTypeEnum.EfficientSugiyama)
            {
                var prms = _gArea.LogicCore.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.EfficientSugiyama) as EfficientSugiyamaLayoutParameters;
                prms.EdgeRouting = SugiyamaEdgeRoutings.Orthogonal;
                prms.LayerDistance = prms.VertexDistance = 100;
                _gArea.LogicCore.EdgeCurvingEnabled = false;
                _gArea.LogicCore.DefaultLayoutAlgorithmParams = prms;
                comboBox3.SelectedItem = EdgeRoutingAlgorithmTypeEnum.None;
            }
            else
            {
                _gArea.LogicCore.EdgeCurvingEnabled = true;
            }
            if (late == LayoutAlgorithmTypeEnum.BoundedFR)
            {
                _gArea.LogicCore.DefaultLayoutAlgorithmParams = _gArea.LogicCore.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.BoundedFR);
            }
            if (late == LayoutAlgorithmTypeEnum.FR)
            {
                _gArea.LogicCore.DefaultLayoutAlgorithmParams = _gArea.LogicCore.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.FR);
            }

            var core = _gArea.LogicCore;
            core.DefaultOverlapRemovalAlgorithm = (OverlapRemovalAlgorithmTypeEnum)comboBox2.SelectedItem;
            if (core.DefaultOverlapRemovalAlgorithm == OverlapRemovalAlgorithmTypeEnum.FSA || core.DefaultOverlapRemovalAlgorithm == OverlapRemovalAlgorithmTypeEnum.OneWayFSA)
            {
                core.DefaultOverlapRemovalAlgorithmParams.HorizontalGap = 120;
                core.DefaultOverlapRemovalAlgorithmParams.VerticalGap = 120;
            }

            _gArea.LogicCore.DefaultEdgeRoutingAlgorithm = (EdgeRoutingAlgorithmTypeEnum)comboBox3.SelectedItem;
            if ((EdgeRoutingAlgorithmTypeEnum)comboBox3.SelectedItem == EdgeRoutingAlgorithmTypeEnum.Bundling)
            {
                BundleEdgeRoutingParameters prm = new BundleEdgeRoutingParameters();
                _gArea.LogicCore.DefaultEdgeRoutingAlgorithmParams = prm;
                prm.Iterations = 200;
                prm.SpringConstant = 5;
                prm.Threshold = .1f;
                _gArea.LogicCore.EdgeCurvingEnabled = true;
            }
            else
                _gArea.LogicCore.EdgeCurvingEnabled = false;

            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            _gArea.RelayoutGraph();
            Zoom.ZoomToFill();
        }

        private void _gArea_VertexSelected(object sender, VertexSelectedEventArgs args)
        {
            var vert = (DataVertex)args.VertexControl.DataContext;
            treeView1.Nodes.Clear();
            if (!string.IsNullOrEmpty(vert.Tips))
                treeView1.Nodes.Add(ParseStringToNode(vert.Tips));
        }

        private void _gArea_EdgeSelected(object sender, EdgeSelectedEventArgs args)
        {
            var edge = (DataEdge)args.EdgeControl.DataContext;
            treeView1.Nodes.Clear();
            if (!string.IsNullOrEmpty(edge.Tips))
                treeView1.Nodes.Add(ParseStringToNode(edge.Tips));
        }

        private TipsNode ParseStringToNode(string inputString) 
        {
            var root = new TipsNode
            {
                Parent = null,
                Value = string.Empty,
                ChildNodes = new List<TipsNode>()
            };
            var node = root;
            var escape = false;
            foreach (var c in inputString)
            {
                if (escape)
                {
                    if (c != ' ') node.Value += c;
                    escape = false;
                }
                else
                {
                    switch (c)
                    {
                        case '(':
                            node = new TipsNode { Parent = node, Value = string.Empty, ChildNodes = new List<TipsNode>() };
                            node.Parent.ChildNodes.Add(node);
                            break;
                        case ')':
                            if (node.Parent != null)
                            {
                                node = new TipsNode { Parent = node.Parent.Parent, Value = string.Empty, ChildNodes = new List<TipsNode>() };
                                node.Parent?.ChildNodes.Add(node);
                            }
                            break;
                        case ',':
                            node = new TipsNode { Parent = node.Parent, Value = string.Empty, ChildNodes = new List<TipsNode>() };
                            node.Parent?.ChildNodes.Add(node);
                            escape = true;
                            break;
                        default:
                            node.Value += c;
                            break;
                    }
                }
            }

            Console.WriteLine("Output 1:");
            Print(root, string.Empty, false);
            Console.WriteLine();

            Console.WriteLine("Output 2:");
            Print(root, string.Empty, true);

            return root;
        }

        private static void Print(TipsNode node, string level, bool isOrdered)
        {
            if (node.Value.Length > 0) Console.WriteLine(level + node.Value);

            IEnumerable<TipsNode> nodes = null;
            if (isOrdered)
                nodes = node.ChildNodes.OrderBy(q => q);
            else
                nodes = node.ChildNodes;

            foreach (var n in nodes)
            {
                if (node.Parent == null)
                    Print(n, "- " + level, isOrdered);
                else
                    Print(n, "  " + level, isOrdered);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GraphArea.RelayoutGraph();
        }

        private void gArea_RelayoutFinished(object sender, EventArgs e)
        {
            Zoom.ZoomToFill();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Relayout();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Relayout();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Relayout();
        }
    }
}