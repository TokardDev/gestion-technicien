using System;
using System.Collections.Generic;


namespace projetSlamTest
{
    /// <summary>
    /// Cette classe représente le matériel informatique utilisé dans votre système.
    /// </summary>
    public class Materiel
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe Materiel.
        /// </summary>
        /// <param name="id">L'identifiant du matériel.</param>
        /// <param name="processeur">Le processeur du matériel.</param>
        /// <param name="memoire">La mémoire du matériel.</param>
        /// <param name="disque">Le disque dur du matériel.</param>
        /// <param name="logiciels">La liste des logiciels installés sur le matériel.</param>
        /// <param name="dateAchat">La date d'achat du matériel.</param>
        /// <param name="garantie">La garantie du matériel.</param>
        /// <param name="fournisseur">Le fournisseur du matériel.</param>
        public Materiel(int id, string processeur, string memoire, string disque, List<string> logiciels, DateTime dateAchat, string garantie, string fournisseur)
        {
            Id = id;
            Processeur = processeur;
            Memoire = memoire;
            Disque = disque;
            Logiciels = logiciels;
            DateAchat = dateAchat;
            Garantie = garantie;
            Fournisseur = fournisseur;
        }

        // Ajout de matériel sans ID
        public Materiel( string processeur, string memoire, string disque, List<string> logiciels, DateTime dateAchat, string garantie, string fournisseur)
        {
            Processeur = processeur;
            Memoire = memoire;
            Disque = disque;
            Logiciels = logiciels;
            DateAchat = dateAchat;
            Garantie = garantie;
            Fournisseur = fournisseur;
        }

        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit le fournisseur du matériel.
        /// </summary>
        public string Fournisseur { get; set; }

        /// <summary>
        /// Obtient ou définit la garantie du matériel.
        /// </summary>
        public string Garantie { get; set; }

        /// <summary>
        /// Obtient ou définit la date d'achat du matériel.
        /// </summary>
        public DateTime DateAchat { get; set; }

        /// <summary>
        /// Obtient la liste des logiciels installés sur le matériel.
        /// </summary>
        public List<string> Logiciels { get; }

        /// <summary>
        /// Obtient ou définit le disque dur du matériel.
        /// </summary>
        public string Disque { get; set; }

        /// <summary>
        /// Obtient ou définit la mémoire du matériel.
        /// </summary>
        public string Memoire { get; set; }

        /// <summary>
        /// Obtient ou définit le processeur du matériel.
        /// </summary>
        public string Processeur { get; set; }
    }
}
