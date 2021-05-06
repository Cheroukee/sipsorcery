using System;
using System.Collections.Generic;
using System.Text;
using SIPSorcery.SIP.App;

namespace SIPSorcery.SoftPhone.Signalling
{
    class BaseSIPAccount : ISIPAccount
    {
        public BaseSIPAccount(string userName, string password)
        {
            SIPPassword = password;
            SIPUsername = userName;
        }

        public string ID => "";

        public string SIPUsername { get; private set; }

        public string SIPPassword { get; private set; }

        public string HA1Digest => "";

        public string SIPDomain => "";

        public bool IsDisabled => false;
    }
}
