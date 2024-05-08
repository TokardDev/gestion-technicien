using System;

namespace projetSlamTest
{
    /// <summary>
    /// Cette classe représente le personnel de votre système.
    /// </summary>
    public class Personnel
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe Personnel.
        /// </summary>
        /// <param name="matricule">Le matricule du personnel.</param>
        /// <param name="dateEmbauche">La date d'embauche du personnel.</param>
        /// <param name="motDePasse">Le mot de passe du personnel.</param>
        /// <param name="type">Le type du personnel (0 : utilisateur, 1 : technicien, 2 : responsable).</param>
        /// <param name="materielId">L'identifiant du matériel associé au personnel.</param>
        public Personnel(string matricule, DateTime dateEmbauche, string motDePasse, int type, int materielId)
        {
            Matricule = matricule;
            DateEmbauche = dateEmbauche;
            MotDePasse = motDePasse;
            Type = type; // 0 : utilisateur, 1 : technicien, 2 : responsable
            MaterielId = materielId;
        }

        /// <summary>
        /// Obtient ou définit le matricule du personnel.
        /// </summary>
        public string Matricule { get; set; }

        /// <summary>
        /// Obtient ou définit la date d'embauche du personnel.
        /// </summary>
        public DateTime DateEmbauche { get; set; }

        /// <summary>
        /// Obtient ou définit le mot de passe du personnel.
        /// </summary>
        public string MotDePasse { get; set; }

        /// <summary>
        /// Obtient ou définit le type du personnel (0 : utilisateur, 1 : technicien, 2 : responsable).
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Obtient ou définit l'identifiant du matériel associé au personnel.
        /// </summary>
        public int MaterielId { get; set; }
    }
}
