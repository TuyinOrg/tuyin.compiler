using addin.common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    public static class BDataGridConst                                                  
    {
        public const int TAB_HEIGHT = 22;
        public const int ITEM_HEIGHT = 20;
    }

    public interface IBdataGridItem                                                     
    {
        object GetColumnItem(int column);
    }

    public class BDataGridMouseClickEventArgs<T>                                        : MouseEventArgs                       
    {
        public T            Item    { get { return (T)Row.Content; } }
        public BDataGridRow Row     { get; private set; }

        public BDataGridMouseClickEventArgs(BDataGridRow row, MouseButtons button, int clicks, int x, int y) 
            : base(button, clicks, x, y, 0)                                             
        {
            Row = row;
        }
    }

    public class BDataGridMouseDoubleClickEventArgs<T>                                  : MouseEventArgs                 
    {
        public T            Item    { get { return (T)Row.Content; } }
        public BDataGridRow Row     { get; private set; }

        public BDataGridMouseDoubleClickEventArgs(BDataGridRow row, MouseButtons button, int clicks, int x, int y) 
            : base(button, clicks, x, y, 0)                                             
        {
            Row = row;
        }
    }

    public class BDataGridRowEnterEventArgs<T>                                          : MouseEventArgs                         
    {
        public T            Item    { get { return (T)Row.Content; } }
        public BDataGridRow Row     { get; private set; }

        public BDataGridRowEnterEventArgs(BDataGridRow row, MouseButtons button, int clicks, int x, int y) 
            : base(button, clicks, x, y, 0)                                             
        {
            Row = row;
        }
    }

    public class BDataGridRowLeaveEventArgs<T>                                          : MouseEventArgs                         
    {
        public T            Item    { get { return (T)Row.Content; } }
        public BDataGridRow Row     { get; private set; }

        public BDataGridRowLeaveEventArgs(BDataGridRow row, MouseButtons button, int clicks, int x, int y) 
            : base(button, clicks, x, y, 0)                                             
        {
            Row = row;
        }
    }

    enum SortDirection                                                                  
    {
        // 摘要:
        //     从小到大排序。 例如，从 A 到 Z。
        Ascending = 0,
        //
        // 摘要:
        //     从大到小排序。 例如，从 Z 到 A。
        Descending = 1,
    }

    class BDataGridSortClass                                                            
    {
        private string _sortProperty;

        public string SortProperty                                                      
        {
            get { return _sortProperty; }
            set { _sortProperty = value; }
        }

        private SortDirection _sortDirection;
        public SortDirection SortDirection                                              
        {
            get { return _sortDirection; }
            set { _sortDirection = value; }
        }

        public BDataGridSortClass(string sortProperty, SortDirection sortDirection)     
        {
            _sortProperty = sortProperty;
            _sortDirection = sortDirection;
        }
    }

    class BDataGridComparer<T>                                                          : IComparer<T>                                           
    {
        private List<BDataGridSortClass> _sortClasses;

        /// <summary>
        /// Collection of sorting criteria
        /// </summary>
        public List<BDataGridSortClass> SortClasses                                     
        {
            get { return _sortClasses; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public BDataGridComparer()                                                      
        {
            _sortClasses = new List<BDataGridSortClass>();
        }

        /// <summary>
        /// Constructor that takes a sorting class collection as param
        /// </summary>
        /// <param name="sortClass">
        /// Collection of sorting criteria 
        ///</param>
        public BDataGridComparer(List<BDataGridSortClass> sortClass)                    
        {
            _sortClasses = sortClass;
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="sortProperty">Property to sort on</param>
        /// <param name="sortDirection">Direction to sort</param>
        public BDataGridComparer(string sortProperty, SortDirection sortDirection)      
        {
            _sortClasses = new List<BDataGridSortClass>();
            _sortClasses.Add(new BDataGridSortClass(sortProperty, sortDirection));
        }

        /// <summary>
        /// Implementation of IComparer interface to compare to object
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(T x, T y)                                                    
        {
            if (SortClasses.Count == 0)
            {
                return 0;
            }
            return CheckSort(0, x, y);
        }

        /// <summary>
        /// Recursive function to do sorting
        /// </summary>
        /// <param name="sortLevel">Current level sorting at</param>
        /// <param name="myObject1"></param>
        /// <param name="myObject2"></param>
        /// <returns></returns>
        private int CheckSort(int sortLevel, T myObject1, T myObject2)                  
        {
            int returnVal = 0;
            if (SortClasses.Count - 1 >= sortLevel)
            {
                object valueOf1 = myObject1.GetType().GetProperty(SortClasses[sortLevel].SortProperty).GetValue(myObject1, null);
                object valueOf2 = myObject2.GetType().GetProperty(SortClasses[sortLevel].SortProperty).GetValue(myObject2, null);
                if (SortClasses[sortLevel].SortDirection == SortDirection.Ascending)
                {
                    returnVal = ((IComparable)valueOf1).CompareTo((IComparable)valueOf2);
                }
                else
                {
                    returnVal = ((IComparable)valueOf2).CompareTo((IComparable)valueOf1);
                }

                if (returnVal == 0)
                {
                    returnVal = CheckSort(sortLevel + 1, myObject1, myObject2);
                }
            }


            return returnVal;
        }
    }

    class BDataGridListSorter                                                           
    {
        public static IList<T> SortList<T>(IEnumerable<T> listToSort,
                                       List<string> sortExpression,
                                       List<SortDirection> sortDirection)               
        {
            var list = listToSort.ToList();
            //check parameters           
            if (sortExpression.Count != sortDirection.Count ||
                sortExpression.Count == 0 ||
                sortDirection.Count == 0)
            {
                throw new Exception("Invalid sort arguments!");
            }

            //get myComparer
            BDataGridComparer<T> myComparer = new BDataGridComparer<T>();
            for (int i = 0; i < sortExpression.Count; i++)
            {
                BDataGridSortClass sortClass = new BDataGridSortClass(sortExpression[i], sortDirection[i]);
                myComparer.SortClasses.Add(sortClass);
            }

            list.Sort(myComparer);
            return list;

        }

        public static IList<T> SortList<T>(IEnumerable<T> listToSort,
                                       string sortExpression,
                                       SortDirection sortDirection)                     
        {
            var list = listToSort.ToList();
            //check parameters
            if (string.IsNullOrEmpty(sortExpression)) // sortExpression == null || sortExpression == string.Empty || sortDirection == SortDirection.)
            {
                return list;
            }

            BDataGridComparer<T> myComparer = new BDataGridComparer<T>();
            myComparer.SortClasses.Add(new BDataGridSortClass(sortExpression, sortDirection));
            list.Sort(myComparer);
            return list;
        }
    }

    public class BDataGridColumn                                                        
    {
        private BUIElement mUie;
        private string mName;
        private Font mFont;
        private int mMinWidth;
        private int mWidth;
        private int mCustomMinWidth = -1;
        private int mCustomMaxWidth = -1;

        public string PropertyName                                                      
        {
            get;
            set;
        }

        public string DisplayName                                                       
        {
            get { return mName; }
            set
            {
                if (mName != value)
                {
                    mName = value;
                    ComputeMinWidth();
                }
            }
        }

        public Font Font                                                                
        {
            get { return mFont; }
            set
            {
                if (mFont != value)
                {
                    mFont = value;
                    ComputeMinWidth();
                }
            }
        }

        public bool IsSortByThis                                                        
        {
            get;
            set;
        }

        public int Width                                                                
        {
            get { return mWidth; }
            set 
            {
                mWidth = value;
                if (mWidth >= 0 && mWidth < mMinWidth)
                    mWidth = mMinWidth;
            }
        }

        public int MinWidth                                                             
        {
            get { return mCustomMinWidth > 0 ? mCustomMinWidth : mMinWidth; }
            set { mCustomMinWidth = value; }
        }

        public int MaxWidth
        {
            get { return mCustomMaxWidth; }
            set { mCustomMaxWidth = value; }
        }

        public int RealWidth                                                            
        {
            get;
            set;
        }

        public BDataGridColumn(BUIElement uie, string name, Font font)                  
        {
            mUie = uie;
            mName = name;
            mFont = font;
            ComputeMinWidth();
        }

        private void ComputeMinWidth()                                                  
        {
            mMinWidth = Math.Max(
                BDataGridConst.TAB_HEIGHT,
                (int)Math.Ceiling(Graphics.FromHwnd(mUie.Host.Handle).MeasureString(mName, mFont).Width + 20));

            if (mWidth < mMinWidth)
                mWidth = mMinWidth;
        }
    }

    public class BDataGridRow                                                           
    {
        public int      Index       { get; set; }
        public object   Content     { get; set; }
        public Point    Location    { get; set; }
    }

    public class BDataGrid<T>                                                           : BUIElement where T : IBdataGridItem
    {
        private Point mScalePoint;
        private int mCurrScaleDownWidth;
        private int mNextScaleDownWidth;
        private float mScaleDownAutoScaleSingleWidth;
        private Dictionary<int, int> mColumnDefaultWidths;

        private BScrollBar mScrollBar;
        private BDataGridColumn mScaleColumn;
        private BDataGridColumn mHoverColumn;
        private BDataGridColumn mMouseColumn;
        private BDataGridColumn mPressedColumn;

        private BDataGridRow mHoverRow;
        private BDataGridRow mPressedRow;
        private BDataGridRow mSelectedRow;

        private Color mColumnDefaultColor;
        private Color mColumnHoverColor;
        private Color mColumnPressedColor;
        private Brush mColumnDefaultBrush;
        private Brush mColumnHoverBrush;
        private Brush mColumnPressedBrush;

        private Color mRowDefaultColor;
        private Color mRowHoverColor;
        private Color mRowPressedColor;
        private SolidBrush mRowDefaultBrush;
        private SolidBrush mRowHoverBrush;
        private SolidBrush mRowPressedBrush;

        private Color mForeNormalColor;
        private Color mForeHoverColor;
        private Color mForePressedColor;
        private Color mForeDisenableColor;

        private SolidBrush mForeNormalBrush;
        private SolidBrush mForeHoverBrush;
        private SolidBrush mForePressedBrush;
        private SolidBrush mForeDisenableBrush;

        private WinFormInputHelper<BDataGridColumn> mColumSortInputHelper;
        private WinFormInputHelper<BDataGridColumn> mColumnScaleInputHelper;
        private WinFormInputHelper<BDataGridRow> mRowInputHelper;

        private List<BDataGridColumn> mColumns;
        public List<BDataGridColumn> Columns                                                    
        {
            get { return mColumns; }
        }

        public Dictionary<int, Func<Graphics, T, PointF, Size, int>> ColumsCustomPainters
        {
            get;
        } = new Dictionary<int, Func<Graphics, T, PointF, Size, int>>();

        private IEnumerable<T> mDataSource;
        public IEnumerable<T> DataSource                                                        
        {
            get { return mDataSource; }
            set 
            {
                if (mDataSource != value) 
                {
                    mDataSource = value;
                    // 反射取得类型
                    var columns = new List<BDataGridColumn>();
                    var itemType = typeof(T);
                    foreach (var property in itemType.GetProperties().Where(X => X.DeclaringType == itemType))
                    {
                        if (property.CanRead)
                        {
                            var defualtWidth = mColumnDefaultWidths.ContainsKey(columns.Count) ? mColumnDefaultWidths[columns.Count] : 50;
                            var displayName = Lang.Tran(TypeService.GetDescription(property)); 
                            var width = IDE.AppConfig.Custom.GetProperty<int>(string.Format("{1}_{0}_Width", columns.Count, Header), defualtWidth);
                            var sortBy = IDE.AppConfig.Custom.GetProperty<int>(string.Format("{0}_SortBy", Header), 0);

                            columns.Add(new BDataGridColumn(this, displayName, Host.Font) 
                            {
                                PropertyName = property.Name,
                                Width = width,
                                IsSortByThis = sortBy == columns.Count
                            });
                        }
                    }

                    mColumns = columns;
                }
            }
        }

        public Color ColumnDefaultColor                                                         
        {
            get { return mColumnDefaultColor; }
            set
            {
                if (mColumnDefaultColor != value)
                {
                    mColumnDefaultColor = value;
                    mColumnDefaultBrush = new SolidBrush(value);
                    Host.Invalidate();
                }
            }
        }

        public Color ColumnHoverColor                                                           
        {
            get { return mColumnHoverColor; }
            set
            {
                if (mColumnHoverColor != value)
                {
                    mColumnHoverColor = value;
                    mColumnHoverBrush = new SolidBrush(value);
                    Host.Invalidate();
                }
            }
        }

        public Color ColumnPressedColor                                                         
        {
            get { return mColumnPressedColor; }
            set
            {
                if (mColumnPressedColor != value)
                {
                    mColumnPressedColor = value;
                    mColumnPressedBrush = new SolidBrush(value);
                    Host.Invalidate();
                }
            }
        }

        public Color RowDefaultColor                                                            
        {
            get { return mRowDefaultColor; }
            set
            {
                if (mRowDefaultColor != value)
                {
                    mRowDefaultColor = value;
                    mRowDefaultBrush = new SolidBrush(value);
                    Host.Invalidate();
                }
            }
        }

        public Color RowHoverColor                                                              
        {
            get { return mRowHoverColor; }
            set
            {
                if (mRowHoverColor != value)
                {
                    mRowHoverColor = value;
                    mRowHoverBrush = new SolidBrush(value);
                    Host.Invalidate();
                }
            }
        }

        public Color RowPressedColor                                                            
        {
            get { return mRowPressedColor; }
            set
            {
                if (mRowPressedColor != value)
                {
                    mRowPressedColor = value;
                    mRowPressedBrush = new SolidBrush(value);
                    Host.Invalidate();
                }
            }
        }

        public Color ForeNormalColor                                                            
        {
            get { return mForeNormalColor; }
            set
            {
                if (mForeNormalColor != value)
                {
                    mForeNormalColor = value;
                    mForeNormalBrush = new SolidBrush(mForeNormalColor);
                    Host.Invalidate();
                }
            }
        }

        public Color ForeHoverColor                                                             
        {
            get { return mForeHoverColor; }
            set
            {
                if (mForeHoverColor != value)
                {
                    mForeHoverColor = value;
                    mForeHoverBrush = new SolidBrush(mForeHoverColor);
                    Host.Invalidate();
                }
            }
        }

        public Color ForePressedColor                                                           
        {
            get { return mForePressedColor; }
            set
            {
                if (mForePressedColor != value)
                {
                    mForePressedColor = value;
                    mForePressedBrush = new SolidBrush(mForePressedColor);
                    Host.Invalidate();
                }
            }
        }

        public Color ForeDisenableColor                                                         
        {
            get { return mForeDisenableColor; }
            set
            {
                if (mForeDisenableColor != value)
                {
                    mForeDisenableColor = value;
                    mForeDisenableBrush = new SolidBrush(mForeDisenableColor);
                    Host.Invalidate();
                }
            }
        }

        public Color ColumnDescrptionColor                                                      
        {
            get;
            set;
        }

        public T SelectedItem                                                                   
        {
            get { return mSelectedRow == null ? default : (T)mSelectedRow.Content; }
        }

        public BDataGridRow MouseRow                                                            
        {
            get { return mHoverRow; }
        }

        public BDataGridColumn MouseColumn                                                      
        {
            get { return mMouseColumn; }
        }

        public string Header                                                                    
        {
            get;
        }

        public int PageItemCount                                                                
        {
            get;
            set;
        } = 30;

        public BDataGrid(BControl host, string header)                                          
            : base(host)                                                                        
        {
            Header = header;
            mColumnDefaultWidths = new Dictionary<int, int>();

            ColumnDescrptionColor = Color.FromArgb(45, 45, 48);

            mScrollBar = new BScrollBar(host);

            mColumnScaleInputHelper = new WinFormInputHelper<BDataGridColumn>(host);
            mColumnScaleInputHelper.MouseEnter += mColumnInputHelper_MouseEnter;
            mColumnScaleInputHelper.MouseLeave += mColumnInputHelper_MouseLeave;
            mColumnScaleInputHelper.MouseDown += mColumnInputHelper_MouseDown;

            mColumSortInputHelper = new WinFormInputHelper<BDataGridColumn>(host);
            mColumSortInputHelper.MouseEnter += mColumSortInputHelper_MouseEnter;
            mColumSortInputHelper.MouseLeave += mColumSortInputHelper_MouseLeave;
            mColumSortInputHelper.MouseDown += mColumSortInputHelper_MouseDown;

            mRowInputHelper = new WinFormInputHelper<BDataGridRow>(host);
            mRowInputHelper.MouseEnter += mRowInputHelper_MouseEnter;
            mRowInputHelper.MouseLeave += mRowInputHelper_MouseLeave;
            mRowInputHelper.MouseClick += mRowInputHelper_MouseClick;
            mRowInputHelper.MouseDoubleClick += mRowInputHelper_MouseDoubleClick;
            mRowInputHelper.MouseDown += mRowInputHelper_MouseDown;

            SetColumnColor(IDE.AppConfig.Skin.AccentColor);
            //SetRowColor(IDE.AppConfig.Skin.AccentColor);

            RowDefaultColor = Color.Transparent;
            RowHoverColor = ControlPaint.Light(IDE.AppConfig.Skin.AccentColor);
            RowPressedColor = IDE.AppConfig.Skin.AccentColor;

            Host.MouseMove += Host_MouseMove;
            Host.MouseUp += Host_MouseUp;
        }

        void mRowInputHelper_MouseDown(WinFormMouseInputEventArgs<BDataGridRow> args)           
        {
            mPressedRow = args.Area.Tag;
            Host.Invalidate();
        }

        void mRowInputHelper_MouseClick(WinFormMouseInputEventArgs<BDataGridRow> args)          
        {
            mSelectedRow = args.Area.Tag;

            if (MouseClick != null)
                MouseClick(this, new BDataGridMouseClickEventArgs<T>(mSelectedRow, args.Args.Button, args.Args.Clicks, args.Args.X, args.Args.Y));

            Host.Invalidate();
        }

        void mRowInputHelper_MouseDoubleClick(WinFormMouseInputEventArgs<BDataGridRow> args)    
        {
            mSelectedRow = args.Area.Tag;

            if(MouseDoubleClick != null)
                MouseDoubleClick(this, new BDataGridMouseDoubleClickEventArgs<T>(mSelectedRow, args.Args.Button, args.Args.Clicks, args.Args.X, args.Args.Y));

            Host.Invalidate();
        }

        void mRowInputHelper_MouseLeave(WinFormMouseInputEventArgs<BDataGridRow> args)          
        {
            if (RowLeave != null)
                RowLeave(this, new BDataGridRowLeaveEventArgs<T>(mHoverRow, args.Args.Button, args.Args.Clicks, args.Args.X, args.Args.Y));

            mHoverRow = null;
            Host.Invalidate();
        }

        void mRowInputHelper_MouseEnter(WinFormMouseInputEventArgs<BDataGridRow> args)          
        {
            mHoverRow = args.Area.Tag;

            if(RowEnter != null)
                RowEnter(this, new BDataGridRowEnterEventArgs<T>(mHoverRow, args.Args.Button, args.Args.Clicks, args.Args.X, args.Args.Y));

            Host.Invalidate();
        }

        void mColumSortInputHelper_MouseDown(WinFormMouseInputEventArgs<BDataGridColumn> args)  
        {
            mPressedColumn = args.Area.Tag;
            var columnIndex = Columns.IndexOf(mPressedColumn);
            if (columnIndex < 0) return;

            var sortBy = $"{Header}_SortBy";
            if (IDE.AppConfig.Custom.GetProperty<int>(sortBy, 0) == columnIndex)
            {
                IDE.AppConfig.Custom.SetProperty($"{Header}_IsDescending", !IDE.AppConfig.Custom.GetProperty<bool>($"{Header}_IsDescending", false));
            }
            else
            {
                IDE.AppConfig.Custom.SetProperty(sortBy, columnIndex);
                IDE.AppConfig.Custom.SetProperty($"{Header}_IsDescending", false);
            }

            Host.Invalidate();
        }

        void mColumSortInputHelper_MouseLeave(WinFormMouseInputEventArgs<BDataGridColumn> args) 
        {
            mHoverColumn = null;

            Host.Invalidate();
        }

        void mColumSortInputHelper_MouseEnter(WinFormMouseInputEventArgs<BDataGridColumn> args) 
        {
            mHoverColumn = args.Area.Tag;

            Host.Invalidate();
        }

        void Host_MouseUp(object sender, MouseEventArgs e)                                      
        {
            mPressedRow = null;
            mPressedColumn = null;

            if (!mScalePoint.IsEmpty && e.Button == MouseButtons.Left)
            {
                mScalePoint = Point.Empty;
                Host.Cursor = Cursors.Default;
            }

            Host.Invalidate();
        }

        void Host_MouseMove(object sender, MouseEventArgs e)                                    
        {
            //var lastColumn = mMouseColumn;
            var doDraw = false;
            if (Columns != null && Columns.Count > 0)
            {
                var x = 0;
                //mMouseColumn = Columns[Columns.Count - 1];
                for (var i = 0; i < Columns.Count; i++)
                {
                    var column = Columns[i];
                    x = x + column.RealWidth;
                    if (x >= e.X)
                    {
                        if (mMouseColumn != Columns[i])
                        {
                            mMouseColumn = Columns[i];
                            doDraw = true;
                        }
                        break;
                    }
                    x = x + 1;
                }
            }

             
            if (!mScalePoint.IsEmpty && e.Button == MouseButtons.Left)
            {
                var value = e.Location.X - mScalePoint.X;

                if (value == 0) return;

                var currColumnIndex = Columns.IndexOf(mScaleColumn);
                var nextColumnIndex = currColumnIndex + 1;

                BDataGridColumn currColumn = Columns[currColumnIndex];
                BDataGridColumn nextColumn = Columns[nextColumnIndex];

                var tempCurrWidth = (mCurrScaleDownWidth < 0 ? (int)Math.Abs(mScaleDownAutoScaleSingleWidth * mCurrScaleDownWidth) : mCurrScaleDownWidth) + value;
                var tempNextWidth = (mNextScaleDownWidth < 0 ? (int)Math.Abs(mScaleDownAutoScaleSingleWidth * mNextScaleDownWidth) : mNextScaleDownWidth) - value;

                if (tempCurrWidth < currColumn.MinWidth) return;
                if (tempNextWidth < nextColumn.MinWidth) return;

                if (currColumn.MaxWidth != -1 && tempCurrWidth > currColumn.MaxWidth) return;
                if (nextColumn.MaxWidth != -1 && tempNextWidth < nextColumn.MaxWidth) return;

                if (mCurrScaleDownWidth > 0) currColumn.Width = tempCurrWidth;
                if (mNextScaleDownWidth > 0) nextColumn.Width = tempNextWidth;

                IDE.AppConfig.Custom.SetProperty($"{Header}_{currColumnIndex}_Width", currColumn.Width);
                IDE.AppConfig.Custom.SetProperty($"{Header}_{nextColumnIndex}_Width", nextColumn.Width);

                doDraw = true;
            }

            if(doDraw) Host.Invalidate();
        }

        void mColumnInputHelper_MouseDown(WinFormMouseInputEventArgs<BDataGridColumn> args)     
        {
            if (args.Args.Button == MouseButtons.Left)
            {
                mScalePoint = args.Args.Location;
                mScaleColumn = args.Area.Tag;
                BDataGridColumn nextColumn = Columns[Columns.IndexOf(mScaleColumn) + 1];
                mCurrScaleDownWidth = args.Area.Tag.Width;
                mNextScaleDownWidth = nextColumn.Width;

                var startX = Location.X + Padding.Left;
                var width = Math.Max(Columns.Count, Host.Width - startX - Padding.Right);
                var autoScaleColumns = Columns.Where(X => X.Width < 0);
                var autoScaleMinWidth = autoScaleColumns.Sum(X => X.MinWidth);
                var autoScaleTotalRatio = Math.Abs(autoScaleColumns.Sum(X => X.Width));
                var autoScaleTotalWidth = width - Columns.Where(X => X.Width > 0).Sum(X => X.Width);
                var autoScaleSingleWidth = autoScaleTotalWidth / (float)autoScaleTotalRatio;

                mScaleDownAutoScaleSingleWidth = autoScaleSingleWidth;
            }
        }

        void mColumnInputHelper_MouseLeave(WinFormMouseInputEventArgs<BDataGridColumn> args)    
        {
            if (!mScalePoint.IsEmpty && args.Args.Button == MouseButtons.Left)
            {
            }
            else
            {
                Host.Cursor = Cursors.Default;
            }
        }

        void mColumnInputHelper_MouseEnter(WinFormMouseInputEventArgs<BDataGridColumn> args)    
        {
            var c = args.Area.Tag;
            if (c.MaxWidth - c.RealWidth > 0 || c.RealWidth - c.MinWidth > 0)
            {
                Host.Cursor = Cursors.SizeWE;
            }
        }

        protected override void OnPaint(PaintEventArgs e)                                       
        {

            if (Columns == null) return;

            //mColumSortInputHelper.ClearRegions();
            //mColumnScaleInputHelper.ClearRegions();
            //mRowInputHelper.ClearRegions();

            var startX = Location.X + Padding.Left;
            var startY = Location.Y + Padding.Top;
            var width = Math.Max(Columns.Count, Host.Width - startX - Padding.Right);
            var height = Host.Height - startY - Padding.Bottom;

            List<Tuple<BDataGridColumn, Rectangle>> columnInputAreas = new List<Tuple<BDataGridColumn, Rectangle>>();

            // 首先找到所有自动拉伸项
            var autoScaleColumns = Columns.Where(X => X.Width < 0);
            var autoScaleTotalRatio = Math.Abs(autoScaleColumns.Sum(X => X.Width));
            var autoScaleOneRatioWidth = (width - Columns.Where(X => X.Width >= 0).Sum(X => X.Width)) / Math.Min(1f, autoScaleTotalRatio);

            var sortBy = IDE.AppConfig.Custom.GetProperty<int>($"{Header}_SortBy", 0);
            var isDescending = IDE.AppConfig.Custom.GetProperty<bool>($"{Header}_IsDescending", false);

            var dbrush = new SolidBrush(ColumnDescrptionColor);
            BDataGridColumn sortColumn = null;
            var columnStartX = startX;
            for (var i = 0; i < Columns.Count; i++) 
            {
                var column = Columns[i];
                var columnWidth = column.Width >= 0 ? column.Width : (int)Math.Abs(column.Width * autoScaleOneRatioWidth);

                var rect = new Rectangle(columnStartX, startY, columnWidth, BDataGridConst.TAB_HEIGHT);
                var brush = mColumnDefaultBrush;
                if (mPressedColumn == column)
                    brush = mColumnPressedBrush;
                else if (mScalePoint.IsEmpty && mHoverColumn == column)
                    brush = mColumnHoverBrush;

                e.Graphics.FillRectangle(
                    brush, 
                    rect);

                column.RealWidth = columnWidth;

                mColumSortInputHelper.AddRegion(column, rect);

                var textSize = e.Graphics.MeasureString(column.DisplayName, column.Font);
                e.Graphics.DrawString(
                    column.DisplayName,
                    Host.Font,
                    dbrush,
                    new PointF(3 + rect.Left, (rect.Height - textSize.Height) / 2));

                // 绘制箭头
                if (sortBy == i) 
                {
                    e.Graphics.FillTriangle(
                        IDE.AppConfig.Skin.BackBrush,
                        new RectangleF(
                            3 + rect.Left + textSize.Width + (isDescending ? 1 : 0),
                            rect.Top + (rect.Height - 6) / 2,
                            12 - (isDescending ? 2 : 0),
                            6 - (isDescending ? 1f : 0)),
                            isDescending ? 180 : 0);

                    sortColumn = column;
                }


                columnInputAreas.Add(
                    new Tuple<BDataGridColumn,Rectangle>(
                        column, 
                        new Rectangle(columnStartX + columnWidth - 1, startY, 3, BDataGridConst.TAB_HEIGHT)));

                columnStartX = columnStartX + columnWidth + 1;
            }

            var rowlist = (IList<T>)mDataSource;
            rowlist = BDataGridListSorter.SortList(
                rowlist, 
                sortColumn.PropertyName,
                isDescending ? SortDirection.Descending : SortDirection.Ascending);

            var rowStartIndex = mScrollBar.Depth / (BDataGridConst.ITEM_HEIGHT + 1);
            var rowShowCount = (height - BDataGridConst.TAB_HEIGHT) / (BDataGridConst.ITEM_HEIGHT + 1) + 1;
            var clipRect = new Rectangle(startX, BDataGridConst.TAB_HEIGHT + 1, width, height - 1 - BDataGridConst.TAB_HEIGHT);
            e.Graphics.SetClip(clipRect);

            int rowStartY = BDataGridConst.TAB_HEIGHT;
            //for (var rowIndex = rowStartIndex; rowIndex < rowStartIndex + rowShowCount; rowIndex++)
            //var endIndex = Math.Min(rowlist.Count, mCurrentItemIndex + PageItemCount);
            for (var rowIndex = 0; rowIndex < rowlist.Count; rowIndex++)
            {
                var rowStartX = startX;
                var row = rowlist[rowIndex];
                var maxRowHeight = BDataGridConst.ITEM_HEIGHT + 1;
                var rowRect = Rectangle.Empty;
                var doDraw = false;

                // 计算行高度
                for (var i = 0; i < Columns.Count; i++)
                {
                    var column = Columns[i];
                    var item = row.GetColumnItem(i);
                    if (item != null)
                    {
                        var tempItemHeight = 0;
                        var customPainter = ColumsCustomPainters.ContainsKey(i) ? ColumsCustomPainters[i] : null;
                        if (customPainter == null)
                        {
                            var itemStringArray = StringHelper.CheckLength(
                                column.Font,
                                item.ToString(),
                                column.RealWidth);

                            tempItemHeight = itemStringArray.Length * (BDataGridConst.ITEM_HEIGHT + 1);
                        }
                        else
                        {
                            tempItemHeight = customPainter.Invoke(null, row, Point.Empty, Size.Empty) + 1;
                        }

                        if (maxRowHeight < tempItemHeight)
                            maxRowHeight = tempItemHeight;
                    }

                    rowRect = new Rectangle(rowStartX, rowStartY - mScrollBar.Depth, width, maxRowHeight);
                    doDraw = !(rowRect.Bottom < clipRect.Top || rowRect.Top > clipRect.Bottom);
                }

                // 绘制底部间隔和选中状态
                SolidBrush brush = null;
                SolidBrush foreBrush = null;
                if (doDraw)
                {
                    var drawRect = false;

                    brush = mRowDefaultBrush;
                    foreBrush = mForeNormalBrush;
                    if ((mPressedRow != null && row.Equals(mPressedRow.Content)) ||
                        (mSelectedRow != null && row.Equals(mSelectedRow.Content)))
                    {
                        brush = mRowPressedBrush;
                        foreBrush = mForePressedBrush;
                        drawRect = true;
                    }
                    else if (mScalePoint.IsEmpty && (mHoverRow != null && row.Equals(mHoverRow.Content)))
                    {
                        brush = mRowHoverBrush;
                        foreBrush = mForeHoverBrush;
                        drawRect = true;
                    }

                    //if (rowIndex % 2 == 0)
                    //    e.Graphics.FillRectangle(IDE.AppConfig.Skin.BufferBrush, rowRect);

                    if (drawRect)
                        e.Graphics.FillRectangle(brush, rowRect);
                }

                if (doDraw)
                {
                    // 绘制当前行
                    for (var i = 0; i < Columns.Count; i++)
                    {
                        var column = Columns[i];
                        var item = row.GetColumnItem(i);// column.Property.GetValue(row);
                        if (item != null)
                        {
                            var customPainter = ColumsCustomPainters.ContainsKey(i) ? ColumsCustomPainters[i] : null;
                            if (customPainter == null)
                            {
                                var itemStringArray = StringHelper.CheckLength(
                                    column.Font,
                                    item.ToString(),
                                    column.RealWidth);

                                var tempItemHeight = itemStringArray.Length * (BDataGridConst.ITEM_HEIGHT + 1);
                                if (maxRowHeight < tempItemHeight)
                                    maxRowHeight = tempItemHeight;

                                if (doDraw)
                                {
                                    var yOffset = 1; // rowRect.Height - tempItemHeight;
                                    var tempStartY = rowStartY;
                                    foreach (var str in itemStringArray)
                                    {
                                        var itemSize = e.Graphics.MeasureString(str, column.Font);
                                        e.Graphics.DrawString(
                                            str,
                                            column.Font,
                                            foreBrush,
                                            new PointF(rowStartX, rowStartY + (BDataGridConst.ITEM_HEIGHT - itemSize.Height) / 2 - mScrollBar.Depth + yOffset));

                                        rowStartY = rowStartY + BDataGridConst.ITEM_HEIGHT + 1;
                                    }

                                    rowStartY = tempStartY;
                                }
                            }
                            else
                            {
                                if (doDraw)
                                {
                                    var tempItemHeight = customPainter.Invoke(e.Graphics, row, new PointF(rowStartX, rowStartY - mScrollBar.Depth), new Size(column.RealWidth, rowRect.Height)) + 1;
                                    if (maxRowHeight < tempItemHeight)
                                        maxRowHeight = tempItemHeight;
                                }
                            }
                        }

                        rowStartX = rowStartX + column.RealWidth + 1;
                    }

                    // 添加行鼠标选项
                    mRowInputHelper.AddRegion(
                        new BDataGridRow()
                        {
                            Index = rowIndex,
                            Content = row,
                            Location = new Point(startX, rowStartY - mScrollBar.Depth)
                        },
                        rowRect);
                }

                rowStartY = rowStartY + maxRowHeight;
            }

            e.Graphics.ResetClip();
            dbrush.Dispose();
            foreach (var item in columnInputAreas)
                mColumnScaleInputHelper.AddRegion(item.Item1, item.Item2);

            mScrollBar.Location = new Point(Location.X + Size.Width - mScrollBar.Width - 1, Location.Y + BDataGridConst.TAB_HEIGHT + 1);
            mScrollBar.Length = Size.Height - BDataGridConst.TAB_HEIGHT - 2;
            mScrollBar.MaxDepth = rowStartY - BDataGridConst.TAB_HEIGHT;
            mScrollBar.Paint(e);
     
            base.OnPaint(e);
        }

        public void SetColumnDefaultWidth(int index, int width)                                 
        {
            mColumnDefaultWidths[index] = width;
        }

        private void SetColumnColor(Color color)                                                
        {
            ColumnDefaultColor = color; // Color.FromArgb((byte)(30 / 255f * color.A), color.R, color.G, color.B);
            ColumnHoverColor = Color.FromArgb(color.A, color.R + 28, color.G + 28, color.B + 28); //   //(byte)(160 / 255f * color.A), color.R, color.G, color.B);
            ColumnPressedColor = color; // Color.FromArgb((byte)(225 / 255f * color.A), color.R, color.G, color.B);
        }

        private void SetRowColor(Color color)                                                   
        {
            RowDefaultColor = Host.BackColor; // Color.FromArgb((byte)(30 / 255f * color.A), color.R, color.G, color.B);
            RowHoverColor = Color.FromArgb(color.A, color.R + 28, color.G + 28, color.B + 28); //   //(byte)(160 / 255f * color.A), color.R, color.G, color.B);
            RowPressedColor = color; // Color.FromArgb((byte)(225 / 255f * color.A), color.R, color.G, color.B);
        }

        public event EventHandler<BDataGridMouseClickEventArgs<T>>          MouseClick;
        public event EventHandler<BDataGridMouseDoubleClickEventArgs<T>>    MouseDoubleClick;
        public event EventHandler<BDataGridRowEnterEventArgs<T>>            RowEnter;
        public event EventHandler<BDataGridRowLeaveEventArgs<T>>            RowLeave;

    }
}
