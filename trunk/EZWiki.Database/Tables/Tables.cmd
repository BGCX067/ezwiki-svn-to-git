@echo off
REM: Command File Created by Microsoft Visual Database Tools
REM: Date Generated: 9/5/2006
REM: Authentication type: Windows NT
REM: Usage: CommandFilename [Server] [Database]

if '%1' == '' goto usage
if '%2' == '' goto usage

if '%1' == '/?' goto usage
if '%1' == '-?' goto usage
if '%1' == '?' goto usage
if '%1' == '/help' goto usage

osql -S %1 -d %2 -E -b -i "dbo.Area.tab"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.DBConnection.tab"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.SearchWord.tab"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.SearchWordIndexMember.tab"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.Topic.tab"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.Area.kci"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.DBConnection.kci"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.SearchWord.kci"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.SearchWordIndexMember.kci"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.Topic.kci"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.Area.fky"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.DBConnection.fky"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.SearchWord.fky"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.SearchWordIndexMember.fky"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.Topic.fky"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.Area.ext"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.DBConnection.ext"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.SearchWord.ext"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.SearchWordIndexMember.ext"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.Topic.ext"
if %ERRORLEVEL% NEQ 0 goto errors

goto finish

REM: How to use screen
:usage
echo.
echo Usage: MyScript Server Database
echo Server: the name of the target SQL Server
echo Database: the name of the target database
echo.
echo Example: MyScript.cmd MainServer MainDatabase
echo.
echo.
goto done

REM: error handler
:errors
echo.
echo WARNING! Error(s) were detected!
echo --------------------------------
echo Please evaluate the situation and, if needed,
echo restart this command file. You may need to
echo supply command parameters when executing
echo this command file.
echo.
pause
goto done

REM: finished execution
:finish
echo.
echo Script execution is complete!
:done
@echo on
