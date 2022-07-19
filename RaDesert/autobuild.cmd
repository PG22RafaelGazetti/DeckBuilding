@echo on

set SRC=.
set DEST=.\build\
set EXEC_NAME=RaDesert.exe
set RELEASE=.\release
set LOGFILE=.\autobuild.log
set UNITY_LOGFILE=.\unity-build.log

:: Removing all previous text from the logfile
type NUL > %LOGFILE%

goto :getopts
:usage
@echo Usage:
@echo   c:\> autobuild [--debug] [--help] [--pull]
@echo .
goto :bye


:: Setup the enviroment
:getopts
if /I "%1"=="/?" goto :usage
if /I "%1"=="--help" goto :usage
if /I "%1"=="--debug" set DEBUG=true & shift
if /I "%1"=="--pull" set PULL=true & shift
shift
if not "%1"=="" goto :getopts
if DEFINED DEBUG (
    @echo Debug: %DEBUG%
    @echo Pull from Git: %PULL%
)

:: In this version each time you pull it's gonna also build and generate a release version
if DEFINED PULL goto :source-control
goto :build-it goto:generate-release

:: pull from source control
:source-control
@echo :
@echo : Pulling

:: doing a git pull go get the most recent version of the project
git pull

:: Build the project
:build-it

:: Checking if a build folder exists, if so delete it then create a new one, otherwise just create one
if EXIST %DEST% rmdir /S /Q %DEST% >>%LOGFILE%
mkdir %DEST%  >>%LOGFILE%
@echo :
@echo : Building

:: Adding a line showing when the build started to the logfile
@echo : Build Started at %data% %time% > %LOGFILE%

::Lauches the Unity Project and build the game, then generates a logfile with the build information
"C:\Program Files\Unity\Hub\Editor\2021.2.17f1\Editor\Unity.exe" -projectPath .\ -buildWindows64Player %DEST%%EXEC_NAME% -logfile %UNITY_LOGFILE% -quit

:: Adding a line showing when the build finished to the logfile
@echo : Building finished at %data% %time% >>  %LOGFILE%

:: Adding the Erros (if any) that happened during the build to the logfile
findstr c:/"Error" %UNITY_LOGFILE% /N >> %LOGFILE%

:: Create the release
:generate-release

:: Checking if a release folder exists, if so delete it then create a new one, otherwise just create one
if EXIST %RELEASE% rmdir /S /Q %RELEASE% >>%LOGFILE%
mkdir %RELEASE%  >>%LOGFILE%

@echo :
@echo : Generating release

:: copying the build folder to the release folder
robocopy ".\build"  %RELEASE% /move /S

:: Putting the release folder with the build into a specific folder
move %RELEASE% "C:\Users\Public"

:: Removing the folder from the project folder
rmdir /S /Q %RELEASE%

:: Finish the autobuild
goto :bye

:: Publish the release
:publish

:: Clean up the variables
:bye
@echo :
@echo : Exit
set DEBUG=
set PULL=
set SRC=
set DEST=
set EXEC_NAME=
set RELEASE=