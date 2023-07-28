using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {

        public IStringLocalizer Create(Type resourceSource)
        {
            return new JsonStringLocalizer();
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new JsonStringLocalizer();
        }
    }
}
