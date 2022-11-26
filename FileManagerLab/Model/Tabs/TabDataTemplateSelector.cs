using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FileManagerLab.Model.Tabs;

public class TabDataTemplateSelector : DataTemplateSelector
{
    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        FrameworkElement element = container as FrameworkElement;
        if (element == null || item == null || item is not BaseTab) return null;
        if (item is FileManagerTab) return element.FindResource(new DataTemplateKey(typeof(FileManagerTab))) as DataTemplate;
        else if (item is ImageFileTab) return element.FindResource(new DataTemplateKey(typeof(ImageFileTab))) as DataTemplate;
        else if (item is TextFileTab) return element.FindResource(new DataTemplateKey(typeof(TextFileTab))) as DataTemplate;
        else if (item is ByteFileTab) return element.FindResource(new DataTemplateKey(typeof(ByteFileTab))) as DataTemplate;
        else return null;
    }
}