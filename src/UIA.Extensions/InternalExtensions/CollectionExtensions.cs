using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UIA.Extensions.InternalExtensions
{
    static class CollectionExtensions
    {
        public static IEnumerable<T> As<T>(this BaseCollection collection)
        {
            return collection.Cast<T>();
        }

        public static IEnumerable<T> Select<T>(this DataGridViewRowCollection rows, Func<DataGridViewRow, T> selector)
        {
            return rows.Cast<DataGridViewRow>().Select(selector);
        }

        public static IEnumerable<T> Select<T>(this DataGridViewColumnCollection columns, Func<DataGridViewColumn, T> selector)
        {
            return columns.As<DataGridViewColumn>().Select(selector);
        }

        public static IEnumerable<T> Select<T>(this DataGridViewCellCollection cells, Func<DataGridViewCell, T> selector)
        {
            return cells.As<DataGridViewCell>().Select(selector);
        }

        public static IEnumerable<DataGridViewColumn> Where(this DataGridViewColumnCollection columns, Func<DataGridViewColumn, bool> predicate)
        {
            return columns.As<DataGridViewColumn>().Where(predicate);
        }
    }
}