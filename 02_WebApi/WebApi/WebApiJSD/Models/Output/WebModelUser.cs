using Com.Weehong.Elearning.Domain.WebModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WebApiZSK.Models.Output
{
    public class WebModelUser : WebModelIsSucceed
    {
        public LoginUserTicket User { get; set; }

        public string Ticket { get; set; }
    }
}