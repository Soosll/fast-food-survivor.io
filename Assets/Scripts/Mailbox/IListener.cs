namespace Mailbox
{
    public interface IListener
    {
        public bool IsUnhandled { get; }

        public void MarkAsHandled();
    }
}