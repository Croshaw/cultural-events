using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using CulturalEvent.GUI.Admin.Common;
using CulturalEvents.App.Core;
using CulturalEvents.App.Core.Entity;
using CulturalEvents.App.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CulturalEvent.GUI.Admin.Controls;

public class TabDbPage<T> : TabPage where T : BaseAuditableEntity
{
    public DataGridView DataGridView { get; private set; }
    private Dictionary<ComboBox, object> _sets;
    private ValueWrapper _value;
    private DbContext _context;
    private Type _type;
    private PropertyInfo[] _displayProperties;
    private PropertyInfo[] _editableProperties;
    private BaseRepository<T> _repository;

    private Dictionary<PropertyInfo, Func<object>> _properties;

    public Action Clear { get; private set; }

    public TabDbPage(DbContext dbContext)
    {
        _type = typeof(T);
        _context = dbContext;
        _repository = new BaseRepository<T>(dbContext);
        _value = new ValueWrapper(null);
        _properties = [];
        _sets = [];
        _displayProperties = _type.GetProperties().Where(p => Attribute.IsDefined(p, typeof(DisplayAttribute)))
            .ToArray();
        _editableProperties = _type.GetProperties()
            .Where(p => p.GetCustomAttribute<EditableAttribute>()?.AllowEdit ?? false).ToArray();
        Init();
    }

    public void UnLoad()
    {
        DataGridView.Rows.Clear();
        foreach (var comboBox in _sets.Keys)
        {
            comboBox.Items.Clear();
            comboBox.SelectedIndex = -1;
        }
    }

    public void Load()
    {
        UpdateTable();
        foreach (var (key, value) in _sets)
        {
            key.Items.Clear();
            var set = value as IEnumerable<object>;
            var maxString = "";
            foreach (var val in set)
            {
                var str = (val?.ToString() ?? "");
                if(maxString.Length < str.Length)
                    maxString = str;
                key.Items.Add(val);
            }

            using var g = key.CreateGraphics();
            key.Width = Math.Max(key.Width , (int)g.MeasureString(maxString, key.Font).Width + 10);
        }
    }
    private void Init()
    {
        Panel tablePanel = new()
        {
            Dock = DockStyle.Fill,
        };
        DataGridView = new DataGridView()
        {
            Dock = DockStyle.Fill,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeRows = false,
            RowHeadersVisible = false,
            ReadOnly = true,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
        };
        DataGridView.Columns.Add("id", "id");
        DataGridView.Columns[0].Visible = false;
        foreach (var property in _displayProperties)
            DataGridView.Columns.Add(null, Helper.GetDisplayName(property));
        tablePanel.Controls.Add(DataGridView);

        var panel = new FlowLayoutPanel()
        {
            Dock = DockStyle.Bottom,
        };

        Text = Helper.GetDisplayName(_type);

        foreach (var property in _editableProperties)
        {
            panel.Controls.Add(GetEditableControl(property));
        }

        var addButton = new Button()
        {
            Text = "Добавить",
        };
        addButton.Click += (s, e) => Add();
        var remButton = new Button()
        {
            Text = "Удалить",
        };
        remButton.Click += (s, e) => Remove();
        var editCb = new CheckBox()
        {
            Text = "Редактировать",
            AutoSize = true,
        };
        editCb.CheckedChanged += (s, e) =>
        {
            if (!editCb.Checked)
            {
                addButton.Text = "Добавить";
                _value.Value = null;
                return;
            }

            addButton.Text = "Изменить";
            if (DataGridView.SelectedRows.Count == 0 || !int.TryParse(DataGridView.SelectedRows[0].Cells["id"].Value.ToString(), out var id))
                return;
            var res  = _repository.Get(id);
            _value.Value = res.IsSome ? res.Value : null;
        };

        DataGridView.CellClick += (s, e) =>
        {
            if (!editCb.Checked || e.RowIndex < 0)
                return;
            if(!int.TryParse(DataGridView.Rows[e.RowIndex].Cells["id"].Value.ToString(), out var id))
                return;
            var res  = _repository.Get(id);
            _value.Value = res.IsSome ? res.Value : null;
        };
        
        panel.Controls.Add(addButton);
        panel.Controls.Add(remButton);
        panel.Controls.Add(editCb);

        Controls.Add(tablePanel);
        Controls.Add(panel);
        UpdateTable();
    }

    private string[] GetRow(T entity)
    {
        return _displayProperties.Select(propertyInfo => propertyInfo.GetValue(entity)?.ToString() ?? "")
            .Prepend(entity.Id.ToString()).ToArray();
    }

    public void UpdateTable()
    {
        DataGridView.Rows.Clear();
        var array = _repository.Get();
        foreach (var entity in array)
        {
            var row = GetRow(entity);
            DataGridView.Rows.Add(row);
        }
    }

