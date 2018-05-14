registrationModule.directive('registrationRequisitionView', [
    '$log', '$timeout', '$window', 
    function ($log, $timeout, $window) {
        'use strict';
        $log.debug('registrationRequisitionView.ctor()...');

        return {
            restrict: 'E',
            templateUrl:'/app/registration/registration-requisition/views/registration-requisition-view.html',
            controller: 'RegistrationRequisitionController',
            scope: {
                args: '=',
                modalInstance:"&"
            },
            link: function (scope, element, attrs) {
             
                var width = 650;    // We will scale the photo width to this
                var height =450;   
                // |streaming| indicates whether or not we're currently streaming
                var streaming = false;
                // video from the camera. Obviously, we start at false.
                var canvas = document.getElementById("requisitionCanvas"),
                      photo = document.getElementById('requisitionPhoto'),
                      video = document.getElementById("requisitionVideo"),
                      fileSelectList = $("#fileList");
                scope.isSafari = (navigator.userAgent.indexOf('Safari') != -1&&navigator.userAgent.indexOf('Chrome') == -1);
                scope.injectVideoEvent = function () {
                        var context = canvas.getContext("2d"),
                               videoObj = { "video": true, audio: false },
                               errBack = function (error) { console.log("Video capture error: ", error.code); };
                        navigator.getMedia = (navigator.getUserMedia ||
                                      navigator.webkitGetUserMedia ||
                                      navigator.mozGetUserMedia ||
                                      navigator.msGetUserMedia);

                        navigator.getMedia(
                          {
                              video: true,
                              audio: false
                          },
                          function (stream) {
                              if (navigator.mozGetUserMedia) {
                                  video.mozSrcObject = stream;
                              } else {
                                  var vendorURL = window.URL || window.webkitURL;
                                  video.src = vendorURL.createObjectURL(stream);
                              }
                              video.play();
                          }, errBack
                        );

                        video.addEventListener('canplay', function (ev) {
                            if (!streaming) {
                                video.setAttribute('width', width);
                                video.setAttribute('height', height);
                                scope.isStreaming = true;
                                scope.$broadcast('focusScan');
                            }
                        }, false);
                };

                // Fill the photo with an indication that none has been
                // captured.
                scope.clearphoto = function () {
                    var context = canvas.getContext('2d');
                    context.fillStyle = "#AAA";
                    context.fillRect(0, 0, canvas.width, canvas.height);
                    if (fabricCanvas) {
                        fabricCanvas.clear();
                    }
                    };
     
                // drawing it into a canvas, then converting that to a PNG
                // drawing that to the screen, we can change its size and/or apply
                var fabricCanvas;
                var rotatedEvent = 'object:rotating';
                scope.scan = function () {
                    if (!scope.isSafari) {
                        scope.capturePhoto();
                    }
                    else {
                        var context = canvas.getContext('2d');
                        if (width && height) {
                            initializeFabricCanvas();
                            context.drawImage(video, 0, 0, width*0.75, height*0.75);
                            fabric.Image.fromURL(canvas.toDataURL(), function (img) {
                                var image64Str = img.toDataURL();
                                scope.saveTempImage(image64Str);
                            });
                        } else {
                            scope.clearphoto();
                        }
                    }
                };
                
                var initializeFabricCanvas = function () {
                    if (fabricCanvas) {
                        fabricCanvas.clear();
                        scope.isRotated = false;
                    }
                    else {
                        canvas.setAttribute('width', width);
                        canvas.setAttribute('height', height);
                        fabricCanvas = new fabric.Canvas(canvas, { width: width, height: height});
                        fabricCanvas.on(rotatedEvent, function () { scope.isRotated = true; });
                    }
                };

                scope.update = function () {
                    var imgObj = fabricCanvas.item(0);
                    imgObj.scaleX = imgObj.scaleY = 1;
                    imgObj.setCoords();
                    fabricCanvas.renderAll();
                    var updateData = scope.selectedFile;
                    updateData.base64Str = imgObj.toDataURL();
                    updateData.isUpdate = true;
                    scope.updateImage(updateData).success(function (data) {

                    })
                    .error(function (error) {

                    });
                };
                scope.selectScanFile = function (selectedFile) {
                    if (!selectedFile)
                    {
                        return;
                    }
                    _.each(scope.fileList, function (f) { f.isSelected = false; });
                    selectedFile.isSelected = true;
                    scope.selectedFile = selectedFile;
                    initializeFabricCanvas();
                    fabric.Image.fromURL(selectedFile.base64Str, function (img) {
                        fabricCanvas.add(img);
                        fabricCanvas.centerObject(img);
                    });
                };

                scope.rotateLeft = function () {
                    setAngel(-90);
                };
                scope.rotateRight = function () {
                    setAngel(90);
                };

                var setAngel = function (angleOffset) {
                    var obj = fabricCanvas.item(0),
                           resetOrigin = false;
                    if (!obj) return;
                    var angle = obj.getAngle() + angleOffset;
                    if ((obj.originX !== 'center' || obj.originY !== 'center') && obj.centeredRotation) {
                        obj.setOriginToCenter && obj.setOriginToCenter();
                        resetOrigin = true;
                    }
                    angle = angle > 360 ? 90 : angle < 0 ? 270 : angle;
                    obj.setAngle(angle).setCoords();
                    if (resetOrigin) {
                        obj.setCenterToOrigin && obj.setCenterToOrigin();
                    }
                    fabricCanvas.renderAll();
                    scope.isRotated = true;
                };
            }
        };
    }]);