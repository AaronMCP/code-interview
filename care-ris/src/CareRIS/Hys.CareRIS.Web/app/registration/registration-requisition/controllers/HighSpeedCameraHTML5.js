var HighSpeedCameraHTML5 = function () {

    var lastIsDelete = true;
    var filelist = document.getElementById('hidfilelist');
    var relativeFTPPath;
    var strImageQualityLevel;
    var m_bHispeedInited = false;
    var ERNo;
    var imgX = 0, imgY = 0;
    this.imgScale = 1;
    var movePos = 0;
    var canvas = document.getElementById("canvas");
    //var pos = GetWindowToCanvas(canvas, 0, 0);
    //var oriimgWidth;
    //var oriimgHeight;
    var img;
    var context = canvas.getContext("2d");
    var encodeTempath = '';
    var deTempath = '';
    var g_angle = 0;
    // Put event listeners into place
    window.addEventListener("DOMContentLoaded", function () {
        // Grab elements, create settings, etc.
        var canvas = document.getElementById("canvas"),
        context = canvas.getContext("2d"),
        video = document.getElementById("video"),
        videoObj = { "video": true },
        errBack = function (error) {
            console.log("Video capture error: ", error.code);
        };

        // Put video listeners into place
        if (navigator.getUserMedia) { // Standard --opera
            navigator.getUserMedia(videoObj, function (stream) {
                video.src = stream;
                video.play();
            }, errBack);
        } else if (navigator.webkitGetUserMedia) { // WebKit-prefixed---chrome
            navigator.webkitGetUserMedia(videoObj, function (stream) {
                video.src = window.webkitURL.createObjectURL(stream);
                video.play();
            }, errBack);
        } else if (navigator.mozGetUserMedia) { // WebKit-prefixed
            navigator.mozGetUserMedia(videoObj, function (stream) {
                video.src = window.URL.createObjectURL(stream);
                video.play();
            }, errBack);
        }

        // Trigger photo take
        document.getElementById("Snapbtn").addEventListener("click", function () {
            context.drawImage(video, 0, 0, 640, 480);
        });
    }, false);



    //var dlButton = document.getElementById("downloadImageBtn");
    //var loadButton = document.getElementById("loadImageBtn");
    var expandButton = document.getElementById("ExamZoomInbtn");
    var narrowButton = document.getElementById("ExamZoomOutbtn");
    var LeftButton = document.getElementById("ExamLeftbtn");
    var RightButton = document.getElementById("ExamRightbtn");


    //bindButtonEvent(dlButton, "click", saveAsLocalImage);
    //bindButtonEvent(loadButton, "click", loadImage);
    bindButtonEvent(expandButton, "click", this.ZoomIn);
    bindButtonEvent(narrowButton, "click", this.ZoomOut);
    bindButtonEvent(LeftButton, "click", this.RotateLeft);
    bindButtonEvent(RightButton, "click", this.RotateRight);



    this.GetAngle = function () {
        return g_angle;
    }

    this.update = function () {


        var urlDeFTPTemPath = $("#__hidDeFTPPath").val();

        $.ajax({
            type: "POST",
            url: urlDeFTPTemPath,
            async: false,
            data: { tempFilePath: encodeTempath },
            success: function (response) {
                deTempath = response;
            },
            failure: function () {
                alert("解密临时文件目录失败");
            }

        });

        var stroFileName = this.getSelectedFileName();
        var strFileName = deTempath + '\\' + stroFileName;
        var sRelativePath = relativeFTPPath;
        var urlUploadFile = $("#__hidUploadFile").val();
        var imgData = canvas.toDataURL("image/png");
        imgData = imgData.substr(22);
        sRelativePath = scanlist[0].value;
        $.ajax({
            type: "POST",
            url: urlUploadFile,
            async: false,
            data: { imageData: imgData, strFileName: strFileName, strRelativePath: sRelativePath, ImageQualityLevel: -1 },
            success: function (response) {
                if (response) {
                    //alert('ok');
                }
            },
            failure: function () {
                alert("上传文件目录失败");
            }

        });

    }



    this.ViewMode = function (isViewMode) {
        //alert("hello html5");
    }

    var urlIniFTP = $("#__hidIniFTP").val();
    this.InitFTPInfo = function (ftpServer, ftpPort, ftpUserID, ftpPwd, ftpTempFolderPath) {
        var res = 0;
        encodeTempath = ftpTempFolderPath;
        $.ajax({
            type: "POST",
            url: urlIniFTP,
            async: false,
            data: { FTPIP: ftpServer, Port: ftpPort, UserName: ftpUserID, Pwd: ftpPwd, tempFilePath: ftpTempFolderPath },
            success: function (response) {
                if (response) {
                    res = 1;
                }
            },
            failure: function () {
                alert("检查共享文件失败");
            }

        });
        return res;
    }

    this.GetRequisitonArr = function (arr) {
        //activeXObj.GetRequisitonArr(arr);

        var scanlist = document.getElementById("scanlist");
        var arrArr = arr.split(',');
        for (var i = 0; i < arrArr.length; i++) {
            arrArrArr = arrArr[i].split('|');
            var opt = document.createElement('option');
            opt.value = arrArrArr[0];
            opt.innerHTML = arrArrArr[1];
            scanlist.add(opt);
        }
    }
    this.SelectDefaultImage = function (index) {

        if (typeof (index) == "number") {
            var filename = "";
            var scanlist = document.getElementById("scanlist");
            if (index >= 0) {
                //filename = document.getElementsByTagName("option")[index].innerHTML;
                filename = scanlist[index].innerHTML
            }

            var path = '';
            if ((index) >= 0) {
                path = scanlist[index].value;
            }
            this.LoadImage(path, filename);
        }
    }

    this.OverScan = function () {

        var retString = '';
        var scanlist = document.getElementById("scanlist");
        for (var i = 0; i < scanlist.length; i++) {
            retString = retString + '#' + scanlist[i].innerHTML;
        }
        return retString;
    }

    this.DelteTempFiles = function () {
        //activeXObj.DelteTempFiles();

        var urlDelFTPFiles = $("#__hidDelFTPFiles").val();
        $.ajax({
            type: "POST",
            url: urlDelFTPFiles,
            async: false,
            data: {},
            success: function (response) {
                if (response) {
                    res = 1;
                }
            },
            failure: function () {
                alert("临时文件删除失败");
            }

        });
    }

    this.isValidFTP = function (ftpServer, ftpPort, ftpUserID, ftpPwd) {
        //return activeXObj.isValidFTP(ftpServer, ftpPort, ftpUserID, ftpPwd);
        var isvalid = 0;
        var urlIsValid = $("#__hidValidFTP").val();
        $.ajax({
            type: "POST",
            url: urlIsValid,
            async: false,
            data: { ip: ftpServer, port: ftpPort, user: ftpUserID, password: ftpPwd },
            success: function (response) {
                if (response) {
                    isvalid = 1;
                }
            },
            failure: function () {
                alert("验证FTP失败");
            }

        });

        return isvalid;
    }

    this.getSelectedFileName = function () {

        var selectecFileName = "";
        var seIndex = document.getElementById("scanlist").selectedIndex;
        if (seIndex >= 0) {
            selectecFileName = document.getElementsByTagName("option")[seIndex].innerHTML;
        }
        else {
            seIndex = $("#scanlist").get(0).options.length - 1;
            if (seIndex >= 0) {
                selectecFileName = document.getElementsByTagName("option")[seIndex].innerHTML;
            }
            else {
                selectecFileName = '';
            }
        }
        return selectecFileName;
    }

    this.GetImageOnPanel = function () {
        //return activeXObj.GetImageOnPanel();

        var imageName = '';
        var scanlist = document.getElementById("scanlist");
        if (scanlist.length == 1) {
            return imageName;
        }
        for (var i = 0; i < scanlist.length; i++) {
            imageName = imageName + scanlist[i].innerHTML + "#";
        }
        imageName = imageName.substr(0, imageName.length - 1);
        return imageName;

    }
    this.deleteRequisition = function (accno, path, delfilename, deletefile) {
        //activeXObj.deleteRequisition(accno, path, delfilename, isbool);
        var urlDelFTPFil = $("#__hidDelFTPFile").val();
        var strAccno = accno.trim();
        var bFTPFileDeleted = false;
        if (strAccno.length == 0) {
            return 0;
        }
        var strFileName = delfilename.trim();

        relativeFTPPath = path;

        if (deletefile) {
            $.ajax({
                type: "POST",
                url: urlDelFTPFil,
                async: false,
                data: { relativePath: path, FileName: delfilename },
                success: function (response) {
                    bFTPFileDeleted = response;
                },
                failure: function () {

                }
            });

            if (bFTPFileDeleted) {
                var scanlist = document.getElementById("scanlist");
                if (scanlist.length == 1) {
                    //add canvas refresh content---- scanner.cleanPic()
                    var selIndex = scanlist.selectedIndex;
                    for (var i = 0; i < filelist.length; i++) {
                        if (filelist[i].innerHTML == scanlist[selIndex].innerHTML) {
                            filelist.remove(i);
                        }
                    }
                    for (var j = 0; j < scanlist.length; j++) {
                        scanlist.remove(j);
                    }
                    lastIsDelete = true;
                }
                return 1;
            }
            else {
                return 2;
            }
        }
        else {
            if (filelist.length == 1) {
                //add canvas refresh content---- scanner.cleanPic()
                var selIndex = scanlist.selectedIndex;
                for (var i = 0; i < filelist.length; i++) {
                    if (filelist[i].innerHTML == scanlist[selIndex].innerHTML) {
                        filelist.remove(i);
                    }
                }
                for (var j = 0; j < scanlist.length; j++) {
                    scanlist.remove(j);
                }
                lastIsDelete = true;
            }
            return 1;
        }


    }
    this.RenewRequisition = function () {
        //activeXObj.RenewRequisition();

        var scanlist = document.getElementById("scanlist");
        var selIndex = scanlist.selectedIndex;
        for (var i = 0; i < filelist.length; i++) {
            if (filelist[i].innerHTML == scanlist[selIndex].innerHTML) {
                filelist.remove(i);
            }
        }
        scanlist.remove(selIndex);;
    }

    this.InputRelativePath = function (path) {
        //activeXObj.InputRelativePath(path);
        relativeFTPPath = path;
    }

    this.InputERNo = function (erno) {
        //activeXObj.InputERNo(erno);
        ERNo = erno;
    }

    this.Save = function (ImageQualityLevel) {
        //return activeXObj.Save(ImageQualityLevel);
        strImageQualityLevel = ImageQualityLevel;
        var scanlist = document.getElementById("scanlist");
        if (!m_bHispeedInited) {
            //enable camera
        }
        else {
            //if   disable preview panel
        }

        var nMax = 0;
        var stroFileName = '';
        var strMaxFTPFile = '';
        var imgCounts = scanlist.length;
        var randomInt = 0;
        if (lastIsDelete) {
            strMaxFTPFile = this.getMaxFromFTPFilelist();

            randomInt = 1;
            if (strMaxFTPFile.length <= 3) {
                randomInt = parseInt(strMaxFTPFile) + 1;
            }
            var randomStr = randomInt.toString();
            while (randomStr.length < 3) {
                randomStr = '0' + randomStr;
            }
            stroFileName = ERNo + randomStr + '.png';
            lastIsDelete = false;
        }
        else {
            for (var i = 0; i < imgCounts; i++) {
                var lbimageItemName = scanlist[i].innerHTML;
                lbimageItemName = lbimageItemName.substr(0, lbimageItemName.length - 4);
                if (lbimageItemName.length < 3) {
                    var addCount = 3 - lbimageItemName.length;
                    for (var j = 0; j < addCount; j++) {
                        lbimageItemName = '0' + lbimageItemName;
                    }
                }
                var strCur = lbimageItemName.substr(lbimageItemName.length - 3, 3);
                var nCur = parseInt(strCur);
                if (nMax == 0) {
                    nMax = nCur;
                }
                else {
                    if (nMax < nCur) {
                        nMax = nCur;
                    }
                }
            }
            randomInt = nMax + 1;
            var strrandomInt = randomInt.toString();
            while (strrandomInt.length < 3) {
                strrandomInt = '0' + strrandomInt;
            }
            stroFileName = ERNo + strrandomInt + '.png';
        }

        var urlDeFTPTemPath = $("#__hidDeFTPPath").val();

        $.ajax({
            type: "POST",
            url: urlDeFTPTemPath,
            async: false,
            data: { tempFilePath: encodeTempath },
            success: function (response) {
                deTempath = response;
            },
            failure: function () {
                alert("解密临时文件目录失败");
            }

        });


        var strFileName = deTempath + '\\' + stroFileName;
        var sRelativePath = relativeFTPPath;
        var urlUploadFile = $("#__hidUploadFile").val();
        var imgData = canvas.toDataURL("image/png");
        imgData = imgData.substr(22);

        $.ajax({
            type: "POST",
            url: urlUploadFile,
            async: false,
            data: { imageData: imgData, strFileName: strFileName, strRelativePath: sRelativePath, ImageQualityLevel: ImageQualityLevel },
            success: function (response) {
                if (response) {
                    var opt = document.createElement('option');
                    opt.value = sRelativePath;
                    opt.innerHTML = stroFileName;
                    scanlist.add(opt);

                }
            },
            failure: function () {
                alert("解密临时文件目录失败");
            }

        });

    }


    this.getMaxFromFTPFilelist = function () {
        var scanlist = document.getElementById("scanlist");
        if (scanlist[0] != '') {
            var fileNOlist = new Array();
            var max = 0;
            var strno = '';
            for (var i = 0; i < scanlist.length; i++) {
                //strno = scanlist[i].substr(scanlist[i].length - 7, 3);

                var lbimageItemName = scanlist[i].innerHTML;
                lbimageItemName = lbimageItemName.substr(0, lbimageItemName.length - 4);
                strno = lbimageItemName.substr(lbimageItemName.length - 3, 3);

                while (strno[0] == '0') {
                    strno = strno.substr(1, strno.length - 1);
                }
                fileNOlist.push(strno);
            }
            for (var i = 0; i < fileNOlist.length; i++) {
                var ino = parseInt(fileNOlist[i]);
                if (max > ino) {
                    max = max;
                }
                else {
                    max = ino;
                }
            }
            return max.toString();
        }
        else {
            return '000';
        }
    }

    this.getSelectedFilePath = function () {
        //return activeXObj.getSelectedFilePath();
        var scanlist = document.getElementById("scanlist");
        var seIndex = scanlist.selectedIndex;
        if (seIndex >= 0) {
            return scanlist[seIndex].value;
        }
        else {
            seIndex = $("#scanlist").get(0).options.length - 1;
            if (seIndex >= 0) {
                return scanlist[seIndex].value;
            }
            else {
                return '';
            }
        }
    }

    this.Rotate = function (n) {
        this.imgScale = 1;
        var clearPos = this.getClearArea();
        context.clearRect(0, 0, clearPos.x, clearPos.y);
        switch (n) {
            default:
            case 0:
                canvas.setAttribute('width', img.width);
                canvas.setAttribute('height', img.height);
                g_angle = 0;
                movePos = 0;
                context.rotate(g_angle * Math.PI / 180);
                context.translate(img, 0, 0);
                context.drawImage(img, 0, 0);

                break;
            case 1:
                canvas.setAttribute('width', img.height);
                canvas.setAttribute('height', img.width);
                g_angle = 90;
                movePos = 1;
                context.rotate(g_angle * Math.PI / 180);
                context.translate(0, -img.height);
                context.drawImage(img, 0, 0);

                break;
            case 2:
                canvas.setAttribute('width', img.width);
                canvas.setAttribute('height', img.height);
                g_angle = 180;
                movePos = 0;
                context.rotate(g_angle * Math.PI / 180);
                context.translate(-img.width, -img.height);
                context.drawImage(img, 0, 0);

                break;
            case 3:
                canvas.setAttribute('width', img.height);
                canvas.setAttribute('height', img.width);
                g_angle = 270;
                movePos = 1;
                context.rotate(g_angle * Math.PI / 180);
                context.translate(-img.width, 0);
                context.drawImage(img, 0, 0);

                break;
        };
    }





    this.RotateLeft = function (strFileItemName, strFilePath, isbool) {
        //activeXObj.RotateLeft(strFileItemName, strFilePath, isbool);
        var n = img.getAttribute('step');
        if (n == null) n = 1;
        else {
            (n == 0) ? n = 3 : n--;
        }
        img.setAttribute('step', n);

        this.Rotate(n);
        //////upload to ftp server
        //this.update();
    }

    this.RotateRight = function (strFileItemName, strFilePath, isbool) {
        var n = img.getAttribute('step');
        if (n == null) n = 3;
        else {
            (n == 3) ? n = 0 : n++;
        }
        img.setAttribute('step', n);
        this.Rotate(n);

        //this.update();
    }

    this.ZoomIn = function () {

        var pos = this.GetWindowToCanvas(canvas, 0, 0);
        this.imgScale *= 2;
        if (g_angle == 90 || g_angle == 270) {
            if (movePos == 1) {
                imgX = imgX * 2;// + pos.x * 2;
                imgY = imgY * 2;//+ pos.y * 2;
            }
            else {
                imgX = imgX * 2;
                imgY = imgY * 2;
            }
            movePos++;
        }
        else {
            imgX = imgX * 2;
            imgY = imgY * 2;
        }
        this.RedrawImage();
        //var clearPos = this.getClearArea();
        //context.clearRect(0, 0, clearPos.x, clearPos.y);
        //context.drawImage(img, 0, 0, img.width, img.height, imgX, imgY, img.width * imgScale, img.height * imgScale);
    }

    this.ZoomOut = function () {
        var pos = this.GetWindowToCanvas(canvas, 0, 0);
        this.imgScale /= 2;
        if (g_angle == 90 || g_angle == 270) {
            if (movePos == 1) {
                imgX = imgX * 0.5 + pos.x * 0.5;
                imgY = imgY * 0.5 + pos.y * 0.5;
            }
            else {
                imgX = imgX * 0.5;
                imgY = imgY * 0.5;
            }
            movePos++;
        }
        else {
            imgX = imgX * 0.5;
            imgY = imgY * 0.5;
        }
        this.RedrawImage();
        //var clearPos = this.getClearArea();
        //context.clearRect(0, 0, clearPos.x, clearPos.y);
        //context.drawImage(img, 0, 0, img.width, img.height, imgX, imgY, img.width * imgScale, img.height * imgScale);

    }

    this.RedrawImage = function () {
        //var canvas = document.getElementById("canvas");
        //context.clearRect(0, 0, canvas.width, canvas.height);
        var clearPos = this.getClearArea();
        context.clearRect(0, 0, clearPos.x, clearPos.y);
        context.drawImage(img, 0, 0, img.width, img.height, imgX, imgY, img.width * this.imgScale, img.height * this.imgScale);
    }

    this.RedrawOutImage = function (ocanvas, oimgX, oimgY) {

        var clearPos = this.getClearArea();
        context.clearRect(0, 0, clearPos.x, clearPos.y);
        context.drawImage(img, 0, 0, img.width, img.height, oimgX, oimgY, img.width * this.imgScale, img.height * this.imgScale);
    }

    this.Clear = function () {
        var clearPos = this.getClearArea();
        context.clearRect(0, 0, clearPos.x, clearPos.y);
        this.Rotate(0);
    }


    this.getClearArea = function () {
        var finalX;
        var finalY;
        if (g_angle == 0) {
            finalX = img.width;
            finalY = img.height;
        }
        else if (g_angle == 90) {
            finalX = img.width;
            finalY = img.height;
        }
        else if (g_angle == 180) {
            finalX = img.width;
            finalY = img.height;
        }
        else if (g_angle == 270) {
            finalX = img.width;
            finalY = img.height;
        }
        return { x: finalX, y: finalY };
    }


    this.LoadImage = function (vRelativePaht, vFileName) {

        var loadImgUrl = $("#__hidDownloadFTPFile").val();

        $.ajax({
            url: loadImgUrl,
            type: "post",
            data: { strRelativePath: vRelativePaht, strFileName: vFileName },
            async: false,
            success: function (bDownload) {
                if (bDownload) {
                    var imgIsLoaded;
                    img = new Image();
                    img.onload = function () {
                        imgIsLoaded = true;
                        oriimgWidth = img.width;
                        oriimgHeight = img.height;
                        drawImage(img);
                    }
                    img.src = '../temp/' + vFileName;
                    return img;
                }
                else {
                    alert("FTP下载失败");
                }
            },
            error: function (e) {
                alert("图片加载错误");
            }
        })
    }

    this.ReturnImage = function () {
        return img;
    }

    this.axisTrans = function (x, y, angle) {

        var finalX = x * Math.cos(angle * Math.PI / 180) - y * Math.sin(angle * Math.PI / 180);
        var finalY = x * Math.sin(angle * Math.PI / 180) + y * Math.cos(angle * Math.PI / 180);

        return { x: finalX, y: finalY };

    }

    this.GetWindowToCanvas = function (canvas, x, y) {
        var pbbx = canvas.getBoundingClientRect();
        return {
            x: x - pbbx.left - (pbbx.width - canvas.width) / 2,
            y: y - pbbx.top - (pbbx.height - canvas.height) / 2
        }
    }

    this.GetScanDate = function () {
        var now = new Date();
        year = "" + now.getFullYear();
        month = "" + (now.getMonth() + 1); if (month.length == 1) { month = "0" + month; }
        day = "" + now.getDate(); if (day.length == 1) { day = "0" + day; }
        hour = "" + now.getHours(); if (hour.length == 1) { hour = "0" + hour; }
        minute = "" + now.getMinutes(); if (minute.length == 1) { minute = "0" + minute; }
        second = "" + now.getSeconds(); if (second.length == 1) { second = "0" + second; }
        var tt = year + "-" + month + "-" + day + " " + hour + ":" + minute + ":" + second;
        return tt;
    }
}