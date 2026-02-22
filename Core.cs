using MelonLoader;

[assembly: MelonInfo(typeof(TACO.Core), "TACO", "1.0.0", "Nyu", null)]
[assembly: MelonGame("Floris Kaayk", "NextSpaceRebels")]

namespace TACO
{
    /// <summary>
    /// Core entry point for the MelonLoader mod instance.
    /// Manages the mod's lifecycle and global initialization processes.
    /// </summary>
    public class Core : MelonMod
    {
        /// <summary>
        /// Executed after the mod assembly is loaded and the core Unity environment is initialized.
        /// Serves as the primary setup phase before scene loading occurs.
        /// </summary>
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized.");
        }
    }
}