namespace Mailbox
{
    public class MailboxEvent
    {
        protected int _version;
        
        public void Invoke()
        {
            unchecked
            {
                _version++;
            }
        }

        public Listener GetListener(bool isDefaultUnhandled)
        {
            return new Listener(this, isDefaultUnhandled);
        }
        
        public sealed class Listener : IListener
        {
            private readonly MailboxEvent _mailboxEvent;
            private int _version;

            public bool IsUnhandled => _version != _mailboxEvent._version;
            
            private Listener() { }

            public Listener(MailboxEvent mailboxEvent, bool isDefaultUnhandled)
            {
                _mailboxEvent = mailboxEvent;
                _version = _mailboxEvent._version;

                if (isDefaultUnhandled)
                {
                    unchecked
                    {
                        _version--;
                    }
                }
            }

            public void MarkAsHandled()
            {
                _version = _mailboxEvent._version;
            }
        }
    }
}