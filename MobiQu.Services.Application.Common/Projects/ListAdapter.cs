using System;
using System.Collections.Generic;
using System.Text;

namespace MobiQu.Services.Application.Common.MobiQu.Common.Projects
{
    public class ListAdapter
    {
        public static IList<TList> EntityList<TList>() where TList : class, new()
        {
            //to do generic list oluşturacan
            return new List<TList>();
        }
    }
}
