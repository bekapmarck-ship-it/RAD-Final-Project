using System;
using System.Drawing;
using System.Windows.Forms;

namespace HotelBooking
{
    // shared look for every screen: beige background + dark green header with logo and hotel name
    // inherit from this and put controls in ContentPanel
    public class BaseForm : Form
    {
        protected readonly Panel HeaderPanel = new Panel();
        protected readonly Panel ContentPanel = new Panel();
        protected readonly PictureBox LogoBox = new PictureBox();
        protected readonly Label HotelNameLabel = new Label();

        private string _hotelName = "The Hotel";

        // can come from Settings / db later
        public string HotelName
        {
            get => _hotelName;
            set { _hotelName = value; HotelNameLabel.Text = value.ToUpper(); }
        }

        public BaseForm()
        {
            // form
            Text = "The Hotel Reservations";
            BackColor = Theme.Background;
            ForeColor = Theme.Primary;
            Font = Theme.Body();
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1100, 720);
            MinimumSize = new Size(900, 600);

            LoadIcon();

            // header
            HeaderPanel.Dock = DockStyle.Top;
            HeaderPanel.Height = 84;
            HeaderPanel.BackColor = Theme.Primary;
            HeaderPanel.Padding = new Padding(30, 10, 30, 10);

            LogoBox.Dock = DockStyle.Left;
            LogoBox.Width = 90;
            LogoBox.SizeMode = PictureBoxSizeMode.Zoom;
            LogoBox.BackColor = Color.Transparent;
            LoadLogo();

            HotelNameLabel.Dock = DockStyle.Fill;
            HotelNameLabel.Text = _hotelName.ToUpper();
            HotelNameLabel.Font = Theme.Heading(22f);
            HotelNameLabel.ForeColor = Theme.TextLight;
            HotelNameLabel.TextAlign = ContentAlignment.MiddleLeft;
            HotelNameLabel.Padding = new Padding(15, 0, 0, 0);

            // thin gold line under the header
            var line = new Panel { Dock = DockStyle.Top, Height = 2, BackColor = Theme.Accent };

            HeaderPanel.Controls.Add(HotelNameLabel);
            HeaderPanel.Controls.Add(LogoBox);

            // content area for child forms
            ContentPanel.Dock = DockStyle.Fill;
            ContentPanel.BackColor = Theme.Background;
            ContentPanel.Padding = new Padding(40, 24, 40, 24);

            // add order matters for docking (fill first, then top stuff)
            Controls.Add(ContentPanel);
            Controls.Add(line);
            Controls.Add(HeaderPanel);
        }

        private void LoadLogo()
        {
            try
            {
                string p = Theme.AssetPath("logo_icon.png");
                if (p != null) LogoBox.Image = Image.FromFile(p);
            }
            catch { } // app still runs without the logo
        }

        private void LoadIcon()
        {
            try
            {
                string p = Theme.AssetPath("app_icon.ico");
                if (p != null) Icon = new Icon(p);
            }
            catch { }
        }
    }
}
