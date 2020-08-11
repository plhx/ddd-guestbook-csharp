using System;


namespace Presentation.Request {
    public class GuestbookGetRequest {
        public int count { get; set; }
    }

    public class GuestbookAddRequest {
        public int count { get; set; }
        public string name { get; set; }
        public string message { get; set; }
    }
}
