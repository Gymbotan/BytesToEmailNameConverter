using System;
using BitsToSymbols;
using System.Numerics;

namespace Converting
{
    public class Program
    {
        static void Main(string[] args)
        {
            Converter converter = new Converter();
            Console.WriteLine("Initial byte array:");
            byte[] array = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33 };
            Print(array);
            Console.WriteLine("\nEmail name:");
            string name = converter.ConvertBytesToEmailName(array);
            Console.WriteLine(name);
            Console.WriteLine($"Name's length: {name.Length}");
            byte[] publicKey = converter.ConvertStringToByteArray(name);
            Console.WriteLine("\nEmail name converted to byte array:");
            Print(publicKey);

            //Console.WriteLine("\nEmail name:");
            //name = "friend";
            //Console.WriteLine(name);
            //Console.WriteLine($"Name's length: {name.Length}");
            //publicKey = converter.ConvertStringToByteArray(name);
            //Console.WriteLine("\nEmail name converted to byte array:");
            //Print(publicKey);

            Console.WriteLine("\nEmail name:");
            name = "hfbvmtj46djg7xvgdxxmoloe35fgjnmnfd73khj5fxfb2egloj2bx";
            Console.WriteLine(name);
            Console.WriteLine($"Name's length: {name.Length}");
            publicKey = converter.ConvertStringToByteArray(name);
            Console.WriteLine("\nEmail name converted to byte array:");
            Print(publicKey);


            //array = new byte[] { 0, 0, 255 };
            //Print(array);
            //Console.WriteLine("\nEmail name:");
            //name = converter.ConvertBytesToEmailName(array);
            //Console.WriteLine(name);
            //publicKey = converter.ConvertStringToByteArray(name);
            //Console.WriteLine("\nEmail name converted to byte array:");
            //Print(publicKey);
        }

        public static void Print(byte[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }
            Console.WriteLine();
        }
    }
}
