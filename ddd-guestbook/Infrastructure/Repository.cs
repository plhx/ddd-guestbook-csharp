using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;

using Domain.Model;
using Domain.Repository;


namespace Infrastructure {
    public class MemoryGuestbookRepository : IGuestbookRepository {
        private List<SavedEntry> Entries = new List<SavedEntry>();

        public void Add(Entry entry) {
            var savedEntry = SavedEntry.FromEntry(this.NewPostId(), entry);
            this.Entries.Add(savedEntry);
        }

        public IEnumerable<SavedEntry> Get(int count) {
            count = Math.Min(this.Entries.Count, count);
            return this.Entries.GetRange(this.Entries.Count - count, count);
        }

        public PostId NewPostId() {
            if(this.Entries.Count == 0)
                return new PostId(0);
            return this.Entries.Last().PostId + 1;
        }
    }

    public class SQLiteGuestbookRepository : IGuestbookRepository {
        public SQLiteGuestbookRepository() {
            SQLitePCL.Batteries.Init();
        }

        public SqliteConnection MemoryConnection() {
            var connectionBuilder = new SqliteConnectionStringBuilder {
                DataSource = ":memory:"
            };
            return new SqliteConnection(connectionBuilder.ToString());
        }

        public void Add(Entry entry) {
            using(var connection = this.MemoryConnection()) {
                connection.Open();
                using(var transaction = connection.BeginTransaction()) {
                    try {
                        using(var command = connection.CreateCommand()) {
                            command.CommandText = "CREATE TABLE IF NOT EXISTS entry ("
                                + "post_id INTEGER PRIMARY KEY AUTOINCREMENT,"
                                + "name TEXT NOT NULL,"
                                + "message TEXT NOT NULL,"
                                + "timestamp REAL NOT NULL,"
                                + "ip_address TEXT"
                                + ")";
                            command.ExecuteNonQuery();
                        }
                        using(var command = connection.CreateCommand()) {
                            command.CommandText = "INSERT INTO entry"
                                + "(name, message, timestamp, ip_address)"
                                + "VALUES"
                                + "(@name, @message, @timestamp, @ip_address)";
                            command.Parameters.Add(new SqliteParameter("@name", entry.Name.Value));
                            command.Parameters.Add(new SqliteParameter("@message", entry.Message.Value));
                            command.Parameters.Add(new SqliteParameter("@timestamp", entry.Timestamp.UnixTime));
                            command.Parameters.Add(new SqliteParameter("@ip_address", entry.IPAddress.Value.ToString()));
                            command.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch(Exception e) {
                        transaction.Rollback();
#if DEBUG
                        throw e;
#endif
                    }
                }
            }
        }

        public IEnumerable<SavedEntry> Get(int count) {
            List<SavedEntry> result = new List<SavedEntry>();
            using(var connection = this.MemoryConnection()) {
                connection.Open();
                using(var transaction = connection.BeginTransaction())
                using(var command = connection.CreateCommand()) {
                    command.CommandText = "SELECT"
                        + "post_id, name, message, timestamp, ip_address"
                        + "FROM entry ORDER BY post_id DESC";
                    try {
                        using(var reader = command.ExecuteReader()) {
                            while(reader.Read()) {
                                var postId = new PostId((int)reader["post_id"]);
                                var name = new Name((string)reader["name"]);
                                var message = new Message((string)reader["message"]);
                                var timestamp = Timestamp.FromUnixTime((double)reader["timestamp"]);
                                var ipAddress = IPAddress.FromString((string)reader["ip_address"]);
                                result.Add(new SavedEntry(postId, name, message, timestamp, ipAddress));
                            }
                        }
                    }
                    catch(Exception) {
                    }
                }
            }
            return result;
        }
    }
}
