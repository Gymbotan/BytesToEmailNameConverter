# BytesToEmailNameConverter
Converter that can convert byte array into a string that can be an email name and vice versa.

For example, to convert byte[]={31,32,33} into string, the converter represents it as a sequence of bits and divides it into groups of five bits (counting from the end). Groups replaced by symbols [a-z], [1-6]:

00011111(31), 00100000(32), 00100001(33) became

0001.11110.01000.00001.00001 and then

b.5.i.b.b
