@echo off
Echo.
Echo ************************************
Echo * dotnetzoom Command Line Compiler *
Echo ************************************
Echo.
cd c:\DNZ\DNZsource
:MkRspCheck
Rem - Let's make sure the response file is there.
If exist mk.rsp GoTo VBCExistCheck
Echo You don't seem to have the response file.
Echo Please place the MK.RSP file in your dotnetzoom Root directory.
Goto End
:VBCExistCheck
Rem - Check for the VBC component first
If exist %SYSTEMROOT%\Microsoft.NET\Framework\v2.0.50727\VBC.exe GoTo VBC2Exist
If exist %SYSTEMROOT%\Microsoft.NET\Framework\v1.1.4322\VBC.exe GoTo VBC1Exist
If exist %SYSTEMROOT%\Microsoft.NET\Framework\v1.0.3705\VBC.exe GoTo VBC0Exist
Rem - The file VBC.EXE doesn't appear to exist.
Echo You don't seem to have the Microsoft .NET Framwork installed.
Echo Please read the documentation for further help.
GoTo End
Rem - The file VBC.EXE appears to exist in the v1.0 directory
:VBC0Exist
SET DotNetFrameworkDir=v1.0.3705
GoTo Action
Rem - The file VBC.EXE appears to exist in the v1.1 directory
:VBC1Exist
SET DotNetFrameworkDir=v1.1.4322
GoTo Action
Rem - The file VBC.EXE appears to exist in the v2.0 directory
:VBC2Exist
SET DotNetFrameworkDir=v2.0.50727
%SYSTEMROOT%\Microsoft.NET\Framework\%DotNetFrameworkDir%\VBC.EXE /noconfig @mk.rsp 
GoTo PRECOMPILE
GoTo End
:PRECOMPILE
%SYSTEMROOT%\Microsoft.NET\Framework\%DotNetFrameworkDir%\Aspnet_compiler -v / -p C:\DNZ\DNZsource -d -errorstack -f C:\DNZ\dotnetzoom
pause
XCOPY C:\DNZ\DNZsource\*.skin C:\DNZ\dotnetzoom /s 
GoTo End
:Action
Set VBCPATH=%SYSTEMROOT%\Microsoft.NET\Framework\%DotNetFrameworkDir%\VBC.EXE %VBCPATH% @mk.rsp 
GoTo End
:End
pause
Echo OK
