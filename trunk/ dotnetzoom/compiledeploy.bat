@echo off 

XCOPY C:\DNZ\DNZsource C:\inetpub\wwwdnz /D /E /R /K /H /I /f /Y /EXCLUDE:C:\DNZ\DNZsource\Exclude.txt

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