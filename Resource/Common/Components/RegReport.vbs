Dim WshShell,myvar,RegPath
Dim fso,file,strDesktop
RegPath="HKLM\SOFTWARE\Webb Electronics\WebbReport\"
Set WshShell= Wscript.CreateObject("Wscript.Shell")
WshShell.RegWrite RegPath & "WebbReportViewer\Version","Release 1.3.0.9a" 
 
'rem  delete link to WebbReport.exe in your desktop
strDesktop = WshShell.SpecialFolders("Desktop")
file=strDesktop+"\WebbReport.lnk"
Set fso=CreateObject("Scripting.FileSystemObject")
if(fso.FileExists(file)) then
    fso.deletefile(file)
end if
set fso=nothing 
 
'rem  register dll in computer
WshShell.run("regsvr32.exe /s ""C:\Program Files\Webb Electronics\Interactive Report\WebbGameData.dll""")
WshShell.run("regsvr32.exe /s ""C:\Program Files\Webb Electronics\Interactive Report\WebbShareMemory.dll""") 
 
Set WshShell =nothing
msgbox "Succeeded in installing ""Webb Report Browser(Release 1.3.0.9a)""",0+64,"Webb Report Browser(WRB)"
