using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienditaDePraga
{
    public interface IFileStorage
    {
        Task<bool> CreateReport(string name);
        Task<bool> AddStringToReport(string content);
    }
}
