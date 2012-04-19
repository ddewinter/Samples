namespace RussianDownloader.Logic.Simulators
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ResourceAccessorOptions : IEnumerable<KeyValuePair<string, string>>
    {
        private Dictionary<string, string> _options = new Dictionary<string, string>();
 
        public string this[string optionName]
        {
            get
            {
                return _options[optionName];
            }
            set
            {
                _options[optionName] = value;
            }
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _options.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}