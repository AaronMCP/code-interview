rem Please run this bat file in VS.Net 2008 Command Prompt

cd %0/../

MSBuild HL7GatewayBVT.sln /t:Rebuild;Clean /p:Configuration=Release

