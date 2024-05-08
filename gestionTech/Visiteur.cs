using System;

namespace projetSlamTest
{
    /// <summary>
    /// Cette classe représente un visiteur dans votre système.
    /// </summary>
    public class Visiteur : Personnel
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe Visiteur.
        /// </summary>
        /// <param name="matricule">Le matricule du visiteur.</param>
        /// <param name="dateEmbauche">La date d'embauche du visiteur.</param>
        /// <param name="motDePasse">Le mot de passe du visiteur.</param>
        /// <param name="type">Le type de visiteur.</param>
        /// <param name="materielId">L'identifiant du matériel associé.</param>
        /// <param name="id">L'identifiant du visiteur.</param>
        /// <param name="prime">La prime associée au visiteur.</param>
        /// <param name="budget">Le budget associé au visiteur.</param>
        /// <param name="objectif">L'objectif du visiteur.</param>
        public Visiteur(string matricule, DateTime dateEmbauche, string motDePasse, int type, int materielId, int id,
            string prime, string budget, string objectif) : base(matricule, dateEmbauche, motDePasse, type, materielId)
        {
            Id = id;
            Prime = prime;
            Budget = budget;
            Objectif = objectif;
        }

        /// <summary>
        /// Obtient ou définit l'objectif du visiteur.
        /// </summary>
        public string Objectif { get; }

        /// <summary>
        /// Obtient ou définit le budget du visiteur.
        /// </summary>
        public string Budget { get; }

        /// <summary>
        /// Obtient ou définit la prime du visiteur.
        /// </summary>
        public string Prime { get; }

        /// <summary>
        /// Obtient ou définit l'identifiant du visiteur.
        /// </summary>
        public int Id { get; }
    }
}