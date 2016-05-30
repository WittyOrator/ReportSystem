for %%i in (*.exe) do (
 if not "%%i"=="signtool.exe" signtool sign /f "WebbSign.pfx" /p "Webb1410" "%%i"
)                      
pause
