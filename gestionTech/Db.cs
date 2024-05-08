using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;

namespace projetSlamTest
{
    /// <summary>
    /// Cette classe représente les actions sur la base de données.
    /// </summary>
    public abstract class Db
    {
        // classe de la base de donnée

        private const string ConnectionString = "Server=127.0.0.1;Database=projet_cs;Uid=root;Password=;SslMode=none";
        // pour culture générale :
        // const, ne change pas de valeur (defini à la compilation)
        // readonly, ne change pas de valeur (defini à l'execution)
        private static readonly MySqlConnection Connection = new MySqlConnection(ConnectionString);

        /// <summary>
        /// récupère tout les tickets ouverts par un utilisateur précis
        /// </summary>
        /// <param name="personnel">le personnel dont on veut avoir les tickets</param>
        /// <returns>une liste de tickets par un utilisateur précis</returns>
        public static List<Ticket> GetTicketsByUser(Personnel personnel)
        {
            var ticketList = new List<Ticket>();
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM tickets WHERE personnelMatricule = @personnelMatricule AND etatDemande = 'En cours'";
            cmd.Parameters.AddWithValue("@personnelMatricule", personnel.Matricule);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var ticket = new Ticket((int)reader["id"], (string)reader["objet"], 
                    (int)reader["niveauUrgence"], (DateTime)reader["dateCreation"], 
                    (string)reader["etatDemande"], (int)reader["technicienId"], 
                    (int)reader["materielId"], (string)reader["personnelMatricule"]);
                ticketList.Add(ticket);
            }
            Connection.Close();
            return ticketList;
        }

