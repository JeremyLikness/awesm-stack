using System;
using System.Threading;
using System.Web;

namespace Awesm.Models
{
    public class HttpContextFactory
    {
        private static HttpContextBase _context;
        private static readonly object Mutex = new object();

        public static HttpContextBase Current
        {
            get
            {
                Monitor.Enter(Mutex);
                try
                {
                    if (_context != null)
                    {
                        return _context;
                    }

                    if (HttpContext.Current == null)
                    {
                        throw new InvalidOperationException("HttpContext not available");
                    }

                    return new HttpContextWrapper(HttpContext.Current);
                }
                finally
                {
                    Monitor.Exit(Mutex);
                }
            }
        }

        public static void SetCurrentContext(HttpContextBase context)
        {
            Monitor.Enter(Mutex);
            _context = context;
            Monitor.Exit(Mutex);
        }
    }
}