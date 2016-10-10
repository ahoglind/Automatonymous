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
#if !NETSTANDARD && !NETCORE
    protected PayloadFactoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}