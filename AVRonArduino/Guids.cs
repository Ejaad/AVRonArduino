// Guids.cs
// MUST match guids.h
using System;

namespace EjaadTech.AVRonArduino
{
    static class GuidList
    {
        public const string guidAVRonArduinoPkgString = "ae02b4e0-a1e6-428b-8369-cee04478ffa5";
        public const string guidAVRonArduinoCmdSetString = "23ad2712-ced6-4150-a57f-470040b42a95";
        public const string guidToolWindowPersistanceString = "37f8d237-a495-4add-9369-ea7c78a089b9";

        public static readonly Guid guidAVRonArduinoCmdSet = new Guid(guidAVRonArduinoCmdSetString);
    };
}