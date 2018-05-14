#region

using System.Runtime.Serialization;

#endregion

namespace Hys.CareAgent.WcfService.Contract
{
    [DataContract]
    public class CardInfo
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Number { get; set; }

        [DataMember]
        public string Identifier { get; set; }
    }
}