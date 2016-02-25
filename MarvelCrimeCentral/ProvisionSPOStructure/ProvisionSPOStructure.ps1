Invoke-Expression (New-Object Net.WebClient).DownloadString('https://raw.githubusercontent.com/OfficeDev/PnP-PowerShell/master/Samples/Modules.Install/Install-OfficeDevPnPPowerShell.ps1')
Connect-SPOnline –Url https://aspc1605.sharepoint.com/ASPC/ –Credentials (Get-Credential)

Get-Command -Module *PnP*
#Super Hero List
Add-SPOContentType -Name "Super Hero" -Description "Use to store Super Hero data" -Group "Marvel Content Types" -ParentContentType $ct

Add-SPOField -DisplayName "Hero Name" -InternalName "HeroName" -Type Text -Group "Marvel CCU" -AddToDefaultView
Add-SPOField -DisplayName "Hero ID" -InternalName "HeroID" -Type Number -Group "Marvel CCU" -AddToDefaultView
Add-SPOField -DisplayName "Hero Location" -InternalName "HeroLocation" -Type Geolocation -Group "Marvel CCU" -AddToDefaultView
Add-SPOField -DisplayName "Hero Availability" -InternalName "HeroAvailability" -Type Choice -Group "Marvel CCU" -AddToDefaultView -Choices "Available","Busy"

Add-SPOFieldToContentType -Field "HeroName" -ContentType "Super Hero"
Add-SPOFieldToContentType -Field "HeroID" -ContentType "Super Hero"
Add-SPOFieldToContentType -Field "Hero Location" -ContentType "Super Hero"
Add-SPOFieldToContentType -Field "HeroAvailability" -ContentType "Super Hero"

New-SPOList -Title "Super Hero" -Url "SuperHero" -Template GenericList

Add-SPOContentTypeToList -List "Super Hero" -ContentType "Super Hero" -DefaultContentType
