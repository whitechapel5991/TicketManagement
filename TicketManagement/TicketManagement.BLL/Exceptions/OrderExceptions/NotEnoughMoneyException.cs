﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.BLL.Exceptions.OrderExceptions
{
    public class NotEnoughMoneyException: Exception
    {
        public NotEnoughMoneyException(string message)
            : base(message)
        {
        }
    }
}
