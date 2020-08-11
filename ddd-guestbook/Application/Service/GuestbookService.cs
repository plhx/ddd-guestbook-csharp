using System;
using System.Linq;
using System.Collections.Generic;

using Application.Model;
using Application.Command;
using Domain.Model;
using Domain.Repository;


namespace Application.Service {
    public class GuestbookService {
        private readonly IGuestbookRepository Repository;

        public GuestbookService(IGuestbookRepository repository) {
            this.Repository = repository ?? throw new ArgumentNullException("repository cannot be null");
        }

        public IEnumerable<SavedEntryModel> Get(GuestbookGetCommand command) {
            return this.Repository.Get(command.Count).Select(
                entry => new SavedEntryModel(entry)
            );
        }

        public void Add(GuestbookAddCommand command) {
            this.Repository.Add(new Entry(
                command.Name,
                command.Message,
                Timestamp.UtcNow,
                command.IPAddress
            ));
        }
    }
}
