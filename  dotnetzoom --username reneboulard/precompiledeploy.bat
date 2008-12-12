@echo off 

del c:\inetpub\wwwtest\*.*
del c:\inetpub\wwwtest\bin\*.* 
RMDIR c:\inetpub\wwwtest\Documentation /S
RMDIR c:\inetpub\wwwtest\Components /S
RMDIR c:\inetpub\wwwtest\controls\AdvFileManager /S
RMDIR c:\inetpub\wwwtest\controls\CountryListBox /S
RMDIR c:\inetpub\wwwtest\controls\SharpZipLib /S
RMDIR c:\dnz\dotnetzoom\Documentation /S
RMDIR c:\dnz\dotnetzoom\Components /S
RMDIR c:\dnz\dotnetzoom\controls\AdvFileManager /S
RMDIR c:\dnz\dotnetzoom\controls\CountryListBox /S
RMDIR c:\dnz\dotnetzoom\controls\SharpZipLib /S
pause

XCOPY C:\DNZ\dotnetzoom C:\inetpub\wwwtest /D /E /R /K /H /I /f /Y /EXCLUDE:C:\DNZ\DNZsource\Exclude.txt
	


if errorlevel 4 goto lowmemory 
if errorlevel 2 goto abort 
if errorlevel 0 goto exit 

:lowmemory 
echo Insufficient memory to copy files or 
echo invalid drive or command-line syntax. 
goto exit 

:abort 
echo You pressed CTRL+C to end the copy operation. 
goto exit 

:exit 
echo OK
pause