set filepath=E:\Webb.Net1.1Version
set patchDirve=E:
set Patchpath=E:\����ر�

set version=1.3.0.9a

set winrRarPath="C:\Program Files\WinRAR\Winrar.exe"
set DesignerPatch="%Patchpath%\Interactive Report(Release %version%)).exe"
set BrowserPatch="%Patchpath%\Interactive Report(Browser)(Release %version%).exe"

rem ����RegReport.vbs
set RegFile="%Patchpath%\Interactive Report\RegReport.vbs"
set OldRegFile="%Patchpath%\Interactive Report\Reg.bat"
if exist %OldRegFile% del %OldRegFile%
if exist %RegFile% del %RegFile%
echo Dim WshShell,myvar,RegPath>>%RegFile%
echo RegPath="HKLM\SOFTWARE\Webb Electronics\WebbReport\">>%RegFile%
echo Set WshShell= Wscript.CreateObject("Wscript.Shell")>>%RegFile%
echo WshShell.RegWrite RegPath ^& "Version","%version%" >>%RegFile%
echo. >>%RegFile%
echo 'rem  register dll in computer>>%RegFile%
echo WshShell.run("regsvr32.exe /s ""C:\Program Files\Webb Electronics\Interactive Report\WQPActive.ocx""") >>%RegFile%
echo WshShell.run("regsvr32.exe /s ""C:\Program Files\Webb Electronics\WebbRepWizard\WebbGameData.dll""")>>%RegFile%
echo WshShell.run("regsvr32.exe /s ""C:\Program Files\Webb Electronics\WebbRepWizard\WebbShareMemory.dll""") >>%RegFile%
echo. >>%RegFile%
echo Set WshShell =nothing>>%RegFile%
echo msgbox "Success to install ""Webb Report Designer(%version%)""",0+64,"Success installing Designer">>%RegFile%


rem ɾ���ļ�
if exist %DesignerPatch% del  %DesignerPatch%
if exist %BrowserPatch% del  %BrowserPatch%

rem �����ļ�
copy "%filepath%\Bin\Designer\Webb.dll" "%Patchpath%\Interactive Report\"
copy "%filepath%\Bin\Designer\Webb.*.dll" "%Patchpath%\Interactive Report\"
copy "%filepath%\Bin\Designer\WebbReport.exe" "%Patchpath%\Interactive Report\"
copy "%filepath%\Bin\Browser\WebbRepBrowser.exe" "%Patchpath%\Interactive Report\"
copy "%filepath%\Resource\DevExpressLibrary\Dev*.*" "%Patchpath%\Interactive Report\"

%patchDirve%
cd %Patchpath%


rem '����ļ�
%winrRarPath% a -sfx -z"%Patchpath%\Comment(D&B).txt" %DesignerPatch% "Interactive Report"

del /f /s /q "%Patchpath%\Interactive Report\WebbReport.exe"

rem ����RegReport.vbs
if exist %RegFile% del %RegFile%
echo Dim WshShell,myvar,RegPath>>%RegFile%
echo Dim fso,file,strDesktop>>%RegFile%
echo RegPath="HKLM\SOFTWARE\Webb Electronics\WebbReport\">>%RegFile%
echo Set WshShell= Wscript.CreateObject("Wscript.Shell")>>%RegFile%
echo WshShell.RegWrite RegPath ^& "Version","%version%" >>%RegFile%
echo. >>%RegFile%
echo 'rem  delete link to WebbReport.exe in your desktop>>%RegFile%
echo strDesktop = WshShell.SpecialFolders("Desktop")>>%RegFile%
echo file=strDesktop+"\WebbReport.lnk">>%RegFile%
echo Set fso=CreateObject("Scripting.FileSystemObject")>>%RegFile%
echo if(fso.FileExists(file)) then>>%RegFile%
echo     fso.deletefile(file)>>%RegFile%
echo end if>>%RegFile%
echo set fso=nothing >>%RegFile%
echo. >>%RegFile%
echo 'rem  register dll in computer>>%RegFile%
echo WshShell.run("regsvr32.exe /s ""C:\Program Files\Webb Electronics\Interactive Report\WQPActive.ocx""") >>%RegFile%
echo WshShell.run("regsvr32.exe /s ""C:\Program Files\Webb Electronics\WebbRepWizard\WebbGameData.dll""")>>%RegFile%
echo WshShell.run("regsvr32.exe /s ""C:\Program Files\Webb Electronics\WebbRepWizard\WebbShareMemory.dll""") >>%RegFile%
echo. >>%RegFile%
echo Set WshShell =nothing>>%RegFile%
echo msgbox "Success to install ""Webb Report Browser(%version%)""",0+64,"Success installing Browser">>%RegFile%

%winrRarPath%  a -sfx -z"%Patchpath%\Comment(B).txt" %BrowserPatch% "Interactive Report"
