namespace Automatonymous
{
#if NETSTANDARD
  using Newtonsoft.Json;
#endif
  using System;
  using System.Runtime.Serialization;


#if NETSTANDARD
  [JsonObject(MemberSerialization.OptIn)]
#else
  [Serializable]
#endif
   public class PayloadException :
        AutomatonymousException
    {
        public PayloadException()
        {
        }

        public PayloadException(string message)
            : base(message)
        {
        }

        public PayloadException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
#if !NETSTANDARD
        protected PayloadException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}