##Invoke-Expression (New-Object Net.WebClient).DownloadString('https://raw.githubusercontent.com/OfficeDev/PnP-PowerShell/master/Samples/Modules.Install/Install-OfficeDevPnPPowerShell.ps1')

$username = "admin@aspc1605.onmicrosoft.com"
$passwordAsSecureString = ConvertTo-SecureString "01000000d08c9ddf0115d1118c7a00c04fc297eb010000003457d85592b17a40962301bf7702ed640000000002000000000003660000c000000010000000e4fca020d7ac6590ee4b528e64dfac400000000004800000a000000010000000554b5f2d7e5b2db84651583c4ae86fe418000000f05a59c426dab874224d7d8a3036936fe093f23cf39986b21400000095e43020e15b2ab28550ba4a64235de4d8008bd5"
$creds = New-Object System.Management.Automation.PSCredential $username, $passwordAsSecureString
$tenant = "https://aspc1605.sharepoint.com"

Connect-SPOnline –Url $tenant/ASPC –Credentials $creds
Set-SPOTraceLog -On -Level Debug
Apply-SPOProvisioningTemplate -Path template.xml

#New-SPOWeb -Title ASPC -Url ASPC -Description "" -Locale 1033 -Template STS#0

#Get-Command -Module *PnP*
#Super Hero List
#New-SPOList -Title "Super Hero" -Url "SuperHero" -Template GenericList
#Add-SPOContentTypeToList -List "Super Hero" -ContentType "Super Hero" -DefaultContentType

#Alert List
#New-SPOList -Title "Alerts" -Url "Alerts" -Template GenericList
#Add-SPOContentTypeToList -List "Alerts" -ContentType "Alert" -DefaultContentType

#Monitor Live Data List
#New-SPOList -Title "MonitorLiveData" -Url "MonitorLiveData" -Template GenericList
#Add-SPOContentTypeToList -List "MonitorLiveData" -ContentType "Live Data" -DefaultContentType
