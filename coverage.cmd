@echo off

cmd /c "nuget restore Biblioseca.sln"
cmd /c "msbuild Biblioseca.sln /m /nr:false /t:Rebuild /p:Configuration=Debug /p:Platform="Any CPU" /verbosity:minimal /p:CreateHardLinksForCopyLocalIfPossible=true" /clp:Summary;ShowCommandLine;ErrorsOnly

cmd /c "cd /d Biblioseca.Test && msbuild /m /nr:false /t:Rebuild /p:Configuration=Debug /verbosity:minimal /clp:Summary;ShowCommandLine;ErrorsOnly



mkdir TestResults

packages\OpenCover.4.7.1221\tools\OpenCover.Console.exe -register:user -target:"packages\NUnit.ConsoleRunner.3.12.0\tools\nunit3-console.exe" -targetargs:"Biblioseca.Test\bin\Debug\Biblioseca.Test.dll" -output:TestResults\opencover.xml -register:user -filter:"+[*]* -[Moq*]*"

packages\ReportGenerator.4.8.12\tools\net47\ReportGenerator.exe  -reports:TestResults\opencover.xml -targetdir:TestResults