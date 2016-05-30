set patchDirve=D:
set Patchpath=D:\WebbReport\Webb.Net3.5Version\Webb Report Wizard
set SigntoolPath=D:\WebbReport\Webb.Net3.5Version\Resource\SignTool

set TargetProductDir=C:\Program Files\Webb Electronics

rem if exist "%Patchpath%\BackUp" xcopy "%Patchpath%\BackUp\*.*" "%Patchpath%\Debug\BackUp\"

set version=%1
if "%version%"=="" set version=2.1a
set winrRarPath="C:\Program Files\WinRAR\rar.exe"
set PatchFile="%Patchpath%\WebbRepWizard(Ver %version%) Setup.exe"

for %%i in ("%Patchpath%\*.exe") do (
    del "%%i"
   )

set RegFile="%Patchpath%\Debug\StartSetup.vbs"
if exist %RegFile% del %RegFile%

set RunBatAfterInstall="%TargetProductDir%\RemoveInstallDirectory.bat"

echo Option Explicit>>%RegFile% 
echo Dim WshShell,WUninstallReturn,WInstallReturn>>%RegFile%
rem  echo WScript.sleep(500)>>%RegFile%
echo Set WshShell = WScript.CreateObject("WScript.Shell")>>%RegFile%
echo WInstallReturn=0>>%RegFile%
echo WUninstallReturn=RemoveLastInstall(WshShell)>>%RegFile%
echo if WUninstallReturn then>>%RegFile%
echo     WInstallReturn=WshShell.run("MsiExec.exe /i ""%TargetProductDir%\Debug\Webb Report Wizard.msi""",1,true)>>%RegFile% 
echo end if>>%RegFile%

echo WshShell.run "cmd.exe /c echo rd /s /q ""%TargetProductDir%\Debug"">"%RunBatAfterInstall%"",0,true>>%RegFile%
echo WshShell.run "cmd.exe /c echo del /f /q %%0>>"%RunBatAfterInstall%"",0,true>>%RegFile% 
echo WshShell.run ""%RunBatAfterInstall%"",0,false>>%RegFile% 
echo set WshShell=nothing>>%RegFile% 

echo wscript.quit>>%RegFile% 


