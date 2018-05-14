rem CreateSolutionFolder <IntegrationSolutionFolderPath> <sourcePath (optional, with path slash at the end)>

mkdir %1
mkdir %1\osql
mkdir %1\Device
mkdir %1\Device\Adapter
mkdir %1\Device\MsgBox
mkdir %1\Device\FileIn
mkdir %1\Device\FileIn\XML
mkdir %1\Device\FileIn\XML\CDA
mkdir %1\Device\FileOut
mkdir %1\Device\LogOut
mkdir %1\Portal
mkdir %1\Tools
mkdir %1\Tools\ScriptService
mkdir %1\Tools\XmlTest

rem --- Copy Solution Files ---

copy %2..\bin\Debug\HYS.IM.Messaging.Composer.exe %1
copy %2..\bin\Debug\HYS.IM.Common.DataAccess.dll %1
copy %2..\bin\Debug\HYS.IM.Common.Logging.dll %1
copy %2..\bin\Debug\HYS.Common.Xml.dll %1
copy %2..\bin\Debug\HYS.IM.Messaging.Base.dll %1
copy %2..\bin\Debug\HYS.IM.Messaging.Mapping.dll %1
copy %2..\bin\Debug\HYS.IM.Messaging.Objects.dll %1
copy %2..\bin\Debug\HYS.IM.Messaging.Queuing.dll %1

copy %2..\..\..\..\3rdParty\osql\*.* %1\osql

copy %2CreateDB.sql %1
copy %2DropDB.sql %1

rem --- Copy Device Files ---

copy %2..\bin\Debug\HYS.IM.Common.DataAccess.dll %1\Device\Adapter
copy %2..\bin\Debug\HYS.IM.Common.Logging.dll %1\Device\Adapter
copy %2..\bin\Debug\HYS.Common.Xml.dll %1\Device\Adapter
copy %2..\bin\Debug\HYS.IM.Messaging.Base.dll %1\Device\Adapter
copy %2..\bin\Debug\HYS.IM.Messaging.Mapping.dll %1\Device\Adapter
copy %2..\bin\Debug\HYS.IM.Messaging.Objects.dll %1\Device\Adapter
copy %2..\bin\Debug\HYS.IM.Messaging.Queuing.dll %1\Device\Adapter
copy %2..\bin\Debug\GotDotNet.Exslt.dll %1\Device\Adapter
copy %2..\bin\Debug\NMatrix.Schematron.dll %1\Device\Adapter

copy %2..\..\..\..\3rdParty\dotnet_tools\InstallUtil.exe %1\Device\Adapter

copy %2..\..\..\MessageBox\Agent\bin\Debug\HYS.IM.MessageBox.Agent.dll %1\Device\Adapter
copy %2..\..\..\MessageBox\Agent\bin\Debug\HYS.IM.MessageBox.Objects.dll %1\Device\Adapter

copy %2..\..\Config\bin\Debug\HYS.IM.Messaging.Config.exe %1\Device\Adapter
copy %2..\..\Service\bin\Debug\HYS.IM.Messaging.Service.exe %1\Device\Adapter
copy %2..\..\ServiceConfig\bin\Debug\HYS.IM.Messaging.ServiceConfig.exe %1\Device\Adapter
copy %2..\..\ServiceConfig\bin\Debug\HYS.IM.Messaging.Management.dll %1\Device\Adapter
copy %2..\..\Test\bin\Debug\HYS.IM.Messaging.Test.exe %1\Device\Adapter

copy %2ApplyEntityConfig.bat %1\Device\Adapter
copy %2InitializeEntityConfig.bat %1\Device\Adapter
copy %2InstallService.bat %1\Device\Adapter
copy %2RegisterEntityToSolution.bat %1\Device\Adapter
copy %2SetServiceAutomatic.bat %1\Device\Adapter
copy %2SetServiceManual.bat %1\Device\Adapter
copy %2UninstallService.bat %1\Device\Adapter
copy %2UnregisterEntityFromSolution.bat %1\Device\Adapter

copy %2CreateInterfaceFolder.bat %1\Device
copy %2CreateMsgBoxFolder.bat %1\Device

rem --- Copy Message Box Files ---

