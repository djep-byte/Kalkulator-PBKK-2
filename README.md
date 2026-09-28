# Kalkulator-PBKK-2
Membuat kalkulator sederhana

### Langkah Pengerjaan
Jalankan code berikut pada terminal secara berurutan
```
dotnet new winforms -n kalkulator
cd kalkulator
code .
```
Ubah file ```Form1.cs``` dengan code berikut
```
using System;
using System.Globalization;
using System.Windows.Forms;

namespace kalkulator
{
    public partial class Form1 : Form
    {
        private double _firstNumber = 0;
        private double _secondNumber = 0;
        private string _selectedOperator = "";
        private bool _isNewEntry = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnNumber_Click(object? sender, EventArgs e)
        {
            if (sender is not Button button) return;

            if (txtDisplay.Text == "0" || _isNewEntry)
            {
                txtDisplay.Text = button.Text;
                _isNewEntry = false;
            }
            else
            {
                txtDisplay.Text += button.Text;
            }
        }

        private void BtnOperator_Click(object? sender, EventArgs e)
        {
            if (sender is not Button button) return;

            if (double.TryParse(txtDisplay.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out _firstNumber))
            {
                _selectedOperator = button.Text;
                lblHistory.Text = $"{_firstNumber.ToString(CultureInfo.InvariantCulture)} {_selectedOperator}";
                _isNewEntry = true;
            }
        }

        private void BtnEquals_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedOperator)) return;

            if (!double.TryParse(txtDisplay.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out _secondNumber))
                return;

            try
            {
                double result = _selectedOperator switch
                {
                    "+" => _firstNumber + _secondNumber,
                    "−" or "-" => _firstNumber - _secondNumber,
                    "×" or "*" => _firstNumber * _secondNumber,
                    "÷" or "/" => _secondNumber != 0 
                        ? _firstNumber / _secondNumber 
                        : throw new DivideByZeroException("Tidak bisa membagi dengan nol."),
                    _ => _secondNumber
                };

                lblHistory.Text = $"{_firstNumber.ToString(CultureInfo.InvariantCulture)} {_selectedOperator} {_secondNumber.ToString(CultureInfo.InvariantCulture)} =";
                txtDisplay.Text = result.ToString(CultureInfo.InvariantCulture);
                _firstNumber = result;
                _isNewEntry = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ResetCalculator();
            }
        }

        private void BtnDecimal_Click(object? sender, EventArgs e)
        {
            if (_isNewEntry)
            {
                txtDisplay.Text = "0.";
                _isNewEntry = false;
            }
            else if (!txtDisplay.Text.Contains("."))
            {
                txtDisplay.Text += ".";
            }
        }

        private void BtnBackspace_Click(object? sender, EventArgs e)
        {
            if (_isNewEntry) return;

            if (txtDisplay.Text.Length > 1)
            {
                txtDisplay.Text = txtDisplay.Text[..^1];
            }
            else
            {
                txtDisplay.Text = "0";
                _isNewEntry = true;
            }
        }

        private void BtnToggleSign_Click(object? sender, EventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
            {
                value = -value;
                txtDisplay.Text = value.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void BtnPercent_Click(object? sender, EventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
            {
                value /= 100;
                txtDisplay.Text = value.ToString(CultureInfo.InvariantCulture);
                _isNewEntry = true;
            }
        }

        private void BtnClear_Click(object? sender, EventArgs e)
        {
            ResetCalculator();
        }

        private void ResetCalculator()
        {
            _firstNumber = 0;
            _secondNumber = 0;
            _selectedOperator = "";
            txtDisplay.Text = "0";
            lblHistory.Text = "";
            _isNewEntry = true;
        }
    }
}
```
Kemudian ubah juga file ```Form1.Designer.cs``` dengan code ini untuk tampilan UI nya
```
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
```
Kemudian jalankan pada terminal dengan code ini
```dotnet run```
### Dokumentasi Hasil Run
<img width="179" height="280" alt="image" src="https://github.com/user-attachments/assets/1132626d-1028-4374-ad0f-4f60409248b4" />
### Penjumlahan
<img width="179" height="280" alt="image" src="https://github.com/user-attachments/assets/6a6c361e-910b-441e-bad4-ce56b572f0a8" />
### Pengurangan
<img width="180" height="280" alt="image" src="https://github.com/user-attachments/assets/a3c8fe58-939c-478f-93b2-51337db53aeb" />
### Perkalian
<img width="180" height="280" alt="image" src="https://github.com/user-attachments/assets/e704abb2-0636-4bde-9a3c-4e3d933ed3bf" />
### Pembagian
<img width="182" height="281" alt="image" src="https://github.com/user-attachments/assets/812d50f9-d8ff-4195-9964-cf21da388739" />
