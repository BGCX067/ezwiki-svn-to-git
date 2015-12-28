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

osql -S %1 -d %2 -E -b -i "dbo.Area_Delete.prc"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.Area_GetAll.prc"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.Area_GetByAreaID.prc"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.Area_Persist.prc"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.DBConnection_Delete.prc"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.DBConnection_GetByConnectionNameAndAreaID.prc"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.DBConnection_Persist.prc"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.SearchWord_Delete.prc"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.SearchWord_DeleteAll.prc"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.SearchWord_Persist.prc"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.SearchWordIndexMember_Delete.prc"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.SearchWordIndexMember_DeleteAll.prc"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.SearchWordIndexMember_DeleteByTopicID.prc"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.SearchWordIndexMember_Persist.prc"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.Topic_Delete.prc"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.Topic_GetAll.prc"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.Topic_GetBySearchWordAndCurrency.prc"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.Topic_GetByTitleAndAreaID.prc"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.Topic_GetByTitleVersionAndAreaID.prc"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.Topic_GetByTopicID.prc"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.Topic_GetHistoryByTitle.prc"
if %ERRORLEVEL% NEQ 0 goto errors
osql -S %1 -d %2 -E -b -i "dbo.Topic_Persist.prc"
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
