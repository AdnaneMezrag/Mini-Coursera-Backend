using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Utilities
{
    public interface IImageStorageService
    {
        Task<string> SaveImageAsync(Stream imageStream);
    }
}
