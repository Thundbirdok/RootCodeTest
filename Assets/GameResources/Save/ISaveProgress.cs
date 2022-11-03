namespace GameResources.Save
{
    public interface ISaveProgress
    {
        /// <summary>
        /// Save progress
        /// </summary>
        public void Save();
        
        /// <summary>
        /// Load progress
        /// </summary>
        public void Load();
    }
}
