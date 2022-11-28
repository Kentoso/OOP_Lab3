using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace FileManagerLab.Model.Tabs;

public partial class ByteFileTab : BaseTab
{
    public enum ByteNumberSystems
    {
        Hex,
        Binary,
        Decimal,
        PlainText
    }
    public ByteNumberSystems[] ByteNSOptions { get; set; }
    [ObservableProperty] public ByteNumberSystems currentNS;
    private byte[] _fileBytes;
    public ByteFileTab(string path)
    {
        _path = path;
        ByteNSOptions = Enum.GetValues<ByteNumberSystems>();
        _fileBytes = File.ReadAllBytes(_path);
    }

    [RelayCommand]
    private void RegenerateContent()
    {
        Regenerate();
    }
    
    public override void GenerateContent()
    {
        TabName = Path.GetFileName(_path);
        Regenerate();
    }

    private void Regenerate()
    {
        if (CurrentNS == ByteNumberSystems.PlainText)
        {
            Content = File.ReadAllText(_path);
            return;
        }
        IEnumerable<string> newBytes;
        switch (currentNS)
        {
            case ByteNumberSystems.Hex:
                newBytes = _fileBytes.Select(b => b.ToString("X"));
                break;
            case ByteNumberSystems.Binary:
                newBytes = _fileBytes.Select(b => Convert.ToString(b, 2).PadLeft(8, '0'));
                break;
            case ByteNumberSystems.Decimal:
                newBytes = _fileBytes.Select(b => Convert.ToString(b, 10));
                break;
            default:
                newBytes = new List<string>();
                break;
        }

        Content = string.Join(" ", newBytes);
    }
}