using System;
using System.Collections.Generic;
using System.Text;

namespace BugzillaWebDriver.Configuration
{
    public class BugzillaSettings
    {
        public string Browser { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Website { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }
    }
}
