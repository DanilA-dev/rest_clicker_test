namespace _Project.Scripts.Breeds
{
    public class Breed
    {
        #region Properties

        public string Id { get; }
        public string Name { get; }
        public string Description { get; }

        #endregion

        #region Constructor

        public Breed(string id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        #endregion
    }
}
