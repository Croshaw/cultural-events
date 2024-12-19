using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using CulturalEvent.GUI.Admin.Common;
using Microsoft.EntityFrameworkCore;

namespace CulturalEvent.GUI.Admin.Controls;

public class Paginator<T> : UserControl, INotifyPropertyChanged
{
    private readonly FlowLayoutPanel _numbersPanel;
    private readonly PropertyInfo[] _properties;
    private readonly DataTable _dataTable;
    private readonly IQueryable<T> _rawQuery;
    private uint _pageSize;
    private uint _page;
    private DataGridView _target;
    private Expression<Func<T, bool>>? _filter;
    private Func<T, object[]>? _selector;
    private int _pageToView;

    public int PageToView
    {
        get => _pageToView;
        set => SetField(ref _pageToView, value);
    }

    public uint PageSize
    {
        get => _pageSize;
        set => SetField(ref _pageSize, value);
    }

    public uint Page
    {
        get => _page;
        set => SetField(ref _page, value);
    }

    public DataGridView Target
    {
        get => _target;
        set => SetField(ref _target, value);
    }

    public Expression<Func<T, bool>>? Filter
    {
        get => _filter;
        set => SetField(ref _filter, value);
    }

    public Func<T, object[]>? Selector
    {
        get => _selector;
        set => SetField(ref _selector, value);
    }
    public uint TotalPages { get; set; }

    public Paginator(IQueryable<T> rawQuery, List<string>? columns = null)
    {
        var type = typeof(T);
        _properties = type.GetProperties().Where(p => p.GetCustomAttribute<DisplayAttribute>() is not null).ToArray();
        _dataTable = new DataTable(Helper.GetDisplayName(type));
        if(columns is not null)
            foreach (var column in columns)
                _dataTable.Columns.Add(column);
        else
            foreach (var propertyInfo in _properties)
                _dataTable.Columns.Add(Helper.GetDisplayName(propertyInfo));
        _rawQuery = rawQuery;
        _numbersPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
        };
    }

    public void NextPage()
    {
        Page++;
    }

    public void PreviousPage()
    {
        if(Page > 0)
            Page--;
    }

    public void SetPage(uint page)
    {
        Page = page;
    }
    private IQueryable<T> GetQuery()
    {
        var query = _rawQuery;
        if (Filter is not null)
            query = query.Where(Filter);
        return query;
    }
    public async void UpdateAsync()
    {
        var query = GetQuery();
        var totalPages = (uint)((await query.CountAsync()) / PageSize);
        if (TotalPages != totalPages)
        {
            var offset = (long)totalPages - (long)TotalPages;
            if (offset > 0)
            {
                for(var i = 0; i < offset; i++)
                    _numbersPanel.Controls.Add(new Button() {Text = $"{i+1+_numbersPanel.Controls.Count}"});
            }
            else
            {
                offset = Math.Abs(offset);
                for(var i = 0; i < offset; i++)
                    _numbersPanel.Controls.RemoveAt(_numbersPanel.Controls.Count - 1);
            }
            TotalPages = totalPages;
        }
        if (TotalPages <= Page)
            Page = 0;
        var rows = await query.Skip((int)(Page * PageSize)).Take((int)PageSize).ToArrayAsync();
        _dataTable.Clear();
        foreach (var row in rows)
        {
            if(Selector is not null)
                _dataTable.Rows.Add(Selector(row));
            else
                _dataTable.Rows.Add(_properties.Select(row => row.GetValue(row)));
        }
        Target.DataSource = _dataTable;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        UpdateAsync();
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}