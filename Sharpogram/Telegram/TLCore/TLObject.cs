using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Ionic.Crc;
using System.Runtime.Serialization;

namespace Telegram.TLCore
{
    /**
     * Basic class for all tl-objects. Contains methods for serializing and deserializing object.
     * Each tl-object has class id for using in object header for identifying object class for deserialization.
     * This number might be unique and often equals to crc32 of tl-record of tl-constructor.
     * <p/>
     * It is recommended to declare public static final CLASS_ID with tl class id and
     * return this in getClassId and passing it to TLContext.registerClass method during class registration
     *
     * Based on (@author Korshakov Stepan <me@ex3ndr.com> for Java)
     */
    public abstract class TLObject : Object/*, CRC32*/ {

        /**
         * Getting TL Class identification
         *
         * @return id of class
         */
        public abstract uint getClassId();

        /**
         * Serializing object to byte array
         *
         * @return serialized object with header
         * @throws IOException
         */
        public byte[] serialize() {
            try {
                /*ByteArrayOutputStream*/
                StreamWriter stream = new StreamWriter("")/*ByteArrayOutputStream()*/;
                serialize(stream);

                var tmpStream = new MemoryStream();
                stream.BaseStream.CopyTo(tmpStream);

                return tmpStream.ToArray();
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Serializing object to stream
         *
         * @param stream destination stream
         * @throws IOException
         */
        public void serialize(/*OutputStream*/StreamWriter stream)
        {
            try {
                StreamingUtils.writeInt(getClassId(), stream);
                serializeBody(stream);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Deserializing object from stream and current TLContext
         *
         * @param stream  source stream
         * @param context tl context
         * @throws IOException
         */
        public void deserialize(/*InputStream*/BufferedStream stream, TLContext context) {
            try {
//                int classId = stream.Read();
                byte[] byte_classId = new byte[] { };
                stream.Read(byte_classId, 0, (int)stream.Length);
                int classId = Convert.ToInt32(byte_classId);

                if (classId != getClassId()) {
                    throw new /*DeserializeException*/IOException("Wrong class id. Founded:" + classId.ToString("X") +
                            ", expected: " + getClassId().ToString("X"));
                }
                deserializeBody(stream, context);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        /**
         * Serializing object body to stream
         *
         * @param stream destination stream
         * @throws IOException
         */
//        public void serializeBody(/*OutputStream*/StreamWriter stream) {}
        public abstract void serializeBody(StreamWriter stream);

        /**
         * Deserializing object from stream and context
         *
         * @param stream  source stream
         * @param context tl context
         * @throws IOException
         */
//        public void deserializeBody(/*InputStream*/BufferedStream stream, TLContext context) {
//            CRC32 crc32 = new CRC32();
//        }
        public abstract void deserializeBody(BufferedStream stream, TLContext context);
    }
}
