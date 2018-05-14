commonModule.filter('risTextHighlight', function() {
    // create highlight bit map, highlighted character will be marked value as 'true'
    // for example, {0: false, 1: true, 2: true}
    var buildHighlightMap = function(text, searchString) {
        var searchTermArray = searchString.trim().split(/\s+/gi);

        var highlightMap = {};
        for (var i = 0; i < text.length; i++) {
            // set all as 'false' by default
            highlightMap[i] = false;
        }

        var lowerCaseText = text.toLowerCase();
        searchTermArray.forEach(function(term) {
            var startIndex = -1;
            do {
                startIndex = lowerCaseText.indexOf(term.toLowerCase(), startIndex + 1);
                if (startIndex !== -1) {
                    for (var k = startIndex; k < startIndex + term.length; k++) {
                        highlightMap[k] = true;
                    }
                } else {
                    break;
                }
            } while ((startIndex + term.length) <= text.length);
        });

        return highlightMap;
    };

    // split the text to be slices by hightlighted text. 
    // for example, text = 'James', searchString = 'ame'. textSliceArray will be [{'J', false}, {'ame', true}, {'s', false}]  
    var splitTextByHighlightedText = function(text, highlightMap) {
        // append a flag for the ending
        highlightMap[text.length] = !highlightMap[text.length - 1];
        var textSliceArray = [];
        var highlightFlag = highlightMap[0];
        var textSiice = [];
        for (var j = 0; j <= text.length; j++) {
            if (highlightFlag !== highlightMap[j]) {
                textSliceArray.push({
                    value: textSiice.join(''),
                    highlight: highlightFlag
                });
                textSiice = [];
                highlightFlag = highlightMap[j];
            }
            if (j < text.length) {
                textSiice.push(text[j]);
            }
        };
        return textSliceArray;
    };

    var buildHighlightHTML = function(text, highlightMap) {
        var textSliceArray = splitTextByHighlightedText(text, highlightMap);
        var highlightedHTML = '';
        _.each(textSliceArray, function(textSlice) {
            highlightedHTML += textSlice.highlight ? ('<span class="search-text-highlight">' + _.escape(textSlice.value) + '</span>') : _.escape(textSlice.value);
        });
        return highlightedHTML;
    };

    var buildSSNHighlightHTML = function(text) {
        // for SSN display ***-**-**** and show text as tooltip
        return '<span class="search-text-highlight" title="' + text + '">***-**-****</span>';
    };

    return function(text, searchString, isSSN) {
        /// <summary>
        /// Filter to highlight text. This filter will return html, so it will work with 'ng-bind-html'.
        /// This filter also depends on css class '.search-text-highlight' for the highlight style.
        /// @example,
        /// <span ng-bind-html="item.value | risTextHighlight : highlightTexts"></span>
        /// <span ng-bind-html="item.value | risTextHighlight : highlightTexts : item.name==='SSN'"></span>
        /// </summary>
        /// <param name="text">text to be highlighted</param>
        /// <param name="searchString">search string which can contain multiple terms with a space ' '.</param>
        /// <param name="isSSN">indicate if the text is SSN</param>
        /// <returns type="">formatted html for highlighting</returns>
        if ((text || angular.isNumber(text)) && (searchString || angular.isNumber(searchString))) {

            text = text.toString();
            searchString = searchString.toString();

            if (isSSN) {
                return buildSSNHighlightHTML(text);
            }

            // normal case to highlight matched characters
            var highlightMap = buildHighlightMap(text, searchString);
            var hasMatchedText = _.contains(highlightMap, true);

            if (hasMatchedText) {
                return buildHighlightHTML(text, highlightMap);
            } else {
                return _.escape(text);
            }
        } else {
                return _.escape(text);
        }
    };
});