using System;
using System.Collections.Generic;
using System.Linq;

namespace TACO
{
    /// <summary>
    /// Core optimization engine for Next Space Rebels.
    /// Provides combinatorial analysis of video tags to determine the highest yielding subscriber outcome.
    /// </summary>
    public static class ComboCalculator
    {
        /// <summary>
        /// Analyzes all available tags and generates permutations to find the best performing combination.
        /// </summary>
        /// <param name="availableTags">The list of TagInstances currently proposed by the game's Style manager.</param>
        /// <param name="maxSlots">Maximum number of tags the player can select based on current rank.</param>
        /// <param name="playerRank">Current player rank used for sub calculation scaling.</param>
        /// <returns>A list of Tags forming the optimal combination.</returns>
        public static List<Tag> FindBestCombo(List<TagInstance> availableTags, int maxSlots, int playerRank)
        {
            if (availableTags == null || availableTags.Count == 0) return new List<Tag>();

            // Convert instances to raw Tag objects for easier manipulation
            List<Tag> pool = availableTags.Select(ti => ti.tag).ToList();
            int slotsToFill = Math.Min(maxSlots, pool.Count);

            List<Tag> bestCombination = new List<Tag>();
            int highestScore = -1;

            // We iterate from 1 to the maximum allowed slots
            for (int i = 1; i <= slotsToFill; i++)
            {
                IEnumerable<List<Tag>> combinations = GetCombinations(pool, i);

                foreach (List<Tag> combo in combinations)
                {
                    int currentScore = EvaluateCombo(combo, playerRank);
                    if (currentScore > highestScore)
                    {
                        highestScore = currentScore;
                        bestCombination = combo;
                    }
                }
            }

            return bestCombination;
        }

        /// <summary>
        /// Simulates the subscriber gain by invoking the game's native CalculateSubs method.
        /// </summary>
        /// <param name="combo">The specific combination of tags to evaluate.</param>
        /// <param name="rank">The player rank to pass to the game's logic.</param>
        /// <returns>The total number of subscribers this combo would generate.</returns>
        private static int EvaluateCombo(List<Tag> combo, int rank)
        {
            // Transform Tag list into TagUse list required by GameStateLaunch.CalculateSubs
            List<TagUse> mockUses = combo.Select(t =>
            {
                TagUse tu = new TagUse(t.category);
                // Important: we must fetch the historical usage to account for "Disinterest" penalty
                tu.uploadsAgo = t.GetSave().GetNUploadsAgo();
                return tu;
            }).ToList();

            float dummyNegativity = 0f;

            // Direct call to the game's internal static math
            // Note: CalculateSubs returns an int and populates the 'subs' field in each TagUse
            return GameStateLaunch.CalculateSubs(ref mockUses, ref dummyNegativity, rank);
        }

        /// <summary>
        /// Generates all unique combinations of a specific size from a given list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The source list of items.</param>
        /// <param name="length">The size of the combinations to generate.</param>
        /// <returns>An enumeration of unique combinations.</returns>
        private static IEnumerable<List<T>> GetCombinations<T>(List<T> list, int length)
        {
            if (length == 1) return list.Select(t => new List<T> { t });

            return GetCombinations(list, length - 1)
                .SelectMany(t => list.Where(o => list.IndexOf(o) > list.IndexOf(t.Last())),
                    (t1, t2) => t1.Concat(new T[] { t2 }).ToList());
        }
    }
}