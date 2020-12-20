using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TicketManagement.WcfService.Exceptions.Base
{
    [DataContract]
    public class WcfException
    {
        public WcfException(Type exception)
        {
            this.Exception = exception;
        }

        [DataMember]
        public Type Exception { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string Details { get; set; }
    }
}