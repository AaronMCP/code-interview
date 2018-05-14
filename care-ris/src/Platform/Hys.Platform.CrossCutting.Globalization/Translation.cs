#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;

#endregion

namespace Hys.Platform.CrossCutting.Globalization
{
    /// <summary>
    /// Implementation class to do translation given a name or group information
    /// </summary>
    public class Translation : ITranslation
    {
        private readonly Dictionary<string, Dictionary<string, Dictionary<string, string>>> _cache =
            new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

        private string languageName;
        private readonly string languageFilePath;

        public Translation(string languageName, string languageFilePath)
        {
            this.languageName = languageName;
            this.languageFilePath = languageFilePath;
            ReInitializeCache();
        }

        private void ReInitializeCache()
        {
            var oneCache = new Dictionary<string, Dictionary<string, string>>();
            _cache.Add(languageName, oneCache);

            var files = Directory.GetFiles(languageFilePath, "*." + languageName + ".xml");
            foreach (var file in files)
            {
                var onlyFileName = Path.GetFileName(file);
                var group = onlyFileName.Substring(0, onlyFileName.IndexOf('.'));
                oneCache.Add(group, new Dictionary<string, string>());
                var element = XElement.Load(file);
                var query = from message in element.Elements("Message")
                    select
                        new
                        {
                            Name = (string) message.Element("Name"),
                            Display = (string) message.Element("Display")
                        };
                foreach (var item in query)
                {
                    try
                    {
                        oneCache[group].Add(item.Name, item.Display);
                    }
                    catch
                    {
                    }
                }
            }
        }

        #region ITranslation Members

        /// <summary>
        /// Translates to native message.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string NativeMessage(string languageName, string name)
        {
            return NativeMessage(languageName, "Global", name);
        }

        public string NativeMessage(string languageName, string name, params object[] parameters)
        {
            var value = NativeMessage(languageName, name);
            return string.Format(value, parameters);
        }

        public string NativeMessage(string languageName, string group, string name)
        {
            if (!_cache.ContainsKey(languageName))
            {
                throw new ArgumentException("No this language key:" + languageName);
            }
            if (!_cache[languageName].ContainsKey(group))
            {
                throw new ArgumentException("No this key:" + group);
            }
            var values = _cache[languageName][group];
            string value;
            if (!values.TryGetValue(name, out value))
            {
                value = name;
                Trace.WriteLine("[WARN]Need translation:" + name);
            }
            return value;
        }

        public string NativeMessage(string languageName, string group, string name, params object[] parameters)
        {
            var value = NativeMessage(languageName, group, name);
            return string.Format(value, parameters);
        }

        public Dictionary<string, string> GetAllTranslationInfo(string languageName,string group)
        {
            return _cache[languageName][group];
        }

        #endregion ITranslation Members

        #region ITranslation Members

        public string LanguageName
        {
            get { return languageName; }
            set
            {
                if (languageName == value) return;
                languageName = value;
                ReInitializeCache();
            }
        }

        #endregion ITranslation Members
    }
}