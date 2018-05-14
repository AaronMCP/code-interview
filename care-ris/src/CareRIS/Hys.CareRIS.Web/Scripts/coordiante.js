(function (window, $) {
    $.coordinate = function (container, setting) {
        //calculate coordinate of container according to setting
        var config = {
            xLen: 8    //column
            , yLen: 6  //row
            , padding_x: 10 //horizon padding
            , padding_y: 1 //vertical padding
            , margin_x: 4   //horizon margin
            , margin_y: 4   //vertical margin
            , border_width: 1
        };

        setting = setting || {};
        $.extend(config, setting, true);

        config.width = container.width();
        config.height = container.height();
        config.adjustWidth = config.width - 2 * config.padding_x;
        config.adjustHeight = config.height - 2 * config.padding_y;
        config.cellCount = config.xLen * config.yLen;
        config.unitWidth = config.adjustWidth / config.xLen;
        config.unitHeight = config.adjustHeight / config.yLen;
        config.cellWidth = config.unitWidth - 2 * (config.margin_x + config.border_width);
        config.cellHeight = config.unitHeight - 2 * (config.margin_y + config.border_width);
        var cells = [];
        var row = [];
        var carry = 0;

        cells.calculatePosition = function (cell, pos) {
            cell.left = pos.left;
            cell.top = pos.top;
            cell.width = pos.width;
            cell.height = pos.height;
            cell.point = {
                A: { x: pos.left, y: pos.top }
                    , B: { x: pos.left + pos.width + config.border_width, y: pos.top }
                    , C: { x: pos.left + pos.width + config.border_width, y: pos.top + pos.height + config.border_width }
                    , D: { x: pos.left, y: pos.top + pos.height + config.border_width }
            }
        };

        for (var i = 0; i < config.cellCount; ++i) {
            var mod = i % config.xLen;
            var left = mod * config.unitWidth + config.margin_x + config.padding_x;
            var top = carry * config.unitHeight + config.margin_y + config.padding_y;

            var cellObj = {
                row: carry, //row position
                col: mod    //column position
            };
            cells.calculatePosition(cellObj, {
                left: left,
                top: top,
                width: config.cellWidth,
                height: config.cellHeight
            })

            //var cellObj = {
            //    row: carry      //row position
            //    , col: mod      //column position
            //    , left: left
            //    , top: top
            //    , width: config.cellWidth
            //    , height: config.cellHeight
            //    //four point of square,top left corner point is A,than anticlockwise B,C,D
            //    , point: {
            //        A: { x: left, y: top }
            //        , B: { x: left + config.cellWidth + config.border_width, y: top }
            //        , C: { x: left + config.cellWidth + config.border_width, y: top + config.cellHeight + config.border_width }
            //        , D: { x: left, y: top + config.cellHeight + config.border_width }
            //    }
            //};

            cells.push(cellObj);
            if (mod == config.xLen - 1) {
                carry++;
            }
        }
        cells.rowLen = config.yLen;
        cells.colLen = config.xLen;
        cells.getItemByCoordinate = function (row, col) {
            ///get item with row and col
            var cellLen = this.length;
            for (var i = 0; i < cellLen; ++i) {
                var item = this[i];
                if (item.row == row && item.col == col)
                    return item;
            }
            return null;
        };
        cells.getSquaresByCoordinate = function (pointA, pointB) {
            ///get squares cells from point A to B
            var ts = this;
            pointA = pointA || { row: 0, col: 0 };
            pointB = pointB || { row: ts.rowLen - 1, col: ts.colLen - 1 };

            var minPoint = {}, maxPoint = {};
            if (pointA.row < pointB.row) {
                minPoint.row = pointA.row;
                maxPoint.row = pointB.row;
            }
            else {
                minPoint.row = pointB.row;
                maxPoint.row = pointA.row;
            }
            if (pointA.col < pointB.col) {
                minPoint.col = pointA.col;
                maxPoint.col = pointB.col;
            }
            else {
                minPoint.col = pointB.col;
                maxPoint.col = pointA.col;
            }
            var squareXLen = maxPoint.col - minPoint.col + 1;
            var squareYlen = maxPoint.row - minPoint.row + 1;
            var squareLen = squareXLen * squareYlen;
            var squares = [];
            for (var i = 0; i < ts.length; ++i) {
                var item = ts[i];
                if (item.row == minPoint.row && item.col == minPoint.col) {
                    squares.push(item);
                    //set min cell of squares
                    squares.minCell = item;
                    continue;
                }
                if (item.row == maxPoint.row && item.col == maxPoint.col) {
                    squares.push(item);
                    //set max cell of squares
                    squares.maxCell = item;
                    continue;
                }
                if (item.row <= maxPoint.row && item.row >= minPoint.row && item.col <= maxPoint.col && item.col >= minPoint.col) {
                    squares.push(item);
                }
                if (squares.length == squareLen)
                    break;
            }
            if (squareLen == 1) {
                squares.maxCell = squares.minCell;
            }

            return squares;
        }
        cells.getSquaresByOffset = function (positionA, positionB) {
            ///get squares cells from offset position A to B
            positionA = positionA || { left: 0, top: 0 };
            positionB = positionB || { left: 0, top: 0 };

            var minPosition = {}, maxPosition = {};
            if (positionA.left < positionB.left) {
                minPosition.left = positionA.left;
                maxPosition.left = positionB.left;
            } else {
                minPosition.left = positionB.left;
                maxPosition.left = positionA.left;
            }
            if (positionA.top < positionB.top) {
                minPosition.top = positionA.top;
                maxPosition.top = positionB.top;
            } else {
                minPosition.top = positionB.top;
                maxPosition.top = positionA.top;
            }

            var squares = [], minCell = null, maxCell = null;
            for (var i = 0; i < this.length; ++i) {
                var item = this[i];
                if (minPosition.left <= item.point.C.x && minPosition.left >= item.point.A.x
                    && minPosition.top <= item.point.C.y && minPosition.top >= item.point.A.y) {
                    minCell = item;
                }
                if (maxPosition.left <= item.point.C.x && maxPosition.left >= item.point.A.x
                    && maxPosition.top <= item.point.C.y && maxPosition.top >= item.point.A.y) {
                    maxCell = item;
                }
            }
            if (minCell && maxCell) {
                return this.getSquaresByCoordinate({ row: minCell.row, col: minCell.col }, { row: maxCell.row, col: maxCell.col });
            }
            if (minCell) {
                minPosition.left = minCell.point.A.x;
                minPosition.top = minCell.point.A.y;
            }
            if (maxCell) {
                maxPosition.left = maxCell.point.C.x;
                maxPosition.top = maxCell.point.C.y;
            }
            for (var i = 0; i < this.length; ++i) {
                var item = this[i];
                for (var p in item.point) {
                    var tmpPoint = item.point[p];
                    if (tmpPoint.x >= minPosition.left && tmpPoint.x <= maxPosition.left && tmpPoint.y >= minPosition.top && tmpPoint.y <= maxPosition.top) {
                        squares.push(item);
                        break;
                    }
                }
            }

            if (squares.length > 0) {
                minCell = squares[0];
                maxCell = squares[0];

                for (var i = 1; i < squares.length; ++i) {
                    var cell = squares[i];
                    if (cell.row <= minCell.row && cell.col <= minCell.col) minCell = cell;
                    if (cell.row >= maxCell.row && cell.col >= maxCell.col) maxCell = cell;
                }

                squares.minCell = minCell;
                squares.maxCell = maxCell;

                return squares;
            }
            return null;
        };
        return cells;
    };
})(window, $);