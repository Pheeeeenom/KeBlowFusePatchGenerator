using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace KeBlowFusePatchGenerator.Classes
{
    public static class Utilities
    {
        public static bool IsHexadecimal(this string str)
        {
            bool isHex = true;
            foreach (var character in str)
            {
                isHex = ((character >= '0' && character <= '9') ||
                         (character >= 'a' && character <= 'f') ||
                         (character >= 'A' && character <= 'F'));

                if (!isHex)
                {
                    return false;
                }
            }

            return isHex;
        }

        public static byte[] HexStringToByteArray(string hexStr)
        {
            return Enumerable.Range(0, hexStr.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(hexStr.Substring(x, 2), 16))
                     .ToArray();
        }

        public static string ByteArrayToHexString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }

        public static string NibblesToHex(this List<byte> nibbles, bool prefix = true)
        {
            List<byte> bytes = new List<byte>();

            for (int i = 0; i < nibbles.Count;)
            {
                byte highNibble = nibbles[i];
                byte lowNibble = nibbles[i + 1];

                byte byteToAdd = (byte)(highNibble << 4 | lowNibble);
                bytes.Add(byteToAdd);
                i += 2;
            }

            if (prefix)
            {
                return "0x" + BitConverter.ToString(bytes.ToArray()).Replace("-", string.Empty);
            }

            else
            {
                return BitConverter.ToString(bytes.ToArray()).Replace("-", string.Empty);
            }
        }

        public static T ConvertFromByteArray<T>(byte[] bytes, int index, Constants.Endianness inputEndianness, Constants.Endianness outputEndianness) where T : IComparable<T>
        {
            bool reversed = false;

            if (bytes != null)
            {
                if (BitConverter.IsLittleEndian)
                {
                    if (inputEndianness == Constants.Endianness.Big)
                    {
                        Array.Reverse(bytes);
                        reversed = true;
                    }
                }

                else
                {
                    if (inputEndianness == Constants.Endianness.Little)
                    {
                        Array.Reverse(bytes);
                        reversed = true;
                    }
                }

                T ret = default(T);

                if (typeof(T) == typeof(ushort))
                {
                    ret = (T)Convert.ChangeType(BitConverter.ToUInt16(bytes, index), typeof(T));
                }

                if (typeof(T) == typeof(short))
                {
                    ret = (T)Convert.ChangeType(BitConverter.ToInt16(bytes, index), typeof(T));
                }

                if (typeof(T) == typeof(int))
                {
                    ret = (T)Convert.ChangeType(BitConverter.ToInt32(bytes, index), typeof(T));
                }

                if (typeof(T) == typeof(long))
                {
                    ret = (T)Convert.ChangeType(BitConverter.ToInt64(bytes, index), typeof(T));
                }

                if (outputEndianness == Constants.Endianness.Little)
                {
                    if (reversed && inputEndianness == Constants.Endianness.Little)
                    {
                        Array.Reverse(bytes);
                    }
                }

                else if (outputEndianness == Constants.Endianness.Big)
                {
                    if (reversed && inputEndianness == Constants.Endianness.Big)
                    {
                        Array.Reverse(bytes);
                    }
                }

                return ret;
            }

            return default(T);
        }

        public static int AsBigEndian(this int integer)
        {
            if (BitConverter.IsLittleEndian)
            {
                var bitOne = (integer >> 0) & 0xff;
                var bitTwo = (integer >> 8) & 0xff;
                var bitThree = (integer >> 16) & 0xff;
                var bitFour = (integer >> 24) & 0xff;

                return bitOne << 24 | bitTwo << 16 | bitThree << 8 | bitFour << 0;
            }

            else
            {
                return integer;
            }
        }

        public static uint AsBigEndian(this uint integer)
        {
            if (BitConverter.IsLittleEndian)
            {
                var bitOne = (integer >> 0) & 0xff;
                var bitTwo = (integer >> 8) & 0xff;
                var bitThree = (integer >> 16) & 0xff;
                var bitFour = (integer >> 24) & 0xff;

                return bitOne << 24 | bitTwo << 16 | bitThree << 8 | bitFour << 0;
            }

            else
            {
                return integer;
            }
        }

        public static bool VerifyCPUKey(string cpuKey)
        {
            if (cpuKey.IsHexadecimal())
            {
                return VerifyCPUKey(Utilities.HexStringToByteArray(cpuKey));
            }

            return false;
        }

        public static bool VerifyCPUKey(byte[] cpuKey)
        {
            if (cpuKey == null || cpuKey.Length != Constants.CPUKeyBytesLength)
            {
                return false;
            }

            int hamming = 0;
            byte[] hammingArray = new byte[13];

            Buffer.BlockCopy(cpuKey, 0, hammingArray, 0, 13);
            BitArray bitArray = new BitArray(hammingArray);

            foreach (bool s in bitArray)
            {
                if (s)
                {
                    hamming++;
                }
            }

            //if hamming is 53 already, great. make sure both checks below fail.
            //Don't pull your hair out like I did for a bit, 13 is A6 in the cut off portion. I forgot about 0 hehe
            //1010 0110 in binary 

            if (cpuKey[13].GetBit(0))
            {
                hamming++;
            }

            if (cpuKey[13].GetBit(1))
            {
                hamming++;
            }

            if (hamming != 53)
            {
                return false;
            }

            //We know the hamming is good, so now let's get our whole CPU key with the correct ECD
            byte[] cpuKeyWithECD = GenerateCPUKeyWithECD(cpuKey);
            return cpuKey.SequenceEqual(cpuKeyWithECD);
        }

        public static byte[] GenerateRandomCPUKeyBytes()
        {
            byte[] cpuKey = new byte[16];
            RNGCryptoServiceProvider generator = new RNGCryptoServiceProvider();

            while (true)
            {
                generator.GetNonZeroBytes(cpuKey);
                cpuKey = GenerateCPUKeyWithECD(cpuKey);
                if (VerifyCPUKey(cpuKey))
                {
                    break;
                }
            }

            return cpuKey;
        }

        public static string GenerateRandomCPUKeyString()
        {
            byte[] cpuKey = GenerateRandomCPUKeyBytes();
            return ByteArrayToHexString(cpuKey);
        }

        private static byte[] GenerateCPUKeyWithECD(byte[] cpuKey)
        {
            byte[] ecd = new byte[Constants.CPUKeyBytesLength];
            Buffer.BlockCopy(cpuKey, 0, ecd, 0, Constants.CPUKeyBytesLength);

            uint acc1 = 0, acc2 = 0;

            for (var cnt = 0; cnt < 0x80; cnt++, acc1 >>= 1)
            {
                var bTmp = ecd[cnt >> 3];
                var dwTmp = (uint)((bTmp >> (cnt & 7)) & 1);
                if (cnt < 0x6A)
                {
                    acc1 = dwTmp ^ acc1;

                    if ((acc1 & 1) > 0)
                    {
                        acc1 = acc1 ^ 0x360325;
                    }

                    acc2 = dwTmp ^ acc2;
                }

                else if (cnt < 0x7F)
                {
                    if (dwTmp != (acc1 & 1))
                    {
                        ecd[(cnt >> 3)] = (byte)((1 << (cnt & 7)) ^ (bTmp & 0xFF));
                    }

                    acc2 = (acc1 & 1) ^ acc2;
                }

                else if (dwTmp != acc2)
                {
                    ecd[0xF] = (byte)((0x80 ^ bTmp) & 0xFF);
                }
            }

            return ecd;
        }
    }
}
