namespace EventProcessing.Framework
{
    public abstract class Entity
    {
        /// <summary>
        /// This will be the Id of the doc / aggregate root in the repository
        /// </summary>
        public string Id { get; private set; } 

        /// <summary>
        /// Consider the Id's to be generated externally
        /// </summary>
        /// <param name="id"></param>
        public Entity(string id)
        {
            Id = id;
        }
    }
}
