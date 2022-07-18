using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    /// <summary>
    /// Class for rich textbox
    /// </summary>
    public static class TextboxWriter
    {
        /// <summary>
        /// The method writes time to rich textbox
        /// </summary>
        /// <param name="source">This argument indicates the rich textbox to which time is wrote</param>
        /// <param name="time">Time to output</param>
        /// <param name="header">Header in front of rich textbox</param>
        public static void OutputTime(this RichTextBox source, TimeSpan time, string header)
        {
            string content = $"{header}時間:{time.Hours: 00}:{time.Minutes: 00}:{time.Seconds: 00}:{time.Milliseconds: 000}";

            if (source.Text.Length == 0)
            {
                source.Text = content;
            }
            else
            {
                source.AppendText("\r\n" + content);
            }
        }
    }
}
