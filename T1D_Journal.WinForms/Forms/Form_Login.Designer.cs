namespace T1D_Journal.WinForms.Forms
{
	partial class Form_Login
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			labelLogin = new Label();
			labelPassword = new Label();
			textBoxLogin = new TextBox();
			textBoxPassword = new TextBox();
			buttonLogin = new Button();
			buttonExit = new Button();
			SuspendLayout();
			// 
			// labelLogin
			// 
			labelLogin.AutoSize = true;
			labelLogin.Location = new Point(121, 69);
			labelLogin.Name = "labelLogin";
			labelLogin.Size = new Size(41, 15);
			labelLogin.TabIndex = 0;
			labelLogin.Text = "Логин";
			// 
			// labelPassword
			// 
			labelPassword.AutoSize = true;
			labelPassword.Location = new Point(113, 129);
			labelPassword.Name = "labelPassword";
			labelPassword.Size = new Size(49, 15);
			labelPassword.TabIndex = 1;
			labelPassword.Text = "Пароль";
			// 
			// textBoxLogin
			// 
			textBoxLogin.BorderStyle = BorderStyle.FixedSingle;
			textBoxLogin.Location = new Point(298, 61);
			textBoxLogin.Name = "textBoxLogin";
			textBoxLogin.Size = new Size(100, 23);
			textBoxLogin.TabIndex = 2;
			textBoxLogin.TextAlign = HorizontalAlignment.Center;
			// 
			// textBoxPassword
			// 
			textBoxPassword.BorderStyle = BorderStyle.FixedSingle;
			textBoxPassword.Location = new Point(298, 127);
			textBoxPassword.Name = "textBoxPassword";
			textBoxPassword.PasswordChar = '*';
			textBoxPassword.Size = new Size(100, 23);
			textBoxPassword.TabIndex = 3;
			textBoxPassword.TextAlign = HorizontalAlignment.Center;
			// 
			// buttonLogin
			// 
			buttonLogin.Location = new Point(121, 235);
			buttonLogin.Name = "buttonLogin";
			buttonLogin.Size = new Size(75, 23);
			buttonLogin.TabIndex = 4;
			buttonLogin.Text = "Войти";
			buttonLogin.UseVisualStyleBackColor = true;
			buttonLogin.Click += buttonLogin_Click;
			// 
			// buttonExit
			// 
			buttonExit.Location = new Point(323, 235);
			buttonExit.Name = "buttonExit";
			buttonExit.Size = new Size(75, 23);
			buttonExit.TabIndex = 5;
			buttonExit.Text = "Выход";
			buttonExit.UseVisualStyleBackColor = true;
			buttonExit.Click += buttonExit_Click;
			// 
			// Form_Login
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(buttonExit);
			Controls.Add(buttonLogin);
			Controls.Add(textBoxPassword);
			Controls.Add(textBoxLogin);
			Controls.Add(labelPassword);
			Controls.Add(labelLogin);
			Name = "Form_Login";
			Text = "Form1";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label labelLogin;
		private Label labelPassword;
		private TextBox textBoxLogin;
		private TextBox textBoxPassword;
		private Button buttonLogin;
		private Button buttonExit;
	}
}