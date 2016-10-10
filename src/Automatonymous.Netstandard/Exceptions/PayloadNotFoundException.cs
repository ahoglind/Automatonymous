namespace Automatonymous
{
#if NETSTANDARD || NETCORE
  using Newtonsoft.Json;
#endif
  using System;
  using System.Runtime.Serialization;


#if NETSTANDARD || NETCORE
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
#if !NETSTANDARD && !NETCORE
    protected PayloadNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}