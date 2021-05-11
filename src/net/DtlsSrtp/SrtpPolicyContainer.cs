//-----------------------------------------------------------------------------
// Filename: SrtpPolicyContainer.cs
//
// Description:
//
// Author(s):
// Jean-Philippe Fournier (fournierje.info@gmail.com)
//
// History:
// 11 may 2021 : created
//
// License:
// BSD 3-Clause "New" or "Revised" License, see included LICENSE.md file.
//-----------------------------------------------------------------------------
namespace SIPSorcery.Net
{
    public class SrtpPolicyContainer
    {
        public byte[] MasterClientKey { get; }
        public byte[] MasterClientSalt { get; }
        public byte[] MasterServerKey { get; }
        public byte[] MasterServerSalt { get; }
        public SrtpPolicy SrtpPolicy { get; }
        public SrtpPolicy SrtcpPolicy { get; }

        public SrtpPolicyContainer(SrtpPolicy srtpPolicy, SrtpPolicy srtcpPolicy, byte[] masterServerKey, byte[] masterServerSalt, byte[] masterClientKey, byte[] masterClientSalt)
        {
            MasterServerKey = masterServerKey;
            MasterServerSalt = masterServerSalt;
            MasterClientKey = masterClientKey;
            MasterClientSalt = masterClientSalt;
            SrtpPolicy = srtpPolicy;
            SrtcpPolicy = srtcpPolicy;
        }
    }
}