
set OUTPATH=%1
set ARCHIVE=%2
set TARGET=%3
cd %OUTPATH%
REM "c:\archivos de programa\winrar\rar.exe" a  %ARCHIVE%  %TARGET% *.dll
"c:\archivos de programa\winrar\winrar.exe" a -afzip %ARCHIVE%  %TARGET% *.dll *.chm README.txt
exit 0