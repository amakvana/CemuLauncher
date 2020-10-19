# CemuLauncher Guide

## Requirements
* Latest [Microsoft .NET Framework](https://go.microsoft.com/fwlink/?linkid=2088631)
* Latest [Cemu](https://cemu.info/), setup and [configured](https://cemu.cfw.guide/installing-cemu)
* [Visual C++ 2017 X64 Redistributable](https://aka.ms/vs/16/release/vc_redist.x64.exe)

## Methodology 
* Reads ```settings.xml``` to create baseline configuration
* Creates key-value pair for each Title ID & chosen API
* Switches API before game execution 

## Initial Setup  
1. Ensure Cemu is up-to-date and fully configured (mlc01 & gamepaths set)
2. Place CemuLauncher.exe alongside Cemu.exe in the root of your Cemu folder
3. Run CemuLauncher.exe
4. ```Right-click``` on each game and click on ```Edit Graphics API``` to set per-game API's 
5. Double-click on a game to run cemu with chosen Per-Game Graphics API

* Per-Game Graphics API settings are stored in CemuLauncherSettings.xml

## GUI Usage 
1. Run CemuLauncher 
2. ```Right-click``` on each game and click on ```Edit Graphics API``` to set per-game API's 
3. Double-click on a game to run cemu with chosen Per-Game Graphics API

* Per-Game Graphics API settings are stored in CemuLauncherSettings.xml

## Command Line Usage 
1. Open a command prompt 
2. Change directories into the Cemu root folder 
3. Run CemuLauncher.exe from the command prompt using the ```-g``` switch. 

* ```CemuLauncher.exe -g "<full path to game rpx>"```

### Examples 
* ```CemuLauncher.exe -g "D:\Games\Captain Toad Treasure Tracker [AKBP01]\code\Kinopio.rpx"```
* ```CemuLauncher.exe -g "Kinopio.rpx"```

## Emulation FrontEnd Usage
1. Follow the setup guides for both Cemu and CemuLauncher
2. In your chosen FrontEnd, replace game entries ```cemu.exe -g``` with ```cemulauncher.exe -g```
