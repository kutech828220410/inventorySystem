@echo off
title SunloginKeeper
cls

set runAppPath=".\\�����Įw�t��.exe"

set _interval=10

set runAppFolder=''
set _processName=''
set _processNameExt=''
if '%runAppPath%'=='' (goto end)

for %%a in (%runAppPath%) do (
set runAppFolder=%%~dpa
set _processName=%%~na
set _processNameExt=%%~nxa
)
::echo %runAppPath%
::echo %runAppFolder%
::echo %_processName%
::echo %_processNameExt%
goto checkstart
 
:checkstart


:startApp

pushd %runAppFolder%

echo %date:~0,10% %time:~0,8%: %runAppPath%

start "" %runAppPath%

popd
 
:checkend

choice /t %_interval% /d y /n >nul
goto checkstart
 
:end
echo end.
pause