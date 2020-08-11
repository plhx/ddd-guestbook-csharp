using System;


namespace Domain.Model {
    public class Entry {
        public readonly Name Name;
        public readonly Message Message;
        public readonly Timestamp Timestamp;
        public readonly IPAddress IPAddress;

        public Entry(Name name, Message message, Timestamp timestamp, IPAddress ipAddress) {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Message = message ?? throw new ArgumentNullException(nameof(message));
            this.Timestamp = timestamp ?? throw new ArgumentNullException(nameof(timestamp));
            this.IPAddress = ipAddress ?? throw new ArgumentNullException(nameof(ipAddress));
        }
    }

    public class SavedEntry : Entry {
        public readonly PostId PostId;

        public SavedEntry(PostId postId, Name name, Message message, Timestamp timestamp, IPAddress ipAddress)
        : base(name, message, timestamp, ipAddress) {
            this.PostId = postId ?? throw new ArgumentNullException(nameof(postId));
        }

        public static SavedEntry FromEntry(PostId postId, Entry entry) {
            return new SavedEntry(postId, entry.Name, entry.Message, entry.Timestamp, entry.IPAddress);
        }
    }
}