copy %2..\bin\Debug\HYS.IM.Common.DataAccess.dll %1\Device\MsgBox
copy %2..\bin\Debug\HYS.IM.Common.Logging.dll %1\Device\MsgBox
copy %2..\bin\Debug\HYS.Common.Xml.dll %1\Device\MsgBox
copy %2..\bin\Debug\HYS.IM.Messaging.Base.dll %1\Device\MsgBox
copy %2..\bin\Debug\HYS.IM.Messaging.Mapping.dll %1\Device\MsgBox
copy %2..\bin\Debug\HYS.IM.Messaging.Objects.dll %1\Device\MsgBox
copy %2..\bin\Debug\HYS.IM.Messaging.Queuing.dll %1\Device\MsgBox

copy %2..\..\..\..\3rdParty\dotnet_tools\InstallUtil.exe %1\Device\MsgBox

copy %2..\..\..\MessageBox\Config\bin\Debug\HYS.IM.MessageBox.Config.exe %1\Device\MsgBox
copy %2..\..\..\MessageBox\Config\bin\Debug\HYS.IM.MessageBox.Objects.dll %1\Device\MsgBox
copy %2..\..\..\MessageBox\Manager\bin\Debug\HYS.IM.MessageBox.Manager.exe %1\Device\MsgBox

copy %2..\..\Config\bin\Debug\HYS.IM.Messaging.Config.exe %1\Device\MsgBox
copy %2..\..\Service\bin\Debug\HYS.IM.Messaging.Service.exe %1\Device\MsgBox
copy %2..\..\ServiceConfig\bin\Debug\HYS.IM.Messaging.ServiceConfig.exe %1\Device\MsgBox
copy %2..\..\ServiceConfig\bin\Debug\HYS.IM.Messaging.Management.dll %1\Device\MsgBox
copy %2..\..\Test\bin\Debug\HYS.IM.Messaging.Test.exe %1\Device\MsgBox

copy %2ApplyEntityConfig.bat %1\Device\MsgBox
copy %2InitializeEntityConfig.bat %1\Device\MsgBox
copy %2InstallService.bat %1\Device\MsgBox
copy %2RegisterEntityToSolution.bat %1\Device\MsgBox
copy %2SetServiceAutomatic.bat %1\Device\MsgBox
copy %2SetServiceManual.bat %1\Device\MsgBox
copy %2UninstallService.bat %1\Device\MsgBox
copy %2UnregisterEntityFromSolution.bat %1\Device\MsgBox

