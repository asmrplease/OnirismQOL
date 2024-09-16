Quality of life patches for the game Onirism. Requires [BepInEx 5](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.23.2). 

# Consistent Camera Zoom #
Saves camera zoom between levels and saves, so that if you like to zoom out, you don't have to redo the zoom manually every time the level changes. 

# High-Framerate Input Drop Fix #
The game's physics runs at 100hz. Playing at framerates greater than 100fps leads to cases when single frame inputs for jumping or other actions are considered released before the physics code can see that they occurred. This patch latches the keypress values high until they're read by the physics update code. 

# Installation: #
- Install [BepInEx 5](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.23.2) in your Onirism directory if you don't already have it. 
- Extract OnirismQOL.rar using WinRAR or similar software. Paste the resulting OnirismQOL.dll into the Onirism/BepInEx/plugins directory.
- You can confirm the plugin is running by looking for the following lines in your Onirism/BepInEx/LogOutput.log file:
>[Info   :   BepInEx] Loading [OnirismQOL 1.0.0]
>
>[Info   :OnirismQOL] Plugin OnirismQOL is loaded!
