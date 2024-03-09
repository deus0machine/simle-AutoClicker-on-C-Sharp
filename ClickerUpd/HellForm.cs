using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ClickerUpd
{
    public partial class HellForm : Form
    {
        Form1 startForm;
        private const int HOTKEY_ID = 1;
        private bool isRunning = false;
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, Keys vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        public HellForm(Form1 form)
        {
            InitializeComponent();
            RegisterHotKey(this.Handle, HOTKEY_ID, 0, Keys.F2); // Регистрация горячей клавиши F1
            startForm = form;
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            const int WM_HOTKEY = 0x0312;
            const int HOTKEY_ID = 1;

            // Проверка сообщения о нажатии горячей клавиши
            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_ID)
            {
                isRunning = !isRunning;
                if (isRunning)
                {
                    Thread loopThread = new Thread(Loop);
                    loopThread.Start();
                }
            }
        }
        private void Loop()
        {
            int i = 1;
            while (isRunning)
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                if (i % 10 == 0)
                {
                    Thread.Sleep(1);
                    i = 1;
                }
                i++;
            }
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        // Константы для событий мыши
        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        private const uint MOUSEEVENTF_LEFTUP = 0x04;
        private void HellForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKey(this.Handle, HOTKEY_ID);
            startForm.Show();
        }
    }
}
