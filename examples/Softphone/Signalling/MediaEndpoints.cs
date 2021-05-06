//-----------------------------------------------------------------------------
// Filename: Program.cs
//
// Description: A getting started program to demonstrate how to use the SIPSorcery
// library to place a call.
//
// Author(s):
// Aaron Clauson (aaron@sipsorcery.com)
//
// History:
// 13 Oct 2019	Aaron Clauson	Created, Dublin, Ireland.
// 31 Dec 2019  Aaron Clauson   Changed from an OPTIONS example to a call example.
// 20 Feb 2020  Aaron Clauson   Switched to RtpAVSession and simplified.
// 02 Feb 2021  Aaron Clauson   Removed logging to make main logic more obvious.
//
// License: 
// BSD 3-Clause "New" or "Revised" License, see included LICENSE.md file.
//-----------------------------------------------------------------------------

using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;

using SIPSorcery.Media;
using SIPSorcery.SIP;
using SIPSorcery.SIP.App;
using SIPSorceryMedia.Windows;

using System;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using SIPSorceryMedia.Abstractions;
using System.Collections.Generic;
namespace SIPSorcery.SoftPhone
{
    public class CustomEmptyAudioSource : IAudioSource
    {
        public event EncodedSampleDelegate OnAudioSourceEncodedSample;
        public event RawAudioSampleDelegate OnAudioSourceRawSample;
        public event SourceErrorDelegate OnAudioSourceError;

        public Task CloseAudio() => Task.CompletedTask;

        public void ExternalAudioSourceRawSample(AudioSamplingRatesEnum samplingRate, uint durationMilliseconds, short[] sample)
        {
        }

        public List<AudioFormat> GetAudioSourceFormats() => new List<AudioFormat>() { AudioVideoWellKnown.WellKnownAudioFormats[SDPWellKnownMediaFormatsEnum.PCMU] };

        public bool HasEncodedAudioSubscribers() => true;
        public bool IsAudioSourcePaused() => false;

        public Task PauseAudio() => Task.CompletedTask;
        public void RestrictFormats(Func<AudioFormat, bool> filter)
        {
        }

        public Task ResumeAudio() => Task.CompletedTask;

        public void SetAudioSourceFormat(AudioFormat audioFormat)
        {
        }

        public Task StartAudio() => Task.CompletedTask;
    }

    public class EmptyAudioSink : IAudioSink
    {
        public event SourceErrorDelegate OnAudioSinkError;

        public Task CloseAudioSink() => Task.CompletedTask;
        public List<AudioFormat> GetAudioSinkFormats() => new List<AudioFormat>() { AudioVideoWellKnown.WellKnownAudioFormats[SDPWellKnownMediaFormatsEnum.PCMU] };
        public void GotAudioRtp(IPEndPoint remoteEndPoint, uint ssrc, uint seqnum, uint timestamp, int payloadID, bool marker, byte[] payload)
        {

        }

        public Task PauseAudioSink() => Task.CompletedTask;

        public void RestrictFormats(Func<AudioFormat, bool> filter)
        {

        }

        public Task ResumeAudioSink() => Task.CompletedTask;

        public void SetAudioSinkFormat(AudioFormat audioFormat)
        {

        }

        public Task StartAudioSink() => Task.CompletedTask;
    }

}