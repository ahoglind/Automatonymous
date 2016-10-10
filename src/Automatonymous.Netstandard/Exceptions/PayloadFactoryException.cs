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
    public class PayloadFactoryException :
        PayloadException
    {
        public PayloadFactoryException()
        {
        }

        public PayloadFactoryException(string message)
            : base(message)
        {
        }

        public PayloadFactoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
#if !NETSTANDARD
        protected PayloadFactoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}