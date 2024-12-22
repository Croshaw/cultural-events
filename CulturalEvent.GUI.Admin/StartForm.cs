using Microsoft.Extensions.Configuration;

namespace CulturalEvent.GUI.Admin;

public partial class StartForm : Form
{
    public StartForm()
    {
        InitializeComponent();
        Text = "Начало";
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var connectionString = configuration.GetConnectionString("DefaultConnection")??"";
        var aboutProgram =
            "\tАдмин панель\n\nВ этом разделе администраторы могут управлять данными всех таблиц системы. Это удобный инструмент для редактирования информации о мероприятиях, культурных заведениях, заказах и других сущностях.\n" +
            "\n\n\tОтчёты\n\nРаздел предназначен для просмотра аналитики и статистики. Здесь можно генерировать и просматривать отчёты по проданным билетам, посещаемости мероприятий.\n"+
            "\n\n\tКлиент\n\nЭтот раздел создан для пользователей, желающих приобрести билеты на интересующие мероприятия. Удобная форма позволит выбрать мероприятие, указать количество мест и оформить заказ.\n" +
            "\n\nКонтакты: test123@yandex.ru\nФИО: Безруков Кирилл Русланович\nВсе права защищены";
        tableLayoutPanel1.Controls.Add(CreateButton("Админ панель", () => new AdminForm(connectionString).Show()), 1, 1);
        tableLayoutPanel1.Controls.Add(CreateButton("Отчёты", () => new EmployeeForm(connectionString).Show()), 1, 2);
        tableLayoutPanel1.Controls.Add(CreateButton("Клиент"), 1, 3);
        tableLayoutPanel1.Controls.Add(CreateButton("О программе", () => MessageBox.Show(aboutProgram, "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information)), 1, 4);
    }

    private static Button CreateButton(string text, Action? onClick = null)
    {
        var button = new Button() { Text = text, Dock = DockStyle.Top, AutoSize = true };
        if(onClick is not null)
            button.Click += (_,_) => onClick?.Invoke();
        return button;
    }
}