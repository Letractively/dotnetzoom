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
Rem - The file VBC.EXE appears to exist in the v2.0 directory
:VBC2Exist
SET DotNetFrameworkDir=v2.0.50727
GoTo Action
Rem - This where we will action what we have learned.
Rem - There is a small change where we will pull the information required for
Rem - the build from a response file. This is easier to read and understand.
:Action
Set VBCPATH=%SYSTEMROOT%\Microsoft.NET\Framework\%DotNetFrameworkDir%\VBC.EXE /noconfig
%VBCPATH% @mk.rsp
GoTo End
:End
Pause
