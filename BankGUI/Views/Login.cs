﻿using Bank.Exceptions;
using Bank.Service;
using BankGUI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankGUI
{
    public partial class Login : Form
    {
        private ViewService viewService;
        private IAccountService accountService;

        public Login(ViewService viewService, IAccountService accountService)
        {
            this.viewService = viewService;
            this.accountService = accountService;
            InitializeComponent();
        }

        private void setLoading(bool loading)
        {
            usernameBox.Enabled = !loading;
            passwordBox.Enabled = !loading;
            submitButton.Enabled = !loading;
            Application.UseWaitCursor = loading;
            Application.DoEvents();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            setLoading(true);

            Session session;
            try
            {
                session = accountService.Login(usernameBox.Text, passwordBox.Text);
            }
            catch (InvalidCredentialsException ex)
            {
                MessageBox.Show(ex.Message);
                setLoading(false);
                return;
            }

            setLoading(false);
            viewService.ShowMainMenuView(session);
            Hide();
        }
    }
}