# OnirismQOL
Quality of life patches for the game Onirism.

# Consistent Camera Zoom 
Saves camera zoom between levels and saves, so that if you like to zoom out, you don't have to redo the zoom manually every time the level changes. 

# High-Framerate Input Drop Fix
The game's physics runs at 100hz. Playing at framerates greater than 100fps leads to cases when single frame inputs for jumping or other actions are considered released before the physics code can see that they occurred. This patch latches the keypress values high until they're read by the physics update code. 
