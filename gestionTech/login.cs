using System;
using System.Windows.Forms;

namespace projetSlamTest
{
    /// <summary>
    /// Cette classe représente la fenêtre de connexion de l'application.
    /// </summary>
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gère le clic sur le bouton de connexion.
        /// </summary>
        private void loginButton_Click(object sender, EventArgs e)
        {
            var login = loginTB.Text;
            var password = pwTB.Text;
            if (Db.Authentification(login, password))
            {
                DialogResult = DialogResult.OK;
                Form1.Utilisateur = Db.GetUser(login);
                Close();
            }
            else
            {
                MessageBox.Show("Login ou mot de passe incorrect");
            }
        }

        /// <summary>
        /// Je sais pas trop ce que c'est mais j'arrive pas à le supprimer
        /// </summary>
        private void label1_Click(object sender, EventArgs e)
        {
            // je sais pas ce que c'est mais j'arrive pas à le supprimer
            // il a l'air de se sentir bien ici donc je vais pas le déranger
        }

        /// <summary>
        /// Gère l'événement de chargement de la fenêtre de connexion.
        /// </summary>
        private void Login_Load(object sender, EventArgs e)
        {
            // pareil, trop choupi la fonction
        }
    }
}