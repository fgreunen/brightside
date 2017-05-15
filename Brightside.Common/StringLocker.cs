using System.Collections.Concurrent;

namespace Brightside.Common
{
    public static class StringLocker
    {
        private static ConcurrentDictionary<string, object> _locks =
            new ConcurrentDictionary<string, object>();

        public static object GetLockObject(string s)
        {
            return _locks.GetOrAdd(s, k => new object());
        }
    }
}