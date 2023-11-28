# LethalClunk
Mod to replace the Large Axle drop sound with the metal bar meme sound

# Manual Installation
Copy the entire `BepInEx` folder from the downloaded zip file into your Lethal Company installation folder:

[your_steam_install_path]\steamapps\common\Lethal Company\BepInEx

# Running From Source
This mod depends on the Lethal Company assembly to compile. These assemblies are not checked in to this repo and must be placed manually on local machines:

 1. Create a folder called `deps` at the root of the project (this will be git ignored)
 2. Copy [your_steam_install_path]\steamapps\common\Lethal Company\Lethal Company_Data\Managed\\`Assembly-CSharp.dll` into the `deps` folder
 3. Rename `Assembly-CSharp.dll` in the `deps` folder to `LC.dll`

Then, to compile the project, use:

`dotnet build`
