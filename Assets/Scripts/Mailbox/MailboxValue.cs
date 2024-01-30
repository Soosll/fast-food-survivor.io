namespace Mailbox
{
    public sealed class MailboxValue<T>
    {
        private readonly MailboxEvent _event = new();
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                _event.Invoke();
            }
        }

        public MailboxEvent.Listener GetListener(bool isDefaultUnhandled = false)
        {
            return _event.GetListener(isDefaultUnhandled);
        }

        public static implicit operator T(MailboxValue<T> mailboxValue) => mailboxValue.Value;
    }
}