(function (window, undefined) {
    RIS = window.RIS || {};

    //string extention
    if (!String.prototype.format) {
        String.prototype.format = function () {
            var args = arguments;
            return this.replace(/{(\d+)}/g, function (match, number) {
                return typeof args[number] !== 'undefined' ? args[number] : match;
            });
        };
    }

    if (!String.prototype.formatObj) {
        String.prototype.formatObj = function () {
            var str = this.toString();
            if (!arguments.length) {
                return str;
            }
            var args = typeof arguments[0],
                args = (("string" === args || "number" === args) ? arguments : arguments[0]);
            for (arg in args) {
                str = str.replace(RegExp("\\{" + arg + "\\}", "gi"), args[arg]);
            }
            return str;
        }
    }

    if (!String.prototype.endsWith) {
        String.prototype.endsWith = function (pattern) {
            var d = this.length - pattern.length;
            return d >= 0 && this.lastIndexOf(pattern) === d;
        };
    }

    if (!Number.prototype.formatAmount) {
        Number.prototype.formatAmount = function (decPlaces) {
            decPlaces = isNaN(decPlaces = Math.abs(decPlaces)) ? 2 : decPlaces;
            var n = this.toFixed(decPlaces);
            if (decPlaces) {
                var i = n.substr(0, n.length - (decPlaces + 1));
                var j = '.' + n.substr(-decPlaces);
            } else {
                i = n;
                j = '';
            }

            function reverse(str) {
                var sr = '';
                for (var l = str.length - 1; l >= 0; l--) {
                    sr += str.charAt(l);
                }
                return sr;
            }

            if (parseInt(i)) {
                i = reverse(reverse(i).replace(/(\d{3})(?=\d)/g, "$1" + ','));
            }
            return i + j;
        };
    }

    if (!Array.prototype.sortBy) {
        Array.prototype.sortBy = function (f) {
            return this.sort(function (a, b) {
                if (a[f] === b[f])
                    return 0;
                if (a[f] && b[f]) {
                    return a[f].toLowerCase() < b[f].toLowerCase() ? -1 : 1;
                }
                return a[f] < b[f] ? -1 : 1;
            });
        };
    }

    if (!Object.keys) {
        Object.keys = function (obj) {
            if (obj !== Object(obj)) {
                throw new TypeError('Object.keys called on non-object');
            }
            var ret = [],
                p;
            for (p in obj) {
                if (Object.prototype.hasOwnProperty.call(obj, p)) {
                    ret.push(p);
                }
            }
            return ret;
        }
    }

    RIS.newGuid = function () {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }

    RIS.queryString = function (obj) {
        var str = [];
        for (var p in obj)
            if (obj.hasOwnProperty(p)) {
                str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
            }
        return str.join("&");
    }

    // https://github.com/BlueHuskyStudios/Micro-JS-Enum/wiki
    RIS.enum = function (enumArray) {
        var self = {
            all: [],
            keys: enumArray,
        };
        for (var i = enumArray.length; i--;) {
            self[enumArray[i]] = self.all[i] = i;
        }
        return self;
    };

    // get callee arguments
    // export injected args to global
    // only work in non use strict mode
    // usage RIS.debugger(); // 'use strict';
    RIS.debugger = function () {
        var fn = arguments.callee.caller;
        if (fn) {
            var tmp = fn.toString().match(/\(.*?\)/)[0];
            var argumentNames = tmp.replace(/[()\s]/g, '').split(',');

            var result = {};
            [].splice.call(fn.arguments, 0).forEach(function (arg, i) {
                result[argumentNames[i]] = arg;
            });
            angular.extend(window, result);
        } else {
            console.error('debugger not work, check and disable strict mode');
        }
    }

    // http://anentropic.wordpress.com/2009/06/25/javascript-iso8601-parser-and-pretty-dates/
    // get the milliseconds from 1970/1/1 until now.  
    // input date format: ISO8601 format date string.
    RIS.parseISO8601 = function (str) {
        // we assume str is a UTC date ending in ‘Z’
        var parts = str.split('T');
        var dateParts = parts[0].split('-');
        var timeParts = parts[1].split('Z');
        timeParts = timeParts[0].split('-');
        timeParts = timeParts[0].split('+');
        var timeSubParts = timeParts[0].split(':');
        var timeSecParts = timeSubParts[2].split('.');
        var timeHours = Number(timeSubParts[0]);
        var date = new Date;

        date.setUTCFullYear(Number(dateParts[0]));
        date.setUTCMonth(Number(dateParts[1]) - 1);
        date.setUTCDate(Number(dateParts[2]));
        date.setUTCHours(Number(timeHours));
        date.setUTCMinutes(Number(timeSubParts[1]));
        date.setUTCSeconds(Number(timeSecParts[0]));
        if (timeSecParts[1]) date.setUTCMilliseconds(Number(timeSecParts[1]));

        return date;
    };

    // Parse date object from a local time string.
    RIS.parseFromLocalTimeString = function (dateString) {

        var parts = dateString.split('T');
        var dateParts = parts[0].split('-');
        var timeParts = parts[1].split('Z');
        timeParts = timeParts[0].split('-');
        timeParts = timeParts[0].split('+');
        var timeSubParts = timeParts[0].split(':');
        var timeSecParts = timeSubParts[2].split('.');
        var timeHours = Number(timeSubParts[0]);
        var date = new Date;

        date.setFullYear(Number(dateParts[0]));
        date.setMonth(Number(dateParts[1]) - 1);
        date.setDate(Number(dateParts[2]));
        date.setHours(Number(timeHours));
        date.setMinutes(Number(timeSubParts[1]));
        date.setSeconds(Number(timeSecParts[0]));
        if (timeSecParts[1]) date.setMilliseconds(Number(timeSecParts[1]));

        return date;
    };

    RIS.getDateFromTimeSpan = function (dateString, timeSpan, isLocalTime) {
        var datePart = dateString.split('T')[0];
        var timeString = datePart + 'T' + timeSpan;

        if (isLocalTime) {
            return RIS.parseFromLocalTimeString(timeString);
        }
        else {
            return RIS.parseISO8601(timeString);
        }
    }

    // convert the database utc time to local time.
    RIS.toLocalTime = function (dateString) {
        return (new Date(RIS.parseISO8601(dateString)));
    };

    // date extention
    if (!Date.prototype.formatDateTime) {
        Date.prototype.formatDateTime = function (formatStr) {
            var date = this;
            var zeroize = function (value, length) {
                if (!length) {
                    length = 2;
                }

                value = new String(value);
                for (var i = 0, zeros = ''; i < (length - value.length) ; i++) {
                    zeros += '0';
                }
                return zeros + value;
            };

            return formatStr.replace(/"[^"]*"|'[^']*'|\b(?:d{1,4}|M{1,4}|yy(?:yy)?|([hHmstT])\1?|[lLZ])\b/g, function ($0) {
                switch ($0) {
                    case 'd':
                        return date.getDate();
                    case 'dd':
                        return zeroize(date.getDate());
                    case 'ddd':
                        return ['Sun', 'Mon', 'Tue', 'Wed', 'Thr', 'Fri', 'Sat'][date.getDay()];
                    case 'dddd':
                        return ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'][date.getDay()];
                    case 'M':
                        return date.getMonth() + 1;
                    case 'MM':
                        return zeroize(date.getMonth() + 1);
                    case 'MMM':
                        return ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'][date.getMonth()];
                    case 'MMMM':
                        return ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'][date.getMonth()];
                    case 'yy':
                        return new String(date.getFullYear()).substr(2);
                    case 'yyyy':
                        return date.getFullYear();
                    case 'h':
                        return date.getHours() % 12 || 12;
                    case 'hh':
                        return zeroize(date.getHours() % 12 || 12);
                    case 'H':
                        return date.getHours();
                    case 'HH':
                        return zeroize(date.getHours());
                    case 'm':
                        return date.getMinutes();
                    case 'mm':
                        return zeroize(date.getMinutes());
                    case 's':
                        return date.getSeconds();
                    case 'ss':
                        return zeroize(date.getSeconds());
                    case 'l':
                        return date.getMilliseconds();
                    case 'll':
                        return zeroize(date.getMilliseconds());
                    case 'tt':
                        return date.getHours() < 12 ? 'am' : 'pm';
                    case 'TT':
                        return date.getHours() < 12 ? 'AM' : 'PM';
                }
            });
        };
    }

    RIS.clearArray = function (array) {
        if (_.isArray(array)) {
            while (array.length > 0) {
                array.pop();
            }
        }
    };

    RIS.today = function () {
        var now = new Date();
        var today = new Date(now.getFullYear(), now.getMonth(), now.getDate());
        return today;
    };

    RIS.toDate = function (value) {
        if (_.isString(value)) {
            value = new Date(value);
        }

        if (_.isDate(value)) {
            var date = new Date(value.getFullYear(), value.getMonth(), value.getDate());
            return date;
        }

        return null;
    };
})(window)