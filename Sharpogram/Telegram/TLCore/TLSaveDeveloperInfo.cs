using System;
using System.Text;
using System.IO;

namespace Telegram.TLCore
{
    public class TLSaveDeveloperInfo : TLMethod<TLBool> {

        public static readonly Int64 CLASS_ID = -757418007;
        private String name;
        private String email;
        private String phone_number;
        private Int64 age;
        private String city;

        public TLSaveDeveloperInfo(String name, String email, String phoneNumber, Int64 age, String city)
        {
            this.setName(name);
            this.setEmail(email);
            this.setPhoneNumber(phoneNumber);
            this.setAge(age);
            this.setCity(city);
        }

        public TLBool deserializeResponse(BufferedStream stream, TLContext context) {
            try {
                TLObject res = StreamingUtils.readTLObject(stream, context);
                if (res == null) {
                    throw new IOException("Unable to parse response");
                }
                if ((res is TLBool)) {
                    return (TLBool) res;
                }
                throw new IOException("Incorrect response type. Expected TLBool, got: " + res.GetType().Name);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }


        override public void serializeBody(StreamWriter stream) {
            try {
                StreamingUtils.writeTLString(this.getName(), stream);
                StreamingUtils.writeTLString(this.getEmail(), stream);
                StreamingUtils.writeTLString(this.getPhoneNumber(), stream);
                StreamingUtils.writeInt((uint)this.getAge(), stream);
                StreamingUtils.writeTLString(this.getCity(), stream);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }
        }

        override public void deserializeBody(BufferedStream stream, TLContext context) {
            try {
                this.setName(StreamingUtils.readTLString(stream));
                this.setEmail(StreamingUtils.readTLString(stream));
                this.setPhoneNumber(StreamingUtils.readTLString(stream));
                this.setAge(StreamingUtils.readInt(stream));
                this.setCity(StreamingUtils.readTLString(stream));
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }
        }

        public String toString() {
            return "register.saveDeveloperInfo#d2dab7e9";
        }

        override public Int64 getClassId()
        {
            return -757418007;
        }

        public String getName() {
            return name;
        }

        public void setName(String name) {
            this.name = name;
        }

        public String getEmail() {
            return email;
        }

        public void setEmail(String email) {
            this.email = email;
        }

        public String getPhoneNumber() {
            return phone_number;
        }

        public void setPhoneNumber(String phoneNumber) {
            this.phone_number = phoneNumber;
        }

        public Int64 getAge()
        {
            return age;
        }

        public void setAge(Int64 age)
        {
            this.age = age;
        }

        public String getCity() {
            return city;
        }

        public void setCity(String city) {
            this.city = city;
        }
    }
}
