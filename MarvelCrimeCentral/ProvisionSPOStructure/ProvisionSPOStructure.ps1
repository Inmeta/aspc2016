##Invoke-Expression (New-Object Net.WebClient).DownloadString('https://raw.githubusercontent.com/OfficeDev/PnP-PowerShell/master/Samples/Modules.Install/Install-OfficeDevPnPPowerShell.ps1')
$creds = Get-Credential
$tenant = "https://aspc1605.sharepoint.com"
Connect-SPOnline –Url $tenant –Credentials $creds
Set-SPOTraceLog -On -Level Debug
Apply-SPOProvisioningTemplate -Path .\template.xml
New-SPOWeb -Title ASPC -Url ASPC -Description "" -Locale 1033 -Template STS#0
Connect-SPOnline –Url $tenant/ASPC –Credentials $creds
#Get-Command -Module *PnP*
#Super Hero List
New-SPOList -Title "Super Hero" -Url "SuperHero" -Template GenericList
Add-SPOContentTypeToList -List "Super Hero" -ContentType "Super Hero" -DefaultContentType

#Alert List
New-SPOList -Title "Alerts" -Url "Alerts" -Template GenericList
Add-SPOContentTypeToList -List "Alerts" -ContentType "Alert" -DefaultContentType

#Monitor Live Data List
New-SPOList -Title "MonitorLiveData" -Url "MonitorLiveData" -Template GenericList
Add-SPOContentTypeToList -List "MonitorLiveData" -ContentType "Live Data" -DefaultContentType
