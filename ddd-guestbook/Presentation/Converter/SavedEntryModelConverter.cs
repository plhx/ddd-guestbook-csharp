using System;
using System.Collections.Generic;
using Application.Model;


namespace Presentation.Converter {
    public class SavedEntryModelConverter {
        private readonly SavedEntryModel SavedEntry;

        public SavedEntryModelConverter(SavedEntryModel savedEntry) {
            this.SavedEntry = savedEntry;
        }

        public Dictionary<string, object> ToDictionary() {
            return new Dictionary<string, object> {
                {"post_id", this.SavedEntry.PostId.Value},
                {"name", this.SavedEntry.Name.Value},
                {"message", this.SavedEntry.Message.Value},
                {"timestamp", this.SavedEntry.Timestamp.UnixTime},
            };
        }
    }
}
