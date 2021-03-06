//
//      Copyright (C) 2012 DataStax Inc.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//
﻿namespace Cassandra
{

    internal class FrameHeader
    {
        public const int MaxFrameSize = 256 * 1024 * 1024;
        public const int Size = 8;
        public byte Version;
        public byte Flags;
        public byte StreamId;
        public byte Opcode;
        public byte[] Len = new byte[4];
        public ResponseFrame MakeFrame(IProtoBuf stream)
        {
            var bodyLen = TypeInterpreter.BytesToInt32(Len, 0);

            if (MaxFrameSize - 8 < bodyLen) throw new DriverInternalError("Frame length mismatch");

            var frame = new ResponseFrame() { FrameHeader = this, RawStream = stream };
            return frame;
        }
    }
    
    internal class ResponseFrame
    {
        public FrameHeader FrameHeader;
        public IProtoBuf RawStream;
    }

    internal struct RequestFrame
    {
        public byte[] Buffer;

        public const int VersionIdx = 0;
        public const int FlagsIdx = 1;
        public const int StreamIdIdx = 2;
        public const int OpcodeIdIdx = 3;
        public const int LenIdx = 4;
        public const int BodyIdx = 8;

    }


}
