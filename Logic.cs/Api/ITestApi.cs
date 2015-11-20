using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransfer;
using Refit;

namespace Logic.Api
{
    public interface ITestApi
    {
        [Refit.Get("/test")]
        Task<SimpleMessage> Get();

    }
}
