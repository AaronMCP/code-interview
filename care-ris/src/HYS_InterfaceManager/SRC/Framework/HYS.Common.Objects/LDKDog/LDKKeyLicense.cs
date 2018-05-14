using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.Common.Objects.LDKDog
{
    class LDKKeyLicense
    {
        private const string DEV_String = "GW20";
        static public string dev_string
        {
            get
            {
                return DEV_String;
            }
        }

        private const string expireTimeInfoString =
"<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
"<haspformat root=\"hasp_info\">" +
"    <feature>" +
"        <attribute name=\"id\" />" +
"        <element name=\"license\" />" +
"    </feature>" +
"</haspformat>" +
"";
        static public string ExpireTimeInfoString
        {
            get
            {
                return expireTimeInfoString;
            }
        }

        private const string featureInfoString =
"<haspformat root=\"hasp_info\">" +
"    <feature>" +
"       <attribute name=\"id\" />" +
"       <attribute name=\"expired\" />" +
"       <attribute name=\"disabled\" />" +
"       <attribute name=\"usable\" />" +
"    </feature>" +
"</haspformat>";

        static public string FeatureInfoString
        {
            get
            {
                return featureInfoString;
            }
        }

        private const string unfilteredScope =
            "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
            "<haspscope/>";
        static public string UnfilteredScope
        {
            get
            {
                return unfilteredScope;
            }
        }

        // 2015-09-16, Oscar added. (US26343)
        public const string Scope_LocalOnly = 
@"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<haspscope>
    <license_manager hostname=""localhost"" />
</haspscope>";

        /// <summary>
        /// HASP vendor code for 93200 key
        /// </summary>
        private const string vendorCodeString=
            "tDhQGwsJBattCGg0P9crf5W6zTZ1ZHrtXH379ql0qT/2awBoNoR1DqGL0DkY4OahY7D/GuHbrvrJztGc" +
"15jDsjZlXklrQroVK0y+mGSSa1PBqbm0X04aDDyBpf2xgdhxRcBTDPJ56+8s1qkAxV6F12BzbMqgd1Ks" +
"pAXLqPNAusXEwVv4MOQT0svfrM/E5tHUEo6QTp0HHhR1CwYyZ+3PC0BjNYzp1kiP4wq4iN6aXS4vXRrU" +
"ff6IWZ1PsHoRNzxY8YwsFqnhi9flGVs26duiL1uFyICVPIr/7nJVLvFLqh8xGP62sD80nvGPgpt6pJeP" +
"6Li0L5jQiSMTjouS+KkSgBq+TKUnKauZzAwwTQdn/QcO1VuPjLxrbUNw+SRurb0Juf5kJp0N6p10lr/l" +
"vUt2ADHkq8zB2+jVtJbIUIQL+jN3dmGjZDuiqiYZGUzmiuAr8EkKAdKrJVz2s1aj/P3C7cAEmjkNW6SW" +
"ZkfCNaAhHNlvwZtoAcQlGnwVowlrSH9+vyNQ6LTOaiDmJVmnfbkVyfa2GCQenCGDY9Bih0RVO+5GwhIL" +
"E1PR8Ry7H2ZicMby8QzhKS8oBMukFpSaVNseyH4hSdNMn0DujXOymFy7tT+UrfQYCC1u739LTpa+FkK8" +
"jJV6VSAxXBJwQlTc4ghcKxw1jyxu1Bc1IdZEMCxrpoc5PUcOFzRAseBYOS2iLeN6rNtHp+n3fd6GqAXI" +
"UZ63aSfLtG/E6lOani0PiCLTPsksePZ9vVvITC1sT31oXMFNI8iQvIwYSz5tRg0Na0XtBhzZTiSWpZHc" +
"dh9qagGNGV5C9eYmvvOsPFKpmLFsIaI3uFeKp9e1CEbz1iSqk0P/fOkPgdj1PxjBG0sircCNv06EpRvD" +
"VaqebUngqk27OPwBNMDZzG3UkQVa3XCloPxwo1c4+IUveb7yoUUFtf1A7J+ZMgGeJ4uiK1Y26/FtojDJ" +
"cdG5Xae2U/Oi4xVx9aJb/Q==";

        static public string VendorCode
        {
            get
            {
                return vendorCodeString;
            }
        }

        /* 
        /// <summary>
        /// HASP vendor code for demo keys
        /// </summary>
        private const string vendorCodeString =
            "AzIceaqfA1hX5wS+M8cGnYh5ceevUnOZIzJBbXFD6dgf3tBkb9cvUF/Tkd/iKu2fsg9wAysYKw7RMA" +
            "sVvIp4KcXle/v1RaXrLVnNBJ2H2DmrbUMOZbQUFXe698qmJsqNpLXRA367xpZ54i8kC5DTXwDhfxWT" +
            "OZrBrh5sRKHcoVLumztIQjgWh37AzmSd1bLOfUGI0xjAL9zJWO3fRaeB0NS2KlmoKaVT5Y04zZEc06" +
            "waU2r6AU2Dc4uipJqJmObqKM+tfNKAS0rZr5IudRiC7pUwnmtaHRe5fgSI8M7yvypvm+13Wm4Gwd4V" +
            "nYiZvSxf8ImN3ZOG9wEzfyMIlH2+rKPUVHI+igsqla0Wd9m7ZUR9vFotj1uYV0OzG7hX0+huN2E/Id" +
            "gLDjbiapj1e2fKHrMmGFaIvI6xzzJIQJF9GiRZ7+0jNFLKSyzX/K3JAyFrIPObfwM+y+zAgE1sWcZ1" +
            "YnuBhICyRHBhaJDKIZL8MywrEfB2yF+R3k9wFG1oN48gSLyfrfEKuB/qgNp+BeTruWUk0AwRE9XVMU" +
            "uRbjpxa4YA67SKunFEgFGgUfHBeHJTivvUl0u4Dki1UKAT973P+nXy2O0u239If/kRpNUVhMg8kpk7" +
            "s8i6Arp7l/705/bLCx4kN5hHHSXIqkiG9tHdeNV8VYo5+72hgaCx3/uVoVLmtvxbOIvo120uTJbuLV" +
            "TvT8KtsOlb3DxwUrwLzaEMoAQAFk6Q9bNipHxfkRQER4kR7IYTMzSoW5mxh3H9O8Ge5BqVeYMEW36q" +
            "9wnOYfxOLNw6yQMf8f9sJN4KhZty02xm707S7VEfJJ1KNq7b5pP/3RjE0IKtB2gE6vAPRvRLzEohu0" +
            "m7q1aUp8wAvSiqjZy7FLaTtLEApXYvLvz6PEJdj4TegCZugj7c8bIOEqLXmloZ6EgVnjQ7/ttys7VF" +
            "ITB3mazzFiyQuKf4J6+b/a/Y";

        /// <summary>
        /// Returns the vendor code as a string.
        /// </summary>
        static public string VendorCode
        {
            get
            {
                return vendorCodeString;
            }
        }
        */
       
    }
}
