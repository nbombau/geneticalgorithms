using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics
{
    public class SelectionFactory
    {
        public static ISelection CreateSelection(string typeName)
        {
            ISelection selection;
            try
            {
                Type type = Type.GetType(typeName, true);
                selection = Activator.CreateInstance(type) as ISelection;
            }
            catch (Exception)
            {
                selection = default(ISelection);
            }
            return selection;
        }
    }
}
