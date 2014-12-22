﻿using System.IO;
using Windows.Networking.Sockets;

namespace Http.Tcp.WinRT
{
    public class UserAgent : PortableUserAgent
    {
        private readonly StreamSocket socket;

        public UserAgent(StreamSocket streamSocket)
            : base(streamSocket.InputStream.AsStreamForRead(), streamSocket.OutputStream.AsStreamForWrite())
        {
            this.socket = streamSocket;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.socket.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}