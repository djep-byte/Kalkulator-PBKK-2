namespace kalkulator
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblHistory;
        private System.Windows.Forms.TextBox txtDisplay;
        private System.Windows.Forms.TableLayoutPanel layoutGrid;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblHistory = new Label();
            txtDisplay = new TextBox();
            layoutGrid = new TableLayoutPanel();

            SuspendLayout();

            Color bgForm = Color.FromArgb(18, 18, 20);
            Color bgDisplay = Color.FromArgb(28, 28, 32);
            Color textMain = Color.FromArgb(240, 240, 245);
            Color btnNumberBg = Color.FromArgb(40, 40, 45);
            Color btnOperatorBg = Color.FromArgb(255, 149, 0);
            Color btnFuncBg = Color.FromArgb(60, 60, 65);

            BackColor = bgForm;
            ClientSize = new Size(360, 520);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Kalkulator Modern";

            lblHistory.Dock = DockStyle.Top;
            lblHistory.Height = 30;
            lblHistory.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            lblHistory.ForeColor = Color.DarkGray;
            lblHistory.TextAlign = ContentAlignment.BottomRight;
            lblHistory.Padding = new Padding(0, 0, 15, 0);

            txtDisplay.Dock = DockStyle.Top;
            txtDisplay.Height = 60;
            txtDisplay.BackColor = bgDisplay;
            txtDisplay.ForeColor = textMain;
            txtDisplay.BorderStyle = BorderStyle.None;
            txtDisplay.Font = new Font("Segoe UI Semibold", 26F, FontStyle.Bold);
            txtDisplay.ReadOnly = true;
            txtDisplay.TextAlign = HorizontalAlignment.Right;
            txtDisplay.Text = "0";

            layoutGrid.Dock = DockStyle.Fill;
            layoutGrid.ColumnCount = 4;
            layoutGrid.RowCount = 5;
            layoutGrid.Padding = new Padding(10);

            for (int i = 0; i < 4; i++)
                layoutGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            for (int i = 0; i < 5; i++)
                layoutGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));

            (string text, int type, Color color)[] buttons = new[]
            {
                ("C", 3, btnFuncBg), ("±", 6, btnFuncBg), ("%", 7, btnFuncBg), ("÷", 1, btnOperatorBg),
                ("7", 0, btnNumberBg), ("8", 0, btnNumberBg), ("9", 0, btnNumberBg), ("×", 1, btnOperatorBg),
                ("4", 0, btnNumberBg), ("5", 0, btnNumberBg), ("6", 0, btnNumberBg), ("−", 1, btnOperatorBg),
                ("1", 0, btnNumberBg), ("2", 0, btnNumberBg), ("3", 0, btnNumberBg), ("+", 1, btnOperatorBg),
                ("⌫", 5, btnFuncBg), ("0", 0, btnNumberBg), (".", 4, btnNumberBg), ("=", 2, btnOperatorBg)
            };

            int index = 0;
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    var item = buttons[index++];
                    Button btn = new Button
                    {
                        Text = item.text,
                        Dock = DockStyle.Fill,
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                        ForeColor = textMain,
                        BackColor = item.color,
                        Margin = new Padding(4)
                    };
                    btn.FlatAppearance.BorderSize = 0;

                    switch (item.type)
                    {
                        case 0: btn.Click += BtnNumber_Click; break;
                        case 1: btn.Click += BtnOperator_Click; break;
                        case 2: btn.Click += BtnEquals_Click; break;
                        case 3: btn.Click += BtnClear_Click; break;
                        case 4: btn.Click += BtnDecimal_Click; break;
                        case 5: btn.Click += BtnBackspace_Click; break;
                        case 6: btn.Click += BtnToggleSign_Click; break;
                        case 7: btn.Click += BtnPercent_Click; break;
                    }

                    layoutGrid.Controls.Add(btn, col, row);
                }
            }

            Controls.Add(layoutGrid);
            Controls.Add(txtDisplay);
            Controls.Add(lblHistory);

            ResumeLayout(false);
            PerformLayout();
        }
    }
}