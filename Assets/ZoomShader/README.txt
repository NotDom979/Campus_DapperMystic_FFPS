Thanks for download asset. You can write in alexsampublic@outlook.com for questions.
## WHAT'S IN THE ASSET ?
The composition of the asset includes a material that allows you to increase the objects behind it. 
This function can be used to implement sniper scope, magnifying glass effect.
## How to check the work of the material?
Run the DemoScene. Rotate the mouse to control the direction of view. Use the mouse wheel to zoom in and out.
## What is included in an asset?
- The asset includes a demo scene illustrating the possibilities of the material(DemoScene).
- The Camera Rotation script is located on the camera and is responsible for the rotations. 
  Run the scene and try to rotate the computer mouse.
- The Zoom script allows you to change the scale of the image in the sight, by scrolling with the mouse wheel.
- ZoomMaterial a material that allows you to enlarge images.
- ThermalZoom a material that allows you to enlarge images with thermal effect.
## How to use the material?
- Add an Image or SpriteRender and adjust the size and position of the object.
- Add your texture if you want to display it.
- Add a ZoomMaterial
- The Factor property is responsible for the size of the image's magnification.
If Factor = 1 - this is the image without magnification. All images behind the material object (relative to the camera) will be enlarged. 
- The Tint option allows you to change the color of the enlarged image.
- The UVCenterOffset parameter allows you to adjust the direction in which the center of the image will shift when zoomed in. 
It is most convenient to adjust these cardinates in the process of changing the Factor parameter.
## How to set up scripts from the demo scene?
- You can change the speed of camera rotation by changing the Speed parameter of the CameraRotation script. 
You can also change the rotated object by dragging it into the RotationRoot.
- You can change the zoom speed with ZoomSpeed from the Zoom script(The script is on the Aim object).
## How to turn it on thermal imaging effect
- Replace ZoomMaterial with ThermalZoomMaterial in the Zoom component and set ThermalZoomMaterial on the scope sprite
- or just turn on aimTemperature and turn off aim in the Demo scene


