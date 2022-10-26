BEFORE YOU START:
- you need Unity  2021.3 or higher 
- you need HD SRP pipeline 12.1 if you use higher etc custom shaders could not work but seems they should. 
That's why we provide 12.1 version which seems to work with much higher versions aswell. 
For all higher RP versions please use 12.1 HD RP support pack.

!!!! IMPORTANT !!!!  We uploaded pack at 2021.3 and seams it have retarded baked gobal ilumination, we get strange results on few models. 
We turned off baked GI, try to bake it at higher versions like 2021.1 etc when they fix it. Bake will take few hours.

Be patient this tech is so fluid... we coudn't fallow every beta version

Step 1 
	- !!!! IMPORTANT !!!! Open "Project settings" ->"Gaphics"-> "HDRP global settings" ->  "Diffusion Profile Assets" and drag and 
        drop our SSS settings diffusion profiles for foliage into Diffusion profile list:
		  NM_SSSSettings_Skin_Foliage
		  NM_SSSSettings_Skin_NM Foliage
		  NM_SSSSettings_Skin_NM Foliage Trees
	Without this foliage, materials will not become affected by scattering and they will look wrong.
	"Open HDRenderPiplineAsset" and:
	- LOD Bias to = 1.5 or 2

Step 2 Go to project settings and quality and set:
	- Set VSync to don't sync

Step 3 Find "Graveyard_Demo" and open it.

Step 4 - HIT PLAY!:)

About scene construction:
		- There is post process profile: Post Process Volume. Manage post process by scene post process object.
		- There is Sky and Fog Volume object, It's are important like hell because basically it's the core of rendering and light management.
		- There are Density Volume objects which manage volumetric fog density in specific areas
		- Prefab wind manage wind speed and direction at the scene

Play with it give us feedback and learn about hd rp power.

