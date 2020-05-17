using JetBrains.Annotations;
using NetScriptFramework.SkyrimSE;

namespace UtilityLibrary
{
    public static partial class UtilityLibrary
    {
        private const uint CreatureKeywordID = 0x13795;
        private static BGSKeyword _creatureKeyword;

        private const uint UndeadKeywordID = 0x13796;
        private static BGSKeyword _undeadKeyword;

        private const uint AnimalKeywordID = 0x13798;
        private static BGSKeyword _animalKeyword;

        /// <summary>
        /// Checks if the given actor is valid by doing null checks and checking
        /// if the base from of the actor is either NPC or Leveled Character.
        /// </summary>
        /// <param name="actor">The Actor</param>
        /// <returns><c>true</c> if valid actor, <c>false</c> if not</returns>
        public static bool IsValidActor(this Actor actor)
        {
            if (actor == null)
                return false;

            if (actor.IsPlayer)
                return true;

            if (actor.BaseForm.FormType != FormTypes.NPC || actor.BaseForm.FormType != FormTypes.LeveledCharacter)
                return false;

            return true;
        }

        /// <summary>
        /// Returns the name of an actor. This goes beyond the normal
        /// <see cref="TESForm.Name"/> and checks the <see cref="Actor.BaseActor"/>
        /// and <see cref="TESObjectREFR.BaseForm"/> as well.
        /// </summary>
        /// <param name="actor">The Actor</param>
        /// <returns>Either the name of an actor or null if the actor has no name</returns>
        [CanBeNull]
        public static string GetName([NotNull] this Actor actor)
        {
            if (string.IsNullOrWhiteSpace(actor.Name))
            {
                return string.IsNullOrWhiteSpace(actor.BaseActor.Name)
                    ? actor.BaseForm.Name
                    : null;
            }

            return actor.Name;
        }

        /// <summary>
        /// Checks if the given actor is Female.
        /// </summary>
        /// <param name="actor">The Actor</param>
        /// <returns><c>true</c> if female, <c>false</c> if not</returns>
        public static bool IsFemale([NotNull] this Actor actor)
        {
            return (actor.BaseActor.ActorData.ActorBaseFlags & TESActorBaseData.DataFlags.Female) !=
                   TESActorBaseData.DataFlags.None;
        }

        /// <summary>
        /// Checks if the given actor is Male.
        /// </summary>
        /// <param name="actor">The Actor</param>
        /// <returns><c>true</c> if male, <c>false</c> if not</returns>
        public static bool IsMale([NotNull] this Actor actor)
        {
            return !actor.IsFemale();
        }

        /// <summary>
        /// Checks if the given actor is a creature by checking if it
        /// has the creature keyword.
        /// </summary>
        /// <param name="actor">The Actor</param>
        /// <returns><c>true</c> if creature, <c>false</c> if not</returns>
        public static bool IsCreature([NotNull] this Actor actor)
        {
            if (_creatureKeyword != null) return actor.Race.HasKeyword(_creatureKeyword);

            var keywordForm = TESForm.LookupFormById(CreatureKeywordID);
            if (keywordForm is BGSKeyword keyword)
            {
                _creatureKeyword = keyword;
            }

            return actor.Race.HasKeyword(_creatureKeyword);
        }

        /// <summary>
        /// Checks if the given actor is an animal by checking if it
        /// has the animal keyword.
        /// </summary>
        /// <param name="actor">The Actor</param>
        /// <returns><c>true</c> if animal, <c>false</c> if not</returns>
        public static bool IsAnimal([NotNull] this Actor actor)
        {
            if (_animalKeyword != null) return actor.Race.HasKeyword(_animalKeyword);

            var keywordForm = TESForm.LookupFormById(AnimalKeywordID);
            if (keywordForm is BGSKeyword keyword)
            {
                _animalKeyword = keyword;
            }

            return actor.Race.HasKeyword(_animalKeyword);
        }

        /// <summary>
        /// Checks if the given actor is an undead by checking if it
        /// has the undead keyword.
        /// </summary>
        /// <param name="actor">The Actor</param>
        /// <returns><c>true</c> if undead, <c>false</c> if not</returns>
        public static bool IsUndead([NotNull] this Actor actor)
        {
            if (_undeadKeyword != null) return actor.Race.HasKeyword(_undeadKeyword);

            var keywordForm = TESForm.LookupFormById(UndeadKeywordID);
            if (keywordForm is BGSKeyword keyword)
            {
                _undeadKeyword = keyword;
            }

            return actor.Race.HasKeyword(_undeadKeyword);
        }
    }
}
