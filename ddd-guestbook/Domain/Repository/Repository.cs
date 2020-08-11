using System;
using System.Collections.Generic;
using Domain.Model;


namespace Domain.Repository {
    public interface IGuestbookRepository {
        public IEnumerable<SavedEntry> Get(int count);
        public void Add(Entry entry);
    }
}
