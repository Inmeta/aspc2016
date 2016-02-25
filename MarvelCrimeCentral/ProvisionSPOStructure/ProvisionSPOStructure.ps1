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

#Alert List
Add-SPOContentType -Name "Alert" -Description "Use to store alert information" -Group "Marvel Content Types" -ParentContentType $ct

Add-SPOField -DisplayName "Alert ID" -InternalName "AlertID" -Type Text -Group "Marvel CCU" -AddToDefaultView
Add-SPOField -DisplayName "AlertCoordinatesX" -InternalName "AlertCoordinatesX" -Type Text -Group "Marvel CCU" -AddToDefaultView
Add-SPOField -DisplayName "AlertCoordinatesY" -InternalName "AlertCoordinatesY" -Type Text -Group "Marvel CCU" -AddToDefaultView
Add-SPOField -DisplayName "Alert SuperHero" -InternalName "AlertSuperHero" -Type Choice -Group "Marvel CCU" -AddToDefaultView -Choices "Hulk","Thor","Black Widow", "Ironman", "Spiderman"
Add-SPOField -DisplayName "Alert Status" -InternalName "AlertStatus" -Type Choice -Group "Marvel CCU" -AddToDefaultView -Choices "Received","Assigned","Completed"

Add-SPOFieldToContentType -Field "AlertID" -ContentType "Alert"
Add-SPOFieldToContentType -Field "AlertCoordinatesX" -ContentType "Alert"
Add-SPOFieldToContentType -Field "AlertCoordinatesY" -ContentType "Alert"
Add-SPOFieldToContentType -Field "AlertSuperHero" -ContentType "Alert"
Add-SPOFieldToContentType -Field "AlertStatus" -ContentType "Alert"

New-SPOList -Title "Alerts" -Url "Alerts" -Template GenericList

Add-SPOContentTypeToList -List "Alerts" -ContentType "Alert" -DefaultContentType

#Monitor Live Data List
Add-SPOContentType -Name "Live Data" -Description "Use to monitor live Data sent from the Band" -Group "Marvel Content Types" -ParentContentType $ct

Add-SPOField -DisplayName "Data Type" -InternalName "DataType" -Type Text -Group "Marvel CCU" -AddToDefaultView
Add-SPOField -DisplayName "Data Value" -InternalName "DataValue" -Type Text -Group "Marvel CCU" -AddToDefaultView


Add-SPOFieldToContentType -Field "AlertID" -ContentType "Live Data"
Add-SPOFieldToContentType -Field "DataType" -ContentType "Live Data"
Add-SPOFieldToContentType -Field "DataValue" -ContentType "Live Data"

New-SPOList -Title "MonitorLiveData" -Url "MonitorLiveData" -Template GenericList

Add-SPOContentTypeToList -List "MonitorLiveData" -ContentType "Live Data" -DefaultContentType
