using System;
using System.Threading.Tasks;

namespace StudentsManager
{
    public class DebounceService
    {
        private int _queryVersion = 0;
        public async Task Debounce(TimeSpan time, Func<string,Task> func, string text)
        {
            _queryVersion++;
            var savedVersion = _queryVersion;

            await Task.Delay(time);

            if (savedVersion == _queryVersion)
            {
                await func.Invoke(text); 
            }
        }
    }
}
