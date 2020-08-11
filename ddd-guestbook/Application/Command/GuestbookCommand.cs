using System;
using Domain.Model;


namespace Application.Command {
    public class GuestbookGetCommand {
        public readonly int Count;

        public GuestbookGetCommand(int count) {
            this.Count = count;
        }
    }

    public class GuestbookAddCommand {
        public readonly Name Name;
        public readonly Message Message;
        public readonly IPAddress IPAddress;

        public GuestbookAddCommand(Name name, Message message, IPAddress ipAddress) {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Message = message ?? throw new ArgumentNullException(nameof(message));
            this.IPAddress = ipAddress ?? throw new ArgumentNullException(nameof(ipAddress));
        }
    }
}
