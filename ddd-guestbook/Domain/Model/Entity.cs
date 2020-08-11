using System;


namespace Domain.Model {
    public class PostId {
        public readonly int Value;

        public PostId(int value) {
            if(value < 0)
                throw new ArgumentOutOfRangeException("postid must be greater than or equal to 0");
            this.Value = value;
        }

        public static PostId operator+(PostId self, int value) {
            return new PostId(self.Value + value);
        }
    }

    public class Name {
        public const int MaxLength = 32;
        public readonly string Value;

        public Name(string value) {
            if(value == null)
                throw new ArgumentNullException("name cannot be null");
            if(value.Length == 0 || value.Length > MaxLength)
                throw new ArgumentOutOfRangeException($"name cannot be empty or more than {MaxLength} characters");
            this.Value = value;
        }
    }

    public class Message {
        public const int MaxLength = 1024;
        public readonly string Value;

        public Message(string value) {
            if(value == null)
                throw new ArgumentNullException("name cannot be null");
            if(value.Length == 0 || value.Length > MaxLength)
                throw new ArgumentOutOfRangeException($"name cannot be empty or more than {MaxLength} characters");
            this.Value = value;
        }
    }

    public class Timestamp {
        public readonly DateTime Value;

        public Timestamp(DateTime value) {
            if(value == null)
                throw new ArgumentNullException("timestamp cannot be null");
            this.Value = value;
        }

        public double UnixTime {
            get {
                return (this.Value - DateTime.UnixEpoch).TotalSeconds;
            }
        }

        public static Timestamp FromUnixTime(double unixtime) {
            return new Timestamp(DateTime.UnixEpoch.AddSeconds(unixtime));
        }

        public static Timestamp UtcNow {
            get {
                return new Timestamp(DateTime.UtcNow);
            }
        }
    }

    public class IPAddress {
        public readonly System.Net.IPAddress Value;

        public IPAddress(System.Net.IPAddress value) {
            if(value == null)
                throw new ArgumentNullException("ipaddress cannot be null");
            this.Value = value;
        }

        public static IPAddress FromString(string value) {
            return new IPAddress(System.Net.IPAddress.Parse(value));
        }
    }
}
