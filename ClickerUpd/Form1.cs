using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ClickerUpd
{
    public partial class Form1 : Form
    {
        private const int HOTKEY_ID = 1;
        private bool isRunning = false;
        private int Delay = 1;
        private System.Threading.Timer timer;
        private System.Threading.Timer timer1;
        private System.Threading.Timer timer2;
        private System.Threading.Timer timer3;
        private System.Threading.Timer timer4;
        private System.Threading.Timer timer5;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, Keys vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        public Form1()
        {
            InitializeComponent();
            RegisterHotKey(this.Handle, HOTKEY_ID, 0, Keys.F1); // Регистрация горячей клавиши F1
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            const int WM_HOTKEY = 0x0312;
            const int HOTKEY_ID = 1;

            // Проверка сообщения о нажатии горячей клавиши
            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_ID)
            {
                // Включение/выключение цикла
                isRunning = !isRunning;
                if (isRunning)
                {
                    if (checkBox1.Checked == true)
                        MessageBox.Show("Цикл запущен.");
                    

                    // Создание нового таймера
                    Delay = int.Parse(textBox1.Text);
                    timer = new System.Threading.Timer(TimerCallback, null, 0, Delay);
                    if (checkBox2.Checked == true)
                        timer1 = new System.Threading.Timer(TimerCallback, null, 0, Delay);
                    if (checkBox3.Checked == true)
                        timer2 = new System.Threading.Timer(TimerCallback, null, 0, Delay);
                    if (checkBox4.Checked == true)
                        timer3 = new System.Threading.Timer(TimerCallback, null, 0, Delay);
                    if (checkBox5.Checked == true)
                        timer4 = new System.Threading.Timer(TimerCallback, null, 0, Delay);
                    if (checkBox6.Checked == true)
                        timer5 = new System.Threading.Timer(TimerCallback, null, 0, Delay);

                }
                else
                {
                    if (timer != null)
                    {
                        timer.Dispose();
                        if (checkBox2.Checked == true)
                            timer1.Dispose();
                        if (checkBox3.Checked == true)
                            timer2.Dispose();
                        if (checkBox4.Checked == true)
                            timer3.Dispose();
                        if (checkBox5.Checked == true)
                            timer4.Dispose();
                        if (checkBox6.Checked == true)
                            timer5.Dispose();
                        timer.Dispose();
                        if (checkBox2.Checked == true)
                            timer1.Dispose();
                        if (checkBox3.Checked == true)
                            timer2.Dispose();
                        if (checkBox4.Checked == true)
                            timer3.Dispose();
                        if (checkBox5.Checked == true)
                            timer4.Dispose();
                        if (checkBox6.Checked == true)
                            timer5.Dispose();
                    }
                }
            }
        }

        static void TimerCallback(object state)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }



        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        // Константы для событий мыши
        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        private const uint MOUSEEVENTF_LEFTUP = 0x04;

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKey(this.Handle, HOTKEY_ID); // Удаление регистрации горячей клавиши перед закрытием формы
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            HellForm form = new HellForm(this);
            form.Show();
        }
    }
}