        internal static List<Technicien> getAllTechniciens()
        {
            var technicienList = new List<Technicien>();
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM techniciens";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var technicien = new Technicien((int)reader["id"], (string)reader["niveau"], (string)reader["formation"], (string)reader["competences"], (string)reader["matricule"]);
                technicienList.Add(technicien);
            }
            Connection.Close();
            return technicienList;
        }

        public static Technicien GetTechnicienById(int id)
        {
            Technicien technicien = null;
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM techniciens WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                technicien = new Technicien((int)reader["id"], (string)reader["niveau"], (string)reader["formation"], (string)reader["competences"], (string)reader["matricule"]);
            }
            Connection.Close();
            return technicien;
        }

        public static Technicien GetTechnicienByMatricule(string matricule)
        {
            Technicien technicien = null;
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM techniciens WHERE matricule = @matricule";
            cmd.Parameters.AddWithValue("@matricule", matricule);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                technicien = new Technicien((int)reader["id"], (string)reader["niveau"], (string)reader["formation"], (string)reader["competences"], (string)reader["matricule"]);
            }
            Connection.Close();
            return technicien;
        }

        public static List<string> GetAllNonTechnicienMatricules()
        {
            var matriculeList = new List<string>();
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT personnels.matricule FROM personnels LEFT JOIN techniciens ON personnels.matricule = techniciens.matricule WHERE techniciens.matricule IS NULL";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                matriculeList.Add((string)reader["matricule"]);
            }
            Connection.Close();
            return matriculeList;
        }

        /// <summary>
        /// réupère un ticket en fonction de son id
        /// </summary>
        /// /// <param name="id">donne l'id du ticket ciblé</param>
        /// /// <returns>un objet ticket</returns>
        public static Ticket GetTicketById(int id)
        {
            Ticket ticket = null;
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM tickets WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                ticket = new Ticket((int)reader["id"], (string)reader["objet"],
                    (int)reader["niveauUrgence"], (DateTime)reader["dateCreation"],
                    (string)reader["etatDemande"], (int)reader["technicienId"],
                    (int)reader["materielId"], (string)reader["personnelMatricule"]);
            }
            Connection.Close();
            return ticket;
        }

        /// <summary>
        /// change l'état d'un ticket en fonction de son ID
        /// </summary>
        /// /// <param name="id">donne l'id du ticket ciblé</param>
        public static void CloseTicket(int id)
        {
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "UPDATE tickets SET etatDemande = 'Résolu' WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            Connection.Close();
        }

        /// <summary>
        /// retourne tout les tickets pour les techniciens et les responsables
        /// </summary>
        /// <returns>une liste de tout les tickets</returns>
        public static List<Ticket> GetAllTickets()
        {
            var ticketList = new List<Ticket>();
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM tickets WHERE etatDemande = 'En cours'";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var ticket = new Ticket((int)reader["id"], (string)reader["objet"], 
                    (int)reader["niveauUrgence"], (DateTime)reader["dateCreation"], 
                    (string)reader["etatDemande"], (int)reader["technicienId"], 
                    (int)reader["materielId"], (string)reader["personnelMatricule"]);
                ticketList.Add(ticket);
            }
            Connection.Close();
            return ticketList;
        }

        public static List<Ticket> GetAllTicketInChargeByTechnicien(Technicien technicien)
        {
            var ticketList = new List<Ticket>();
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM tickets WHERE etatDemande = 'En cours' AND technicienId = @technicienId";
            cmd.Parameters.AddWithValue("@technicienId", technicien.Id);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var ticket = new Ticket((int)reader["id"], (string)reader["objet"],
                    (int)reader["niveauUrgence"], (DateTime)reader["dateCreation"],
                    (string)reader["etatDemande"], (int)reader["technicienId"],
                    (int)reader["materielId"], (string)reader["personnelMatricule"]);
                ticketList.Add(ticket);
            }
            Connection.Close();
            return ticketList;
        }

        public static int GetAllSolvedTicketsByTechnicien(Technicien technicien)
        {
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM tickets WHERE etatDemande = 'Résolu' AND technicienId = @technicienId";
            cmd.Parameters.AddWithValue("@technicienId", technicien.Id);
            var reader = cmd.ExecuteReader();
            reader.Read();
            int resultat = reader.GetInt16(0);
            Connection.Close();
            return resultat;
        }

        /// <summary>
        /// ajoute un technicien dans la base de donnée
        /// </summary>
        /// <param name="technicien">retourne un technicien sous la forme d'objet</param>
        public static void AddTechnicien(Technicien technicien)
        {
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "INSERT INTO techniciens (niveau, formation, competences, matricule) VALUES (@niveau, @formation, @competences, @matricule)";
            cmd.Parameters.AddWithValue("@niveau", technicien.Niveau);
            cmd.Parameters.AddWithValue("@formation", technicien.Formation);
            cmd.Parameters.AddWithValue("@competences", technicien.Competences);
            cmd.Parameters.AddWithValue("@matricule", technicien.Matricule);
            cmd.ExecuteNonQuery();
            Connection.Close();
        }

        /// <summary>
        /// permet de modifier un technicien dans la base de donnée
        /// </summary>
        /// <param name="technicien">donne un technicien sous forme d'un objet</param>
        public static void EditTechnicien(Technicien technicien)
        {
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "UPDATE techniciens SET niveau = @niveau, formation = @formation, competences = @competences, matricule = @matricule WHERE id = @id";
            cmd.Parameters.AddWithValue("@niveau", technicien.Niveau);
            cmd.Parameters.AddWithValue("@formation", technicien.Formation);
            cmd.Parameters.AddWithValue("@competences", technicien.Competences);
            cmd.Parameters.AddWithValue("@matricule", technicien.Matricule);
            cmd.Parameters.AddWithValue("@id", technicien.Id);
            cmd.ExecuteNonQuery();
            Connection.Close();
        }
        /// <summary>
        /// permet de supprimer un technicien à partir d'un objet technicien
        /// </summary>
        /// <param name="technicien">technicien sous la forme d'objet</param>
        public static void DeleteTechnicien(Technicien technicien)
        {
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "DELETE FROM techniciens WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", technicien.Id);
            cmd.ExecuteNonQuery();
            Connection.Close();
        }
        
        /// <summary>
        /// permet de définir qu'un technicien s'occupe d'un ticket
        /// </summary>
        /// <param name="ticket">le ticket (objet) que l'ont doit prendre en charge</param>
        /// <param name="technicien">le technicien qui prend en charge le ticket</param>
        public static void PriseEnChargeIncident(Ticket ticket, Technicien technicien)
        {
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "UPDATE tickets SET technicienId = @technicienId WHERE id = @ticketId";
            cmd.Parameters.AddWithValue("@ticketId", ticket.Id);
            cmd.Parameters.AddWithValue("@technicienId", technicien.Id);
            cmd.ExecuteNonQuery();
            Connection.Close();
        }

        /// <summary>
        /// permet de modifier un personnel
        /// </summary>
        /// <param name="personnel">le personnel modifié sous forme d'objet</param>
        public static void EditUser(Personnel personnel)
        {
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "UPDATE personnels SET dateEmbauche = @dateEmbauche, motDePasse = @motDePasse, type = @type, materielId = @materielId WHERE matricule = @matricule";
            cmd.Parameters.AddWithValue("@matricule", personnel.Matricule);
            cmd.Parameters.AddWithValue("@dateEmbauche", personnel.DateEmbauche);
            cmd.Parameters.AddWithValue("@motDePasse", personnel.MotDePasse);
            cmd.Parameters.AddWithValue("@type", personnel.Type);
            cmd.Parameters.AddWithValue("@materielId", personnel.MaterielId);
            cmd.ExecuteNonQuery();
            Connection.Close();
        }

        /// <summary>
        /// permet de récupérer le nombre d'incidents total déclarés
        /// </summary>
        /// <returns>le nombre d'incidents déclarés</returns>
        public static int NbIncidents()
        {
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM tickets";
            var reader = cmd.ExecuteReader();
            reader.Read();
            int resultat = reader.GetInt16(0);
            Connection.Close();
            return resultat;
        }

        /// <summary>
        /// permet de retourner le nombre d'incidents résolus
        /// </summary>
        /// <returns>le nombre d'incidents résolus</returns>
        public static int ResolvedIncidents()
        {
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM tickets WHERE etatDemande = 'Résolu'";
            var reader = cmd.ExecuteReader();
            reader.Read();
            int resultat = reader.GetInt16(0);
            Connection.Close();
            return resultat;
        }

        /// <summary>
        /// retourne le nombre d'incidents résolus par un technicien
        /// </summary>
        /// <param name="technicien">le technicien (objet) dont on veut regarder le nombre d'incidents résolus</param>
        /// <returns>le nombre d'incidents résolus par un techniciens</returns>
        public static int SolvedIncidentsByTechnician(Technicien technicien)
        {
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM tickets WHERE etatDemande = 'résolu' AND technicienId = @technicienId";
            cmd.Parameters.AddWithValue("@technicienId", technicien.Id);
            Connection.Close();
            return (int)cmd.ExecuteScalar();
        }
        
        /// <summary>
        /// permet de vérifier le login dans la base de donnée
        /// </summary>
        /// <param name="id">matricule de l'utilisateur</param>
        /// <param name="mdp">le mot de passe de</param>
        /// <returns>true ou false</returns>
        public static bool Authentification(string id, string mdp)
        {
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM personnels WHERE matricule = @matricule AND motDePasse = @motDePasse";
            cmd.Parameters.AddWithValue("@matricule", id);
            cmd.Parameters.AddWithValue("@motDePasse", mdp);
            var reader = cmd.ExecuteReader();
            var result = reader.HasRows;
            Connection.Close();
            return result;
        }
        
        /// <summary>
        /// permet d'ajouter un matériel à la base de donnée
        /// </summary>
        /// <param name="materiel">le materiel (objet) à ajouter à la BDD</param>
        public static void AddMateriel(Materiel materiel)
        {
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "INSERT INTO materiels (processeur, memoire, disque, logicielsInstalles, dateAchat, garantie, " +
                              "fournisseur) VALUES (@processeur, @memoire, @disque, @logiciels, @dateAchat, @garantie, " +
                              "@fournisseur)";
            cmd.Parameters.AddWithValue("@processeur", materiel.Processeur);
            cmd.Parameters.AddWithValue("@memoire", materiel.Memoire);
            cmd.Parameters.AddWithValue("@disque", materiel.Disque);
            var logicielsString = string.Join(",", materiel.Logiciels);
            cmd.Parameters.AddWithValue("@logiciels", logicielsString);
            cmd.Parameters.AddWithValue("@dateAchat", materiel.DateAchat);
            cmd.Parameters.AddWithValue("@garantie", materiel.Garantie);
            cmd.Parameters.AddWithValue("@fournisseur", materiel.Fournisseur);
            cmd.ExecuteNonQuery();
            Connection.Close();
        }
        
        /// <summary>
        /// retourne les informations d'un materiel à partir de son id
        /// </summary>
        /// <param name="identifiant">identifiant du matériel</param>
        /// <returns>retourne le materiel à consulter (objet)</returns>
        public static Materiel ConsultMateriel(int identifiant) {
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM materiels WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", identifiant);
            var reader = cmd.ExecuteReader();
            
            // on récupère la chaine de char, on la split et on enlève les endroits où c'est vide
            var logicielListe = reader.GetString(4).Split(',').Where(s => !string.IsNullOrEmpty(s)).ToList();

            
            reader.Read();
            var materiel = new Materiel(reader.GetInt32(0), reader.GetString(1), 
                reader.GetString(2), reader.GetString(3), logicielListe, reader.GetDateTime(5), 
                reader.GetString(6), reader.GetString(7));
            Connection.Close();
            return materiel;
        }
        
        /// <summary>
        /// permet de supprimer un materiel
        /// </summary>
        /// <param name="materiel">le materiel (objet) à supprimer</param>
        public static void DeleteMateriel(Materiel materiel)
        {
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "DELETE FROM materiels WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", materiel.Id);
            cmd.ExecuteNonQuery();
            Connection.Close();
        }

        public static List<int> GetAllNonUsedMaterielId()
        {
            var idsList = new List<int>();
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT materiels.id FROM materiels LEFT JOIN personnels ON personnels.materielId = materiels.id WHERE personnels.materielId IS NULL";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                idsList.Add((int)reader["id"]);
            }
            Connection.Close();
            return idsList;
        }
        
        /// <summary>
        /// permet de déclarer un incident dans la BDD
        /// </summary>
        /// <param name="ticket">le ticket (objet) à déclarer dans la bdd</param>
        public static void DeclareIncident(Ticket ticket)
        {
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "INSERT INTO tickets (objet, niveauUrgence, dateCreation, etatDemande, " +
                              "technicienId, materielId, personnelMatricule) VALUES (@objet, @niveauUrgence, @dateCreation," +
                              " @etatDemande, '-1', @materielId, @personnelId)";
            cmd.Parameters.AddWithValue("@objet", ticket.Objet);
            cmd.Parameters.AddWithValue("@niveauUrgence", ticket.NiveauUrgence);
            cmd.Parameters.AddWithValue("@dateCreation", ticket.DateCreation);
            cmd.Parameters.AddWithValue("@etatDemande", ticket.Etat);
            cmd.Parameters.AddWithValue("@materielId", ticket.IdMateriel);
            cmd.Parameters.AddWithValue("@personnelId", ticket.Matricule);
            cmd.ExecuteNonQuery();
            Connection.Close();
        }
        
        /// <summary>
        /// retourne un incident à partir de son identifiant
        /// </summary>
        /// <param name="identifiant">identifiant de l'incident à consulter</param>
        /// <returns>l'incident (objet) à consulter</returns>
        public static Ticket ConsultIncident(int identifiant)
        {
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM tickets WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", identifiant);
            var reader = cmd.ExecuteReader();
            reader.Read();
            var ticket = new Ticket(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), 
                reader.GetDateTime(3), reader.GetString(4), reader.GetInt32(5), 
                reader.GetInt32(6), reader.GetString(7));
            Connection.Close();
            return ticket;
        }
        
        /// <summary>
        /// résoud l'incident dans la BDD
        /// </summary>
        /// <param name="ticket">le ticket (objet) à résoudre</param>
        /// <param name="technicien">le technicien (objet) qui résoud le ticket</param>
        public static void ResoudreIncident(Ticket ticket, Technicien technicien)
        {
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "UPDATE tickets SET etatDemande = @etatDemande WHERE id = @id";
            cmd.Parameters.AddWithValue("@etatDemande", ticket.Etat);
            cmd.Parameters.AddWithValue("@id", ticket.Id);
            cmd.ExecuteNonQuery();
            
            cmd.CommandText = "INSERT INTO phaseTechniciens (dateDebut, dateFin, travailRealise) " +
                              "VALUES (@dateDebut, @dateFin, @travailRealise)";
            cmd.Parameters.AddWithValue("@dateDebut", ticket.DateCreation);
            cmd.Parameters.AddWithValue("@dateFin", DateTime.Now);
            cmd.Parameters.AddWithValue("@travailRealise", ticket.Etat);
            cmd.ExecuteNonQuery();
            
            cmd.CommandText = "INSERT INTO intervients (technicienId, ticketId) VALUES (@technicienId, @ticketId)";
            cmd.Parameters.AddWithValue("@technicienId", technicien.Id);
            cmd.Parameters.AddWithValue("@ticketId", ticket.Id);
            cmd.ExecuteNonQuery();
            Connection.Close();
        }
        
        /// <summary>
        /// ajoute un utilisateur dans la bdd
        /// </summary>
        /// <param name="personnel">le personnel à ajouter (objet)</param>
        public static void AddUser(Personnel personnel)
        {
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "INSERT INTO personnels (matricule, dateEmbauche, motDePasse, type, materielId) " +
                              "VALUES (@matricule, @dateEmbauche, @motDePasse, @type, @materielId)";
            cmd.Parameters.AddWithValue("@matricule", personnel.Matricule);
            cmd.Parameters.AddWithValue("@dateEmbauche", personnel.DateEmbauche);
            cmd.Parameters.AddWithValue("@motDePasse", personnel.MotDePasse);
            cmd.Parameters.AddWithValue("@type", personnel.Type);
            cmd.Parameters.AddWithValue("@materielId", personnel.MaterielId);
            cmd.ExecuteNonQuery();
            Connection.Close();
        }
        public static void RemoveUser(Personnel personnel)
        {
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "DELETE FROM personnels WHERE matricule = @matricule";
            cmd.Parameters.AddWithValue("@matricule", personnel.Matricule);
            cmd.ExecuteNonQuery();
            Connection.Close();
        }
        
        /// <summary>
        /// retourne le nombre d'incidents déclarés par un utilisateur
        /// </summary>
        /// <param name="personnel">le personnel à consulter (objet)</param>
        /// <returns>le nombre d'incidents déclarés par cet utilisateur</returns>
        public static int GetStatsUtilisateur(Personnel personnel)
        {
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM tickets WHERE materielId = @materielId";
            cmd.Parameters.AddWithValue("@materielId", personnel.MaterielId);
            var reader = cmd.ExecuteReader();
            reader.Read();
            var nbIncidents = reader.GetInt32(0);
            Connection.Close();
            return nbIncidents;
        }
        
        /// <summary>
        /// permet d'obtenir un utilisateur à partir de son matricule
        /// </summary>
        /// <param name="matricule">le matricule du personnel</param>
        /// <returns>un personnel (objet)</returns>
        public static Personnel GetUser(string matricule)
        {
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM personnels WHERE matricule = @matricule";
            cmd.Parameters.AddWithValue("@matricule", matricule);
            var reader = cmd.ExecuteReader();
            reader.Read();
            var personnel = new Personnel(reader.GetString(0), reader.GetDateTime(1), reader.GetString(2), 
                reader.GetInt32(3), reader.GetInt32(4));
            Connection.Close();
            return personnel;
        }

        public static List<Personnel> GetAllUsers()
        {
            var personnels = new List<Personnel>();
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM personnels";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var personnel = new Personnel(reader.GetString(0), reader.GetDateTime(1), reader.GetString(2),
                reader.GetInt32(3), reader.GetInt32(4));
                personnels.Add(personnel);
            }
            
            Connection.Close();
            return personnels;
        }

        /// <summary>
        /// permet d'avoir tout les materiels existants pour les techniciens et les responsables
        /// </summary>
        /// <returns>retourne une liste de tout les materiels existants</returns>
        public static List<Materiel> GetAllMateriel()
        {
            var materielList = new List<Materiel>();
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM materiels";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                // on récupère la chaine de char, on la split et on enlève les endroits où c'est vide
                var logicielListe = reader.GetString(4).Split(',').Where(s => !string.IsNullOrEmpty(s)).ToList();
                
                var materiel = new Materiel((int)reader["id"], (string)reader["processeur"],
                    (string)reader["memoire"], (string)reader["disque"], logicielListe, 
                    (DateTime)reader["dateAchat"], (string)reader["garantie"], 
                    (string)reader["fournisseur"]);
                materielList.Add(materiel);
            }
            Connection.Close();
            return materielList;
        }

        /// <summary>
        /// récupère un materiel en fonction de son id
        /// </summary>
        /// <param name="id">l'id du materiel</param>
        /// <returns>un materiel (objet)</returns>
        public static Materiel GetMaterielById(int id)
        {
            Materiel materiel = null;
            Connection.Open();
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM materiels WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var logicielListe = reader.GetString(4).Split(',').Where(s => !string.IsNullOrEmpty(s)).ToList();
                materiel = new Materiel((int)reader["id"], (string)reader["processeur"], (string)reader["memoire"], 
                    (string)reader["disque"], logicielListe, Convert.ToDateTime(reader["dateAchat"]), 
                    (string)reader["garantie"], (string)reader["fournisseur"]);
            }
            Connection.Close();
            return materiel;
        }
    }
}