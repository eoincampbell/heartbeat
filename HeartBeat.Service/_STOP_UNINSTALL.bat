REM ######################################################
REM THESE FILES WILL BE COPIED TO THE BIN OUTPUT DIRECTORY
REM ######################################################

NET STOP HeartBeat

PAUSE 

C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /u "HeartBeat.Service.exe"

PAUSE