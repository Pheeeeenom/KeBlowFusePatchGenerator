using System;
using System.Collections.Generic;
using System.Text;

namespace KeBlowFusePatchGenerator.Classes
{
    public static class Constants
    {
        public enum Endianness
        {
            Big,
            Little
        }

        public enum MotherboardRevisions
        {
            Xenon,
            Zephyr,
            Falcon,
            Jasper,
            Trinity,
            Corona,
            CoronaWB,
            Corona4GB
        }

        public enum ImageTypes
        {
            JTAG,
            Glitch,
            Glitch2,
            Glitch2m,
            DevGL
        }

        public static string PhatRetailConsoleType { get; } = "Phat Retail";
        public static string SlimRetailConsoleType { get; } = "Slim Retail";
        public static string DevKitConsoleType { get; } = "Development Kit";

        public static string OutputPatchPath = AppDomain.CurrentDomain.BaseDirectory + @"Output\OutputPatch.bin";

        public static int CPUKeyBytesLength = 16;
        public static int FusesCount = 768;
        public static int FuseNibblesCount = FusesCount / 4;
        public static int CurrentMaxCBSequence = 12;
    }
}
