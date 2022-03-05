using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace Hack2022.Foundation.SitecoreSend.Services
{
    public interface ISendService
    {
        bool Subscribe(string name, string emailAddress, string contactNumber, List<string> customFields);
    }
}