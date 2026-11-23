using System;
using System.Drawing;
using System.Windows.Forms;

namespace HotelBooking
{
    // search-first screen: find a room bar on top, filters on the left, room list on the right
    // room rows are placeholders for now, swap them for real data later
    public class WelcomeForm : BaseForm
    {
        private readonly DateTimePicker _checkIn = new DateTimePicker();
        private readonly DateTimePicker _checkOut = new DateTimePicker();
        private readonly NumericUpDown _guests = new NumericUpDown();
        private readonly Label _resultsLabel = new Label();
        private readonly FlowLayoutPanel _roomList = new FlowLayoutPanel();

        public WelcomeForm()
        {
            Text = "Find a room - The Hotel Reservations";

            var title = new Label
            {
                Text = "Find a room",
                Dock = DockStyle.Top,
                Height = 60,
                Font = Theme.Heading(28f),
                ForeColor = Theme.Primary,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // body = filters (left) + results (fill)
            var body = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, 24, 0, 0) };

            _resultsLabel.Dock = DockStyle.Top;
            _resultsLabel.Height = 36;
            _resultsLabel.Text = "[N] rooms available for your dates";
            _resultsLabel.ForeColor = Theme.Primary;
            _resultsLabel.TextAlign = ContentAlignment.MiddleLeft;

            _roomList.Dock = DockStyle.Fill;
            _roomList.FlowDirection = FlowDirection.TopDown;
            _roomList.WrapContents = false;
            _roomList.AutoScroll = true;
            _roomList.Resize += (s, e) => ResizeRows();

            var results = new Panel { Dock = DockStyle.Fill, Padding = new Padding(32, 0, 0, 0) };
            results.Controls.Add(_roomList);
            results.Controls.Add(_resultsLabel);

            body.Controls.Add(results);
            body.Controls.Add(BuildFilters());

            // order: fill first, then the top pieces
            ContentPanel.Controls.Add(body);
            ContentPanel.Controls.Add(BuildSearchBar());
            ContentPanel.Controls.Add(title);

            // placeholder rooms
            _roomList.Controls.Add(BuildRoomRow("[ROOM TYPE]", "[Beds / size / sleeps]", "[PRICE] / night"));
            _roomList.Controls.Add(BuildRoomRow("[ROOM TYPE]", "[Beds / size / sleeps]", "[PRICE] / night"));
            _roomList.Controls.Add(BuildRoomRow("[ROOM TYPE]", "[Beds / size / sleeps]", "[PRICE] / night"));
            ResizeRows();
        }

        // search bar: check-in, check-out, guests, button
        private Panel BuildSearchBar()
        {
            var bar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 104,
                BackColor = Theme.Surface,
                Padding = new Padding(20, 12, 20, 16)
            };
            // thin gold border (panel inside a gold panel)
            var border = new Panel { Dock = DockStyle.Top, Height = 104, BackColor = Theme.Accent, Padding = new Padding(1) };

