@echo on
@echo :
@echo : this is just a test

set SRC=.
set DEST=.\build\
set EXEC_NAME=RaDesert.exe
set RELEASE=.\release
set LOGFILE=.\autobuild.log
set UNITY_LOGFILE=.\unity-build.log
set TIMESTAMP=%date:~0,3%-%date:~4,2%-%date:~7,2%@%now%

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

::for now if you do a pull it's gonna also build and generate a release version
if DEFINED PULL goto :source-control
goto :build-it goto:generate-release

:: pull from source control
:source-control
@echo :
@echo : Pulling
git pull

:: Build the project
:build-it
if EXIST %DEST% rmdir /S /Q %DEST% >>%LOGFILE%
mkdir %DEST%  >>%LOGFILE%
@echo :
@echo : Building
@echo : Build Started at %data% %time% > %LOGFILE%
::Lauches the Unity Project "C:\Program Files\Unity\Hub\Editor\2021.2.17f1\Editor\Unity.exe" -projectPath "C:\Users\Public\Repos\DeckBuilding\RaDesert"
"C:\Program Files\Unity\Hub\Editor\2021.2.17f1\Editor\Unity.exe" -projectPath .\ -buildWindows64Player %DEST%%EXEC_NAME% -logfile %UNITY_LOGFILE% -quit

@echo : Building finished >>  %LOGFILE%
findstr c:/"Error" %UNITY_LOGFILE% /N >> %LOGFILE%

:: Create the release
:generate-release
if EXIST %RELEASE% rmdir /S /Q %RELEASE% >>%LOGFILE%
mkdir %RELEASE%  >>%LOGFILE%
@echo :
@echo : Generating release
robocopy ".\build"  ".\release" /move /S
move ".\release" "C:\Users\Public"
rmdir /S /Q ".\release"
goto :bye

:: Publish the release
:publish

:: Clean up
:bye
@echo :
@echo : Exit
set DEBUG=
set PULL=
set SRC=
set DEST=
set EXEC_NAME=
set RELEASE=