rem Sub for remove last Install
echo.>>%RegFile%
echo.>>%RegFile%
echo 'Sub for remove last Install >>%RegFile%
echo function RemoveLastInstall(byref WshShell)>>%RegFile%
echo   on error resume next>>%RegFile%
echo   Dim keys,fso,strDesktop,strInstallPath,file,WReturn, strOveerrideMessage,dialogResult>>%RegFile%
echo   WReturn=true>>%RegFile%
echo   keys = wshshell.regread ("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{53E39EA4-AC8F-46A2-9728-9BA5D57D1349}\UninstallString")>>%RegFile%
echo   If err.number=-2147024894 Then    '-2147024894 represents the key didn't exist >>%RegFile%
echo      err.clear>>%RegFile%
echo      Set fso=CreateObject("Scripting.FileSystemObject")>>%RegFile%
echo      strDesktop = WshShell.SpecialFolders("Desktop")>>%RegFile%
echo      strInstallPath="C:\Program Files\Webb Electronics\WebbRepWizard">>%RegFile%    
echo      file=strDesktop+"\Webb Report Wizard.lnk">>%RegFile%
echo      if(fso.FileExists(file)) then>>%RegFile%
echo        fso.deletefile(file)>>%RegFile%
echo      end if>>%RegFile%
echo      strDesktop = WshShell.SpecialFolders("AllUsersDesktop")>>%RegFile%
echo      file=strDesktop+"\Webb Report Wizard.lnk">>%RegFile%
echo      if(fso.FileExists(file)) then>>%RegFile%
echo         fso.deletefile(file)>>%RegFile%
echo      end if>>%RegFile%
echo      strDesktop = WshShell.SpecialFolders("StartMenu")>>%RegFile%
echo      file=strDesktop+"\Webb Report Wizard.lnk">>%RegFile%
echo      if(fso.FileExists(file)) then>>%RegFile%
echo         fso.deletefile(file)>>%RegFile%
echo      end if>>%RegFile%
echo      if fso.FolderExists(strInstallPath) then>>%RegFile%
echo         fso.DeleteFile(strInstallPath+"\*.dll"), True>>%RegFile%
echo         fso.DeleteFile(strInstallPath+"\*.exe"), True>>%RegFile% 
echo         fso.DeleteFile(strInstallPath+"\*.manifest"), True>>%RegFile%
echo         fso.DeleteFile(strInstallPath+"\*.bat"), True>>%RegFile%
echo         fso.DeleteFile(strInstallPath+"\*.vbs"), True>>%RegFile%
echo      end if>>%RegFile%
echo      if fso.FolderExists(strInstallPath+"\Template\BackUp") then>>%RegFile%
echo         fso.DeleteFolder(strInstallPath+"\Template\BackUp"), True>>%RegFile%   
echo      end if>>%RegFile%
echo      set fso=nothing>>%RegFile% 
echo   ElseIf err.number=0 then >>%RegFile%
echo        WScript.sleep(500)>>%RegFile%
echo        strOveerrideMessage = "One version of this product is already installed in your computer,continue?">>%RegFile%
echo        strOveerrideMessage=strOveerrideMessage + Chr(13)+ Chr(10) + "............................................">>%RegFile%
echo        strOveerrideMessage =strOveerrideMessage ++ Chr(13)+ Chr(10)+ Chr(13)+ Chr(10) +"Here are two options that you can select for this process:">>%RegFile%
echo        strOveerrideMessage=strOveerrideMessage+ Chr(13)+ Chr(10) +  Chr(13)+ Chr(10) +"Click ""OK"" to uninstall the previous version and then install the new version">>%RegFile%
echo        strOveerrideMessage=strOveerrideMessage+ Chr(13)+ Chr(10) +  Chr(13)+ Chr(10) +"Click ""Cancel"" to exit the process of this setup.">>%RegFile%         
echo        dialogResult=msgbox(strOveerrideMessage,1+32,"Continue?")>>%RegFile%
echo        if dialogResult=1 then>>%RegFile%
echo            dialogResult=WshShell.run ("MsiExec.exe /x {53E39EA4-AC8F-46A2-9728-9BA5D57D1349} /qr",1,true)>>%RegFile%
rem                           'Use "MsiExec.exe /x{53E39EA4-AC8F-46A2-9728-9BA5D57D1349} /qn" can uninstall it silently
echo            if err then>>%RegFile%   
echo                 msgbox err.number ^& ":" ^& err.description,0+16,"Error" ^& err.Source >>%RegFile%
echo                 WReturn=false>>%RegFile%   
echo            end if>>%RegFile% 
echo            if dialogResult then>>%RegFile%  
echo                 WReturn=false>>%RegFile%    
echo            end if>>%RegFile%  
echo        else>>%RegFile%
echo            WReturn=false>>%RegFile%   
echo        end if>>%RegFile%
echo   Else >>%RegFile%
echo       msgbox err.number ^& ":" ^& err.description,0+16,"Error" ^& err.Source >>%RegFile%
echo       WReturn=false>>%RegFile%    
echo   End If>>%RegFile% 
echo   RemoveLastInstall=WReturn>>%RegFile% 
echo end function>>%RegFile%

%patchDirve%
cd "%Patchpath%"

rem 'package file
%winrRarPath% a -sfx -z"%Patchpath%\Comment(RepWizard).txt" %PatchFile% "Debug"

cd %SigntoolPath%
signtool sign /f "WebbSign.pfx" /d "Webb Report Wizard Setup Package" /p "Webb1410" %PatchFile%

start "" "%Patchpath%"

exit