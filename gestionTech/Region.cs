namespace projetSlamTest
{
    /// <summary>
    /// Cette classe représente une région, qui peut correspondre à un pays.
    /// </summary>
    public class Region
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe Region.
        /// </summary>
        /// <param name="id">L'identifiant de la région.</param>
        /// <param name="nom">Le nom de la région (peut correspondre à un pays).</param>
        public Region(int id, string nom)
        {
            Id = id;
            Nom = nom; // correspond à un pays
        }

        /// <summary>
        /// Obtient l'identifiant de la région.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Obtient le nom de la région, qui peut correspondre à un pays.
        /// </summary>
        public string Nom { get; }
    }
}