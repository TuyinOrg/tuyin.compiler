namespace libtui.controls
{
    public interface IInputElement
    {
        bool IsFocused { get; }

        bool AllowFocus { get; }

        bool AllowFileDrop { get; }

        void OnGetFocus();

        void OnLostFocus();

        void OnMosueEnter(MouseEventArgs e);

        void OnMouseLeave(MouseEventArgs e);

        void OnMouseMove(MouseEventArgs e);
        
        void OnMouseUp(MouseEventArgs e);

        void OnMouseDown(MouseEventArgs e);

        void OnMouseClick(MouseEventArgs e);

        void OnMouseWheel(MouseEventArgs e);

        void OnSizeChanged(SizeChangeEventArgs e);

        void OnCharInput(CharEventArgs e);

        void OnKeyDown(KeyEventArgs e);

        void OnKeyUp(KeyEventArgs e);

        void OnKeyPress(KeyEventArgs e);

        void FileDrop(string[] fileNames);
    }
}
