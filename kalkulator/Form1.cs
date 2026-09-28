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