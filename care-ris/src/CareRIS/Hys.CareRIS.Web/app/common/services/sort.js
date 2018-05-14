commonModule.factory('sortArr', ['$log', function () {
    return {
        sortArray: function (arr, sortOrder, attr) {
            for (var i = 0; i < arr.length; i++) {
                for (var j = i; j < arr.length; j++) {
                    if (sortOrder === 'desc') {
                        if (attr) {
                            if (arr[i][attr] < arr[j][attr]) {
                                var a = arr[i];
                                arr[i] = arr[j];
                                arr[j] = a;
                            }
                        } else {
                            if (arr[i] < arr[j]) {
                                var a = arr[i];
                                arr[i] = arr[j];
                                arr[j] = a;
                            }
                        }
                        
                    } else {
                        if (sortOrder === 'asc') {
                            if (attr) {
                                if (arr[i][attr] > arr[j][attr]) {
                                    var a = arr[i];
                                    arr[i] = arr[j];
                                    arr[j] = a;
                                }
                            } else {
                                if (arr[i] > arr[j]) {
                                    var a = arr[i];
                                    arr[i] = arr[j];
                                    arr[j] = a;
                                }
                            }
                            
                        }
                    }
                    
                }
                
            }
            return arr;
        }
    }
}])