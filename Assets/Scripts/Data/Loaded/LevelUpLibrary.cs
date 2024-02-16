using Data.Static;

namespace Data.Loaded
{
    public class LevelUpLibrary
    {
        private LevelUpData _levelsUpData = new();

        public void SetData(LevelUpData levelUpData) =>
            _levelsUpData = levelUpData;

        public int GetExperienceByLevel(int level) => 
            _levelsUpData.LevelExperience[level];
    }
}