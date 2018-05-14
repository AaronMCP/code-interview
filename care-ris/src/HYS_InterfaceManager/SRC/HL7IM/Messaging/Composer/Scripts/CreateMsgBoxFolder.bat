rem CreateMsgBoxFolder <MsgBoxInstanceFolderName>

mkdir ..\%1

xcopy MsgBox\*.* ..\%1 /E