using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace TRQuoteCore.Quotes.CustomControls
{
    /// <summary>
    /// Customized label that displays numerical values in specified color.
    /// </summary>
    public class ColoredLabel:Label
    {
        private Color negativeColor = Color.Tomato;

        public Color NegativeColor
        {
            get { return negativeColor; }
            set { negativeColor = value; }
        }
        private Color positiveColor = Color.DodgerBlue;

        public Color PositiveColor
        {
            get { return positiveColor; }
            set { positiveColor = value; }
        }
        private Color zeroColor = Color.ForestGreen;

        public Color ZeroColor
        {
            get { return zeroColor; }
            set { zeroColor = value; }
        }

        private Image upImage;

        public Image UpImage
        {
            get { return upImage; }
            set { upImage = value; }
        }
        private Image downImage;

        public Image DownImage
        {
            get { return downImage; }
            set { downImage = value; }
        }


        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                if (value != null)
                {
                    double set_value = 0;
                    bool parsed = double.TryParse(value, out set_value);

                    if (parsed)
                    {
                        if (set_value > 0)
                        {
                            ForeColor = PositiveColor;
                            if (upImage != null)
                            {
                                Image = upImage;
                            }
                        }
                        else if (set_value < 0)
                        {
                            ForeColor = NegativeColor;
                            if (upImage != null)
                            {
                                Image = downImage;
                            }
                        }
                        else if (set_value == 0)
                        {
                            ForeColor = ZeroColor;
                            
                            Image = null;
                            
                        }
                    }
                    else
                    {
                        ForeColor = SystemColors.ControlText;
                    }
                }
            }
        }
    }
}
