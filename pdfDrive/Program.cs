using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using com.sun.tools.javac;
using sun.tools.tree;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Google.Apis.Analytics.v3;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Globalization;


namespace pdfDrive
{
    static class Program
    {
        public static Form globalForm;

        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            globalForm = new mainI();
            Application.Run(globalForm);
        }


        public static string ToTitleCase(this string title)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title.ToLower());
        }
    }
    
}