    private bool CheckResult(Result<T> result)
    {
        if (result.IsSuccess)
            return true;
        var ex = result.Exception;
        switch (ex)
        {
            case DbUpdateException:
            {
                if (ex.InnerException is PostgresException sqlEx)
                {
                    switch (sqlEx.SqlState)
                    {
                        case "23505":
                            MessageBox.Show("Ошибка: Нарушение ограничения уникальности.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case "23503":
                            MessageBox.Show("Ошибка: Нарушение внешнего ключа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case "23502":
                            MessageBox.Show("Ошибка: Поле не может быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    
                        case "22001":
                            MessageBox.Show("Ошибка: Превышение длины строки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "22007":
                            MessageBox.Show("Ошибка: Неправильный формат даты/времени.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        default:
                            MessageBox.Show($"PostgreSQL Error: {sqlEx.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
                else
                {
                    MessageBox.Show($"DbUpdateException: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                break;
            }
            case ValidationException:
                MessageBox.Show($"Ошибка валидации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;
            default: 
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;
        }
        return false;
    }

    public void Add()
    {
        Result<T> result;
        if (_value.Value is null)
        {
            var val = Activator.CreateInstance<T>();
            foreach (var (key, value) in _properties)
                key.SetValue(val, value.Invoke());
            result = _repository.Add(val);
        }
        else
        {
            foreach (var (key, value) in _properties)
                key.SetValue(_value.Value, value.Invoke());
            result = _repository.Update((T)_value.Value);
        }
        if (!CheckResult(result)) return;
        if (_value.Value == null)
            Clear();
        else
            _value.Value = null;
        // Clear?.Invoke();
        UpdateTable();
    }
    public void Remove()
    {
        if (!int.TryParse(DataGridView.SelectedRows[0].Cells["id"].Value.ToString(), out var id)) return;
        var result = _repository.Delete(id);
        if(CheckResult(result))
        {
            UpdateTable();
        }
    }
    private Control GetEditableControl(PropertyInfo propertyInfo)
    {
        var name = Helper.GetDisplayName(propertyInfo);
        var propertyType = propertyInfo.PropertyType;
        if (propertyType == typeof(string))
        {
            var tb = CreateTextBoxField(name);
            Clear += () => tb.Clear();
            _value.ValueChanged += (value) => tb.Text = value is null ? "" : (propertyInfo.GetValue(value) ?? "").ToString();
            _properties[propertyInfo] = () => tb.Text;
            return tb;
        }

        if (propertyType == typeof(bool))
        {
            var cb = CreateCheckBox(name);
            Clear += () => cb.Checked = false;
            _value.ValueChanged += (value) => cb.Checked = value is not null && (bool)(propertyInfo.GetValue(value) ?? false); 
            _properties[propertyInfo] = () => cb.Checked;
            return cb;
        }
        if (IsIntType(propertyType))
        {
            var tb = CreateOnlyNumberTextBox(name);
            Clear += () => tb.Clear();
            _value.ValueChanged += (value) => tb.Text = value is null ? "" : (propertyInfo.GetValue(value) ?? "").ToString();
            _properties[propertyInfo] = () => ConvertToNum(tb.Text, propertyType);
            return tb;
        } 
        if (IsFloatType(propertyType))
        {
            var tb =CreateOnlyFloatNumberTextBox(name);
            Clear += () => tb.Clear();
            _value.ValueChanged += (value) => tb.Text = value is null ? "" : (propertyInfo.GetValue(value) ?? "").ToString();
            _properties[propertyInfo] = () => ConvertToNum(tb.Text, propertyType);
            return tb;
        }

        if (propertyType == typeof(DateOnly) || propertyType == typeof(TimeOnly) || propertyType == typeof(DateTime))
        {
            var dtp = propertyType == typeof(TimeOnly) ? CreateTimePicker(name) : CreateDatePicker(name);
            // Clear += () => dtp.Text = string.Empty;
            _value.ValueChanged += (value) =>
            {
                if (value is null)
                {
                    dtp.Value = DateTime.Now;
                }
                else
                {
                    var val = propertyInfo.GetValue(value);
                    dtp.Value = val switch
                    {
                        null => DateTime.Now,
                        DateOnly date => date.ToDateTime(new TimeOnly()),
                        TimeOnly time => DateOnly.FromDateTime(DateTime.Today).ToDateTime(time),
                        DateTime dateTime => dateTime,
                        _ => dtp.Value
                    };
                }
            };
            _properties[propertyInfo] = () =>
            {
                if(propertyType == typeof(TimeOnly))
                    return TimeOnly.FromDateTime(dtp.Value);
                if(propertyType == typeof(DateOnly))
                    return DateOnly.FromDateTime(dtp.Value);
                return dtp.Value;
            };
            return dtp;
        }

        if (propertyType.IsEnum)
        {
            var values = Enum.GetValues(propertyType);
            var cb = CreateComboBox(name, null);
            foreach (var value in values)
            {
                cb.Items.Add(value);
            }
            _properties[propertyInfo] = () => cb.SelectedItem;
            _value.ValueChanged += (value) =>
            {
                if(value is null)
                {
                    cb.SelectedIndex = -1;
                    return;
                }
                cb.SelectedItem = propertyInfo.GetValue(value);
            };
            return cb;
        }
        
        if (!propertyType.IsSubclassOf(typeof(BaseAuditableEntity))) throw new ArgumentException($"Can't type {name}");
        var genericType = typeof(DbSet<>).MakeGenericType(propertyType);
        var dbSetPropInfo = _context.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == genericType);
        var set = dbSetPropInfo?.GetValue(_context) ?? null;
        if (set != null)
        {
            var list = set as IEnumerable<object>;
            var cb = CreateComboBox(name, list);
            _sets[cb] = set;
            Clear += () => cb.SelectedIndex = -1;
            _properties[propertyInfo] = () => cb.SelectedItem;
            var foreignKey = propertyInfo.GetCustomAttribute<ForeignKeyAttribute>();
            if (foreignKey == null) return cb;
            var prop = typeof(T).GetProperty(foreignKey.Name);
            if (prop is not null)
            {
                _properties[prop] = () => (cb.SelectedItem as BaseEntity)?.Id ?? 0;
                _value.ValueChanged += (value) =>
                {
                    if(value is null)
                    {
                        cb.SelectedIndex = -1;
                        return;
                    }
                    var id = (int)(prop.GetValue(value) ?? -1);
                    cb.SelectedItem = list.FirstOrDefault(x => ((BaseEntity)x).Id == id);
                };

            }
            return cb;
        }
        throw new Exception();

    }


    private static bool IsIntType(Type type)
    {
        return type == typeof(byte) || type == typeof(short) || type == typeof(int) || type== typeof(int?) || type == typeof(long);
    }

    private static object? ConvertToNum(string text, Type type)
    {
        try
        {
            // Проверяем, является ли тип nullable
            Type targetType = Nullable.GetUnderlyingType(type) ?? type;

            // Если текст пустой или null и тип nullable, возвращаем null
            if (string.IsNullOrWhiteSpace(text) && Nullable.GetUnderlyingType(type) != null)
            {
                return null;
            }

            // Пытаемся выполнить преобразование
            return Convert.ChangeType(text, targetType);
        }
        catch
        {
            // Если тип nullable, возвращаем null
            if (Nullable.GetUnderlyingType(type) != null)
            {
                return null;
            }

            // Если тип не nullable, возвращаем default значение для типа
            return Activator.CreateInstance(type);
        }
    }
    private static bool IsFloatType(Type type)
    {
        return type == typeof(float) || type == typeof(double) || type == typeof(decimal);
    }
    private static TextBox CreateTextBoxField(string? placeholder)
    {
        return CreateTextBox(placeholder);
    }
    private static TextBox CreateOnlyNumberTextBox(string? placeholder)
    {
        return CreateTextBox(placeholder, Helper.OnlyNumber);

    }
    private static TextBox CreateOnlyFloatNumberTextBox(string? placeholder)
    {
        return CreateTextBox(placeholder, Helper.OnlyFloatNumber);
    }

    private static DateTimePicker CreateDatePicker(string? placeholder)
    {
        var result = new DateTimePicker();
        // result.Text = placeholder;
        return result;
    }

    private static DateTimePicker CreateTimePicker(string? placeholder)
    {
        var result = new DateTimePicker()
        {
            Format = DateTimePickerFormat.Time,
            ShowUpDown = true,
        };
        // result.Text = placeholder;
        return result;
    }
    private static TextBox CreateTextBox(string? placeholder = null, KeyPressEventHandler? keyPressEventHandler = null)
    {
        var result = new TextBox();
        result.PlaceholderText = placeholder;
        if(keyPressEventHandler is not null)
            result.KeyPress += keyPressEventHandler;
        return result;
    }
    private static CheckBox CreateCheckBox(string label)
    {
        var result = new CheckBox()
        {
            Text = label,
            AutoSize = true,
            AutoEllipsis = true
        };
        return result;
    }
    private static ComboBox CreateComboBox(string label, IEnumerable<object>? values)
    {
        var result = new ComboBox()
        {
            DropDownStyle = ComboBoxStyle.DropDown,
            AutoCompleteSource = AutoCompleteSource.CustomSource,
            AutoCompleteMode = AutoCompleteMode.SuggestAppend,
            AutoSize = true
        };
        if(values is null)
            return result;
        var maxString = "";
        foreach (var value in values)
        {
            var str = (value?.ToString() ?? "");
            if(maxString.Length < str.Length)
                maxString = str;
            result.Items.Add(value);
        }

        using var g = result.CreateGraphics();
        result.Width = Math.Max(result.Width, (int)g.MeasureString(maxString, result.Font).Width + 10);
        return result;
    }
}