using System.Collections.Generic;

namespace Hys.Platform.CrossCutting.Globalization
{
    /// <summary>
    /// An interface to do translation given a name or group information
    /// </summary>
    public interface ITranslation
    {
        /// <summary>
        /// Gets or sets the name of the language.
        /// </summary>
        /// <value>
        /// The name of the language.
        /// </value>
        string LanguageName { get; set; }

        /// <summary>
        /// Translates to native message.
        /// </summary>
        /// <param name="languageName">Name of the language.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <remarks>
        /// By default the group name is "Global".
        /// If the name is not matched, this name will be returned.
        /// </remarks>
        string NativeMessage(string languageName, string name);

        /// <summary>
        /// Translates to native message.
        /// </summary>
        /// <param name="languageName">Name of the language.</param>
        /// <param name="name">The name.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        string NativeMessage(string languageName, string name, params object[] parameters);

        /// <summary>
        /// Translates to native message.
        /// </summary>
        /// <param name="languageName">Name of the language.</param>
        /// <param name="group">The group.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        string NativeMessage(string languageName, string group, string name);

        /// <summary>
        /// Translates to native message.
        /// </summary>
        /// <param name="languageName">Name of the language.</param>
        /// <param name="group">The group.</param>
        /// <param name="name">The name.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        string NativeMessage(string languageName, string group, string name, params object[] parameters);

        /// <summary>
        /// Gets all translation information.
        /// </summary>
        /// <param name="languageName">Name of the language.</param>
        /// <param name="group">The group.</param>
        /// <returns>A dictionary with all item and all translated item</returns>
        Dictionary<string,string> GetAllTranslationInfo(string languageName, string group);
    }
}