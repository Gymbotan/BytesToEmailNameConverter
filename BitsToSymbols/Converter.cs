﻿using System;
using System.Numerics;

namespace BitsToSymbols
{
    /// <summary>
    /// Allows to convert array of bytes to a string of symbols that can be an email name and vice versa. 
    /// Symbols dictionary is [abcdefghijkmnpqrstuvwxyz23456789]. 1 and l, 0 and o looks similar so they were deleted.
    /// </summary>
    public class Converter
    {
        private const int MaxEmailNameSize = 64;
        private const int ByteSize = 8;
        private const int FiveBitsSize = 5;

        public string ConvertBytesToEmailName(byte[] array)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Length < 1)
            {
                throw new ArgumentException("Array should contain at least 1 element.");
            }

            byte[] fiveBitsArray = ConvertEightToFiveBits(array);
            char[] symbolsArray = new char[fiveBitsArray.Length];
            for (int i = 0; i < symbolsArray.Length; i++)
            {
                symbolsArray[i] = ConvertByteToSymbol(fiveBitsArray[i]);
            }
            return new string(symbolsArray);
        }

        public byte[] ConvertStringToByteArray(string name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Email's name can not be empty or whitespace.");
            }

            if (name.Length > MaxEmailNameSize)
            {
                throw new ArgumentException($"Email's name can not be longer than {MaxEmailNameSize} symbols.", nameof(name));
            }

            char[] symbolsArray = name.ToCharArray();
            byte[] fiveBitsArray = new byte[symbolsArray.Length];
            
            for (int i = 0; i < fiveBitsArray.Length; i++)
            {
                fiveBitsArray[i] = ToBits(symbolsArray[i]);
            }

            return ConvertFiveToEightBits(fiveBitsArray);
        }

        private byte[] ConvertEightToFiveBits(byte[] array)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Length < 1)
            {
                throw new ArgumentException("Array should contain at least 1 element.");
            }

            int size = array.Length * ByteSize % FiveBitsSize == 0 ? array.Length * ByteSize / FiveBitsSize : array.Length * ByteSize / FiveBitsSize + 1;
            if (size > MaxEmailNameSize)
            {
                throw new ArgumentException($"Initial array is too big to create correct email's name (name can not be longer than {MaxEmailNameSize} symbols).", nameof(array));
            }
            
            int currentPosition = size - 1;
            byte[] result = new byte[size];
            BigInteger number = new BigInteger(array, true, true);
            while (number != 0 && currentPosition >= 0)
            {
                byte lastFiveBits = (byte)(number & 31);
                result[currentPosition] = lastFiveBits;
                number = number >> FiveBitsSize;
                currentPosition--;
            }

            return result;
        }

        private byte[] ConvertFiveToEightBits(byte[] array)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Length > MaxEmailNameSize)
            {
                throw new ArgumentException($"Email's name can not be longer than {MaxEmailNameSize} symbols.", nameof(array));
            }

            BigInteger number = 0; 
            int size = array.Length * FiveBitsSize / ByteSize;
            byte[] resultArray = new byte[size];
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] >= 32)
                {
                    throw new ArgumentOutOfRangeException(nameof(array),
                        $"Array at index {i} has wrong value. Allowed values are from 0 to 31.");
                }
                number = number << FiveBitsSize;
                number |= array[i];
            }

            byte[] tempArray = number.ToByteArray(true, true);
            if (tempArray.Length >= resultArray.Length)
            {
                return tempArray;
            }
            else
            {
                for (int i = 1; i <= tempArray.Length; i++)
                {
                    resultArray[^i] = tempArray[^i];
                }

                return resultArray;
            }
        }

        private char ConvertByteToSymbol(byte bits) => bits switch
        {
            0 => 'a',
            1 => 'b',
            2 => 'c',
            3 => 'd',
            4 => 'e',
            5 => 'f',
            6 => 'g',
            7 => 'h',
            8 => 'i',
            9 => 'j',
            10 => 'k',
            11 => 'm',
            12 => 'n',
            13 => 'p',
            14 => 'q',
            15 => 'r',
            16 => 's',
            17 => 't',
            18 => 'u',
            19 => 'v',
            20 => 'w',
            21 => 'x',
            22 => 'y',
            23 => 'z',
            24 => '2',
            25 => '3',
            26 => '4',
            27 => '5',
            28 => '6',
            29 => '7',
            30 => '8',
            31 => '9',
            _ => throw new ArgumentOutOfRangeException(nameof(bits)),
        };

        private byte ToBits(char symbol) => symbol switch
        {
            'a' => 0,
            'b' => 1,
            'c' => 2,
            'd' => 3,
            'e' => 4,
            'f' => 5,
            'g' => 6,
            'h' => 7,
            'i' => 8,
            'j' => 9,
            'k' => 10,
            'm' => 11,
            'n' => 12,
            'p' => 13,
            'q' => 14,
            'r' => 15,
            's' => 16,
            't' => 17,
            'u' => 18,
            'v' => 19,
            'w' => 20,
            'x' => 21,
            'y' => 22,
            'z' => 23,
            '2' => 24,
            '3' => 25,
            '4' => 26,
            '5' => 27,
            '6' => 28,
            '7' => 29,
            '8' => 30,
            '9' => 31,
            _ => throw new ArgumentOutOfRangeException(nameof(symbol)),
        };

    }
}
