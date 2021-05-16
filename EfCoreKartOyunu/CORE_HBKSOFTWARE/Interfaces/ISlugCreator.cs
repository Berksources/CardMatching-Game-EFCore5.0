using System;
using System.Collections.Generic;
using System.Text;

namespace CORE_HBKSOFTWARE.Interfaces
{
    public interface ISlugCreator
    {
        string SlugCreatorForAll(string name, string surname, string ID);
    }
}
