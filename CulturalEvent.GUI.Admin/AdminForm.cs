using CulturalEvent.GUI.Admin.Controls;
using CulturalEvents.App.Core.Entity;
using CulturalEvents.App.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CulturalEvent.GUI.Admin;

public class Translator : INpgsqlNameTranslator
{
    public string TranslateTypeName(string clrName)
    {
        return "order_status";
    }

    public string TranslateMemberName(string clrName)
    {
        return clrName switch
        {
            "Delivered" => "Доставлен",
            "Delivery" => "В Процессе доставки",
            "Processing" => "Обработка",
            _ => clrName
        };
    }
}

public partial class AdminForm : Form
{
    DbContext _context;
    public AdminForm(string connectionString)
    {
        InitializeComponent();
        Text = "Админ панель";
        var options =
            new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(connectionString, o => o.MapEnum<OrderStatus>("order_status", nameTranslator:new Translator() )).Options;
        _context = new AppDbContext(options);
        SetupTabs();
        DoubleBuffered = true;
    }

    private void SetupOtchets()
    {
        TabControl tabControl = new TabControl()
        {
            Dock = DockStyle.Fill,
        };
    }
    private void SetupTabs()
    {
        TabControl tabControl = new TabControl()
        {
            Dock = DockStyle.Fill,
        };

        tabControl.TabPages.Add(new TabDbPage<RegionAuditableEntity>(_context));
        tabControl.TabPages.Add(new TabDbPage<CityAuditableEntity>(_context));
        tabControl.TabPages.Add(new TabDbPage<StreetAuditableEntity>(_context));
        tabControl.TabPages.Add(new TabDbPage<AddressAuditableEntity>(_context));
        
        tabControl.TabPages.Add(new TabDbPage<CulturalTypeAuditableEntity>(_context));
        tabControl.TabPages.Add(new TabDbPage<CulturalAuditableEntity>(_context));
        tabControl.TabPages.Add(new TabDbPage<CulturalAddressAuditableEntity>(_context));
        
        tabControl.TabPages.Add(new TabDbPage<EventTypeAuditableEntity>(_context));
        tabControl.TabPages.Add(new TabDbPage<EventAuditableEntity>(_context));
        tabControl.TabPages.Add(new TabDbPage<EventDescriptionAuditableEntity>(_context));
        tabControl.TabPages.Add(new TabDbPage<EventPriceAuditableEntity>(_context));
        
        tabControl.TabPages.Add(new TabDbPage<ClientAuditableEntity>(_context));
        tabControl.TabPages.Add(new TabDbPage<ClientAddressAuditableEntity>(_context));
        
        tabControl.TabPages.Add(new TabDbPage<OrderAuditableEntity>(_context));
        tabControl.TabPages.Add(new TabDbPage<OrderPlaceAuditableEntity>(_context));
        
        var lastSelectedIndex = 0;
        tabControl.SelectedIndexChanged += (s, e) =>
        {
            var unloadPage = tabControl.TabPages[lastSelectedIndex];
            unloadPage.GetType().GetMethod("UnLoad")?.Invoke(unloadPage, null);
            if (tabControl.SelectedIndex < 0) return;
            var loadPage = tabControl.TabPages[tabControl.SelectedIndex];
            loadPage.GetType().GetMethod("Load")?.Invoke(loadPage, null);
            lastSelectedIndex = tabControl.SelectedIndex;
        };
        Controls.Add(tabControl);
    }

}