rem Please run this bat file in VS.Net 2008 Command Prompt

cd %0/../

MSBuild HL7Test.csproj /t:Rebuild;Clean /p:Configuration=Release

