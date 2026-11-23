using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HotelBooking
{
    // the 5 app colors, pulled from the logo + the beige website template. use these instead of hardcoding colors
    public static class Theme
    {
        // page background (beige)
        public static readonly Color Background = ColorTranslator.FromHtml("#EFE6D6");

        // cards, search bar, filters (light cream)
        public static readonly Color Surface = ColorTranslator.FromHtml("#F8F3EA");

        // logo green: header, buttons, main text
        public static readonly Color Primary = ColorTranslator.FromHtml("#1F2C2B");

        // logo gold: header line, button hover
        public static readonly Color Accent = ColorTranslator.FromHtml("#AE9D7D");

        // logo cream: text on the green header
        public static readonly Color TextLight = ColorTranslator.FromHtml("#E2E2DC");

        // finds Assets\<file> next to the exe, or walks up the folders to the project's Assets folder
        // so images still load even if "Copy if newer" wasn't set. returns null if not found
        public static string AssetPath(string file)
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            for (int i = 0; i < 6 && !string.IsNullOrEmpty(dir); i++)
            {
                string p = Path.Combine(dir, "Assets", file);
                if (File.Exists(p)) return p;
                dir = Path.GetDirectoryName(dir.TrimEnd(Path.DirectorySeparatorChar));
            }
            return null;
        }

        // fonts
        public static readonly string HeadingFont = "Georgia";
        public static readonly string BodyFont = "Segoe UI";

        public static Font Heading(float size = 20f) => new Font(HeadingFont, size, FontStyle.Regular);
        public static Font Body(float size = 10f) => new Font(BodyFont, size, FontStyle.Regular);

        // dark green button, turns gold on hover
        public static void StyleButton(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.BackColor = Primary;
            b.ForeColor = Background;
            b.Font = new Font(BodyFont, 10f, FontStyle.Bold);
            b.Cursor = Cursors.Hand;
            b.FlatAppearance.MouseOverBackColor = Accent;
            b.FlatAppearance.MouseDownBackColor = Accent;
            b.MouseEnter += (s, e) => b.ForeColor = Primary;
            b.MouseLeave += (s, e) => b.ForeColor = Background;
        }

        // small uppercase label used above inputs
        public static Label FieldLabel(string text)
        {
            return new Label
            {
                Text = text.ToUpper(),
                Font = new Font(BodyFont, 8.5f, FontStyle.Bold),
                ForeColor = Primary,
                AutoSize = false,
                Height = 22,
                TextAlign = ContentAlignment.BottomLeft
            };
        }
    }
}
