﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace ImageVisualizer
{
    public partial class ImageForm : Form
    {
        public static Font UIFont
        {
          get
            {
#if VS10
                var dteProgID = "VisualStudio.DTE.10.0";
#elif VS11
                var dteProgID = "VisualStudio.DTE.11.0";
#elif VS12
                var dteProgID = "VisualStudio.DTE.12.0";
#elif VS13
                var dteProgID = "VisualStudio.DTE.13.0";
#elif VS14
                var dteProgID = "VisualStudio.DTE.14.0";
#elif VS15
                var dteProgID = "VisualStudio.DTE.15.0";
#endif
                var dte = (EnvDTE.DTE)System.Runtime.InteropServices.Marshal.GetActiveObject(dteProgID);
                var fontProperty = dte.Properties["FontsAndColors", "Dialogs and Tool Windows"];
                object objValue;
                if (fontProperty != null)
                {
                    objValue = fontProperty.Item("FontFamily").Value;
                    var fontFamily = objValue.ToString();
                    objValue = fontProperty.Item("FontSize").Value;
                    var fontSize = Convert.ToSingle(objValue);
                    var font = new Font(fontFamily, fontSize);

                    return font;
                    
                }

                return SystemFonts.DefaultFont;
            }
        }   

        

        public ImageForm(IVisualizerObjectProvider objectProvider)
        {
            InitializeComponent();

#if DEBUG
            this.ShowInTaskbar = true;
#endif

            this.label1.Font = UIFont;
            SetFontAndScale(this.label1, UIFont);
            this.label2.Font = UIFont;
            this.txtExpression.Font = UIFont;
            this.btnClose.Font = UIFont;

            imageControl.SetImage(objectProvider.GetObject());
            txtExpression.Text = objectProvider.GetObject().ToString();
        }

        public void SetFontAndScale(Control ctlControl, Font objFont)
        {
            float sngRatio;
            sngRatio = objFont.Size / ctlControl.Font.Size;
            if (ctlControl is Form)
            {
                ((Form)ctlControl).AutoScaleMode = AutoScaleMode.None;
            }
            ctlControl.Font = objFont;
            ctlControl.Scale(new SizeF(sngRatio, sngRatio));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
