using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web
{
    public interface IWebClient
    {
        string ServerUri { get; }
        Task<string> GetAsString(string endpoint);
    }
}
