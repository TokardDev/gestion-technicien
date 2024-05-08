using System;

namespace projetSlamTest
{
    /// <summary>
    /// Cette classe représente une phase de travail d'un technicien.
    /// </summary>
    public class PhaseTechnicien
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe PhaseTechnicien.
        /// </summary>
        /// <param name="id">L'identifiant de la phase de travail.</param>
        /// <param name="dateDebut">La date de début de la phase de travail.</param>
        /// <param name="dateFin">La date de fin de la phase de travail.</param>
        /// <param name="travailRealise">La description du travail réalisé lors de la phase de travail.</param>
        public PhaseTechnicien(string id, DateTime dateDebut, DateTime dateFin, string travailRealise)
        {
            Id = id;
            DateDebut = dateDebut;
            DateFin = dateFin;
            TravailRealise = travailRealise;
        }

        /// <summary>
        /// Obtient ou définit l'identifiant de la phase de travail.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Obtient ou définit la date de début de la phase de travail.
        /// </summary>
        public DateTime DateDebut { get; }

        /// <summary>
        /// Obtient ou définit la date de fin de la phase de travail.
        /// </summary>
        public DateTime DateFin { get; }

        /// <summary>
        /// Obtient ou définit la description du travail réalisé lors de la phase de travail.
        /// </summary>
        public string TravailRealise { get; }
    }
}