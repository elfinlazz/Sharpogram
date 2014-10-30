using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

using Telegram.TLCore;

namespace Telegram.TLCore
{
    /**
     * Helper class for writing and reading data for tl (de-)serialization.
     *
     * Based on (@author Korshakov Stepan <me@ex3ndr.com> for Java)
     */
    public class StreamingUtils {

        /**
         * Writing byte to stream
         *
         * @param v      value
         * @param stream destination stream
         * @throws IOException
         */
        public static void writeByte(int v, /*OutputStream*/ StreamWriter stream) {
            try {
                /*stream.write(v);*/ stream.Write(v);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Writing byte to stream
         *
         * @param v      value
         * @param stream destination stream
         * @throws IOException
         */
        public static void writeByte(byte v, /*OutputStream*/ StreamWriter stream) {
            try {
                /*stream.write(v);*/ stream.Write(v);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Writing int to stream
         *
         * @param v      value
         * @param stream destination stream
         * @throws IOException
         */
        public static void writeInt(uint v, /*OutputStream*/ StreamWriter stream) {
            try {
                writeByte((byte) (v & 0xFF), stream);
                writeByte((byte) ((v >> 8) & 0xFF), stream);
                writeByte((byte) ((v >> 16) & 0xFF), stream);
                writeByte((byte) ((v >> 24) & 0xFF), stream);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        
        }

        /**
         * Writing long to stream
         *
         * @param v      value
         * @param stream destination stream
         * @throws IOException
         */
        public static void writeLong(long v, /*OutputStream*/ StreamWriter stream) {
            try {
                writeByte((byte) (v & 0xFF), stream);
                writeByte((byte) ((v >> 8) & 0xFF), stream);
                writeByte((byte) ((v >> 16) & 0xFF), stream);
                writeByte((byte) ((v >> 24) & 0xFF), stream);

                writeByte((byte) ((v >> 32) & 0xFF), stream);
                writeByte((byte) ((v >> 40) & 0xFF), stream);
                writeByte((byte) ((v >> 48) & 0xFF), stream);
                writeByte((byte) ((v >> 56) & 0xFF), stream);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Writing double to stream
         *
         * @param v      value
         * @param stream destination stream
         * @throws IOException
         */
        public static void writeDouble(double v, /*OutputStream*/ StreamWriter stream) {
            try {
                writeLong(BitConverter.DoubleToInt64Bits(v), stream);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Writing byte array to stream
         *
         * @param data   data
         * @param stream destination stream
         * @throws IOException
         */
        public static void writeByteArray(byte[] data, /*OutputStream*/ StreamWriter stream) {
            try {
                stream.Write(data);
            } catch (IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Writing byte array to stream
         *
         * @param data   data
         * @param stream destination stream
         * @throws IOException
         */
        public static void writeByteArray(byte[] data, int offset, int len, /*OutputStream*/ StreamWriter stream) {
            try {
                stream.BaseStream.Write(data, offset, len);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Writing tl-bool value
         *
         * @param v      value
         * @param stream destination stream
         * @throws IOException
         */
        public static void writeTLBool(Boolean v, /*OutputStream*/ StreamWriter stream) {
            try {
                if (v) {
                    writeInt(TLBoolTrue.CLASS_ID, stream);
                    
                } else {
                    writeInt(TLBoolFalse.CLASS_ID, stream);
                }
            } catch (IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Writing tl-string value
         *
         * @param v      value
         * @param stream destination stream
         * @throws IOException
         */
        public static void writeTLString(String v, /*OutputStream*/ StreamWriter stream) {
            try {
                writeTLBytes(Encoding.ASCII.GetBytes(v), stream);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Writing tl-bytes value
         *
         * @param v      value
         * @param stream destination stream
         * @throws IOException
         */
        public static void writeTLBytes(byte[] v, /*OutputStream*/ StreamWriter stream) {
            try
            {
                int startOffset = 1;
                if (v.Length >= 254)
                {
                    startOffset = 4;
                    writeByte(254, stream);
                    writeByte(v.Length & 0xFF, stream);
                    writeByte((v.Length >> 8) & 0xFF, stream);
                    writeByte((v.Length >> 16) & 0xFF, stream);
                }
                else
                {
                    writeByte(v.Length, stream);
                }

                writeByteArray(v, stream);

                int offset = (v.Length + startOffset) % 4;
                if (offset != 0)
                {
                    int offsetCount = 4 - offset;
                    writeByteArray(new byte[offsetCount], stream);
                }
            } catch (IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Writing tl-bytes value
         *
         * @param v      value
         * @param stream destination stream
         * @throws IOException
         */
        public static void writeTLBytes(TLBytes v, /*OutputStream*/ StreamWriter stream) {
            try {
                int startOffset = 1;
                if (v.getLength() >= 254) {
                    startOffset = 4;
                    writeByte(254, stream);
                    writeByte(v.getLength() & 0xFF, stream);
                    writeByte((v.getLength() >> 8) & 0xFF, stream);
                    writeByte((v.getLength() >> 16) & 0xFF, stream);
                } else {
                    writeByte(v.getLength(), stream);
                }

                writeByteArray(v.getData(), v.getOffset(), v.getLength(), stream);

                int offset = (v.getLength() + startOffset) % 4;
                if (offset != 0) {
                    int offsetCount = 4 - offset;
                    writeByteArray(new byte[offsetCount], stream);
                }
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Writing tl-object to stream
         *
         * @param v      tl-object
         * @param stream destination stream
         * @throws IOException
         */
        public static void writeTLObject(TLObject v, /*OutputStream*/ StreamWriter stream) {
            try {
                v.serialize(stream);
            } catch (IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Writing tl-method to stream. Same as writeTLObject, but used for pretty code
         *
         * @param v      tl-method
         * @param stream destination stream
         * @throws IOException
         */
        public static void writeTLMethod(/*TLMethod*/TLObject v, /*OutputStream*/ StreamWriter stream) {
            try {
                writeTLObject(v, stream);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Writing tl-vector to stream
         *
         * @param v      tl-vector
         * @param stream destination stream
         * @throws IOException
         */
        public static void writeTLVector(/*TLVector*/TLObject v, /*OutputStream*/ StreamWriter stream) {
            try {
                writeTLObject(v, stream);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Reading int from stream
         *
         * @param stream source stream
         * @return value
         * @throws IOException reading exception
         */
        public static int readInt(/*InputStream*/ BufferedStream stream)
        {
            try {
//                int a = /*stream.read();*/ stream.Read();
                byte[] byte_a = new byte[] {};
                stream.Read(byte_a, 0, (int)stream.Length);
                int a = Convert.ToInt32(byte_a);
                if (a < 0) {
                    throw new IOException();
                }
//                int b = /*stream.read();*/ stream.Read(); 
                byte[] byte_b = new byte[] { };
                stream.Read(byte_b, 0, (int)stream.Length);
                int b = Convert.ToInt32(byte_b);
                if (b < 0) {
                    throw new IOException();
                }
//                int c = /*stream.read();*/ stream.Read(); 
                byte[] byte_c = new byte[] { };
                stream.Read(byte_c, 0, (int)stream.Length);
                int c = Convert.ToInt32(byte_c);
                if (c < 0) {
                    throw new IOException();
                }
//                int d = /*stream.read();*/ stream.Read(); 
                byte[] byte_d = new byte[] { };
                stream.Read(byte_d, 0, (int)stream.Length);
                int d = Convert.ToInt32(byte_d);
                if (d < 0) {
                    throw new IOException();
                }

                return a + (b << 8) + (c << 16) + (d << 24);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Reading uint from stream
         *
         * @param stream source stream
         * @return value
         * @throws IOException reading exception
         */
        public static long readUInt(/*InputStream*/ BufferedStream  stream) {
            try {
//                long a = /*stream.read();*/ stream.Read(); 
                byte[] byte_a = new byte[] { };
                stream.Read(byte_a, 0, (int)stream.Length);
                long a = Convert.ToUInt32(byte_a);

                if (a < 0) {
                    throw new IOException();
                }
//                long b = /*stream.read();*/ stream.Read(); 
                byte[] byte_b = new byte[] { };
                stream.Read(byte_b, 0, (int)stream.Length);
                long b = Convert.ToUInt32(byte_b);

                if (b < 0) {
                    throw new IOException();
                }
 //               long c = /*stream.read();*/ stream.Read(); 
                byte[] byte_c = new byte[] { };
                stream.Read(byte_c, 0, (int)stream.Length);
                long c = Convert.ToUInt32(byte_c);

                if (c < 0) {
                    throw new IOException();
                }
  //              long d = /*stream.read();*/ stream.Read(); 
                byte[] byte_d = new byte[] { };
                stream.Read(byte_d, 0, (int)stream.Length);
                long d = Convert.ToUInt32(byte_d);

                if (d < 0) {
                    throw new IOException();
                }
                
                return a + (b << 8) + (c << 16) + (d << 24);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Reading long from stream
         *
         * @param stream source stream
         * @return value
         * @throws IOException reading exception
         */
        public static long readLong(/*InputStream*/ BufferedStream  stream) {
            try {
                long a = readUInt(stream);
                long b = readUInt(stream);

                return a + (b << 32);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Reading double from stream
         *
         * @param stream source stream
         * @return value
         * @throws IOException reading exception
         */
        public static double readDouble(/*InputStream*/ BufferedStream  stream) {
            try {
                return BitConverter.Int64BitsToDouble(readLong(stream));
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Reading tl-string from stream
         *
         * @param stream source stream
         * @return value
         * @throws IOException reading exception
         */
        public static String readTLString(/*InputStream*/ BufferedStream  stream) {
            try {
                return readTLBytes(stream).ToString();
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Reading tl-object from stream
         *
         * @param stream  source stream
         * @param context tl-context
         * @return tl-object
         * @throws IOException reading exception
         */
        public static TLObject readTLObject(/*InputStream*/ BufferedStream  stream, TLContext context) {
            try {
                return context.deserializeMessage(stream);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Reading tl-method from stream. Same as readTLObject, used for pretty code.
         *
         * @param stream  source stream
         * @param context tl-method
         * @return tl-method
         * @throws IOException reading exception
         */
        public static /*TLMethod*/TLObject readTLMethod(/*InputStream*/ BufferedStream stream, TLContext context)
        {
            try {
                return (/*TLMethod*/TLObject) context.deserializeMessage(stream);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Reading bytes from stream
         *
         * @param count  bytes count
         * @param stream source stream
         * @return readed bytes
         * @throws IOException reading exception
         */
        public static byte[] readBytes(int count, /*InputStream*/ BufferedStream  stream) {
            try {
                byte[] res = new byte[count];
                int offset = 0;
                while (offset < res.Length) {
                    int readed = stream.Read(res, offset, res.Length - offset);
                    if (readed > 0) {
                        offset += readed;
                    } else if (readed < 0) {
                        throw new IOException();
                    } else {
                        Thread.Yield();
                    }
                }
                return res;
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Reading bytes from stream
         *
         * @param count  bytes count
         * @param stream source stream
         * @return readed bytes
         * @throws IOException reading exception
         */
        public static void skipBytes(int count, /*InputStream*/ BufferedStream  stream) {
            try {
                stream.Seek(count, SeekOrigin.Current);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Reading bytes from stream
         *
         * @param count  bytes count
         * @param stream source stream
         * @return readed bytes
         * @throws IOException reading exception
         */
        public static void readBytes(byte[] buffer, int offset, int count, /*InputStream*/ BufferedStream  stream) {
            try {
                int woffset = 0;
                while (woffset < count) {
                    int readed = stream.Read(buffer, woffset + offset, count - woffset);
                    if (readed > 0) {
                        woffset += readed;
                    } else if (readed < 0) {
                        throw new IOException();
                    } else {
                        Thread.Yield();
                    }
                }
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Reading tl-bytes from stream
         *
         * @param stream source stream
         * @return readed bytes
         * @throws IOException reading exception
         */
        public static byte[] readTLBytes(/*InputStream*/ BufferedStream  stream) {
            try {
//                int count = /*stream.read();*/ stream.Read(); 
                byte[] byte_count = new byte[] { };
                stream.Read(byte_count, 0, (int)stream.Length);
                int count = Convert.ToInt32(byte_count);
                int a = Convert.ToInt32(byte_count);
                int b = Convert.ToInt32(byte_count);
                int c = Convert.ToInt32(byte_count);
                int startOffset = 1;
                if (count >= 254) {
                    count = /*stream.Read()*/a + (/*stream.Read()*/b << 8) + (/*stream.Read()*/c << 16);
                    startOffset = 4;
                }

                byte[] raw = readBytes(count, stream);
                int offset = (count + startOffset) % 4;
                if (offset != 0) {
                    int offsetCount = 4 - offset;
                    skipBytes(offsetCount, stream);
                }

                return raw;
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Reading tl-bytes from stream with manual allocation
         *
         * @param stream  source stream
         * @param context tl-context
         * @return readed bytes
         * @throws IOException reading exception
         */
        public static TLBytes readTLBytes(/*InputStream*/ BufferedStream  stream, TLContext context) {
            try {
//                int count = /*stream.read();*/ stream.Read(); 
                byte[] byte_count = new byte[] { };
                stream.Read(byte_count, 0, (int)stream.Length);
                int count = Convert.ToInt32(byte_count);
                int a = Convert.ToInt32(byte_count);
                int b = Convert.ToInt32(byte_count);
                int c = Convert.ToInt32(byte_count);

                int startOffset = 1;
                if (count >= 254) {
                    count = /*stream.Read()*/a + (/*stream.Read()*/b << 8) + (/*stream.Read()*/c << 16);
                    startOffset = 4;
                }
                TLBytes res = context.allocateBytes(count);
                readBytes(res.getData(), res.getOffset(), res.getLength(), stream);

                int offset = (count + startOffset) % 4;
                if (offset != 0) {
                    int offsetCount = 4 - offset;
                    skipBytes(offsetCount, stream);
                }
                return res;
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Reading tl-vector from stream
         *
         * @param stream  source stream
         * @param context tl-context
         * @return tl-vector
         * @throws IOException reading exception
         */
        public static /*TLVector*/TLObject readTLVector(/*InputStream*/ BufferedStream  stream, TLContext context) {
            try {
                return (TLObject)context.deserializeVector<Object>(stream);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Reading tl-vector of integers from stream
         *
         * @param stream  source stream
         * @param context tl-context
         * @return tl-vector of integers
         * @throws IOException reading exception
         */
        public static TLIntVector readTLIntVector(/*InputStream*/ BufferedStream  stream, TLContext context) {
            try {
                return context.deserializeIntVector(stream);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Reading tl-vector of longs from stream
         *
         * @param stream  source stream
         * @param context tl-context
         * @return tl-vector of longs
         * @throws IOException reading exception
         */
        public static TLLongVector readTLLongVector(/*InputStream*/ BufferedStream  stream, TLContext context) {
            try {
                return context.deserializeLongVector(stream);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Reading tl-vector of strings from stream
         *
         * @param stream  source stream
         * @param context tl-context
         * @return tl-vector of strings
         * @throws IOException reading exception
         */
        public static TLStringVector readTLStringVector(/*InputStream*/ BufferedStream  stream, TLContext context) {
            try {
                return context.deserializeStringVector(stream);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Reading tl-bool from stream
         *
         * @param stream source stream
         * @return bool
         * @throws IOException reading exception
         */
        public static Boolean readTLBool(/*InputStream*/ BufferedStream  stream) {
            try {
                int v = readInt(stream);
                if (v == TLBoolTrue.CLASS_ID) {
                    return true;
                } else if (v == TLBoolFalse.CLASS_ID) {
                    return false;
                } else
                    throw new /*DeserializeException*/ Exception("Not bool value: " + v.ToString("X") );
            }
            catch (Exception e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                return false;
            }
            /*
            catch (DeserializeException IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
            */
        }

        /**
         * Converting int to bytes
         *
         * @param value source integer
         * @return bytes of integer
         */
        public static byte[] intToBytes(int value) {

            return new byte[]{
                    (byte) (value & 0xFF),
                    (byte) ((value >> 8) & 0xFF),
                    (byte) ((value >> 16) & 0xFF),
                    (byte) ((value >> 24) & 0xFF)};

        }

        /**
         * Converting long to bytes
         *
         * @param value source long
         * @return bytes of long
         */
        public static byte[] longToBytes(long value) {
        
            return new byte[]{
                    (byte) (value & 0xFF),
                    (byte) ((value >> 8) & 0xFF),
                    (byte) ((value >> 16) & 0xFF),
                    (byte) ((value >> 24) & 0xFF),
                    (byte) ((value >> 32) & 0xFF),
                    (byte) ((value >> 40) & 0xFF),
                    (byte) ((value >> 48) & 0xFF),
                    (byte) ((value >> 56) & 0xFF)};

        }

        /**
         * Reading int from bytes array
         *
         * @param src source bytes
         * @return int value
         */
        public static int readInt(byte[] src) {
            try {
                return readInt(src, 0);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Reading int from bytes array
         *
         * @param src    source bytes
         * @param offset offset in array
         * @return int value
         */
        public static int readInt(byte[] src, int offset) {
            try {
                int a = src[offset + 0] & 0xFF;
                int b = src[offset + 1] & 0xFF;
                int c = src[offset + 2] & 0xFF;
                int d = src[offset + 3] & 0xFF;

                return a + (b << 8) + (c << 16) + (d << 24);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Reading uint from bytes array
         *
         * @param src source bytes
         * @return uint value
         */
        public static long readUInt(byte[] src) {
            try {
                return readUInt(src, 0);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Reading uint from bytes array
         *
         * @param src    source bytes
         * @param offset offset in array
         * @return uint value
         */
        public static long readUInt(byte[] src, int offset) {
            try {
                long a = src[offset + 0] & 0xFF;
                long b = src[offset + 1] & 0xFF;
                long c = src[offset + 2] & 0xFF;
                long d = src[offset + 3] & 0xFF;

                return a + (b << 8) + (c << 16) + (d << 24);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Reading long value from bytes array
         *
         * @param src    source bytes
         * @param offset offset in array
         * @return long value
         */
        public static long readLong(byte[] src, int offset) {
            try {
                long a = readUInt(src, offset);
                long b = readUInt(src, offset + 4);

                return (a & 0xFFFFFFFF) + ((b & 0xFFFFFFFF) << 32);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }
    }
}
