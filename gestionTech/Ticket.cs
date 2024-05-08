using System;

namespace projetSlamTest
{
    /// <summary>
    /// Cette classe représente un ticket dans votre système.
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe Ticket avec un ID.
        /// </summary>
        /// <param name="id">L'identifiant du ticket.</param>
        /// <param name="objet">L'objet du ticket.</param>
        /// <param name="niveauUrgence">Le niveau d'urgence du ticket.</param>
        /// <param name="dateCreation">La date de création du ticket.</param>
        /// <param name="etat">L'état du ticket.</param>
        /// <param name="idTechnicien">L'identifiant du technicien affecté au ticket.</param>
        /// <param name="idMateriel">L'identifiant du matériel associé au ticket.</param>
        /// <param name="personnelMatricule">Le matricule du personnel associé au ticket.</param>
        public Ticket(int id, string objet, int niveauUrgence, DateTime dateCreation, string etat, int idTechnicien, int idMateriel, string personnelMatricule)
        {
            Id = id;
            Objet = objet;
            NiveauUrgence = niveauUrgence;
            DateCreation = dateCreation;
            Etat = etat;
            IdTechnicien = idTechnicien;
            IdMateriel = idMateriel;
            Matricule = personnelMatricule;
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe Ticket sans ID.
        /// </summary>
        /// <param name="objet">L'objet du ticket.</param>
        /// <param name="niveauUrgence">Le niveau d'urgence du ticket.</param>
        /// <param name="dateCreation">La date de création du ticket.</param>
        /// <param name="etat">L'état du ticket.</param>
        /// <param name="idTechnicien">L'identifiant du technicien affecté au ticket.</param>
        /// <param name="idMateriel">L'identifiant du matériel associé au ticket.</param>
        /// <param name="personnelMatricule">Le matricule du personnel associé au ticket.</param>
        public Ticket(string objet, int niveauUrgence, DateTime dateCreation, string etat, int idTechnicien, int idMateriel, string personnelMatricule)
        {
            Objet = objet;
            NiveauUrgence = niveauUrgence;
            DateCreation = dateCreation;
            Etat = etat;
            IdTechnicien = idTechnicien;
            IdMateriel = idMateriel;
            Matricule = personnelMatricule;
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe Ticket sans ID Technicien (quand le ticket vient d'être ouvert et que personne ne travaille encore dessus).
        /// </summary>
        /// <param name="objet">L'objet du ticket.</param>
        /// <param name="niveauUrgence">Le niveau d'urgence du ticket.</param>
        /// <param name="dateCreation">La date de création du ticket.</param>
        /// <param name="etat">L'état du ticket.</param>
        /// <param name="idMateriel">L'identifiant du matériel associé au ticket.</param>
        /// <param name="personnelMatricule">Le matricule du personnel associé au ticket.</param>
        public Ticket(string objet, int niveauUrgence, DateTime dateCreation, string etat, int idMateriel, string personnelMatricule)
        {
            Objet = objet;
            NiveauUrgence = niveauUrgence;
            DateCreation = dateCreation;
            Etat = etat;
            IdMateriel = idMateriel;
            Matricule = personnelMatricule;
        }

        /// <summary>
        /// Obtient l'identifiant du matériel associé au ticket.
        /// </summary>
        public int IdMateriel { get; }

        /// <summary>
        /// Obtient le matricule du personnel associé au ticket.
        /// </summary>
        public string Matricule { get; }

        /// <summary>
        /// Obtient ou définit l'identifiant du technicien affecté au ticket.
        /// </summary>
        public int IdTechnicien { get; }

        /// <summary>
        /// Obtient l'état du ticket.
        /// </summary>
        public string Etat { get; }

        /// <summary>
        /// Obtient la date de création du ticket.
        /// </summary>
        public DateTime DateCreation { get; }

        /// <summary>
        /// Obtient le niveau d'urgence du ticket.
        /// </summary>
        public int NiveauUrgence { get; }

        /// <summary>
        /// Obtient l'objet du ticket.
        /// </summary>
        public string Objet { get; }

        /// <summary>
        /// Obtient l'identifiant du ticket.
        /// </summary>
        public int Id { get; }
    }
}