rem ----- begin : this should be refine in the future (Apply config need to load anther interface's assembly) : -----
copy %2..\..\..\FileAdapter\FileInboundInterface\bin\Debug\HYS.IM.FileAdapter.FileConfig.dll %1\Device\MsgBox
copy %2..\..\..\FileAdapter\FileInboundInterface\bin\Debug\HYS.IM.Common.FileOperation.dll %1\Device\MsgBox
rem ----- end : this should be refine in the future (Apply config need to load anther interface's assembly) : -----

rem --- Copy File Adapter Files ---

copy %2..\..\..\FileAdapter\FileInboundInterface\EntityWebConfig.xml %1\Device\FileIn
copy %2..\..\..\FileAdapter\FileInboundInterface\bin\Debug\HYS.IM.FileAdapter.FileInboundInterface.exe %1\Device\FileIn
copy %2..\..\..\FileAdapter\FileInboundInterface\bin\Debug\HYS.IM.FileAdapter.FileConfig.dll %1\Device\FileIn
copy %2..\..\..\FileAdapter\FileInboundInterface\bin\Debug\HYS.IM.Common.FileOperation.dll %1\Device\FileIn
copy %2..\..\..\EMRMessages\EMRMessages\bin\Debug\HYS.IM.EMRMessages.dll %1\Device\FileIn
copy %2..\..\..\Schema\Adam\Mapforce\Renji\emrx2cda_sc.xsl %1\Device\FileIn\XML\emrx2cda.xslt
copy %2..\..\..\Schema\Renji_IntegrationError.xsd %1\Device\FileIn\XML
copy %2..\..\..\Schema\Adam\Mapforce\Renji\Renji_IntegrationUpload.xsd %1\Device\FileIn\XML
copy %2..\..\..\Schema\Renji_IntegrationUpload.sch %1\Device\FileIn\XML
copy %2..\..\..\Schema\CDA\*.* %1\Device\FileIn\XML\CDA

copy %2..\..\..\FileAdapter\FileOutboundInterface\EntityWebConfig.xml %1\Device\FileOut
copy %2..\..\..\FileAdapter\FileOutboundInterface\bin\Debug\HYS.IM.FileAdapter.FileOutboundInterface.exe %1\Device\FileOut
copy %2..\..\..\FileAdapter\FileInboundInterface\bin\Debug\HYS.IM.FileAdapter.FileConfig.dll %1\Device\FileOut
copy %2..\..\..\EMRMessages\EMRMessages\bin\Debug\HYS.IM.EMRMessages.dll %1\Device\FileOut

copy %2..\..\..\DataTrackingInterface\DataTrackingInterface\EntityWebConfig.xml %1\Device\LogOut
copy %2..\..\..\DataTrackingInterface\DataTrackingInterface\bin\Debug\HYS.IM.DataTrackingInterface.exe %1\Device\LogOut
copy %2..\..\..\DataTrackingInterface\DataTrackingInterface\bin\Debug\HYS.IM.DataTrackingInterfaceConfig.dll %1\Device\LogOut
copy %2..\..\..\EMRMessages\EMRMessages\bin\Debug\HYS.IM.EMRMessages.dll %1\Device\LogOut

rem --- Copy Web Portal ---

xcopy %2..\..\..\PrecompiledWeb\Portal %1\Portal /E
del %1\Portal\bin\*.pdb
del %1\Portal\bin\*.xml

rem --- Copy Script Service Tool ---

copy %2..\bin\Debug\HYS.IM.Common.DataAccess.dll %1\Tools\ScriptService
copy %2..\bin\Debug\HYS.IM.Common.Logging.dll %1\Tools\ScriptService
copy %2..\bin\Debug\HYS.Common.Xml.dll %1\Tools\ScriptService
copy %2..\bin\Debug\HYS.IM.Messaging.Base.dll %1\Tools\ScriptService
copy %2..\bin\Debug\HYS.IM.Messaging.Mapping.dll %1\Tools\ScriptService
copy %2..\bin\Debug\HYS.IM.Messaging.Objects.dll %1\Tools\ScriptService
copy %2..\bin\Debug\HYS.IM.Messaging.Queuing.dll %1\Tools\ScriptService
copy %2..\bin\Debug\GotDotNet.Exslt.dll %1\Tools\ScriptService
copy %2..\bin\Debug\NMatrix.Schematron.dll %1\Tools\ScriptService

copy %2..\..\..\..\3rdParty\dotnet_tools\InstallUtil.exe %1\Tools\ScriptService

copy %2..\..\Service\bin\Debug\HYS.IM.Messaging.Service.exe %1\Tools\ScriptService
copy %2..\..\ServiceConfig\bin\Debug\HYS.IM.Messaging.ServiceConfig.exe %1\Tools\ScriptService
copy %2..\..\ServiceConfig\bin\Debug\HYS.IM.Messaging.Management.dll %1\Tools\ScriptService
copy %2..\..\Test\bin\Debug\HYS.IM.Messaging.Test.exe %1\Tools\ScriptService

copy %2InstallService.bat %1\Tools\ScriptService
copy %2SetServiceAutomatic.bat %1\Tools\ScriptService
copy %2SetServiceManual.bat %1\Tools\ScriptService
copy %2UninstallService.bat %1\Tools\ScriptService

copy %2..\..\..\Portal\Management.Service\bin\Debug\HYS.IM.Messaging.Management.Service.exe %1\Tools\ScriptService
copy %2..\..\..\Portal\Management.Service\NTServiceHost.xml %1\Tools\ScriptService
copy %2..\..\..\Portal\Management.Service\ScriptService.xml %1\Tools\ScriptService

rem --- Copy Xml Test Tool ---

copy %2..\..\..\MessageBox\XmlTest\bin\Debug\HYS.IM.XmlTest.exe %1\Tools\XmlTest
copy %2..\..\..\MessageBox\XmlTest\bin\Debug\HYS.IM.Common.DataAccess.dll %1\Tools\XmlTest
copy %2..\..\..\MessageBox\XmlTest\bin\Debug\HYS.IM.Common.Logging.dll %1\Tools\XmlTest
copy %2..\..\..\MessageBox\XmlTest\bin\Debug\HYS.Common.Xml.dll %1\Tools\XmlTest
copy %2..\..\..\MessageBox\XmlTest\XmlTest.xml %1\Tools\XmlTest

