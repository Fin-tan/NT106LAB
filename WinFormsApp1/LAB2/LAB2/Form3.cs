using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace lab2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt; *.docx; *.csv; *.doc)|*.txt;*.docx;*.csv; *.doc|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    string extension = Path.GetExtension(filePath).ToLower();
                    string[] NotAllow = { ".zip", ".jpg", ".png", ".pdf", ".rar", ".sln" };
                    if (NotAllow.Contains(extension))
                    {
                        MessageBox.Show("Không phải là file văn bản!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        string content = File.ReadAllText(filePath);
                        richTextBox1.Text = content;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể đọc file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Không thể mở File Explorer ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (string.IsNullOrEmpty(richTextBox1.Text))
            {
                MessageBox.Show("Không có nội dung để ghi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string output = saveFileDialog.FileName;
                string[] lines = richTextBox1.Lines;

                try
                {
                    using (StreamWriter sw = new StreamWriter(output))
                    {
                        foreach (string line in lines)
                        {
                            if (string.IsNullOrWhiteSpace(line))
                                continue;

                            string result = Calculate(line);
                            sw.WriteLine(result);
                        }
                    }

                    MessageBox.Show($"Đã xử lý và ghi kết quả vào file {output}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể xử lý và ghi kết quả: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string Calculate(string expression)
        {
            try
            {
                List<string> tokens = Tokenize(expression);
                if (tokens.Count == 0)
                    return $"{expression} => Lỗi: Biểu thức trống!";

                foreach (var token in tokens)
                {
                    if (!double.TryParse(token, out _) && !"+-*/".Contains(token))
                        return $"{expression} => Lỗi: Token không hợp lệ '{token}'!";
                }

                int operandCount = tokens.Count(t => double.TryParse(t, out _));
                int operatorCount = tokens.Count(t => "+-*/".Contains(t));

                if (operandCount != operatorCount + 1)
                    return $"{expression} => Lỗi: Số lượng toán tử và toán hạng không hợp lệ!";

                List<string> postfix = ShuntingYard(tokens);
                double result = EvaluatePostfix(postfix);

                return $"{expression} = {result}";
            }
            catch (DivideByZeroException)
            {
                return $"{expression} => Lỗi: Phép chia cho 0!";
            }
            catch (Exception ex)
            {
                return $"{expression} => Lỗi: {ex.Message}";
            }
        }

        private List<string> Tokenize(string expression)
        {
            List<string> tokens = new List<string>();
            int i = 0;
            int len = expression.Length;

            while (i < len)
            {
                if (char.IsWhiteSpace(expression[i]))
                {
                    i++;
                    continue;
                }

                // Xử lý số (bao gồm cả . và ,)
                if (char.IsDigit(expression[i]) || expression[i] == '.' || expression[i] == ',')
                {
                    StringBuilder num = new StringBuilder();
                    int decimalPointCount = 0;

                    while (i < len && (char.IsDigit(expression[i]) || expression[i] == '.' || expression[i] == ','))
                    {
                        char currentChar = expression[i];

                        // Xử lý dấu thập phân
                        if (currentChar == '.' || currentChar == ',')
                        {
                            if (decimalPointCount >= 1)
                                throw new ArgumentException($"Số không hợp lệ: nhiều dấu thập phân!");

                            currentChar = '.'; // Chuẩn hóa về dấu chấm
                            decimalPointCount++;
                        }

                        num.Append(currentChar);
                        i++;
                    }

                    tokens.Add(num.ToString());
                }
                // Xử lý toán tử và số âm
                else if ("+-*/".Contains(expression[i]))
                {
                    if (expression[i] == '-' && (i == 0 || "+-*/(".Contains(expression[i - 1].ToString())))
                    {
                        // Xử lý số âm
                        StringBuilder num = new StringBuilder();
                        num.Append('-');
                        i++;

                        int decimalPointCount = 0;
                        while (i < len && (char.IsDigit(expression[i]) || expression[i] == '.' || expression[i] == ','))
                        {
                            char currentChar = expression[i];

                            if (currentChar == '.' || currentChar == ',')
                            {
                                if (decimalPointCount >= 1)
                                    throw new ArgumentException($"Số không hợp lệ: nhiều dấu thập phân!");

                                currentChar = '.'; // Chuẩn hóa về dấu chấm
                                decimalPointCount++;
                            }

                            num.Append(currentChar);
                            i++;
                        }

                        tokens.Add(num.ToString());
                    }
                    else
                    {
                        tokens.Add(expression[i].ToString());
                        i++;
                    }
                }
                else
                {
                    throw new ArgumentException($"Ký tự không hợp lệ: '{expression[i]}'");
                }
            }

            return tokens;
        }

        private List<string> ShuntingYard(List<string> tokens)
        {
            List<string> output = new List<string>();
            Stack<string> stack = new Stack<string>();
            Dictionary<string, int> precedence = new Dictionary<string, int>
            {
                { "+", 1 },
                { "-", 1 },
                { "*", 2 },
                { "/", 2 }
            };

            foreach (var token in tokens)
            {
                if (double.TryParse(token, out _))
                {
                    output.Add(token);
                }
                else if (precedence.ContainsKey(token))
                {
                    while (stack.Count > 0 && precedence.ContainsKey(stack.Peek()) && precedence[stack.Peek()] >= precedence[token])
                    {
                        output.Add(stack.Pop());
                    }
                    stack.Push(token);
                }
            }

            while (stack.Count > 0)
                output.Add(stack.Pop());

            return output;
        }

        private double EvaluatePostfix(List<string> postfix)
        {
            Stack<double> stack = new Stack<double>();

            foreach (var token in postfix)
            {
                // Parse số với CultureInfo.InvariantCulture để hiểu dấu chấm
                if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out double num))
                {
                    stack.Push(num);
                }
                else
                {
                    double b = stack.Pop();
                    double a = stack.Pop();
                    switch (token)
                    {
                        case "+":
                            stack.Push(a + b);
                            break;
                        case "-":
                            stack.Push(a - b);
                            break;
                        case "*":
                            stack.Push(a * b);
                            break;
                        case "/":
                            if (b == 0) throw new DivideByZeroException();
                            stack.Push(a / b);
                            break;
                        default:
                            throw new InvalidOperationException($"Toán tử không hợp lệ: {token}");
                    }
                }
            }

            return stack.Pop();
        }
    }
}