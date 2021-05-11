//-----------------------------------------------------------------------------
// Filename: SrtpPacketTransformer.cs
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
    public sealed class SRTPPacketTransformer
    {
        private readonly IPacketTransformer m_srtpEncoder;
        private readonly IPacketTransformer m_srtpDecoder;
        private readonly IPacketTransformer m_srtcpEncoder;
        private readonly IPacketTransformer m_srtcpDecoder;

        private readonly SrtpPolicyContainer m_srtpPolicyContainer;

        private readonly bool m_isClient;

        public SRTPPacketTransformer(bool isClient, SrtpPolicyContainer srtpPolicyContainer)
        {
            m_srtpPolicyContainer = srtpPolicyContainer;
            m_isClient = isClient;
            m_srtpDecoder = GenerateRtpDecoder();
            m_srtpEncoder = GenerateRtpEncoder();
            m_srtcpDecoder = GenerateRtcpDecoder();
            m_srtcpEncoder = GenerateRtcpEncoder();
        }

        private byte[] GetMasterServerKey()
        {
            return m_srtpPolicyContainer.MasterServerKey;
        }

        private byte[] GetMasterServerSalt()
        {
            return m_srtpPolicyContainer.MasterServerSalt;
        }

        private byte[] GetMasterClientKey()
        {
            return m_srtpPolicyContainer.MasterClientKey;
        }

        private byte[] GetMasterClientSalt()
        {
            return m_srtpPolicyContainer.MasterClientSalt;
        }

        private SrtpPolicy GetSrtpPolicy()
        {
            return m_srtpPolicyContainer.SrtpPolicy;
        }

        private SrtpPolicy GetSrtcpPolicy()
        {
            return m_srtpPolicyContainer.SrtcpPolicy;
        }

        private IPacketTransformer GenerateRtpEncoder()
        {
            return GenerateTransformer(m_isClient, true);
        }

        private IPacketTransformer GenerateRtpDecoder()
        {
            //Generate the reverse result of "GenerateRtpEncoder"
            return GenerateTransformer(!m_isClient, true);
        }

        private IPacketTransformer GenerateRtcpEncoder()
        {
            return GenerateTransformer(m_isClient, false);
        }

        private IPacketTransformer GenerateRtcpDecoder()
        {
            //Generate the reverse result of "GenerateRctpEncoder"
            return GenerateTransformer(!m_isClient, false);
        }

        private IPacketTransformer GenerateTransformer(bool isClient, bool isRtp)
        {
            SrtpTransformEngine engine = null;
            if (!isClient)
            {
                engine = new SrtpTransformEngine(GetMasterServerKey(), GetMasterServerSalt(), GetSrtpPolicy(), GetSrtcpPolicy());
            }
            else
            {
                engine = new SrtpTransformEngine(GetMasterClientKey(), GetMasterClientSalt(), GetSrtpPolicy(), GetSrtcpPolicy());
            }

            if (isRtp)
            {
                return engine.GetRTPTransformer();
            }
            else
            {
                return engine.GetRTCPTransformer();
            }
        }

        public byte[] UnprotectRTP(byte[] packet, int offset, int length)
        {
            lock (m_srtpDecoder)
            {
                return m_srtpDecoder.ReverseTransform(packet, offset, length);
            }
        }

        public int UnprotectRTP(byte[] payload, int length, out int outLength)
        {
            var result = UnprotectRTP(payload, 0, length);

            if (result == null)
            {
                outLength = 0;
                return -1;
            }

            System.Buffer.BlockCopy(result, 0, payload, 0, result.Length);
            outLength = result.Length;

            return 0; //No Errors
        }

        public byte[] ProtectRTP(byte[] packet, int offset, int length)
        {
            lock (m_srtpEncoder)
            {
                return m_srtpEncoder.Transform(packet, offset, length);
            }
        }

        public int ProtectRTP(byte[] payload, int length, out int outLength)
        {
            var result = ProtectRTP(payload, 0, length);

            if (result == null)
            {
                outLength = 0;
                return -1;
            }

            System.Buffer.BlockCopy(result, 0, payload, 0, result.Length);
            outLength = result.Length;

            return 0; //No Errors
        }

        public byte[] UnprotectRTCP(byte[] packet, int offset, int length)
        {
            lock (m_srtcpDecoder)
            {
                return m_srtcpDecoder.ReverseTransform(packet, offset, length);
            }
        }

        public int UnprotectRTCP(byte[] payload, int length, out int outLength)
        {
            var result = UnprotectRTCP(payload, 0, length);
            if (result == null)
            {
                outLength = 0;
                return -1;
            }

            System.Buffer.BlockCopy(result, 0, payload, 0, result.Length);
            outLength = result.Length;

            return 0; //No Errors
        }

        public byte[] ProtectRTCP(byte[] packet, int offset, int length)
        {
            lock (m_srtcpEncoder)
            {
                return m_srtcpEncoder.Transform(packet, offset, length);
            }
        }

        public int ProtectRTCP(byte[] payload, int length, out int outLength)
        {
            var result = ProtectRTCP(payload, 0, length);
            if (result == null)
            {
                outLength = 0;
                return -1;
            }

            System.Buffer.BlockCopy(result, 0, payload, 0, result.Length);
            outLength = result.Length;

            return 0; //No Errors
        }
    }
}