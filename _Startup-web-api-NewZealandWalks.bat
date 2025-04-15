REM Le cours sur udemy
REM https://mern.udemy.com/course/build-rest-apis-with-aspnet-core-web-api-entity-framework/learn/lecture/36980348#overview
REM repo github: https://github.com/jfdesjardins2000/aspnet-sandbox

@echo off
chcp 65001 >nul
setlocal enabledelayedexpansion
Set _waitSec=2

REM Obtenir le chemin du dossier contenant ce script
set "root=%~dp0"

REM Nettoyer le \ final si nécessaire (pas obligatoire mais propre)
set "root=%root:~0,-1%"

echo "Ouvrir : udemy - build asp.net core web api (.net 8).docx"
start "" "%root%\UdeMy - Build ASP.NET Core Web API (.NET 8).docx"
timeout /t %_waitsec% >nul

REM Ouvrir le sous-dossier du projet relatif à ce script
echo "Ouvrir dossier : NewZealandWalks"
explorer /e,"%root%\NewZealandWalks"
timeout /t %_waitSec% >nul

echo "Ouvrir : git-cmd.exe"
start "" "C:\Program Files\Git\git-cmd.exe"
timeout /t %_waitSec% >nul

rem ********************** VS2022 ********************************
echo "Ouvrir VS2022 : \prof\NZWalks-Solution-master\NZWalks.sln"
start "" "%root%\prof\NZWalks-Solution-master\NZWalks.sln"
timeout /t %_waitSec% >nul

echo "Ouvrir ma solution VS2022 :  NewZealandWalks.sln"
start "" "%root%\NewZealandWalks\NewZealandWalks.sln"
timeout /t %_waitSec% >nul
rem **************************************************************

exit