namespace ZigmaWeb.Exception
{
    class BussinesException: System.Exception
    {
        public bool ShowMessage { get; set; }

        public BussinesException() : base ()
        {
            ShowMessage = false;
        }

        public BussinesException(string message) : base (message)
        {
            ShowMessage = true;
        }
    }
}
