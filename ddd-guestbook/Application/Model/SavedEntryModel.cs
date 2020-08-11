using System;
using System.Collections.Generic;
using Domain.Model;


namespace Application.Model {
    public class SavedEntryModel {
        public readonly PostId PostId;
        public readonly Name Name;
        public readonly Message Message;
        public readonly Timestamp Timestamp;
        public readonly IPAddress IPAddress;

        public SavedEntryModel(
            PostId postId,
            Name name,
            Message message,
            Timestamp timestamp,
            IPAddress ipAddress
        ) {
            this.PostId = postId ?? throw new ArgumentNullException(nameof(postId));
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Message = message ?? throw new ArgumentNullException(nameof(message));
            this.Timestamp = timestamp ?? throw new ArgumentNullException(nameof(timestamp));
            this.IPAddress = ipAddress ?? throw new ArgumentNullException(nameof(ipAddress));
        }

        public SavedEntryModel(SavedEntry entry) : this(
            entry.PostId,
            entry.Name,
            entry.Message,
            entry.Timestamp,
            entry.IPAddress
        ) {

        }
    }
}