            var table = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 2 };
            for (int i = 0; i < 4; i++) table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            _checkIn.Format = DateTimePickerFormat.Short;
            _checkIn.MinDate = DateTime.Today;
            _checkOut.Format = DateTimePickerFormat.Short;
            _checkOut.MinDate = DateTime.Today.AddDays(1);
            _checkOut.Value = DateTime.Today.AddDays(1);
            _guests.Minimum = 1;
            _guests.Maximum = 10;
            _guests.Value = 2;

            foreach (Control c in new Control[] { _checkIn, _checkOut, _guests })
            {
                c.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                c.Margin = new Padding(0, 0, 16, 0);
                c.Font = Theme.Body(11f);
            }

            var search = new Button { Text = "CHECK AVAILABILITY", Anchor = AnchorStyles.Left | AnchorStyles.Right, Height = 40 };
            Theme.StyleButton(search);
            search.Click += SearchButton_Click;

            table.Controls.Add(Theme.FieldLabel("Check-in"), 0, 0);
            table.Controls.Add(Theme.FieldLabel("Check-out"), 1, 0);
            table.Controls.Add(Theme.FieldLabel("Guests"), 2, 0);
            table.Controls.Add(_checkIn, 0, 1);
            table.Controls.Add(_checkOut, 1, 1);
            table.Controls.Add(_guests, 2, 1);
            table.Controls.Add(search, 3, 1);

            bar.Controls.Add(table);
            border.Controls.Add(bar);
            return border;
        }

        // filters panel on the left (not wired up yet)
        private Panel BuildFilters()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 240,
                BackColor = Theme.Surface,
                Padding = new Padding(20)
            };
            var flow = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false };

            var heading = Theme.FieldLabel("Filters");
            heading.Width = 196;

            var maxPrice = new TextBox { Width = 196, BorderStyle = BorderStyle.FixedSingle };
            var roomType = new ComboBox { Width = 196, DropDownStyle = ComboBoxStyle.DropDownList };
            roomType.Items.AddRange(new object[] { "Any", "[ROOM TYPE]" });
            roomType.SelectedIndex = 0;
            var beds = new NumericUpDown { Width = 196, Minimum = 1, Maximum = 6, Value = 1 };

            flow.Controls.Add(heading);

            foreach (var item in new (string, Control)[] { ("Max price", maxPrice), ("Room type", roomType), ("Beds", beds) })
            {
                var l = Theme.FieldLabel(item.Item1);
                l.Width = 196;
                flow.Controls.Add(l);
                item.Item2.Margin = new Padding(3, 0, 3, 10);
                flow.Controls.Add(item.Item2);
            }

            panel.Controls.Add(flow);
            return panel;
        }

        // one room row: photo placeholder, details, price + reserve button
        private Panel BuildRoomRow(string type, string info, string price)
        {
            var row = new Panel { Height = 130, BackColor = Theme.Surface, Margin = new Padding(0, 0, 0, 16) };

            var photo = new Label
            {
                Dock = DockStyle.Left,
                Width = 200,
                Text = "ROOM PHOTO",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = Theme.Body(9f),
                ForeColor = Theme.Primary,
                BackColor = Theme.Background,
                BorderStyle = BorderStyle.FixedSingle
            };

            var actions = new Panel { Dock = DockStyle.Right, Width = 210, Padding = new Padding(0, 18, 20, 18) };
            var priceLabel = new Label
            {
                Dock = DockStyle.Top,
                Height = 36,
                Text = price,
                TextAlign = ContentAlignment.MiddleRight,
                Font = Theme.Body(11f),
                ForeColor = Theme.Primary
            };
            var reserve = new Button { Text = "RESERVE", Dock = DockStyle.Bottom, Height = 44 };
            Theme.StyleButton(reserve);
            reserve.Click += (s, e) => MessageBox.Show("Reservation form coming in the next milestone.", HotelName);
            actions.Controls.Add(reserve);
            actions.Controls.Add(priceLabel);

            var details = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 22, 0, 0) };
            var name = new Label { Dock = DockStyle.Top, Height = 40, Text = type, Font = Theme.Heading(20f), ForeColor = Theme.Primary };
            var desc = new Label { Dock = DockStyle.Top, Height = 30, Text = info, ForeColor = Theme.Primary };
            details.Controls.Add(desc);
            details.Controls.Add(name);

            // fill first, then right, then left
            row.Controls.Add(details);
            row.Controls.Add(actions);
            row.Controls.Add(photo);
            return row;
        }

        // keep rows as wide as the list (minus the scrollbar)
        private void ResizeRows()
        {
            int w = _roomList.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 4;
            if (w < 300) return;
            foreach (Control c in _roomList.Controls) c.Width = w;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (_checkOut.Value.Date <= _checkIn.Value.Date)
            {
                MessageBox.Show("Check-out must be after check-in.", HotelName);
                return;
            }
            // TODO: query real availability, for now just updates the label
            int nights = (_checkOut.Value.Date - _checkIn.Value.Date).Days;
            _resultsLabel.Text = "3 rooms available for " + nights + (nights == 1 ? " night" : " nights")
                + " (" + _checkIn.Value.ToString("MMM d") + " to " + _checkOut.Value.ToString("MMM d") + ")";
        }
    }
}
