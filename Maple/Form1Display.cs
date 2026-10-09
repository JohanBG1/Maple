using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Maple;
public partial class Form1 : Form
{
    private TextBox currentTextBox;
    public void Write(Point position, int width)
    {
        TextBox textBox = new TextBox();
        textBox.Location = position;
        textBox.Width = width;
        textBox.Text = "";
        textBox.Enter += (object? sender, EventArgs e) => currentTextBox = (TextBox)sender;
        Controls.Add(textBox);
    }

    private void HnadleTextBoxEnter(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void MakeSpaceWriteable(List<Point> corners, int lineSpacing = Configuration.LineSpacing)
    {
        Point topRight = FindTopLeftCorner(corners);
        Point bottomLeft = FindBottomRightCorner(corners);

        int width = bottomLeft.X - topRight.X;

        int lines = (bottomLeft.Y - topRight.Y)/lineSpacing;

        for(int i = 0; i < lines; i++)
        {
            Write(new Point(topRight.X, topRight.Y + i * lineSpacing), width);
        }


        Point FindBottomRightCorner(List<Point> corners)
        {
            int maxX = int.MinValue;
            int maxY = int.MinValue;

            foreach (Point corne in corners)
            {
                if (maxX < corne.X) maxX = corne.X;
                if (maxY < corne.Y) maxY = corne.Y;
            }

            Point corner = (from c in corners
                            where c.X == maxX
                            where c.Y == maxY
                            select c).FirstOrDefault();
            return corner;
        }

        Point FindTopLeftCorner(List<Point> corners)
        {
            int minX = int.MaxValue;
            int minY = int.MaxValue;

            foreach (Point corne in corners)
            {
                if (minX > corne.X) minX = corne.X;
                if (minY > corne.Y) minY = corne.Y;
            }

            Point corner = (from c in corners
                            where c.X == minX
                            where c.Y == minY
                            select c).FirstOrDefault();
            return corner;
        }
    }
}