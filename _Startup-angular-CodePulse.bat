REM Le cours sur udemy
REM https://mern.udemy.com/course/real-world-app-angular-aspnet-core-web-api-and-sql/learn/lecture/38606284#overview
REM repo github: https://github.com/jfdesjardins2000/aspnet-sandbox


@echo off
chcp 65001 >nul
setlocal enabledelayedexpansion
Set _waitSec=2

REM Obtenir le chemin du dossier contenant ce script
set "root=%~dp0"

REM Nettoyer le \ final si nécessaire (pas obligatoire mais propre)
set "root=%root:~0,-1%"

echo "Ouvrir : UdeMy - ANGULAR and ASP.NET Core REST API - Real World Application.docx"
start "" "%root%\UdeMy - ANGULAR and ASP.NET Core REST API - Real World Application.docx"
timeout /t %_waitSec% >nul

REM Ouvrir le sous-dossier du projet relatif à ce script
echo "Ouvrir dossier : CodePulse"
explorer /e,"%root%\CodePulse"
timeout /t %_waitSec% >nul

echo "Ouvrir : git-cmd.exe"
start "" "C:\Program Files\Git\git-cmd.exe"
timeout /t %_waitSec% >nul

rem ********************** VS2022 ********************************
echo "Ouvrir ma solution VS2022 : CodePulse.sln"
start "" "%root%\CodePulse\CodePulse.sln"
timeout /t %_waitSec% >nul
rem **************************************************************

rem ********************** VSCODE ********************************
echo "Ouvrir VSCODE : CodePulse.UI-master"
start /b code "%root%\prof\CodePulse.UI-master"
timeout /t %_waitSec% >nul

echo "Ouvrir VSCODE : CodePulse.UI"
start /b code "%root%\CodePulse\CodePulse.UI"
timeout /t %_waitSec% >nul
rem **************************************************************

exit
