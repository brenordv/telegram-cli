using System.Text;

namespace Telegram.Cli.Extensions;

public static class ListExtensions
{
    /// <summary>
    /// Adds a List item to the list as a single string with the specified separator.
    /// </summary>
    /// <param name="list">The target list to which the item will be added.</param>
    /// <param name="item">The StringBuilder item to be added as a single string to the list.</param>
    /// <param name="separator">The separator to be used between the elements. Default is a space.</param>
    public static void AddAsSingleItem(this IList<string> list, IList<string> item, string separator = " ")
    {
        if (list == null || item == null || !item.Any()) return;
        list.Add(string.Join(separator, item));
    }
    
    /// <summary>
    /// Extracts the message from an array of strings.
    /// </summary>
    /// <param name="args">The array of strings.</param>
    /// <returns>A single string containing the extracted message.</returns>
    public static string ExtractMessage(this string[] args)
    {
        if (args == null || !args.Any()) return string.Empty;
        return string.Join(" ", args).Trim();
    }
}