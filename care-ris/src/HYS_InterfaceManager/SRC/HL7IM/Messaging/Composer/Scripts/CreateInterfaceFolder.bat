rem CreateInterfaceFolder <DeviceFolderName> <InterfaceFolderName>

mkdir ..\%2

xcopy %1\*.* ..\%2 /E

xcopy Adapter\*.* ..\%2 /E