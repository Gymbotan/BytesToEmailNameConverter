using BitsToSymbols;
using NUnit.Framework;
using System;

namespace ConverterTests
{
    public class ConverterTests
    {
        [TestCase(new byte[] { 0, 0, 1 }, "aaaab")]
        [TestCase(new byte[] { 31, 32, 33}, "b6ibb")]
        [TestCase(new byte[] { 233, 74, 89, 152, 12 }, "5ffftgam")]
        [TestCase(new byte[] { 111, 29, 94, 201, 215, 75 }, "dpdvpmtv2l")]
        [TestCase(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33 }, "acaqdaqcqmbyibefawdanbyhraeiscmkbkfqxdamrugy4dupb6ibb")]
        public void BytesToString_CorrectConverting(byte[] array, string expectedResult)
        {
            Converter converter = new Converter();
            var actualResult = converter.ConvertBytesToEmailName(array);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase(new byte[] { 0, 0, 1 })]
        [TestCase(new byte[] { 255, 255, 255 })]
        [TestCase(new byte[] { 1, 1, 1 })]
        [TestCase(new byte[] { 128, 255, 1 })]
        [TestCase(new byte[] { 18, 231, 112 })]
        [TestCase(new byte[] { 138, 31, 192, 211, 234, 7, 77, 177, 54, 139 })]
        public void BytesToStringAndToBytes_CorrectConverting(byte[] array)
        {
            Converter converter = new Converter();
            var actualResult = converter.ConvertStringToByteArray(converter.ConvertBytesToEmailName(array));
            Assert.AreEqual(array, actualResult);
        }

        [TestCase("aaah7", new byte[] { 0, 0, 255 })]
        [TestCase("b6ibb", new byte[] { 31, 32, 33 })]
        [TestCase("friend", new byte[] { 11, 20, 17, 163 })]
        [TestCase("maroon5", new byte[] { 3, 1, 23, 57, 189 })]
        [TestCase("hfbvmtj46djg7xvgdxxmoloe35fgjnmnfd73khj5fxfb2egloj2bx", new byte[] { 114, 134, 172, 154, 121, 225, 164, 223, 189, 76, 59, 221, 142, 91, 137, 190, 148, 201, 107, 26, 81, 255, 106, 58, 122, 91, 148, 58, 33, 150, 228, 232, 55 })]
        public void StringToBytes_CorrectConverting(string name, byte[] expectedResult)
        {
            Converter converter = new Converter();
            var actualResult = converter.ConvertStringToByteArray(name);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase("abc0")]
        [TestCase("abc1")]
        [TestCase("abc8")]
        [TestCase("abc9")]
        [TestCase("abc.")]
        [TestCase("abc,")]
        [TestCase("abc?")]
        [TestCase("abc/")]
        [TestCase("abc\\")]
        [TestCase("abc;")]
        [TestCase("abc:")]
        [TestCase("abc'")]
        [TestCase("abc\"")]
        [TestCase("abc!")]
        [TestCase("abc@")]
        [TestCase("abc#")]
        [TestCase("abc$")]
        [TestCase("abc%")]
        [TestCase("abc^")]
        [TestCase("abc&")]
        [TestCase("abc*")]
        [TestCase("abc(")]
        [TestCase("abc)")]
        [TestCase("abc+")]
        [TestCase("abc-")]
        [TestCase("abc_")]
        [TestCase("abc<")]
        [TestCase("abc>")]
        public void StringToBytes_WrongSymbols_ThrowArgumentOutOfRangeException(string name)
        {
            Converter converter = new Converter();
            Assert.Throws<ArgumentOutOfRangeException>(() => converter.ConvertStringToByteArray(name),
                message: "Not allowed symbol are used in email's name.");
        }

        [TestCase("abcdefjhijklmnopqrstuvwxyz23434567abcdefjhijklmnopqrstuvwxyz23434567a")]
        [TestCase("fhsadjfhbdsfbajhfhwejghdbcnbdsjkghdjgdfg34hgvdjdjshgajhwsdgahfvjsfgkawuf")]
        public void StringToBytes_TooBigString_ThrowArgumentException(string name)
        {
            Converter converter = new Converter();
            Assert.Throws<ArgumentException>(() => converter.ConvertStringToByteArray(name),
                message: "Email's name is too big (name can not be longer than 64 symbols).");
        }

        [Test]
        public void StringToBytes_StringIsNull_ThrowArgumentNullException()
        {
            Converter converter = new Converter();
            Assert.Throws<ArgumentNullException>(() => converter.ConvertStringToByteArray(null),
                message: "Email's name can not be a null.");
        }

        [Test]
        public void BytesToString_ArrayIsNull_ThrowArgumentNullException()
        {
            Converter converter = new Converter();
            Assert.Throws<ArgumentNullException>(() => converter.ConvertBytesToEmailName(null),
                message: "Byte's array can not be a null.");
        }

        [TestCase(new byte[] { 114, 134, 172, 154, 121, 225, 164, 223, 189, 76, 59, 221, 142, 91, 137, 190, 148, 201, 107, 26, 81, 255, 106, 58, 122, 91, 148, 58, 33, 150, 228, 232, 55, 77, 34, 127, 198, 4, 15, 251, 9, 66, 112, 183, 71 })]
        [TestCase(new byte[] { 55, 18, 194, 211, 6, 19, 168, 162, 43, 207, 12, 59, 114, 134, 172, 154, 121, 225, 164, 223, 189, 76, 59, 221, 142, 91, 137, 190, 148, 201, 107, 26, 81, 255, 106, 58, 122, 91, 148, 58, 33, 150, 228, 232, 55 })]
        public void BytesToString_TooBigArray_ThrowArgumentException(byte[] array)
        {
            Converter converter = new Converter();
            Assert.Throws<ArgumentException>(() => converter.ConvertBytesToEmailName(array),
                message: "Initial array is too big (name can not be longer than 64 symbols).");
        }
    }
}