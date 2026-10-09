namespace Maple;
public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        DoubleBuffered = true;
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        KeyPress += HandleKeyPressed;
        


        MakeSpaceWriteable(new List<Point>() { new Point(0, 0), new Point(500, 0), new Point(0, 500), new Point(500, 500) }, 25);
    }

    private void HandleKeyPressed(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {

        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.Clear(Color.White);
    }
}
