namespace Automatonymous
{
  using Newtonsoft.Json;
  using System;
  using System.Runtime.Serialization;


#if NETSTANDARD
  [JsonObject(MemberSerialization.OptIn)]
#else
  [Serializable]
#endif
    public class PayloadNotFoundException :
        PayloadException
    {
        public PayloadNotFoundException()
        {
        }

        public PayloadNotFoundException(string message)
            : base(message)
        {
        }

        public PayloadNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
#if !NETSTANDARD
        protected PayloadNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}