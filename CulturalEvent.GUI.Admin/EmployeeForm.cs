using CulturalEvents.App.Core.Entity;
using CulturalEvents.App.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CulturalEvent.GUI.Admin;

public class EventTicketCountDto
{
    public string CulturalName { get; set; }
    public string EventName { get; set; }
    public int TicketCount { get; set; }
}

public partial class EmployeeForm : Form
{
    private AppDbContext _context;
    public EmployeeForm(string connectionString)
    {
        InitializeComponent();
        Text = "Отчёты";
        var options = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(connectionString).Options;
        _context = new AppDbContext(options);
        Setup();
    }

    async Task Setup()
    {
        var tabControl = new TabControl()
        {
            Dock = DockStyle.Fill,
        };
        
        tabControl.TabPages.Add(GetFirstDocument());
        
        Controls.Add(tabControl);
    }
    TabPage GetFirstDocument()
    {
        var result = new TabPage()
        {
            Text = "Количество проданных билетов"
        };

        var flowLayoutPanel = new FlowLayoutPanel()
        {
            Dock = DockStyle.Right,
            FlowDirection = FlowDirection.TopDown,
        };

        var label = new Label()
        {
            Text = "Вид культурного заведения",
            AutoSize = true,
        };
        
        var comboBox = new ComboBox();
        
        comboBox.Items.AddRange(_context.CulturalTypes.ToArray<object>());
        // comboBox.SelectedIndex = 0;
        var dgv = new DataGridView()
        {
            Dock = DockStyle.Fill,
            RowHeadersVisible = false,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeRows = false,
            ReadOnly = true,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
        };
        dgv.Columns.Add("Cultural", "Культурное заведение");
        dgv.Columns.Add("EventName", "Мероприятие");
        dgv.Columns.Add("TicketCount", "Кол-во билетов");
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        
        flowLayoutPanel.Controls.Add(label);
        flowLayoutPanel.Controls.Add(comboBox);
        
        result.Controls.Add(dgv);
        result.Controls.Add(flowLayoutPanel);
        comboBox.SelectedIndexChanged += (s, e) => Fill();
        Fill();
        return result;

        void Fill()
        {

            var q = from op in _context.OrderPlaces
                join order in _context.Orders on op.OrderId equals order.Id
                join evnt in _context.Events on order.EventId equals evnt.Id
                join cultural in _context.Culturals on evnt.CulturalId equals cultural.Id
                where comboBox.SelectedItem == null || cultural.CulturalTypeId == ((BaseEntity)comboBox.SelectedItem).Id
                group op by new 
                { 
                    EventId = evnt.Id, 
                    EventName = evnt.Name, 
                    CulturalName = cultural.Name 
                } into groupedEvents
                select new EventTicketCountDto
                {
                    CulturalName = groupedEvents.Key.CulturalName,
                    EventName = groupedEvents.Key.EventName,
                    TicketCount = groupedEvents.Count()
                };
            var table = q.ToList();
            dgv.Rows.Clear();
            foreach (var row in table)
                dgv.Rows.Add(row.CulturalName, row.EventName, row.TicketCount);
        }
    }
}
