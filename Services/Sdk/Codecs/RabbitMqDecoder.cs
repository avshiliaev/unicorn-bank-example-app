using System.Text;
using Google.Protobuf;
using UnicornBankSdk;

namespace Sdk
{
    public class RabbitMqDecoder
    {
        public void DecodeExample()
        {
            // Example for the message broker
            // https://developers.google.com/protocol-buffers/docs/csharptutorial#parsing_and_serialization
            // 1. Deserialize
            var reqMsg = "{some:message}";
            var bytes = Encoding.ASCII.GetBytes(reqMsg);
            var test = AccountEvent.Parser.ParseFrom(bytes);
            // 2. Serialize
            var replyMsg = new CodedOutputStream(new byte[0]);
            test.WriteTo(replyMsg);
        }
    }
}