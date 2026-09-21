# SolidWorks to URDF Exporter

A personally maintained fork of the [SolidWorks to URDF Exporter](https://github.com/ros/solidworks_urdf_exporter),
originally authored and maintained by [Stephen Brawner](brawner@gmail.com). Past supporters of the
upstream project include [PickNik Consulting](https://picknik.ai), Verb Surgical, Open Robotics, and
Willow Garage.

Upstream has not seen a release since 2021. This fork carries fixes needed to build and run the
add-in on current tooling, and is shared as-is with no support commitment.

## Changes in this fork

- **Link inertial properties are computed from components rather than bodies.** Bodies carry only
  geometry, so the upstream calculation always reported `volume * material density`, silently
  discarding any mass a user had set through `Tools > Mass Properties > Override Mass Properties`.
  The same bug corrupted the center of mass and moment of inertia of any link built from a
  subassembly. Verified against SOLIDWORKS 2024 to within 1e-12 on mass, center of mass, and all
  nine inertia terms.
- **Retargeted to .NET Framework 4.8.** Visual Studio no longer ships the 4.5.2 targeting pack, so
  the project could not be built as-is.

## Requirements

- **SOLIDWORKS 2020 or newer, 64-bit.** The inertial fix above relies on `IMassProperty2`, which
  first appears in the SOLIDWORKS 2020 API. On an older version the exporter falls back to the
  upstream body-based calculation and logs a warning, so 2018 SP5 and later will still run but will
  not benefit from the fix. Tested on SOLIDWORKS 2024.
- **Visual Studio 2022** with the `.NET desktop development` workload.
- **.NET Framework 4.8 targeting pack**, included in that workload.

## Usage

See the [ROS Wiki](http://wiki.ros.org/sw_urdf_exporter) and associated
[tutorials](http://wiki.ros.org/sw_urdf_exporter/Tutorials).

## Installation

This fork does not publish a prebuilt installer. Build from source and let the build register the
add-in with SOLIDWORKS.

1. Clone the repository no more than three directories below the drive root. The project locates the
   SOLIDWORKS interop assemblies through a relative path with a fixed number of `..` segments, so
   `C:\src\solidworks_urdf_exporter` works while `C:\Users\you\Documents\code\projects\solidworks_urdf_exporter`
   silently resolves to the wrong directory and the build fails to find them.
1. Launch Visual Studio as administrator. Right click it and select `Run as Administrator`.
   Registration writes to `HKLM`, so a non-elevated build fails at the post-build step with
   `error MSB3392: ... Access is denied`.
1. Open `SW2URDF.sln`. Restore the NuGet packages if Visual Studio does not do it automatically.
1. Select the `Debug` configuration and the `x64` platform, then build the solution. Both `SW2URDF`
   and `TestRunner` should build.
1. Start SOLIDWORKS. The exporter appears under `Tools > Export as URDF`.

Note that the build registers the DLL in place, at its path inside the repository, rather than
copying it elsewhere. Moving or deleting the clone breaks the add-in. To unregister it, open the
`x64 Native Tools Command Prompt for VS 2022` as administrator and run the following from the
repository root:

```
RegAsm /unregister SW2URDF\bin\x64\Debug\SW2URDF.dll
```

Use the x64 prompt specifically. The plain `Developer Command Prompt` puts the 32-bit RegAsm on the
PATH, which will not unregister this 64-bit add-in.

Installing the SOLIDWORKS API SDK separately is not required. The interop assemblies the project
references ship with SOLIDWORKS itself and are read straight out of the installation directory.

## Development

To debug the add-in with SOLIDWORKS attached, right click `SW2URDF` in the Solution Explorer, open
the `Debug` tab, and confirm that `Start external program:` points at the SOLIDWORKS executable, for
example `C:\Program Files\SOLIDWORKS Corp\SOLIDWORKS\SLDWORKS.exe`. Starting a debug session then
launches SOLIDWORKS with the add-in loaded.

For running the test suite, see [TestRunner/README.md](TestRunner/README.md). The tests drive a real
SOLIDWORKS instance against the models in [examples/](examples/).

## Converting mesh format from 3dxml to dae

Executing the following command will convert the format of the exported mesh from 3DXML to DAE, and rewrite the URDF, allowing you to display colored meshes in visualization tools like RViz:

```bash
pip3 install scikit-robot -U
convert-urdf-mesh <URDF_PATH> --output <OUTPUT_URDF_PATH>
```

### Trouble Shooting

1. `error MSB3392`, unable to register the assembly, access denied - Visual Studio is not running
   elevated. Close it and reopen it with `Run as Administrator`.
1. The SOLIDWORKS interop references cannot be found - the clone is nested too deeply. See step 1 of
   the installation instructions.
1. `Resourse.resx` error - Check if `SW2URDF/Properties/Resources.resx` exists and is empty. If empty, delete this file then right click the `SW2URDF` in the Solution Explorer and select `Properties`. Navigate to the Resources tab and click the button to create a new file.

## License

MIT, as upstream. See [LICENSE](LICENSE).
