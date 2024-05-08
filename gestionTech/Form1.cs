using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace projetSlamTest
{
    /// <summary>
    /// chargement du formulaire principal
    /// </summary>
    public partial class Form1 : Form
    {
        public static Personnel Utilisateur;

        private List<Ticket> _userTickets;
        private List<Ticket> _allTickets;

        private Ticket _selectedTicket;
        private Materiel _selectedMateriel;
        private Technicien _selectedTechnicien;
        private Personnel _selectedUser;

        /// <summary>
        /// rafrachit les tickets de l'utilisateur et les affiche dans le datagridview correspondant
        /// </summary>
        private void RefreshUserTickets()
        {
            var bindingSource = new BindingSource();
            // Affiche les tickets ouverts par l'utilisateur connecté 
            _userTickets = new List<Ticket>();
            _userTickets = Db.GetTicketsByUser(Utilisateur);
            bindingSource.DataSource = _userTickets;
            dataGridView2.DataSource = bindingSource;
        }

        /// <summary>
        /// raffraichit tous les tickets et les affiche dans le datagridview correspondant pour les techniciens et les responsables
        /// </summary>
        private void RefreshAllTickets()
        {
            var bindingSource = new BindingSource();
            _allTickets = new List<Ticket>();
            _allTickets = Db.GetAllTickets();
            bindingSource.DataSource = _allTickets;
            dataGridView3.DataSource = bindingSource;
        }

        /// <summary>
        /// affiche tout le matériel dans le datagridview pour les techniciens et les responsables
        /// </summary>
        private void RefreshMateriels()
        {
            var bindingSourceMateriel = new BindingSource();
            var materiels = Db.GetAllMateriel();
            bindingSourceMateriel.DataSource = materiels;
            dataGridMateriel.DataSource = bindingSourceMateriel;
        }

        /// <summary>
        /// affiche tous les techniciens dans la gridview visible par les responsables
        /// </summary>
        private void RefreshTechniciens()
        {
            var bindingSourceTechniciens = new BindingSource();
            var techniciens = Db.getAllTechniciens();
            bindingSourceTechniciens.DataSource = techniciens;
            dataGridTechniciens.DataSource = bindingSourceTechniciens;
        }

        private void RefreshComboBoxMatricule()
        {
            comboBox2.Items.Clear();
            var matricules = Db.GetAllNonTechnicienMatricules();
            foreach (string matricule in matricules)
            {
                comboBox2.Items.Add(matricule);
            }
        }

        private void RefreshUtilisateurs()
        {
            var bindingSourceUtilisateurs = new BindingSource();
            var utilisateurs = Db.GetAllUsers();
            bindingSourceUtilisateurs.DataSource = utilisateurs;
            dataGridUtilisateurs.DataSource = bindingSourceUtilisateurs;
        }

        private void RefreshComboBoxMateriels()
        {
            comboBox3.Items.Clear();
            var materielIds = Db.GetAllNonUsedMaterielId();
            foreach (int id in materielIds)
            {
                comboBox3.Items.Add(id);
            }
        }

        private void RefreshStats()
        {
            // Stats incidents
            float totalIncidents = Db.NbIncidents();
            float solvedIncidents = Db.ResolvedIncidents();
            float percentageSolvedIncidents = solvedIncidents / totalIncidents * 100;
            label23.Text = totalIncidents.ToString();
            label24.Text = solvedIncidents.ToString();
            label26.Text = (percentageSolvedIncidents).ToString() + " %";

            // Stats techniciens
            var selectedTechnicien = Db.GetTechnicienByMatricule(comboBox5.Text);
            if(selectedTechnicien != null)
            {
                float incidentsInCharge = Db.GetAllTicketInChargeByTechnicien(selectedTechnicien).Count;
                float solvedIncidentsInCharge = Db.GetAllSolvedTicketsByTechnicien(selectedTechnicien);
                float percentageSolvedIncidentsByTechnicien = (solvedIncidentsInCharge / totalIncidents) * 100;
                label29.Text = incidentsInCharge.ToString();
                label30.Text = solvedIncidentsInCharge.ToString();
                label36.Text = percentageSolvedIncidentsByTechnicien.ToString() + " %";

            }
            else
            {
                comboBox5.Items.Clear();
                var techniciens = Db.getAllTechniciens();
                foreach (Technicien technicien in techniciens)
                {
                    comboBox5.Items.Add(technicien.Matricule);
                }
            }

            // Stats utilisateurs

        }

        /// <summary>
        /// initialise le formulaire principal
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ouvre la fenetre login.cs et attend la fermeture de celle-ci, si le login est bon on active cette fenetre, sinon on ferme le programme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // ouvre la fenetre login.cs et attend la fermeture de celle-ci
            // si le login est bon on active cette fenetre
            // sinon on ferme le programme
            var login = new Login();
            login.ShowDialog();
            comboBox1.SelectedIndex = 0;
            if (login.DialogResult == DialogResult.OK)
            {

                this.Show();

                numericUpDown1.Value = Utilisateur.MaterielId;
                dataGridMateriel.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridTechniciens.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridUtilisateurs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                if (Utilisateur.Type == 0)
                {
                    numericUpDown1.Value = Utilisateur.MaterielId;

                    button3.Visible = false;
                    button4.Visible = false;
                    button5.Visible = false;

                    // supprimer les pages auquels l'utilisateur connecté n'a pas la permission d'accéder
                    tabControl1.TabPages.Remove(tabPage1);
                    tabControl1.TabPages.Remove(tabPage4);
                    tabControl1.TabPages.Remove(tabPage5);
                    tabControl1.TabPages.Remove(tabPage6);

                    RefreshUserTickets();
                }

                if(Utilisateur.Type == 1)
                {
                    RefreshUserTickets();

                    // Affiche tous les tickets
                    RefreshAllTickets();
                    button6.Enabled = true;
                    
                    // affiche tout le matériel dans le datagridview dataGridMateriel
                    RefreshMateriels();

                    // supprimer les pages auquels l'utilisateur connecté n'a pas la permission d'accéder
                    tabControl1.TabPages.Remove(tabPage4);
                    tabControl1.TabPages.Remove(tabPage5);
                    tabControl1.TabPages.Remove(tabPage6);

                }

                if(Utilisateur.Type == 2)
                {
                    RefreshUserTickets();

                    // Affiche tous les tickets
                    RefreshAllTickets();
                    button6.Enabled = true;

                    // affiche tout le matériel dans le datagridview dataGridMateriel
                    RefreshMateriels();

                    RefreshTechniciens();

                    RefreshComboBoxMatricule();

                    RefreshUtilisateurs();

                    RefreshComboBoxMateriels();

                    RefreshStats();
                }
            }
            else
            {
                Application.Exit();
            }
            
        }

        /// <summary>
        /// quand on clique sur le bouton "déclarer un incident" on ajoute un ticket dans la base de données avec les informations rentrées dans les champs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            var currentDateTime = DateTime.Now;
            var ticket = new Ticket(textBox1.Text, Convert.ToInt16(comboBox1.Text), currentDateTime, "En cours", Utilisateur.MaterielId, Utilisateur.Matricule);
            Db.DeclareIncident(ticket);
            //clear les champs
            textBox1.Text = "";
            comboBox1.SelectedIndex = 0;
            RefreshUserTickets();
            if(Utilisateur.Type >= 1)
            {
                RefreshAllTickets();
            }
        }

        private void dataGridView3_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count <= 0) return; // si y'a rien on skip
            
            var selectedRow = dataGridView3.SelectedRows[0]; // Prend la première ligne sélectionnée
            var cellValue = (int)selectedRow.Cells["id"].Value;
            _selectedTicket = Db.GetTicketById(cellValue);
            if (_selectedTicket == null) return;
            button2.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Db.CloseTicket(_selectedTicket.Id);
            RefreshAllTickets();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            var logiciels = new List<string>();
            if (checkBox1.Checked)
            {
                logiciels.Add("Word");
            }
            if (checkBox2.Checked)
            {
                logiciels.Add("Excel");
            }
            if (checkBox3.Checked)
            {
                logiciels.Add("Ratio");
            }
            if (checkBox4.Checked)
            {
                logiciels.Add("Photoshop");
            }
            var materiel = new Materiel(textBox2.Text, textBox3.Text, textBox4.Text, logiciels, dateTimePicker1.Value, textBox5.Text, textBox6.Text);
            Db.AddMateriel(materiel);
            RefreshMateriels();
            RefreshComboBoxMateriels();
            // clear les champs
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            dateTimePicker1.Value = DateTime.Now;
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Db.DeleteMateriel(_selectedMateriel);
            RefreshMateriels();
        }

        private void dataGridMateriel_Click(object sender, EventArgs e)
        {
            if (dataGridMateriel.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridMateriel.SelectedRows[0]; // Prend la première ligne sélectionnée
                                                                             // Vous pouvez accéder aux cellules de la ligne sélectionnée comme ceci :
                int cellValue = (int)selectedRow.Cells["id"].Value;
                _selectedMateriel = Db.GetMaterielById(cellValue);
                if (_selectedMateriel != null)
                {
                    button3.Enabled = true;
                }
            }
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count <= 0) return; // si y'a rien on skip

            var selectedRow = dataGridView2.SelectedRows[0]; // Prend la première ligne sélectionnée
            var cellValue = (int)selectedRow.Cells["id"].Value;
            _selectedTicket = Db.GetTicketById(cellValue);
            if (_selectedTicket == null) return;
            button2.Enabled = true;
        }

        private void dataGridTechniciens_Click(object sender, EventArgs e)
        {
            if (dataGridTechniciens.SelectedRows.Count <= 0) return; // si y'a rien on skip

            var selectedRow = dataGridTechniciens.SelectedRows[0]; // Prend la première ligne sélectionnée
            var cellValue = (int)selectedRow.Cells["id"].Value;
            _selectedTechnicien = Db.GetTechnicienById(cellValue);
            if (_selectedTechnicien == null) return;
            button7.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Db.DeleteTechnicien(_selectedTechnicien);
            RefreshTechniciens();
            RefreshComboBoxMatricule();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var technicien = new Technicien(textBox9.Text, textBox8.Text, textBox7.Text, comboBox2.Text);
            Db.AddTechnicien(technicien);
            RefreshTechniciens();
            RefreshComboBoxMatricule();
        }

        private void dataGridTechniciens_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            DataGridViewCell cell = dataGridTechniciens.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.Value.ToString() != cell.EditedFormattedValue.ToString())
            {
                var technicien = _selectedTechnicien;

                technicien.Niveau = dataGridTechniciens.CurrentRow.Cells["niveau"].Value.ToString();
                technicien.Competences = dataGridTechniciens.CurrentRow.Cells["competences"].Value.ToString();
                technicien.Formation = dataGridTechniciens.CurrentRow.Cells["formation"].Value.ToString();

                Db.EditTechnicien(technicien);

                RefreshTechniciens();

                RefreshComboBoxMatricule();
            }
        }

        private void dataGridUtilisateurs_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = dataGridUtilisateurs.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if(cell.Value.ToString() != cell.EditedFormattedValue.ToString())
            {
                var utilisateur = _selectedUser;

                utilisateur.MaterielId = (int)dataGridUtilisateurs.CurrentRow.Cells["materielId"].Value;
                utilisateur.DateEmbauche = (DateTime)dataGridUtilisateurs.CurrentRow.Cells["dateEmbauche"].Value;
                utilisateur.MotDePasse = dataGridUtilisateurs.CurrentRow.Cells["motDePasse"].Value.ToString();
                utilisateur.Type = (int)dataGridUtilisateurs.CurrentRow.Cells["type"].Value;

                Db.EditUser(utilisateur);

                RefreshUtilisateurs();
            }
        }

        private void dataGridUtilisateurs_Click(object sender, EventArgs e)
        {
            if (dataGridUtilisateurs.SelectedRows.Count <= 0) return; // si y'a rien on skip


            var selectedRow = dataGridUtilisateurs.SelectedRows[0]; // Prend la première ligne sélectionnée
            var cellValue = (string)selectedRow.Cells["matricule"].Value;
            _selectedUser = Db.GetUser(cellValue);
            if (_selectedUser == null) return;
            button10.Enabled = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if(_selectedUser.Matricule == Utilisateur.Matricule) 
            { 
                MessageBox.Show("Vous ne pouvez pas supprimer votre propre compte !"); 
                return; 
            } 
            else
            {
                Db.RemoveUser(_selectedUser);
                RefreshUtilisateurs();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var personnel = new Personnel(textBox10.Text, dateTimePicker2.Value, textBox11.Text, Convert.ToInt16(comboBox4.Text), Convert.ToInt16(comboBox3.Text));

            Db.AddUser(personnel);

            RefreshUtilisateurs();
            RefreshComboBoxMateriels();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshStats();
        }
    }
}