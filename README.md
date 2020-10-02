# CemuLauncher
A custom Launcher for Cemu. 

Designed for those who require advanced features such as Per-Game Graphics API settings and Pause/Resume/Stop functionality for Cemu. 

## Overview
![CemuLauncher](images/cl.png)

### Methodology 
* Reads ```settings.xml``` to create baseline configuration
* Creates key-value pair for each Title ID & chosen API
* Switches API before game execution 

### Usage 
1. Ensure Cemu is up-to-date and fully configured (mlc01 & gamepaths set)
2. Place CemuLauncher.exe alongside Cemu.exe in the root of your Cemu folder
3. Run CemuLauncher
4. ```Right-click``` on each game and click on ```Edit Graphics API``` to set per-game API's 
5. Double-click on a game to run cemu with chosen Per-Game Graphics API

* Per-Game Graphics API settings are stored in CemuLauncherSettings.xml

## Downloads
https://github.com/amakvana/CemuLauncher/releases/latest

Requires: 
* Latest [Microsoft .NET Framework](https://go.microsoft.com/fwlink/?linkid=2088631)
* Latest [Cemu](https://cemu.info/), setup and [configured](https://cemu.cfw.guide/installing-cemu)
* [Visual C++ 2017 X64 Redistributable](https://aka.ms/vs/16/release/vc_redist.x64.exe)

## Installation
CemuLauncher.exe must be placed alongside Cemu.exe.

![CemuLauncherSetup](images/cl-setup.png)

CemuLauncher does not require Administrator privileges to run.

## Hashes 
Hashes of latest CemuLauncher.exe below: 
Hash | Value
--- | ---
MD5 | a1f8c0495512071f2ec10d0837347e34
SHA1 | 4ac90ba08b65db49c8e652ee12205316416c0b26
SHA256 | e4d153bb7855b0d5c86b084c6ce3bc0fb99712873d8d6dc1d0b970422daa518d
SHA512 | 487c156b3fe585301c2776aeff870cc05fbe30c8cf9f3312942093202d1c407caf7a230d5debcb22c1fd5fd03ebe3dc95e061e0f6700af31d34f72de1aa02e5c

## Acknowledgements
Thanks:
* [Cemu Team](https://cemu.info/) - Nintendo Wii U Emulator Developers 
* [Baptiste Roussel](https://www.iconfinder.com/CodMe) - Icons
