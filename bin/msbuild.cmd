@echo off

for /f "usebackq tokens=*" %%i in (`%~dp0vswhere -latest -requires Microsoft.Component.MSBuild -property installationPath`) do (
  set InstallDir=%%i
)

if exist "%InstallDir%\MSBuild\15.0\Bin\MSBuild.exe" (
  "%InstallDir%\MSBuild\15.0\Bin\MSBuild.exe" %*
  exit %errorlevel%
)

echo "MSBuild not found!"
exit 127
