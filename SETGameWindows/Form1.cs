using SETLib;
using System.Diagnostics.Contracts;

namespace SETGameWindows
{
    public partial class Form1 : Form
    {
        public Board board;
        public Deck deck;

        public Bitmap[] cardImages;

        public Form1()
        {
            InitializeComponent();
            cardImages = new Bitmap[81];

            for (int i0 = 1; i0 < 4; i0++)
            {
                for (int i1 = 1; i1 < 4; i1++)
                {
                    for (int i2 = 1; i2 < 4; i2++)
                    {
                        for (int i3 = 1; i3 < 4; i3++)
                        {
                            string fn = Path.Combine(Environment.CurrentDirectory, @"img\", i0.ToString() + i1.ToString() + i2.ToString() + i3.ToString() + ".bmp");
                            cardImages[27 * i0 + 9 * i1 + 3 * i2 + i3 - 40] = new Bitmap(fn);
                        }
                    }
                }
            }

            InitGame();
        }

        private void SetImage(int row, int col, int cardIx)
        {
            PictureBox pb = (PictureBox)this.Controls["panel1"].Controls["pb" + row.ToString() + col.ToString()];
            pb.Image = cardImages[cardIx];
            pb.BorderStyle = BorderStyle.None;
        }

        private void DrawBoard()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    SetImage(row, col, board.Cards[4 * row + col].Index());
                }
            }
        }

        private void InitGame()
        {
            deck = new Deck();
            board = new Board(deck);

            DrawBoard();

            lbCardsInDeck.DataBindings.Clear();
            lbSetsFound.DataBindings.Clear();
            lbCardsInDeck.DataBindings.Add(new Binding("Text", deck, "CardsInDeck"));
            lbSetsFound.DataBindings.Add(new Binding("Text", deck, "SetsFound"));
        }

        private void pb_Click(object sender, EventArgs e)
        {
            string objName = ((PictureBox)sender).Name;
            int col = Convert.ToInt32(objName.Substring(3));
            int row = Convert.ToInt32(objName.Substring(2, 1));

            SelectionChangeResult r = board.SelectCard(row, col);
            PictureBox pb = (PictureBox)this.Controls["panel1"].Controls["pb" + row.ToString() + col.ToString()];
            if (r == SelectionChangeResult.CardSelected || r == SelectionChangeResult.InvalidSet)
            {
                pb.BorderStyle = BorderStyle.FixedSingle;
            }
            if (r == SelectionChangeResult.CardUnselected)
            {
                pb.BorderStyle = BorderStyle.None;
            }
            if (r == SelectionChangeResult.ValidSet)
            {
                deck.SetsFound++;
                board.ReplaceSet(deck);
                DrawBoard();
            }
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            InitGame();
        }
    }
}