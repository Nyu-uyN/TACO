using System;
using System.Collections.Generic;
using HarmonyLib;
namespace TACO
{
    // ============================================================
    // HARMONY PATCHES
    // ============================================================

    /// <summary>
    /// Hooks into the Video Maker UI initialization to automate optimal tag selection.
    /// Bypasses manual clicking by feeding the best combinatorial result directly into the native UI selection method.
    /// </summary>
    [HarmonyPatch(typeof(UIVideoMaker), nameof(UIVideoMaker.Init))]
    public class PatchUIVideoMakerInit
    {
        /// <summary>
        /// Postfix execution triggered after the UI is fully populated.
        /// Evaluates the best tag combination and programmatically invokes the native SelectTag method
        /// to ensure full synchronization of data state, UI visuals, and audio feedback.
        /// </summary>
        /// <param name="__instance">The active UIVideoMaker singleton instance.</param>
        public static void Postfix(UIVideoMaker __instance)
        {
            if (GameStateLaunch.style == null || GameStateLaunch.style.tagsCurrent == null)
            {
                return;
            }

            // 1. Ensure a clean state by deselecting any currently active tags (e.g., from lingering cache)
            foreach (TagInstance currentTag in GameStateLaunch.style.tagsCurrent)
            {
                if (currentTag.selected)
                {
                    __instance.SelectTag(currentTag);
                }
            }

            // 2. Retrieve constraints
            int playerRank = Player.rank;
            int maxSlots = Ranks.GetTagSlots(playerRank);

            // 3. Execute the algorithmic evaluation
            List<Tag> optimalCombo = ComboCalculator.FindBestCombo(GameStateLaunch.style.tagsCurrent, maxSlots, playerRank);

            // 4. Inject the selection via native UI methods
            if (optimalCombo != null && optimalCombo.Count > 0)
            {
                foreach (Tag optimalTag in optimalCombo)
                {
                    // Map the raw Tag back to its TagInstance equivalent in the current launch context
                    TagInstance targetInstance = GameStateLaunch.style.tagsCurrent.Find(ti => ti.tag.category == optimalTag.category);

                    if (targetInstance != null && !targetInstance.selected)
                    {
                        // Programmatically triggers the UI update, sound, and backend state tracking
                        __instance.SelectTag(targetInstance);
                    }
                }
            }
        }
    }
}
